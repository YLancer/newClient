using UnityEngine;
using System.Collections;
using packet.game;
using System;

public class TotalRecrodSub : TotalRecrodSubBase
{
    internal void SetValue(RoomResultModel rs)
    {
        detail.RoomName_Text.text = rs.roomName;
        detail.Time_Text.text = rs.playerTime;
        //detail.Rank_ImageText.Text = "1";
        foreach(PlayerScoreModel ps in rs.playerScore)
        {
            GameObject child = PrefabUtils.AddChild(detail.Grid_GridLayoutGroup, detail.RoomRecordItemSub_RoomRecordItemSub);
            RoomRecordItemSub sub = child.GetComponent<RoomRecordItemSub>();
            sub.SetValue(ps);
        }
    }
}
