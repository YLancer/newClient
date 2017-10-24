using UnityEngine;
using System.Collections;
using packet.game;
using System;

public class RoomRecordItemSub : RoomRecordItemSubBase
{
    internal void SetValue(PlayerScoreModel ps)
    {
        detail.Name_Text.text = ps.playerName;
        detail.Score_Text.text = ps.score.ToString();
    }
}
