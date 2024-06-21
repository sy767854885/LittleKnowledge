using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawIrregularCube : MonoBehaviour
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
    void Start()
    {
        
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //获取每一个点位的位置
        m_PointsPos = new Vector3[m_Points.Length];
        for (int i = 0; i < m_Points.Length; i++)
        {
            m_PointsPos[i] = m_Points[i].transform.localPosition;
        }
        meshFilter.mesh.vertices = m_PointsPos;
        meshFilter.mesh.triangles =  DrawCube(m_PointsPos);
        
    }
    public int[] DrawCube(Vector3[] points)
    {
        Vector3[] vertices = points;
        //算出单独一遍的顶点数量，例如四边形就是四个顶点
        int pointsLength = vertices.Length / 2;
        //计算出四周需要多少个三角形
        //int[] triangles = new int[pointsLength * 6+((pointsLength-2)*3)*2];
        //Debug.Log("pointsLength: " + pointsLength + "   triangles: " + triangles.Length);
        int[] triangles = new int[pointsLength * 6];
        ///三角点位
        int triangleIndex = 0;
        //画出四周的面
        for (int i = 0; i < pointsLength; i++)
        {
            //说明是最后一个面了
            if (i == pointsLength - 1)
            {
             
                triangles[triangleIndex++] = i;
                triangles[triangleIndex++] = i + pointsLength;
                triangles[triangleIndex++] = 0;
                

        
                triangles[triangleIndex++] = 0;
                triangles[triangleIndex++] = i + pointsLength;
                triangles[triangleIndex++] = i + 1;
               
                break;
            }
            //第一个面 的第一个三角形是 0  1 4
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = i + pointsLength;
            triangles[triangleIndex++] = i + 1;
           

            //第一个面的的第二个三角形是1  5  4
            triangles[triangleIndex++] = i + 1;
            triangles[triangleIndex++] = i + pointsLength;
            triangles[triangleIndex++] = i + pointsLength + 1;
           
        }
        /////画出顶部
        //for (int i = pointsLength; i < vertices.Length; i++)
        //{
        //    //第一个三角面是4 5 7
        //    triangles[triangleIndex++] = pointsLength;
        //    triangles[triangleIndex++] = i + 1;
        //    triangles[triangleIndex++] = i + 2;
        //    //说明三角面画完了
        //    if (i + 2 == vertices.Length-1)
        //    {
        //        break;
        //    }
        //}
        //画出底部  记住我们是从底部画，顺时针方向
        //for (int i = pointsLength; i > 0; i--)
        //{
        //    //第一个三角面是0  3 2 
        //    triangles[triangleIndex++] = 0;
        //    triangles[triangleIndex++] = i - 1;
        //    triangles[triangleIndex++] = i - 2;
        //    if (i - 2 == 1)
        //    {
        //        break;
        //    }
        //}
      
        return triangles;
     
    }
}
