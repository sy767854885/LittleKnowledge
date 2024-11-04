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
        int width = Screen.width;   // ��ȡ��Ļ���
        int height = Screen.height; // ��ȡ��Ļ�߶�
        m_TxtWidth.text = "���Ϊ: " + width.ToString();
        m_TxtHeight.text = "�߶�Ϊ: " + height.ToString();
    }
}
