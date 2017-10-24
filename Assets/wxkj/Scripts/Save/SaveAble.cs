using UnityEngine;
using System.Collections;

public class SaveAble
{
	public string key;
	private static XmlSaver xs = new XmlSaver ();
	
	//读档时调用的函数//
	public static SaveAble Load (System.Type type)
	{
		string gameDataFile = Utils.GetDataPath () + "/" + type.Name + ".xml";
		if (xs.hasFile (gameDataFile))
		{
			try
			{
				string dataString = xs.LoadXML (gameDataFile);

				object gameDataFromXML = xs.DeserializeObject (dataString, type);
				SaveAble sa = (SaveAble)gameDataFromXML;
				if (null != sa && sa.key == GDM.UKey)
				{
					//是合法存档//
					SaveAble t = (SaveAble)sa;
                    Debug.Log("读档成功......");
					return t;
				}
				else
				{
					//是非法拷贝存档//
					string fileKey = "";
					if (null != sa)
						fileKey = sa.key;
                    Debug.Log("不是本设备的存档！存档清除：key=" + GDM.UKey + "; FileKey=" + fileKey + "; File:" + type.Name);
					//留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
				}
			} catch (System.Exception e)
			{
                Debug.Log("存档Load出错！存档清除：key=" + GDM.UKey + "; File:" + type.Name);
                Debug.Log("e.Message: " + e.Message);
			}
		}

		return null;
	}
	
	public void Save ()
	{
		System.Type type = this.GetType ();
		this.key = GDM.UKey;
		try
		{
			string gameDataFile = Utils.GetDataPath () + "/" + type.Name + ".xml";
			string dataString = xs.SerializeObject (this, type);
			xs.CreateXML (gameDataFile, dataString);
            Debug.Log("Save " + type.Name + " success......");
		} catch (System.Exception e)
		{
            Debug.Log("存档Save出错！存档清除：key=" + GDM.UKey + "; File:" + type.Name);
            Debug.Log("e.Message: " + e.Message);
		}
	}

	public virtual void OnLoadFinish()
	{
		
	}
}