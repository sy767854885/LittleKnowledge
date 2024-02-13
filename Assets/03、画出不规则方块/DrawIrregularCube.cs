using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawIrregularCube : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Points;
    private Vector3[] m_PointsPos;
    void Start()
    {
        Mesh mesh = new Mesh();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //获取每一个点位的位置
        m_PointsPos = new Vector3[m_Points.Length];
        for (int i = 0; i < m_Points.Length; i++)
        {
            m_PointsPos[i] = m_Points[i].transform.position;
        }

        meshFilter.mesh = mesh;
        CreateWall(m_PointsPos, mesh);
    }
    public void CreateWall(Vector3[] points,Mesh mesh)
    {
        Vector3[] vertices = points;
        int pointsLength = vertices.Length / 2;
        //计算出四周需要多少个三角形
        int[] triangles = new int[pointsLength * 6+((pointsLength-2)*3)];
        int triangleIndex = 0;

        Debug.Log("pointsLength: " + pointsLength);
        for (int i = 0; i < pointsLength; i++)
        {
            if (i == pointsLength - 1)
            {
                // 第一个矩形三角形
                triangles[triangleIndex++] = i;
                triangles[triangleIndex++] = 0;
                triangles[triangleIndex++] = i + pointsLength;

                // 第二个矩形三角形
                triangles[triangleIndex++] = 0;
                triangles[triangleIndex++] = i + 1;
                triangles[triangleIndex++] = i + pointsLength;
                break;
            }
            // 第一个矩形三角形
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = i + 1;
            triangles[triangleIndex++] = i + pointsLength;

            // 第二个矩形三角形
            triangles[triangleIndex++] = i + 1;
            triangles[triangleIndex++] = i + pointsLength + 1;
            triangles[triangleIndex++] = i + pointsLength;
        }
        ///画出顶部
        for (int i = pointsLength; i < vertices.Length; i++)
        {
            triangles[triangleIndex++] = pointsLength;
            triangles[triangleIndex++] = i + 1;
            triangles[triangleIndex++] = i + 2;
            Debug.Log(i);
            if (i + 2 == vertices.Length-1)
            {
                break;
            }
        }


        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
