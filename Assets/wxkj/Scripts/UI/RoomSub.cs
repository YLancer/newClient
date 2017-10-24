using UnityEngine;
using System.Collections;
using packet.game;

public class RoomSub : RoomSubBase
{
    public void SetValue(RoomConfigModel config)
    {
        detail.BaseScore_Text.text = config.baseScore.ToString();
        detail.PlayerNum_Text.text = "0";
        detail.Icon_Image.sprite = Resources.Load<Sprite>("Icons/" + config.icon);
        if (config.maxCoinLimit > 0)
        {
            detail.CoinsRange_Text.text = Utils.GetShotName(config.minCoinLimit) + "-" + Utils.GetShotName(config.maxCoinLimit);
        }
        else
        {
            detail.CoinsRange_Text.text = Utils.GetShotName(config.minCoinLimit) + "以上";
        }
        
    }
}
