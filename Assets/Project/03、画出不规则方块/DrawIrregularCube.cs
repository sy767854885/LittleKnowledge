using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawIrregularCube : MonoBehaviour
{
    /// <summary>
    /// ����ĵ�λ
    /// </summary>
    [SerializeField]
    private GameObject[] m_Points;
    /// <summary>
    /// ��λλ��
    /// </summary>
    private Vector3[] m_PointsPos;
    void Start()
    {
        
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //��ȡÿһ����λ��λ��
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
        //�������һ��Ķ��������������ı��ξ����ĸ�����
        int pointsLength = vertices.Length / 2;
        //�����������Ҫ���ٸ�������
        //int[] triangles = new int[pointsLength * 6+((pointsLength-2)*3)*2];
        //Debug.Log("pointsLength: " + pointsLength + "   triangles: " + triangles.Length);
        int[] triangles = new int[pointsLength * 6];
        ///���ǵ�λ
        int triangleIndex = 0;
        //�������ܵ���
        for (int i = 0; i < pointsLength; i++)
        {
            //˵�������һ������
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
            //��һ���� �ĵ�һ���������� 0  1 4
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = i + pointsLength;
            triangles[triangleIndex++] = i + 1;
           

            //��һ����ĵĵڶ�����������1  5  4
            triangles[triangleIndex++] = i + 1;
            triangles[triangleIndex++] = i + pointsLength;
            triangles[triangleIndex++] = i + pointsLength + 1;
           
        }
        /////��������
        //for (int i = pointsLength; i < vertices.Length; i++)
        //{
        //    //��һ����������4 5 7
        //    triangles[triangleIndex++] = pointsLength;
        //    triangles[triangleIndex++] = i + 1;
        //    triangles[triangleIndex++] = i + 2;
        //    //˵�������滭����
        //    if (i + 2 == vertices.Length-1)
        //    {
        //        break;
        //    }
        //}
        //�����ײ�  ��ס�����Ǵӵײ�����˳ʱ�뷽��
        //for (int i = pointsLength; i > 0; i--)
        //{
        //    //��һ����������0  3 2 
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
