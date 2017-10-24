using  UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ImangiUtilities;

public class ConfigShop : ConfigBase
{
	public int Id{get;set;}
	public string Name{get;set;}
	public int ShopType{get;set;}
	public string Icon{get;set;}
	public int  Num{get;set;}
	public int  Price{get;set;}

    public static string jsonName = "ConfigShop";
    public static bool HasLoad = false;
    private static Dictionary<int, ConfigShop> _map;
    public static Dictionary<int, ConfigShop> map
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
    private static List<ConfigShop> _datas;
    public static List<ConfigShop> datas
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

	public static ConfigShop GetByKey (int key){
        if (!HasLoad)
        {
            Init();
        }

		ConfigShop data;
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
        _map = new Dictionary<int, ConfigShop>(list.Count);
        _datas = new List<ConfigShop>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            Dictionary<string, object> inDict = list[i] as Dictionary<string, object>;
            ConfigShop data = FromJson(inDict);
            _map.Add(data.Id, data);
            _datas.Add(data);
        }
    }

    static ConfigShop FromJson(Dictionary<string, object> inDict)
    {
        ConfigShop data = new ConfigShop();
		if (inDict.ContainsKey("Id"))  data.Id = inDict["Id"].GetJsonConverter().toInt();
		if (inDict.ContainsKey("Name"))  data.Name = inDict["Name"].GetJsonConverter().toStr();
		if (inDict.ContainsKey("ShopType"))  data.ShopType = inDict["ShopType"].GetJsonConverter().toInt();
		if (inDict.ContainsKey("Icon"))  data.Icon = inDict["Icon"].GetJsonConverter().toStr();
		if (inDict.ContainsKey("Num"))  data.Num = inDict["Num"].GetJsonConverter().toInt ();
		if (inDict.ContainsKey("Price"))  data.Price = inDict["Price"].GetJsonConverter().toInt ();

        return data;
    }
}