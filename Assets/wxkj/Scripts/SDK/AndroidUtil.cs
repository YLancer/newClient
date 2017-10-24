using UnityEngine;
using System.Collections;
using System;

public class AndroidUtil : MonoBehaviour
{

    AndroidJavaObject m_kJavaObject;
    public Action<string, string> m_kActOnWeChatLogin;
    public Action<string> m_kActPurchaseSuccess;
    public Action<string> m_kActPurchaseFailures;

    void OnDisable()
    {
        print("AndroidUtil OnDisable");
    }

    void Awake()
    {
        //AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        //if (jc != null)
        //{
        //    m_kJavaObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //    if (m_kJavaObject == null)
        //    {
        //        Debug.LogError("AndroidJavaObject is null...");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("AndroidJavaClass is null...");
        //}
    }

    void Start()
    {
      //  AndroidJNI.AttachCurrentThread();
    }

    public void WeChatLoginCallBack(string response)
    {
        Debug.Log("AndroidUtil.WeChatLoginCallBack..." + response);

        string[] infos = response.Split('|');
        if (m_kActOnWeChatLogin != null)
        {
            m_kActOnWeChatLogin.Invoke(infos[0], infos[1]);
        }
        //WeChatLoginInfo kWeChatLoginInfo = JsonUtility.FromJson<WeChatLoginInfo> (response);

        //if (m_kActOnWeChatLogin != null && kWeChatLoginInfo != null) {
        //	m_kActOnWeChatLogin.Invoke (kWeChatLoginInfo.nickname, kWeChatLoginInfo.headimgurl);
        //} else {
        //	Debug.LogError ("WeChatLoginInfo is null");
        //}
    }

    public void PurchaseSuccess(string productId)
    {

        if (m_kActPurchaseSuccess != null)
        {
            m_kActPurchaseSuccess.Invoke(productId);
        }
    }

    public void PurchaseFailure(string productId)
    {
        if (m_kActPurchaseFailures != null)
        {
            m_kActPurchaseFailures.Invoke(productId);
        }
    }

    public AndroidJavaObject AndroidJavaObject
    {
        get
        {
            return m_kJavaObject;
        }
    }

    public void Share(string roomId = null)
    {
        string room = (roomId == null) ? "" : "房间号是：" + roomId;
        AndroidJavaObject.Call("WeChatShare",
            new object[] {
            "http://www.daqingxinshikong.com/WebServer/?from=singlemessage&isappinstalled=1",
            "新时空大庆麻将3D",
            "今天玩了一个很好玩的游戏——大庆麻将3D，你也来试试吧！哈哈!"+room
            });
    }

    IEnumerator GetCapture()
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
        byte[] imagebytes = tex.EncodeToPNG();//转化为png图  

        //tex.Compress(false);//对屏幕缓存进行压缩  

        //File.WriteAllBytes(Application.persistentDataPath + "/screencapture.png", imagebytes);//存储png图  
        AndroidJavaObject.Call("WeChatShareImage", new object[] { imagebytes });
    }

    public void ShareImage()
    {
        StartCoroutine(GetCapture());
    }
}

[SerializeField]
public class WeChatLoginInfo
{
    public string openid;
    public string nickname;
    public int sex;
    public string language;
    public string city;
    public string province;
    public string headimgurl;
}
