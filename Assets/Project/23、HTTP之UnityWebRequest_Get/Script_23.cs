using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Script_23 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequestDynamicData());
    }
    /// <summary>
    /// Get请求
    /// </summary>
    /// <returns></returns>
    IEnumerator GetRequestHTTPMethods()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://www.httpbin.org/get");

        // 设置 10 秒超时
        request.timeout = 10;

        // 添加下载处理器，以便读取返回内容
        request.downloadHandler = new DownloadHandlerBuffer();

        // 发送请求
        yield return request.SendWebRequest();

        // 输出状态码
        Debug.Log("状态码: " + request.responseCode);

        // 判断请求结果
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("GET 请求成功：" + request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError && request.error.ToLower().Contains("timeout"))
        {
            Debug.LogError("请求超时！");
        }
        else
        {
            Debug.LogError("GET 请求失败：" + request.error);
        }
    }

    /// <summary>
    /// Get 延时5秒请求回数据
    /// </summary>
    /// <returns></returns>
    IEnumerator GetRequestDynamicData()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://www.httpbin.org/delay/5");

        // 设置 10 秒超时
        request.timeout = 10;
        // 添加下载处理器，以便读取返回内容
        request.downloadHandler = new DownloadHandlerBuffer();
        // 发送请求
        yield return request.SendWebRequest();
        // 输出状态码
        Debug.Log("状态码: " + request.responseCode);
        // 判断请求结果
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("延时请求到的数据：" + request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError && request.error.ToLower().Contains("timeout"))
        {
            Debug.LogError("请求超时！");
        }
        else
        {
            Debug.LogError("延时请求失败：" + request.error);
        }
    }
}
