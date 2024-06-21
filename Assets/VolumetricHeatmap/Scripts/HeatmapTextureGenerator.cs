using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace HeatmapVisualization
{
	//HeatmapGenerator
	public class HeatmapTextureGenerator
	{
		#region Settings
		const int influenceExtentsMultiplier = 3;
		#endregion


		#region Globals
		private ComputeShader gaussianComputeShader;
		private Settings settings;
		#endregion


		#region Definitions
		[System.Serializable]
		public class Settings
		{
			public Bounds bounds = new Bounds(Vector3.zero, Vector3.one);
			public Vector3Int resolution = new Vector3Int(64, 64, 64);
			public float gaussStandardDeviation = 1.0f;


			public Settings(
				Bounds bounds,
				Vector3Int resolution,
				float gaussStandardDeviation)
			{
				this.bounds = bounds;
				this.resolution = resolution;
				this.gaussStandardDeviation = gaussStandardDeviation;
			}
		}
		#endregion


		#region Constructors
		public HeatmapTextureGenerator(ComputeShader gaussianComputeShader)
		{
			this.gaussianComputeShader = gaussianComputeShader;
		}
		#endregion


		#region Functions
		/// <summary>
		/// Calculate 3D texture of heat values from points.
		/// </summary>
		/// <returns>A float array representing the texture.</returns>
		public float[] CalculateHeatTexture(Vector3[] points, Settings settings)
		{
			this.settings = settings;

			int voxelCount = this.settings.resolution.x * this.settings.resolution.y * this.settings.resolution.z;
			Vector3 voxelSize = GetVoxelSize();
			Vector3Int kernelExtents = GetKernelExtents(voxelSize);

			//Texture3D with only R channel
			Texture3D heatTexture = new Texture3D(this.settings.resolution.x, this.settings.resolution.y, this.settings.resolution.z, TextureFormat.RFloat, false);

			uint[] pointCounts = GetPointCounts(points, voxelCount);
			float[] heats = pointCounts.Select(p => (float)p).ToArray();
			float[][] kernel = GetKernels(voxelSize, kernelExtents);
			ApplyGaussionBlur(heats, kernel, kernelExtents);

			return heats;
		}


		/// <summary>
		/// Get the voxel in the texture for a given point.
		/// </summary>
		/// <param name="position">The position of the point.</param>
		/// <returns>The position of the cointaining voxel. Might be out of bounds.</returns>
		private Vector3Int GetVoxelPosition(Vector3 position)
		{
			Vector3 boundsMin = settings.bounds.min;
			Vector3 boundsSize = settings.bounds.size;

			Vector3Int pointPosition = new Vector3Int(
				Mathf.RoundToInt(((position.x - boundsMin.x) / boundsSize.x) * (settings.resolution.x - 1)),
				Mathf.RoundToInt(((position.y - boundsMin.y) / boundsSize.y) * (settings.resolution.y - 1)),
				Mathf.RoundToInt(((position.z - boundsMin.z) / boundsSize.z) * (settings.resolution.z - 1)));
			return pointPosition;
		}


		/// <summary>
		/// Check if the given voxel position is inside the textures bounds.
		/// </summary>
		private bool IsInBounds(Vector3Int position)
		{
			return position.x > 0
				&& position.y > 0
				&& position.z > 0
				&& position.x < settings.resolution.x
				&& position.y < settings.resolution.y
				&& position.z < settings.resolution.z;
		}


		/// <summary>
		/// Get the wold size of the voxels from the texture.
		/// </summary>
		private Vector3 GetVoxelSize()
		{
			Vector3 voxelSize = new Vector3(
				settings.bounds.size.x / settings.resolution.x,
				settings.bounds.size.y / settings.resolution.y,
				settings.bounds.size.z / settings.resolution.z);
			return voxelSize;
		}


		/// <summary>
		/// Get the Extents of the Kernel.
		/// </summary>
		private Vector3Int GetKernelExtents(Vector3 voxelSize)
		{
			Vector3Int kernelExtents = influenceExtentsMultiplier * new Vector3Int(
				Mathf.RoundToInt(settings.gaussStandardDeviation / voxelSize.x),
				Mathf.RoundToInt(settings.gaussStandardDeviation / voxelSize.y),
				Mathf.RoundToInt(settings.gaussStandardDeviation / voxelSize.z));
			return kernelExtents;
		}


		/// <summary>
		/// Get the Kernels for X, Y and Z
		/// </summary>
		private float[][] GetKernels(Vector3 voxelSize, Vector3Int influenceExtents)
		{
			float[][] weigths = new float[3][];

			weigths[0] = new float[influenceExtents.x + 1];
			for (int i = 0; i <= influenceExtents.x; i++)
			{
				weigths[0][i] = NormalDistribution(i * voxelSize.x, settings.gaussStandardDeviation);
			}
			weigths[1] = new float[influenceExtents.y + 1];
			for (int i = 0; i <= influenceExtents.y; i++)
			{
				weigths[1][i] = NormalDistribution(i * voxelSize.y, settings.gaussStandardDeviation);
			}
			weigths[2] = new float[influenceExtents.z + 1];
			for (int i = 0; i <= influenceExtents.z; i++)
			{
				weigths[2][i] = NormalDistribution(i * voxelSize.z, settings.gaussStandardDeviation);
			}

			return weigths;
		}


		/// <summary>
		/// Use the Texture Voxels as bins for the points.
		/// </summary>
		/// <returns>The number of points in each voxel.</returns>
		private uint[] GetPointCounts(Vector3[] points, int voxelCount)
		{
			uint[] pointCounts = new uint[voxelCount];

			foreach (var point in points)
			{
				Vector3Int pointPosition = GetVoxelPosition(point);
				if (IsInBounds(pointPosition))
				{
					int pointIndex = PositionToIndex(pointPosition);
					pointCounts[pointIndex]++;
				}
			}

			return pointCounts;
		}


		/// <summary>
		/// Blur the heats image with the given kernel.
		/// </summary>
		private void ApplyGaussionBlur(float[] heats, float[][] kernel, Vector3Int kernelExtents)
		{
			Vector3Int threadGroupSize = new Vector3Int(
				DivideRoundUp(settings.resolution.x, 8),
				DivideRoundUp(settings.resolution.y, 8),
				DivideRoundUp(settings.resolution.z, 8));

			ComputeBuffer kernelBuffer;
			ComputeBuffer heatsBufferA = new ComputeBuffer(heats.Length, 4);
			ComputeBuffer heatsBufferB = new ComputeBuffer(heats.Length, 4);
			heatsBufferA.SetData(heats);

			gaussianComputeShader.SetInts("resolution", new int[] { settings.resolution.x, settings.resolution.y, settings.resolution.z });

			//pass for X-axis
			gaussianComputeShader.EnableKeyword("Axis_Pass_X");
			gaussianComputeShader.DisableKeyword("Axis_Pass_Y");
			gaussianComputeShader.DisableKeyword("Axis_Pass_Z");
			gaussianComputeShader.SetBuffer(0, "heatsIn", heatsBufferA);
			gaussianComputeShader.SetBuffer(0, "heatsOut", heatsBufferB);
			gaussianComputeShader.SetInts("kernelExtents", kernelExtents.x);
			kernelBuffer = new ComputeBuffer(kernel[0].Length, 4);
			kernelBuffer.SetData(kernel[0]);
			gaussianComputeShader.SetBuffer(0, "kernel", kernelBuffer);
			gaussianComputeShader.Dispatch(gaussianComputeShader.FindKernel("Main"), threadGroupSize.x, threadGroupSize.y, threadGroupSize.z);
			kernelBuffer.Release();
			//pass for Y-axis
			gaussianComputeShader.DisableKeyword("Axis_Pass_X");
			gaussianComputeShader.EnableKeyword("Axis_Pass_Y");
			gaussianComputeShader.DisableKeyword("Axis_Pass_Z");
			gaussianComputeShader.SetBuffer(0, "heatsIn", heatsBufferB);
			gaussianComputeShader.SetBuffer(0, "heatsOut", heatsBufferA);
			gaussianComputeShader.SetInts("kernelExtents", kernelExtents.y);
			kernelBuffer = new ComputeBuffer(kernel[1].Length, 4);
			kernelBuffer.SetData(kernel[1]);
			gaussianComputeShader.SetBuffer(0, "kernel", kernelBuffer);
			gaussianComputeShader.Dispatch(gaussianComputeShader.FindKernel("Main"), threadGroupSize.x, threadGroupSize.y, threadGroupSize.z);
			kernelBuffer.Release();
			//pass for Z-axis
			gaussianComputeShader.DisableKeyword("Axis_Pass_X");
			gaussianComputeShader.DisableKeyword("Axis_Pass_Y");
			gaussianComputeShader.EnableKeyword("Axis_Pass_Z");
			gaussianComputeShader.SetBuffer(0, "heatsIn", heatsBufferA);
			gaussianComputeShader.SetBuffer(0, "heatsOut", heatsBufferB);
			gaussianComputeShader.SetInts("kernelExtents", kernelExtents.z);
			kernelBuffer = new ComputeBuffer(kernel[2].Length, 4);
			kernelBuffer.SetData(kernel[2]);
			gaussianComputeShader.SetBuffer(0, "kernel", kernelBuffer);
			gaussianComputeShader.Dispatch(gaussianComputeShader.FindKernel("Main"), threadGroupSize.x, threadGroupSize.y, threadGroupSize.z);
			kernelBuffer.Release();

			heatsBufferB.GetData(heats);

			heatsBufferA.Release();
			heatsBufferB.Release();

			int DivideRoundUp(int a, int b)
			{
				return (a + (a % b)) / b;
			}
		}


		/// <summary>
		/// Get the index of a voxel in a linearized array.
		/// </summary>
		/// <param name="position">The Voxel position.</param>
		/// <returns>Index of the linearized array.</returns>
		private int PositionToIndex(Vector3Int position)
		{
			return (settings.resolution.x * ((settings.resolution.y * position.z) + position.y)) + position.x;
		}


		/// <summary>
		/// get the voxel position of a index in the linearized array.
		/// </summary>
		/// <param name="index">Index of the linearized array.</param>
		/// <returns>The Voxel position.</returns>
		private Vector3Int IndexToPosition(int index)
		{
			int z = index / (settings.resolution.x * settings.resolution.y);
			index -= (z * settings.resolution.x * settings.resolution.y);
			Vector3Int position = new Vector3Int(
				index % settings.resolution.x,
				index / settings.resolution.x,
				z);
			return position;
		}


		/// <summary>
		/// Normal Distribution Function.
		/// </summary>
		private static float NormalDistribution(float distance, float gaussStandardDeviation)
		{
			return Mathf.Exp(-0.5f * Mathf.Pow(distance / gaussStandardDeviation, 2.0f)) / (gaussStandardDeviation * Mathf.Sqrt(2 * Mathf.PI));
		}
		#endregion
	}
}
