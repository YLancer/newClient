using UnityEngine;
using System.Collections;

public class Player
{
    //位置
    public int position;
    public string nickName; //昵称
    public string headImg; //头像
    public int playerId; //id
    public int coin; //金币数
    public int score; //积分数
    public bool isReady = false;
    public bool offline = false;
    public bool ting = false;
    public bool leave = false;  // 离开
    public int sex;
}
