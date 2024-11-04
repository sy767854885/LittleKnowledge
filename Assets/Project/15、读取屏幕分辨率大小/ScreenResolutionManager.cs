using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionManager : MonoBehaviour
{
    [SerializeField]
    private Text m_TxtWidth;
    [SerializeField]
    private Text m_TxtHeight;
    private void Update()
    {
        GetScreenResolution();
    }


    private void GetScreenResolution()
    {
        int width = Screen.width;   // 获取屏幕宽度
        int height = Screen.height; // 获取屏幕高度
        m_TxtWidth.text = "宽度为: " + width.ToString();
        m_TxtHeight.text = "高度为: " + height.ToString();
    }
}
