using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Script_24 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RegisterWithApiKey());
    }


    IEnumerator RegisterWithApiKey()
    {
        string url = "https://reqres.in/api/register";

        string json = "{\"email\": \"eve.holt@reqres.in\", \"password\": \"pistol\"}";
        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 设置必要的请求头
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("x-api-key", "reqres-free-v1");  // ✅ 必须添加！

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ 注册成功: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"❌ 注册失败: {request.responseCode} | {request.error}");
            Debug.LogError("返回内容: " + request.downloadHandler.text);
        }
    }

}
