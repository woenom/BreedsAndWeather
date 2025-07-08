using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class Request : BaseRequest
{
    private string _url;

    public Request(string url)
    {
        _url = url;
    }

    public override async Task Execute()
    {
        _webRequest = UnityWebRequest.Get(_url);
        var operation = _webRequest.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }
        if (_isCancelled) return;
        if (_webRequest.result == UnityWebRequest.Result.Success)
        {
            OnComplete?.Invoke(_webRequest.downloadHandler.text);
        }
    }
}