using UnityEngine;
using System.Collections;
using System;
using packet.mj;

public class AccountSub : AccountSubBase
{
    public void SetValue(PlayerFinalResult result, int maxHu, int maxPao)
    {
        detail.ID_Text.text = "ID:" + result.playerId;
        detail.Name_Text.text = result.playerName;
        //detail.Face_Image.sprite = Game.IconMgr.GetFace(result.headImage);
        Game.IconMgr.SetFace(detail.Face_Image, result.headImage);

        detail.RoomOwnerFlag_Image.gameObject.SetActive(result.roomOwner);
        detail.ZhuangTime_Text.text = result.bankerCount.ToString();
        detail.HuTime_Text.text = result.huCount.ToString();
        detail.PaoTime_Text.text = result.paoCount.ToString();
        detail.BaoTime_Text.text = result.moBaoCount.ToString();
        detail.Bao2Time_Text.text = result.baoZhongBaoCount.ToString();
        detail.ZhaTime_Text.text = result.kaiPaiZhaCount.ToString();
        detail.Score_TextMeshProUGUI.text = result.score.ToString();

        detail.WinFlag_Image.gameObject.SetActive(maxHu > 0 && result.huCount >= maxHu);
        detail.PaoFlag_Image.gameObject.SetActive(maxPao > 0 && result.paoCount >= maxPao);
    }
}
