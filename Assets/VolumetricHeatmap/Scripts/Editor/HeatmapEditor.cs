using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace HeatmapVisualization
{
	[CustomEditor(typeof(Heatmap))]
	public class HeatmapEditor : Editor
	{
		#region Globals
		private new Heatmap target;
		private bool foldoutReferences = false;
		private bool foldoutGenerationSettings = true;
		private bool foldoutRenderingSettings = true;

		#region Target Properties
		private SerializedProperty gaussianComputeShader;
		private SerializedProperty resolution;
		private SerializedProperty cutoffPercentage;
		private SerializedProperty gaussStandardDeviation;
		private SerializedProperty colormap;
		private SerializedProperty renderOnTop;
		private SerializedProperty textureFilterMode;
		#endregion
		#endregion


		#region Functions
		private void OnEnable()
		{
			target = (Heatmap)base.target;

			//get serialized properties
			gaussianComputeShader = serializedObject.FindProperty("gaussianComputeShader");
			resolution = serializedObject.FindProperty("resolution");
			cutoffPercentage = serializedObject.FindProperty("cutoffPercentage");
			gaussStandardDeviation = serializedObject.FindProperty("gaussStandardDeviation");
			colormap = serializedObject.FindProperty("colormap");
			renderOnTop = serializedObject.FindProperty("renderOnTop");
			textureFilterMode = serializedObject.FindProperty("textureFilterMode");

			//get foldout flags
			foldoutGenerationSettings = EditorPrefs.GetBool("HeatmapEditor-foldoutGenerationSettings", foldoutGenerationSettings);
			foldoutRenderingSettings = EditorPrefs.GetBool("HeatmapEditor-foldoutRenderingSettings", foldoutRenderingSettings);
		}


		private void OnDestroy()
		{
			//save foldout flags
			EditorPrefs.SetBool("HeatmapEditor-foldoutGenerationSettings", foldoutGenerationSettings);
			EditorPrefs.SetBool("HeatmapEditor-foldoutRenderingSettings", foldoutRenderingSettings);
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			foldoutReferences = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutReferences, "Prefab References");
			if (foldoutReferences)
			{
				EditorGUILayout.PropertyField(gaussianComputeShader);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			foldoutGenerationSettings = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutGenerationSettings, "Generation Settings");
			if (foldoutGenerationSettings)
			{
				EditorGUILayout.PropertyField(resolution);
				EditorGUILayout.PropertyField(gaussStandardDeviation);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();


			foldoutRenderingSettings = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutRenderingSettings, "Rendering Settings");
			if (foldoutRenderingSettings)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(colormap);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetColormap();
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(cutoffPercentage);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetCutoffPercentage();
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(renderOnTop);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetRenderOnTop();
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(textureFilterMode);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetTextureFilterMode();
				}
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			

			serializedObject.ApplyModifiedProperties();
		}


		[DrawGizmo(GizmoType.InSelectionHierarchy)]
		static void DrawGizmos(Heatmap target, GizmoType gizmoType)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(target.BoundsFromTransform.center, target.BoundsFromTransform.size);
		}
		#endregion
	}
}
