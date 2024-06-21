using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMeshPlane : MonoBehaviour
{
    /// <summary>
    /// 物体的点位
    /// </summary>
    [SerializeField]
    private GameObject[] m_Points;
    /// <summary>
    /// 点位位置
    /// </summary>
    private Vector3[] m_PointsPos;

    private void Start()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        //获取每一个点位的位置
        m_PointsPos = new Vector3[m_Points.Length];
        for (int i = 0; i < m_Points.Length; i++)
        {
            m_PointsPos[i] = m_Points[i].transform.localPosition;
        }
        meshFilter.mesh.vertices = m_PointsPos;
        meshFilter.mesh.triangles = DrawCube(m_PointsPos);
        // 计算并设置UV坐标
        meshFilter.mesh.uv = CalculateUVs(m_PointsPos);
    }
    public int[] DrawCube(Vector3[] points)
    {
        Vector3[] vertices = points;
        //算出单独一遍的顶点数量，例如四边形就是四个顶点
        
        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 0;
        return triangles;

    }
    private Vector2[] CalculateUVs(Vector3[] points)
    {
        Vector2[] uvs = new Vector2[points.Length];

        // Assuming the points are in a clockwise order starting from the bottom-left corner
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 1);

        return uvs;
    }
}
