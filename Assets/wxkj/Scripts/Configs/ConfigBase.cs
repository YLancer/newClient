using UnityEngine;
using System.Collections;

public class ConfigBase
{
    private const string jsonPath = "Configs/{0}";
    public static string JsonStr(string jsonName)
    {
        // 如果有下载的json配置文件优先读下载的
        if (ConfigManager.HasLocalFile(jsonName))
        {
            return fromLocalFile(jsonName);
        }
        // 否则 读取Resources里面自带的
        else
        {
            return fromResources(jsonName);
        }
    }

    private static string fromResources(string jsonName)
    {
        string path = string.Format(jsonPath, jsonName);
        TextAsset textAsset = Resources.Load<TextAsset>(path);

        string json = CheckAndReturn(textAsset.text);
        return json;
    }

    private static string fromLocalFile(string jsonName)
    {
        string path = ConfigManager.GetSaveFilePath(jsonName);
        string localJson = ConfigManager.ReadFromFile(path);

        string json = CheckAndReturn(localJson);
        if (null != json)
        {
            return json;
        }
        return fromResources(jsonName);
    }

    public static string CheckAndReturn(string str)
    {
        string key = str.Substring(0, str.IndexOf(":::"));
        if (null!= key && key.Length > 32)
        {
            // 在某些编码格式下会有多余的不可见字符在头部
            key = key.Substring(key.Length - 32);
        }
        string json = str.Substring(str.IndexOf(":::") + 3);
        if (json.EndsWith("\r\n"))
        {
            json = json.Substring(0, json.Length - 2);
        }

        string key2 = getCheckSum(json);
        if (key == key2)
        {
            return json;
        }
        return null;
    }

    public static string getCheckSum(string data)
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
