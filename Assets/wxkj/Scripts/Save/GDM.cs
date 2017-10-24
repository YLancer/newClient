using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

//管理数据储存的类 GameDataManager
public class GDM
{
	//加密和解密采用相同的key,具体自己填，但是必须为32位//
	public const string SKEY = "57890egf6234gw76890s56787dsfw87f";

#if UNITY_IOS || UNITY_IPHONE || UNITY_EDITOR
	public static bool encrypt = false;	// 是否开启加密
#else
	public static bool encrypt = true;	// 是否开启加密
#endif

	// 公用默认存档
	public static Dictionary<SAVE_DATA_TYPE , SaveAble> m_DicSaveDatas;
	public static Dictionary<Type , SaveAble> m_DicTypeSaveDatas;

	//密钥,用于防止拷贝存档//
	//设定密钥，根据具体平台设定//
    private static string _UKey;
	public static string UKey
	{
		get
		{
            return _UKey;
		}
	}

    public static void LoadAll(System.Action<bool> onFinish = null)
    {
#if UNITY_EDITOR
        _UKey = "ABC";
#else
			 _UKey = SystemInfo.deviceUniqueIdentifier;
#endif

        int index = 0;
        int iCount = Enum.GetValues(typeof(SAVE_DATA_TYPE)).Length;

        m_DicTypeSaveDatas = new Dictionary<Type, SaveAble>();
        m_DicSaveDatas = new Dictionary<SAVE_DATA_TYPE, SaveAble>();

        foreach (SAVE_DATA_TYPE item in Enum.GetValues(typeof(SAVE_DATA_TYPE)))
        {
            try
            {
                Type kTargetType = Type.GetType(item.ToString());
                if (kTargetType != null)
                {
                    SaveAble saveData = SaveAble.Load(kTargetType);
                    if (saveData == null)
                    {
                        saveData = CreateInstance<SaveAble>(kTargetType);
                    }
                    m_DicSaveDatas.Add(item, saveData);
                    m_DicTypeSaveDatas.Add(kTargetType, saveData);
                    saveData.OnLoadFinish();
                }
                else
                {
                    Debug.LogError("cannot find save data class for enum: " + item.ToString());
                }
                index++;
                //m_dProgress = index * 1f / iCount;
            }
            catch
            {
                Debug.LogError("Error loading data : " + item.ToString());
            }
        }

        if (null != onFinish)
        {
            onFinish(true);
        }
    }

	public static void SaveAll ()
	{
		if (m_DicSaveDatas != null)
		{
			foreach (KeyValuePair<SAVE_DATA_TYPE , SaveAble> pair in m_DicSaveDatas)
			{
				if (pair.Value != null)
				{
					pair.Value.Save ();
				}
			}
		}
	}

	public static void Save(SAVE_DATA_TYPE type)
	{
		SaveAble kSaveAble = null;
		if(m_DicSaveDatas != null && m_DicSaveDatas.TryGetValue(type , out kSaveAble))
		{
			kSaveAble.Save ();
		}
	}

	public static T getSaveAbleData<T>() where T : SaveAble
	{
		SaveAble kRet = null;
		if (m_DicTypeSaveDatas != null && m_DicTypeSaveDatas.TryGetValue (typeof(T), out kRet))
		{
			return kRet as T;
		}
		return null;
		/*Type kType = typeof(T);
		string kTypeName = kType.FullName;
		SAVE_DATA_TYPE kEnumType = (SAVE_DATA_TYPE)Enum.Parse(typeof(SAVE_DATA_TYPE), kTypeName);
		T kRet = null;
		if (m_DicSaveDatas != null && m_DicSaveDatas.TryGetValue (kEnumType, out kRet))
		{
			return kRet;
		}
		return null;*/
	}
	
	void OnApplicationPause (bool paused)
	{
	}

    private static Assembly _executingAssembly;

    public static T CreateInstance<T>(Type type = null)
    {
        if (null == _executingAssembly)
        {
            _executingAssembly = Assembly.GetExecutingAssembly();
        }

        type = type ?? typeof(T);
        return (T)_executingAssembly.CreateInstance(type.FullName);
    }
}

public enum SAVE_DATA_TYPE
{
	GameData
}