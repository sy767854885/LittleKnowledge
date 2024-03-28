/****************************************************
    文件：DataBit.cs
	作者：Sy
    邮箱: 767854885@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/

using System;
using UnityEngine;

public class DataBit : MonoBehaviour 
{
    void Start()
    {
        WriteChar();
    }
    /// <summary>
    /// int 
    /// </summary>
    /// <param name="value"></param>
    public void WriteInt()
    {
        int value = -2147483648;
        byte[] arr = BitConverter.GetBytes(value);
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log($"arr {i}: {arr[i]}");
        }

    }

    /// <summary>
    /// char
    /// </summary>
    /// <param name="value"></param>
    public void WriteChar()
    {
        char value = 'A';
        byte[] arr = BitConverter.GetBytes(value);
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log($"arr {i}: {arr[i]}");
        }

    }
}