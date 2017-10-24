using  UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ImangiUtilities;

public class ConfigRooms : ConfigBase
{
	public int RoomId{get;set;}
	public string RoomName{get;set;}
	public RoomType RoomType{get;set;}
	public MatchType MatchType{get;set;}
	public int  BaseScore{get;set;}
	public int  MinCoinLimit{get;set;}
	public int  MaxCoinLimit{get;set;}
	public string Icon{get;set;}

    public static string jsonName = "ConfigRooms";
    public static bool HasLoad = false;
    private static Dictionary<int, ConfigRooms> _map;
    public static Dictionary<int, ConfigRooms> map
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
    private static List<ConfigRooms> _datas;
    public static List<ConfigRooms> datas
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

	public static ConfigRooms GetByKey (int key){
        if (!HasLoad)
        {
            Init();
        }

		ConfigRooms data;
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
        _map = new Dictionary<int, ConfigRooms>(list.Count);
        _datas = new List<ConfigRooms>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            Dictionary<string, object> inDict = list[i] as Dictionary<string, object>;
            ConfigRooms data = FromJson(inDict);
            _map.Add(data.RoomId, data);
            _datas.Add(data);
        }
    }

    static ConfigRooms FromJson(Dictionary<string, object> inDict)
    {
        ConfigRooms data = new ConfigRooms();
		if (inDict.ContainsKey("RoomId"))  data.RoomId = inDict["RoomId"].GetJsonConverter().toInt();
		if (inDict.ContainsKey("RoomName"))  data.RoomName = inDict["RoomName"].GetJsonConverter().toStr();
		if (inDict.ContainsKey("RoomType"))  data.RoomType = inDict["RoomType"].GetJsonConverter().toEnum<RoomType>();
		if (inDict.ContainsKey("MatchType"))  data.MatchType = inDict["MatchType"].GetJsonConverter().toEnum< MatchType>();
		if (inDict.ContainsKey("BaseScore"))  data.BaseScore = inDict["BaseScore"].GetJsonConverter().toInt ();
		if (inDict.ContainsKey("MinCoinLimit"))  data.MinCoinLimit = inDict["MinCoinLimit"].GetJsonConverter().toInt ();
		if (inDict.ContainsKey("MaxCoinLimit"))  data.MaxCoinLimit = inDict["MaxCoinLimit"].GetJsonConverter().toInt ();
		if (inDict.ContainsKey("Icon"))  data.Icon = inDict["Icon"].GetJsonConverter().toStr();

        return data;
    }
}