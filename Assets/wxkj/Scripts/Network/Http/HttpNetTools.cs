using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NetCode
{
    Unknown = -1,      // 未知错误
    Success = 0,       // 成功
    JsonError = 1,     // JSON解析错误
    CheckSumError = 1110,   // checkSum错误
    MissArgs = 2001,   // 缺少参数
    NoConfig = 5001,  // 找不到配置
}

public class HttpNetTools : MonoBehaviour
{
    private static HttpNetTools instance;
    private static HttpNetTools Instance
    {
        get
        {
            if (null == instance)
            {
                GameObject go = new GameObject("HttpNetTools");
                instance = go.AddComponent<HttpNetTools>();
            }
            return instance;
        }
    }

    public static HttpRequestCheckSum GetRequestCheckSum(System.Action<Hashtable> OnComplete = null, System.Action<NetCode, string> OnError = null)
    {
        HttpRequestCheckSum request = new HttpRequestCheckSum();
        if (null != OnComplete) request.OnComplete += OnComplete;
        if (null != OnError) request.OnError += OnError;

//        request.checkSum = HttpRequestCheckSum.GetCheckSum(HttpRequestCheckSum.GetRandomIdentifier() + Game.UserMgr.PlayerId + GlobalConfig.GetVersion);

        return request;
    }

    public static void Call(HttpRequest request)
    {
        Instance.StartCoroutine(request.doReauest());
    }

    public static void CallCheckSum(string uri, Dictionary<string, string> args = null, System.Action<Hashtable> OnComplete = null, System.Action<NetCode, string> OnError = null)
    {
        HttpRequest request = HttpNetTools.GetRequestCheckSum(OnComplete, OnError);
        request.uri = uri;
        request.args = args;
        Call(request);
    }

    public static HttpRequestXXTEA GetRequestXXTEA(System.Action<Hashtable> OnComplete = null, System.Action<NetCode, string> OnError = null)
    {
        HttpRequestXXTEA request = new HttpRequestXXTEA();
        if (null != OnComplete) request.OnComplete += OnComplete;
        if (null != OnError) request.OnError += OnError;
        return request;
    }

    public static void CallXXTEA(string uri, Dictionary<string, string> args = null, System.Action<Hashtable> OnComplete = null, System.Action<NetCode, string> OnError = null,bool useMask = true)
    {
        HttpRequest request = HttpNetTools.GetRequestXXTEA(OnComplete, OnError);
        request.uri = uri;
        request.args = args;
        request.useMask = useMask;
        Call(request);
    }
}
