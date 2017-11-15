using  UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ImangiUtilities;

public class ConfigWord : ConfigBase
{
	public int Id{get;set;}
	public string TextContent{get;set;}
	public int Talk{get;set;}

    public static string jsonName = "ConfigWord";
    public static bool HasLoad = false;
    private static Dictionary<int, ConfigWord> _map;
    public static Dictionary<int, ConfigWord> map
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
    private static List<ConfigWord> _datas;
    public static List<ConfigWord> datas
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

	public static ConfigWord GetByKey (int key){
        if (!HasLoad)
        {
            Init();
        }

		ConfigWord data;
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
        Debug.Log(" <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<" + _map + _datas);
        List<object> list = json.listFromJson();
        _map = new Dictionary<int, ConfigWord>(list.Count);
        _datas = new List<ConfigWord>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            Dictionary<string, object> inDict = list[i] as Dictionary<string, object>;
            ConfigWord data = FromJson(inDict);
            _map.Add(data.Id, data);
            _datas.Add(data);
        }
    }

    static ConfigWord FromJson(Dictionary<string, object> inDict)
    {
        ConfigWord data = new ConfigWord();
		if (inDict.ContainsKey("Id"))  data.Id = inDict["Id"].GetJsonConverter().toInt();
		if (inDict.ContainsKey("TextContent"))  data.TextContent = inDict["TextContent"].GetJsonConverter().toStr();
		if (inDict.ContainsKey("Talk"))  data.Talk = inDict["Talk"].GetJsonConverter().toInt();

        return data;
    }
}