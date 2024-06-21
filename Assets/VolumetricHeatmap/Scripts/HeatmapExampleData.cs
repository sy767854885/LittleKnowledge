using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeatmapVisualization
{
	public class HeatmapExampleData : MonoBehaviour
	{
		#region Settings
		[SerializeField]
		private int examplePointsAmount = 100;
		#endregion


		#region Globals
		private Heatmap ownHeatmap;
		private Heatmap OwnHeatmap { get { if (ownHeatmap == null) { ownHeatmap = GetComponent<Heatmap>(); } return ownHeatmap; } }
		#endregion


		#region Functions
		public void GenerateExampleHeatmap()
		{
			List<Vector3> points = GetRandomPoints(examplePointsAmount, OwnHeatmap.BoundsFromTransform);
			OwnHeatmap.GenerateHeatmap(points);
		}


		private List<Vector3> GetRandomPoints(int amount, Bounds bounds)
		{
			List<Vector3> points = new List<Vector3>();

			for (int i = 0; i < amount; i++)
			{
				Vector3 point = new Vector3(
					Random.Range(bounds.min.x, bounds.max.x),
					Random.Range(bounds.min.y, bounds.max.y),
					Random.Range(bounds.min.z, bounds.max.z));
				points.Add(point);
			}

			return points;
		}
		#endregion
	}
}
