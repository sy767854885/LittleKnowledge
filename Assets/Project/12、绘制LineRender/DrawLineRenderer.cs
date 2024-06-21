using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DrawLineRenderer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> GameObjectPointList = new List<GameObject>();


    private List<Vector3> goPos = new List<Vector3>();

    /// <summary>
    /// 设置颜色的数组
    /// </summary>
    List<GradientColorKey> GradientColorKeyList = new List<GradientColorKey>();
    /// <summary>
    /// 设置透明度的数组
    /// </summary>
    List<GradientAlphaKey> GradientAlphaKeyList = new List<GradientAlphaKey>();
    void Start()
    {
        //先获取点的位置Position
        for(int i =0;i<GameObjectPointList.Count;i++)
        {
            goPos.Add(GameObjectPointList[i].transform.position);
        }




        
        LineRenderer lineRender = transform.GetComponent<LineRenderer>();
        //设置数量
        lineRender.positionCount = goPos.Count;
        //设置位置
        lineRender.SetPositions(goPos.ToArray());

        Gradient gradient = new Gradient();
        //一共八个点，要分为七段，平均一段距离是1/7约等于0.1428
        //红色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 0, 0), 0));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0));
        //蓝色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(0, 0, 1), 0.142f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.142f));
        //绿色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(0, 1, 0), 0.285f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.285f));
        //黄色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 1, 0), 0.428f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.428f));
        //粉色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 0, 1), 0.571f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.571f));
        //橙色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 0.5f, 0), 0.714f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.714f));
        //浅蓝色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(0, 1, 1), 0.857f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.857f));
        //紫色
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(0.5f, 0, 1), 1f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 1));

        gradient.SetKeys(GradientColorKeyList.ToArray(), GradientAlphaKeyList.ToArray());

        lineRender.colorGradient = gradient;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
