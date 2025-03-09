using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolygonAreaCalculator : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_LineRenderer;

    private List<Vector3> points = new List<Vector3>();
    private Camera mainCamera;
    private Plane drawPlane = new Plane(Vector3.up, Vector3.zero); // 假设绘制平面是XZ平面
    /// <summary>
    /// 长度
    /// </summary>
    [SerializeField]
    private Text m_txtLine;
    /// <summary>
    /// 面积
    /// </summary>
    [SerializeField]
    private Text m_txtArea;

    void Start()
    {
        mainCamera = Camera.main;
        m_LineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左键点击添加点
        {
            AddPoint();
        }
    }

    void AddPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (drawPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            hitPoint.y = 0; // 让点保持在XZ平面上

            if (points.Count > 0)
            {
                float length = Vector3.Distance(points[points.Count - 1], hitPoint);
                Debug.Log($"点 {points.Count} 到 点 {points.Count + 1} 的长度: {length}");
                m_txtLine.text = $"点 {points.Count} 到 点 {points.Count + 1} 的长度: {length}";
            }

            points.Add(hitPoint);
            UpdateLineRenderer();

            if (points.Count >= 3)
            {
                double area = CalculatePolygonArea(points);
                Debug.Log($"当前多边形面积: {area}");
                m_txtArea.text = $"当前多边形面积: {area}";
            }
        }
    }

    void UpdateLineRenderer()
    {
        int lineCount = points.Count;

        if (lineCount < 2)
        {
            return;
        }

        // 如果点数大于 3，自动闭合多边形
        if (lineCount >= 3)
        {
            m_LineRenderer.positionCount = lineCount + 1;
            List<Vector3> closedPoints = new List<Vector3>(points) { points[0] }; // 闭合
            m_LineRenderer.SetPositions(closedPoints.ToArray());
        }
        else
        {
            m_LineRenderer.positionCount = lineCount;
            m_LineRenderer.SetPositions(points.ToArray());
        }
    }

    public float CalculatePolygonArea(List<Vector3> points)
    {
        int count = points.Count;
        if (count < 3)
        {
            Debug.LogError("小于三个点无法计算");
            return 0;
        }

        float s = 0;
        for (int i = 0; i < count; ++i)
        {
            int next = (i + 1) % count;
            s += points[i].z * (points[next].x - points[(i - 1 + count) % count].x);
        }

        return Mathf.Abs(s / 2.0f);
    }
}
