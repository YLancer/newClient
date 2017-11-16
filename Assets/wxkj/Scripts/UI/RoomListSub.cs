using UnityEngine;
using System.Collections;
using System;
using packet.game;
using packet.msgbase;

public class RoomListSub : RoomListSubBase
{
    public void SetValue(VipRoomModel room)
    {
        detail.RoomName_Text.text = room.name;
        detail.PlayerNum_Text.text = string.Format("{0}/{1}", room.players.Count, room.roomType);

        detail.WxButton_Button.onClick.AddListener(()=> {
            //Game.AndroidUtil.Share(room.code);
            Game.AndroidUtil.OnShareClick(room.code);
        });
    }
}
