using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ConfigManager : MonoBehaviour {
    public static ConfigManager SharedInstance;

    public System.Action<string> OnLoad;

    // 配置文件的网络地址
    public string Config_RemotePath = "/Config/";
    public bool Loaded = false;

    private Dictionary<string, int> oldMap;
    private Dictionary<string, int> newMap;

    class Loader
    {
        private System.Action<string, string> OnLoad;
        private string jsonName;

        public void LoadConfig(string jsonName, System.Action<string, string> OnLoad)
        {
            this.jsonName = jsonName;
            this.OnLoad = OnLoad;

            string url = GetUrl(jsonName);
            ConfigManager.SharedInstance.StartCoroutine(doLoad(url));
        }

        IEnumerator doLoad(string url)
        {
            Debug.Log(url);
            WWW www = new WWW(url);
            yield return www;

            if (www.isDone && www.error == null)
            {
                string json = www.text;
                OnLoad(jsonName, json);
            }
        }

        string GetUrl(string jsonName)
        {
            //string channel = "";//GetChannelNumber();
            //string platform = "";//GetPlatformSegment();
            string version = "1.0";// GetVersionName();
            string url = ConfigManager.SharedInstance.Config_RemotePath + version + "/" + jsonName + ".txt";
            return url;
        }
    }

    void Awake()
    {
        SharedInstance = this;
        Loaded = false;
        LoadConfig();
    }

    void LoadConfig()
    {
        if (HasLocalFile(ConfigVersion.jsonName))
        {
            string localPath = ConfigManager.GetSaveFilePath(ConfigVersion.jsonName);
            string json = ReadFromFile(localPath);
            Dictionary<string, object> loadedData = MiniJson.Json.Deserialize(json) as Dictionary<string, object>;
//            if (SaveLoad.Load(loadedData) == false)
//            {
//#if !UNITY_EDITOR			
//            return;
//#endif
//            }
//            loadedData = loadedData["data"] as Dictionary<string, object>;

            oldMap = new Dictionary<string, int>();
            foreach (string key in loadedData.Keys)
            {
                int ver = loadedData[key].GetJsonConverter().toInt();
                oldMap.Add(key, ver);
            }
            //Dictionary<string, object> secureData = SaveLoad.Save(oldMap);
            //string jsonString = MiniJson.Json.Serialize(secureData);
        }
        else
        {
            oldMap = new Dictionary<string, int>();
            foreach (ConfigVersion config in ConfigVersion.datas)
            {
                oldMap.Add(config.configName, config.confVersion);
            }
        }

        (new Loader()).LoadConfig(ConfigVersion.jsonName, OnLoadVersionConfig);
    }

    void OnLoadVersionConfig(string jsonName, string json)
    {
        if (jsonName != ConfigVersion.jsonName)
        {
            return;
        }

        // new one
        json = ConfigBase.CheckAndReturn(json);
        if (null == json)
        {
            return;
        }

        ConfigVersion.FromJson(json);
        ConfigVersion.HasLoad = true;

        newMap = new Dictionary<string, int>();
        foreach (ConfigVersion config in ConfigVersion.datas)
        {
            newMap.Add(config.configName, config.confVersion);
        }

        Loaded = true;

        foreach (string jsName in newMap.Keys)
        {
            if (HasNewVersion(jsName))
            {
                DownloadConfig(jsName);
            }
        }
    }

    // 获取存档文件路径//
    public static string GetSaveFilePath(string jsonName)
    {
        string dataPath = Application.persistentDataPath;
        string fileName = Path.Combine(dataPath, "config");
        fileName = Path.Combine(fileName, jsonName + ".txt");
        return fileName;
    }

    // 下载并存起来
    void DownloadConfig(string jsonName)
    {
        (new Loader()).LoadConfig(jsonName, OnDownloadConfig);
    }

    public void OnDownloadConfig(string jsonName,string json)
    {
        string savePath = GetSaveFilePath(jsonName);
        Debug.Log(savePath);
        WriteToFile(savePath, json);

        if (HasNewVersion(jsonName))
        {
            int newVersion = newMap[jsonName];

            if (oldMap.ContainsKey(jsonName))
            {
                oldMap[jsonName] = newVersion;
            }
            else
            {
                oldMap.Add(jsonName, newVersion);
            }

            string jsonString = MiniJson.Json.Serialize(oldMap);
            string fileName = GetSaveFilePath(ConfigVersion.jsonName);
            WriteToFile(fileName, jsonString);
            //TRWorldInfo.TRPersistentData.SetPendingSerialize();
        }
    }

    // 判断服务器是否有新配置
    public bool HasNewVersion(string jsonName)
    {
        if (!Loaded)
        {
            return false;
        }

        if (jsonName == ConfigVersion.jsonName)
        {
            return false;
        }

        if (newMap.ContainsKey(jsonName) == false)
        {
            return false;
        }

        // 如果旧的没有新的有就表示有新版本
        if (oldMap.ContainsKey(jsonName) == false)
        {
            return true;
        }

        int oldVersion = oldMap[jsonName];
        int newVersion = newMap[jsonName];
        if (oldVersion < newVersion)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void WriteToFile(string fileName, string jsonString)
    {
        Debug.Log("WriteToFile to: " + fileName);
        try
        {
            string path = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter fileWriter = File.CreateText(fileName))
            {
                fileWriter.WriteLine(jsonString);
                fileWriter.Close();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("WriteToFile Exception: " + e);
        }
    }

    public static string ReadFromFile(string fileName)
    {
        if (File.Exists(fileName) == false)
        {
            return null;
        }

        string jsonString = "";

        try
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
                jsonString = reader.ReadToEnd();
                reader.Close();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("WriteToFile Exception: " + e);
        }

        return jsonString;
    }

    public static bool HasLocalFile(string jsonName)
    {
        string localPath = ConfigManager.GetSaveFilePath(jsonName);
        return File.Exists(localPath);
    }

}
