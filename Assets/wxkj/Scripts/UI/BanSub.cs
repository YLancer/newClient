using UnityEngine;
using System.Collections;
using System;
using packet.game;
using packet.msgbase;

public class BanSub : BanSubBase
{
    internal void SetValue(PlayerModel player)
    {
        int index = player.position + 1;
        detail.Text_Text.text = index + "."+player.nickName;
    }
}
