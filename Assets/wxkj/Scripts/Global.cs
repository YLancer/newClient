using UnityEngine;
using System.Collections;

public class Global {

}

public enum ErrCode
{
    Null = 1000,
    NotEnoughCoins = 2001,
}

public enum Layer
{
    UI = 5,
    Player = 9,
}

public enum MyPrefabType
{
	Effect,     //特效
	Card,     //麻将
	MJ,     	//UI上用的麻将
}

public enum MoneyType
{
    Diamond = 1,    // 钻石
    Gold = 2,       // 金币
    RMB = 3,        // RMB
}

// Loading页面类型
public enum LoadPageType
{
    OnlyMask = 1,       // 透明蒙版
    OnlyBlackMask = 2,  // 半透蒙版
    LoopCircle = 3,     // 工作中
    ProgressBar = 4,    // 制作完成
}

public enum RoomType
{
    Normal,VIP,Single
}

public enum MatchType
{
    G_DQMJ_MATCH_NOVICE,
    G_DQMJ_MATCH_PRIMARY,
    G_DQMJ_MATCH_ORDINARY,
    G_DQMJ_MATCH_INTERMEDIATE,
    G_DQMJ_MATCH_ADVANCED,
    G_DQMJ_MATCH_TOP,
    G_DQMJ_MATCH_SINGLE,
    G_DQMJ_MATCH_2VIP,
    G_DQMJ_MATCH_4VIP,
}