using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOneCube : MonoBehaviour
{
    public GameObject[] v3;
    void Start()
    {
        //���ÿһ��Shpere��λ��
        Vector3[] spherePosList = new Vector3[v3.Length];
        for (int i = 0; i < v3.Length; i++)
        {
            spherePosList[i] = v3[i].transform.position;
        }


        Mesh mesh = new Mesh();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = mesh;
        mesh.vertices = spherePosList;


        //���ж��ٸ����ǵ㣬����*6
        //������������
        int[] triangles = new int[6 * 6];
        //��һ����
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 4;
        triangles[3] = 1;
        triangles[4] = 5;
        triangles[5] = 4;
        //�ڶ�����
        triangles[6] = 1;
        triangles[7] = 2;
        triangles[8] = 5;
        triangles[9] = 2;
        triangles[10] = 6;
        triangles[11] = 5;
        //��������
        triangles[12] = 2;
        triangles[13] = 3;
        triangles[14] = 6;
        triangles[15] = 3;
        triangles[16] = 7;
        triangles[17] = 6;
        //���ĸ���
        triangles[18] = 3;
        triangles[19] = 0;
        triangles[20] = 7;
        triangles[21] = 0;
        triangles[22] = 4;
        triangles[23] = 7;
        //����
        triangles[24] = 4;
        triangles[25] = 5;
        triangles[26] = 7;
        triangles[27] = 5;
        triangles[28] = 6;
        triangles[29] = 7;

        //����
        triangles[30] = 1;
        triangles[31] = 2;
        triangles[32] = 4;
        triangles[33] = 2;
        triangles[34] = 3;
        triangles[35] = 4;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


    }


}
