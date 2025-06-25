using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Script_26 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PatchRequestHTTP());
    }

    /// <summary>
    /// PATCH 请求示例
    /// </summary>
    IEnumerator PatchRequestHTTP()
    {
        string url = "https://reqres.in/api/users/10";

        // 准备要发送的 JSON 数据（只改 name）
        string json = "{\"name\": \"ddmz-patch\"}";
        byte[] body = Encoding.UTF8.GetBytes(json);

        // 创建 PATCH 请求
        UnityWebRequest request = new UnityWebRequest(url, "PATCH");

        // 设置上传和下载处理器
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 设置请求头
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("x-api-key", "reqres-free-v1"); // 示例 API Key
        // 可选：设置超时（秒）
        request.timeout = 10;

        // 发送请求
        yield return request.SendWebRequest();

        // 输出状态码
        Debug.Log("状态码: " + request.responseCode);

        // 判断请求结果
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("PATCH 请求成功：" + request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError && request.error.ToLower().Contains("timeout"))
        {
            Debug.LogError("请求超时！");
        }
        else
        {
            Debug.LogError("PATCH 请求失败：" + request.error);
        }
    }
}
