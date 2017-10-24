using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class HttpRequestCheckSum : HttpRequest
{
    public string checkSum;

    public override IEnumerator doReauest()
    {
        WWWForm wf = new WWWForm();
//        wf.AddField("userid", Game.UserMgr.PlayerId);
        wf.AddField("identifier", GetRandomIdentifier());
        wf.AddField("checksum", checkSum);
        wf.AddField("platform", GlobalConfig.GetPlatformId.ToString());
        wf.AddField("version", GlobalConfig.GetVersion);

        if (null != args)
        {
            foreach (string key in args.Keys)
            {
                wf.AddField(key, args[key]);
            }
        }

        WWW sdw = new WWW(GlobalConfig.URL_ROOT + uri, wf);
        yield return sdw;
        if (sdw.error != null)
        {
            Debug.LogWarning("uri:" + uri + "; msg:" + sdw.error);
            if (null != OnError)
            {
                OnError(NetCode.Unknown, sdw.error);
            }

            yield break;
        }
        if (sdw.error == null)
        {
            try
            {
                string result = sdw.text;
                Debug.Log("Return Json:" + result);
                Hashtable sdd = MiniJSON2.jsonDecode(result) as Hashtable;

                int code = sdd["code"].GetJsonConverter().toInt();
                NetCode netCode = (NetCode)code;

                if (NetCode.Success == netCode)
                {
                    if (sdd.ContainsKey("data"))
                    {
                        object ob = sdd["data"];
                        Hashtable data = ob as Hashtable;
                        if (null != OnComplete)
                        {
                            OnComplete(data);
                        }
                    }
                    else
                    {
                        if (null != OnComplete)
                        {
                            OnComplete(sdd);
                        }
                    }
                }
                else
                {
                    string msg = "";
                    if (sdd.ContainsKey("msg"))
                    {
                        msg = sdd["msg"].GetJsonConverter().toStr();
                    }

                    Debug.LogWarning("Return Error:" + netCode.ToString() + "; msg:" + msg);
                    if (null != OnError)
                    {
                        OnError(netCode, msg);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Return Error:" + NetCode.JsonError.ToString() + "; msg:" + e.Message);
                if (null != OnError)
                {
                    OnError(NetCode.JsonError, e.Message);
                }
            }
        }
    }

    public static string GetRandomIdentifier()
    {
        string udid = Application.isEditor ? "0000000000000000000000000000000000000000" : SystemInfo.deviceUniqueIdentifier;//TODO Replace with just a random string (there is some code in Frisbee2's in-app validation that can be used if needed)
        udid += UnityEngine.Random.Range(0, int.MaxValue).ToString();
        return udid;
    }

    public static string GetCheckSum(string data)
    {
        return GetMD5Hash(data + GlobalConfig.SECRET);
    }

    private static string GetMD5Hash(string unhashed)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] us = System.Text.Encoding.UTF8.GetBytes(unhashed);// Was System.Text.Encoding.Default
        byte[] hash = md5.ComputeHash(us);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }
        return sb.ToString();
    }
}
