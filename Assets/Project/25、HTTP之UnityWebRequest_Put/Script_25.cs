using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Script_25 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PutRequestHTTP());
    }

    /// <summary>
    /// PUT 请求示例
    /// </summary>
    IEnumerator PutRequestHTTP()
    {
        string url = "https://reqres.in/api/users/2"; // 

        // 准备要发送的 JSON 数据
        string json = "{\"name\": \"ddmz\", \"job\": \"gamer\"}";
        byte[] body = Encoding.UTF8.GetBytes(json);

        // 创建 PUT 请求
        UnityWebRequest request = new UnityWebRequest(url, "PUT");

        // 设置请求体和处理器
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 设置请求头
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("x-api-key", "reqres-free-v1"); // 示例 API Key

        // 设置超时时间（秒）
        request.timeout = 10;

        // 发送请求
        yield return request.SendWebRequest();

        // 输出状态码
        Debug.Log("状态码: " + request.responseCode);

        // 判断请求结果
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("PUT 请求成功：" + request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError && request.error.ToLower().Contains("timeout"))
        {
            Debug.LogError("请求超时！");
        }
        else
        {
            Debug.LogError("PUT 请求失败：" + request.error);
        }
    }


}
