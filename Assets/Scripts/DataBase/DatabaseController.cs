using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

public static class DatabaseController
{
    public static async Task<T> Get<T>(string url)
    {
        var getRequest = await CreateRequest(url);

        getRequest.SendWebRequest();
        while (!getRequest.isDone) await Task.Delay(2);
        return JsonConvert.DeserializeObject<T>(getRequest.downloadHandler.text);
    }
    public static async Task<T> Post<T>(string url, object data)
    {
        var postRequest = await CreateRequest(url, RequestTypes.POST, data);
        postRequest.SendWebRequest();
        while (!postRequest.isDone) await Task.Delay(2);
        return JsonConvert.DeserializeObject<T>(postRequest.downloadHandler.text);

    }
    public static async Task<T> Put<T>(string url, object data)
    {
        var putRequest = await CreateRequest(url, RequestTypes.PUT, data);
        putRequest.SendWebRequest();
        while (!putRequest.isDone) await Task.Delay(2);
        return JsonConvert.DeserializeObject<T>(putRequest.downloadHandler.text);

    }
    private static Task<UnityWebRequest> CreateRequest(string path, RequestTypes type = RequestTypes.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());
        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return Task.FromResult(request);
    }
    private static void AtackHandler(UnityWebRequest request, string key, string value)
    {
        request.SetRequestHeader(key, value);
    }



}

public enum RequestTypes
{
    GET = 0,
    POST = 1,
    PUT = 2,
}
