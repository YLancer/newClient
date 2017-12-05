using UnityEngine;
using System.Collections;
using System;
using packet.mj;
using System.Collections.Generic;

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

        List<int> list = new List<int>();
        list.Add(4);
        list.Add(-4);
        list.Add(106);
        list.Add(402);
        list.Add(174);
        list.Add(478);
        list.Add(424);
        list.Add(-64);
        list.Add(1564);
        PrefabUtils.ClearChild(detail.Content_GridLayoutGroup);
        for(int i = 0; i < list.Count; i++)
        {
            GameObject child = AddChild(detail.Content_GridLayoutGroup,detail.InningRankSub);
            InningRankSub Inningsub = child.GetComponent<InningRankSub>();
            Inningsub.SetValue(list[i], i);
        }
    }
}
