using UnityEngine;
using System.Xml.Serialization;
using System.Collections.Generic;

//GameData,储存数据的类，把需要储存的数据定义在GameData之内就行//
/*
 * 每次存档调用GameDataManager的Save函数，读档调用GameDataManager的Load函数。每次启动后GameDataManager会自动调用Load读档。
 * 如果玩家拿外来存档来覆盖本地存档，则游戏启动后数据清零，任何一次存档后作弊档被自动覆盖。注意：请勿放入二维以上数组，一般一维数据，枚举，自定义类 等等数据类型可放心添加。
 */
[System.Serializable]
public class GameData:SaveAble
{
    //下面是添加需要储存的内容//
    public string username = "";
    public string password = "";
    public bool savePswd = false;
    public bool agreement = false;
    public string usernameVisitor = "";
    public string passwordVisitor = "123456";

    public bool Music = true;
    public bool Shake = false;
    public bool Sound = true;

    // 金币数量
    [XmlIgnore]
	#if UNITY_EDITOR
    public VarInt goldNum = new VarInt(1500);
	#else
	public VarInt goldNum = new VarInt (1500);
#endif
    public int GoldNum
	{
		get{ return goldNum.Value;}
		set{ 
            goldNum.Value = value;
        }
	}

	// 钻石数量
	[XmlIgnore]
	#if UNITY_EDITOR
	public VarInt diamondNum = new VarInt (0);
	#else
	public VarInt diamondNum = new VarInt (0);
#endif
    public int DiamondNum
	{
		get{ return diamondNum.Value;}
		set{ 
            diamondNum.Value = value;
        }
	}
}