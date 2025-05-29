using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;


public class Script_03_04 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DeleteRequestDynamicData());
    }
    #region   HTTP Methods  Testing different HTTP verbs
    /// <summary>
    /// HTTP Methods  Testing different HTTP verbs
    /// The request's DELETE parameters.
    /// </summary>
    /// <returns></returns>
    IEnumerator DeleteRequestHTTPMethods()
    {
        UnityWebRequest request = UnityWebRequest.Delete("https://www.httpbin.org/delete");
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
            Debug.Log("删除成功：" + request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError && request.error.ToLower().Contains("timeout"))
        {
            Debug.LogError("请求超时！");
        }
        else
        {
            Debug.LogError("删除失败：" + request.error);
        }
    }

    #endregion


    #region  Status codes Generates responses with given status code
    /// <summary>
    /// return status code or random status code if more than one are given 
    /// 100
    /// 200
    /// 300
    /// 400
    /// 500
    /// </summary>
    /// <returns></returns>
    IEnumerator DeleteRequestStatusCodes()
    {
        UnityWebRequest request = UnityWebRequest.Delete("https://www.httpbin.org/status/500");

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
            Debug.Log("删除成功：" + request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError && request.error.ToLower().Contains("timeout"))
        {
            Debug.LogError("请求超时！");
        }
        else
        {
            Debug.LogError("删除失败：" + request.error);
        }
    }
    #endregion

    #region  Dynamic data Generates random and dynamic data 

    /// <summary>
    /// returns a delayed response (max of 10 seconds).
    /// </summary>
    /// <returns></returns>
    IEnumerator DeleteRequestDynamicData()
    {
        UnityWebRequest request = UnityWebRequest.Delete("https://www.httpbin.org/delay/5");

        // 设置 10 秒超时
        request.timeout = 3;
        // 添加下载处理器，以便读取返回内容
        request.downloadHandler = new DownloadHandlerBuffer();
        // 发送请求
        yield return request.SendWebRequest();
        // 输出状态码
        Debug.Log("状态码: " + request.responseCode);
        // 判断请求结果
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("删除成功：" + request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError && request.error.ToLower().Contains("timeout"))
        {
            Debug.LogError("请求超时！");
        }
        else
        {
            Debug.LogError("删除失败：" + request.error);
        }
    }
    #endregion


}
