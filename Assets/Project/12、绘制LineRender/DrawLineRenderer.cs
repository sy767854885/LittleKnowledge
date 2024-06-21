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
    /// ������ɫ������
    /// </summary>
    List<GradientColorKey> GradientColorKeyList = new List<GradientColorKey>();
    /// <summary>
    /// ����͸���ȵ�����
    /// </summary>
    List<GradientAlphaKey> GradientAlphaKeyList = new List<GradientAlphaKey>();
    void Start()
    {
        //�Ȼ�ȡ���λ��Position
        for(int i =0;i<GameObjectPointList.Count;i++)
        {
            goPos.Add(GameObjectPointList[i].transform.position);
        }




        
        LineRenderer lineRender = transform.GetComponent<LineRenderer>();
        //��������
        lineRender.positionCount = goPos.Count;
        //����λ��
        lineRender.SetPositions(goPos.ToArray());

        Gradient gradient = new Gradient();
        //һ���˸��㣬Ҫ��Ϊ�߶Σ�ƽ��һ�ξ�����1/7Լ����0.1428
        //��ɫ
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 0, 0), 0));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0));
        //��ɫ
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(0, 0, 1), 0.142f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.142f));
        //��ɫ
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(0, 1, 0), 0.285f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.285f));
        //��ɫ
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 1, 0), 0.428f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.428f));
        //��ɫ
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 0, 1), 0.571f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.571f));
        //��ɫ
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(1, 0.5f, 0), 0.714f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.714f));
        //ǳ��ɫ
        GradientColorKeyList.Add(new GradientColorKey(new UnityEngine.Color(0, 1, 1), 0.857f));
        GradientAlphaKeyList.Add(new GradientAlphaKey(1, 0.857f));
        //��ɫ
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
