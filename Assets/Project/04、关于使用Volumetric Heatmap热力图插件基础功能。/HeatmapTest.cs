/****************************************************
    文件：HeatmapTest.cs
	作者：Sy
    邮箱: 767854885@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/

using HeatmapVisualization;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapTest : MonoBehaviour 
{
    [SerializeField]
    private Heatmap m_OwnHeatmap;
    /// <summary>
    /// 用于存储生成物体的位置
    /// </summary>
    private List<Vector3> m_pointPosList = new List<Vector3>();

    private List<GameObject> m_pointsGoList = new List<GameObject>();


    public void GenerateExampleHeatmap()
    {
        m_pointPosList.Clear();
        //遍历所有生成的物体，获得位置并且存储下来
        for (int i = 0; i < m_pointsGoList.Count; i++)
        {
            Vector3 v3 = m_pointsGoList[i].transform.position;
            m_pointPosList.Add(v3);
        }

        //刷新显示热力点
        m_OwnHeatmap.GenerateHeatmap(m_pointPosList);
    }
    //pri
    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 从鼠标位置发射一条射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果射线与物体碰撞
            if (Physics.Raycast(ray, out hit))
            {

                // 检查是否碰撞到的是 Plan
                if (hit.collider.gameObject.name =="Plane")
                {
                    // 在点击的位置生成空物体
                    GameObject tempGo = new GameObject();
                    tempGo.transform.position = hit.point;
                    m_pointsGoList.Add(tempGo);
                    GenerateExampleHeatmap();
                }
            }
        }
    }
}