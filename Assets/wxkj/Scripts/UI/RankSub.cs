using UnityEngine;
using System.Collections;
using System;
using packet.rank;

public class RankSub : RankSubBase
{
    public void SetupUI(RankItem item,bool isRoundRank)
    {
        detail.Rank1_Image.gameObject.SetActive(false);
        detail.Rank2_Image.gameObject.SetActive(false);
        detail.Rank3_Image.gameObject.SetActive(false);
        detail.Rank_Text.text = "";
        detail.Nickname_Text.text = item.playerName;

        Game.IconMgr.SetFace(detail.UserFace_Image, item.playerHeadImg);
        
        if (isRoundRank)
        {
            detail.Info_Text.text = string.Format("本周开局{0}", item.point);
        }
        else
        {
            detail.Info_Text.text = Utils.GetShotName(item.point);
        }

        if (item.rank == 1)
        {
            detail.Rank1_Image.gameObject.SetActive(true);
        }
        else if (item.rank == 2)
        {
            detail.Rank2_Image.gameObject.SetActive(true);
        }
        else if (item.rank == 3)
        {
            detail.Rank3_Image.gameObject.SetActive(true);
        }
        else
        {
            detail.Rank_Text.text = item.rank.ToString();
        }
    }
}
