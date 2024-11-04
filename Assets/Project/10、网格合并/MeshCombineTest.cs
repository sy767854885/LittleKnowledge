using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshCombineTest : MonoBehaviour
{
    //[MenuItem("GameObject/ºÏ²¢Íø¸ñ",priority = 0)]
    public static void MeshCombine()
    {
        //MeshFilter[] meshFilters = Selection.activeGameObject.GetComponentsInChildren<MeshFilter>();
        //CombineInstance[] meshCombine = new CombineInstance[meshFilters.Length];
        //for(int i =0;i<meshFilters.Length;i++)
        //{
        //    meshCombine[i].mesh = meshFilters[i].sharedMesh;

        //    meshCombine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        //}

        //Mesh mesh = new Mesh();
        //mesh.CombineMeshes(meshCombine);
        //GameObject obj = new GameObject(Selection.activeGameObject.name);
        //obj.AddComponent<MeshFilter>().mesh = mesh;
        //obj.AddComponent<MeshRenderer>().sharedMaterial = meshFilters[0].gameObject.GetComponent<MeshRenderer>().sharedMaterial;
    }

}
