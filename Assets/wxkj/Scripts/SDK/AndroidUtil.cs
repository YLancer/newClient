using UnityEngine;
using System.Collections;
using System;
using cn.sharesdk.unity3d;

public class AndroidUtil : MonoBehaviour
{
    private ShareSDK shareSdk = null;
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
         shareSdk = GameObject.Find("Main Camera").GetComponent<ShareSDK>();
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
            "西凉麻将3D",
            "今天玩了一个很好玩的游戏——西凉麻将3D，你也来试试吧！哈哈!"+room
            });
    }


    public void OnShareClick(string roomId = null)
    {
        print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        ShareContent content = new ShareContent();
        string room = (roomId == null) ? "" : "房间号是：" + roomId;
        //这个地方要参考不同平台需要的参数    可以看ShareSDK提供的   分享内容参数表.docx
        content.SetTitle("西凉麻将3D");                                            //分享标题
        content.SetText("正宗金昌甩九幺——西凉3D麻将，你也来试试吧！"+ room);                            //分享文字
        content.SetImageUrl("http://pic.dafuvip.com/templates/majiang/images/bj-big.jpg");   //分享图片
        content.SetUrl("http://www.dafuv.com/wyxlmj?view=1");                                    //分享网址

        content.SetShareType(ContentType.Webpage);

        //shareSdk.ShowPlatformList(null, content, 100, 100);                      //弹出分享菜单选择列表
        // 改为下：shareSdk.ShowShareContentEditor(PlatformType.QQ, content);                 //指定平台直接分享
        shareSdk.ShowShareContentEditor(PlatformType.WeChat, content);

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
