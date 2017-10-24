using  UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ImangiUtilities;

public class ConfigVersion : ConfigBase
{
	public string configName{get;set;}
	public int confVersion{get;set;}

    public static string jsonName = "ConfigVersion";
    public static bool HasLoad = false;
    private static Dictionary<string, ConfigVersion> _map;
    public static Dictionary<string, ConfigVersion> map
    {
        get
        {
            if (!HasLoad)
            {
                Init();
            }

            return _map;
        }
    }
    private static List<ConfigVersion> _datas;
    public static List<ConfigVersion> datas
    {
        get
        {
            if (!HasLoad)
            {
                Init();
            }

            return _datas;
        }
    }

	public static ConfigVersion GetByKey (string key){
        if (!HasLoad)
        {
            Init();
        }

		ConfigVersion data;
		if (_map.TryGetValue (key, out data)) {
			return data;
		} else {
			return null;
		}
	}

    public static void Init()
    {
        FromJson(JsonStr(jsonName));

        HasLoad = true;
    }

    public static void FromJson(string json)
    {
        List<object> list = json.listFromJson();
        _map = new Dictionary<string, ConfigVersion>(list.Count);
        _datas = new List<ConfigVersion>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            Dictionary<string, object> inDict = list[i] as Dictionary<string, object>;
            ConfigVersion data = FromJson(inDict);
            _map.Add(data.configName, data);
            _datas.Add(data);
        }
    }

    static ConfigVersion FromJson(Dictionary<string, object> inDict)
    {
        ConfigVersion data = new ConfigVersion();
        data.configName = inDict["configName"].GetJsonConverter().toStr();
        data.confVersion = inDict["confVersion"].GetJsonConverter().toInt();

        return data;
    }
}