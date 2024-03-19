using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace HeatmapVisualization
{
	[CustomEditor(typeof(HeatmapExampleData))]
	public class HeatmapExampleDataEditor : Editor
	{
		//Globals
		private new HeatmapExampleData target;


		//Functions
		private void Awake()
		{
			target = (HeatmapExampleData)base.target;
		}


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();


			if (GUILayout.Button("Generate Example Heatmap"))
			{
				Undo.RecordObject(target.GetComponent<Heatmap>(), "Generate heatmap");
				Undo.RecordObject(target.GetComponent<MeshRenderer>(), "Generate heatmap");
				Undo.RecordObject(target.GetComponent<MeshRenderer>().sharedMaterial, "Generate heatmap");
				PrefabUtility.RecordPrefabInstancePropertyModifications(target);
				target.GenerateExampleHeatmap();
			}
		}
	}
}
