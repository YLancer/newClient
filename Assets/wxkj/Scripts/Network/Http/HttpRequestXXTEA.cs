using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class HttpRequestXXTEA : HttpRequest
{
    System.Text.Encoding encoderUTF8 = Encoding.UTF8;
    protected bool mNeedAuthor = false;

    public override IEnumerator doReauest()
    {
        bool hasLoadingPage = Game.UIMgr.IsSceneActive(UIPage.LoadingPage);
        if (useMask && !hasLoadingPage)
        {
            Game.LoadingPage.Show(LoadPageType.OnlyMask);
        }
        
        string url = GlobalConfig.URL_ROOT + uri;

        if (args == null)
        {
            args = new Dictionary<string, string>();
        }

        CheckCommonParam();

        WWWForm wwwform = getForm(url);

        WWW www = new WWW(url, wwwform.data, GetHeader(wwwform));

        yield return www;

        while (!www.isDone)
        {
            yield return 0;
        }

        if (www.error == null)
        {
            NetCode netCode = NetCode.Unknown;
            Hashtable sdd = null;
            string msg = "";
            try
            {
                string jsons = www.text;
                // 解密
#if UNITY_IOS || UNITY_IPHONE
				Debug.Log(url + ": net jsons == " + jsons);
#elif UNITY_ANDROID
				jsons = GetJsonFormXXTea(jsons , url);
				Debug.Log(url + ": net jsons == " + jsons);
#endif

                sdd = MiniJSON2.jsonDecode(jsons) as Hashtable;

                int code = sdd["error_code"].GetJsonConverter().toInt();
                netCode = (NetCode)code;
            }
            catch (System.Exception e)
            {
                netCode = NetCode.JsonError;
                msg = e.Message;
            }

            if (NetCode.Success == netCode)
            {
                Hashtable data = null;
                if (sdd.ContainsKey("data"))
                {
                    object ob = sdd["data"];
                    data = ob as Hashtable;
                    
                }

                if (null != OnComplete)
                {
                    OnComplete(data);
                }
            }
            else
            {
                if (netCode != NetCode.JsonError && null != sdd && sdd.ContainsKey("msg"))
                {
                    msg = sdd["msg"].GetJsonConverter().toStr();
                }

                Debug.LogWarning("Return Error:" + netCode.ToString() + "; msg:" + msg);

                if (null != OnError)
                {
                    OnError(netCode, msg);
                }
            }

            if (useMask && !hasLoadingPage)
            {
                Game.LoadingPage.Hide();
            }
        }
    }

    // 检测公共参数
    private void CheckCommonParam()
    {
        if (!args.ContainsKey("plat_id")) args.Add("plat_id", GlobalConfig.GetPlatformId.ToString());
        if (!args.ContainsKey("version")) args.Add("version", GlobalConfig.GetVersion);
        if (!args.ContainsKey("appkey")) args.Add("appkey", GlobalConfig.GetAppKey);
        if (!args.ContainsKey("deviceid")) args.Add("deviceid", GlobalConfig.DeviceId);
//        if (!args.ContainsKey("userId")) args.Add("userId", Game.UserMgr.PlayerId);
    }

    public Dictionary<string, string> GetHeader(WWWForm wwwform)
    {
        return wwwform.headers;
    }

    protected WWWForm getForm(string url)
    {
        WWWForm form = new WWWForm();

        if (mNeedAuthor)
        {
            addAuthor();
        }

        string postString = normalizeParameters(true);

        Debug.Log("url=" + url + "; postString == " + postString);

        //SkyNet.SkyNetDebugLog(postString);
        // 加密

#if UNITY_IOS || UNITY_IPHONE
		//foreach (DictionaryEntry objDE in args) 
		//{ 
		//	if(objDE.Key != null && objDE.Value != null)
		//	{
		//		form.AddField(objDE.Key.ToString() , objDE.Value.ToString());
		//	}
		//} 
		return form;
#else
        Byte[] bytes = MyXXTEA.XXTEA.Encrypt(encoderUTF8.GetBytes(postString), encoderUTF8.GetBytes(GlobalConfig.SECRET));

        if (bytes == null)
            Debug.LogError(" get the xxxteas is null ");

        string xxteas = Convert.ToBase64String(bytes);
        form.AddField("data", xxteas);
        return form;
#endif
    }

    private void addAuthor()
    {
        try
        {
            string authorString = WWW.EscapeURL(getAuthorString());
            args.Add("Authorization", authorString);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private string getAuthorString()
    {
        return "";
    }

    public string normalizeParameters(bool isPost = false)
    {
        StringComparer ordICCmp = StringComparer.Ordinal;
        ArrayList al = new ArrayList(args.Keys);
        al.Sort(ordICCmp);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string ss = null;
        for (int i = 0; i < al.Count; i++)
        {
            ss = al[i].ToString();
            sb.Append(ss);
            sb.Append("=");
            sb.Append(Convert.ToString(args[ss]));
            if (isPost && i < al.Count - 1)
                sb.Append("&");
        }

        return sb.ToString();
    }

    protected string GetJsonFormXXTea(string xxteaString, string url)
    {
        //Debug.LogError("xxteaString == " + xxteaString);
        string decodeString = "";
        byte[] arrFromBase64String = null;
        try
        {
            arrFromBase64String = Convert.FromBase64String(xxteaString);
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading " + url + ", Convert.FromBase64String with exception : " + e.ToString());
        }
        if (arrFromBase64String != null)
        {
            Byte[] decryptedBytes = MyXXTEA.XXTEA.Decrypt(arrFromBase64String, encoderUTF8.GetBytes(GlobalConfig.SECRET));
            if (decryptedBytes != null)
            {
                try
                {
                    decodeString = encoderUTF8.GetString(decryptedBytes);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error reading " + url + ", encoderUTF8.GetString with Exception : " + e.ToString());
                }
            }
        }
        return decodeString;
    }
}
