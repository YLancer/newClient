using UnityEngine;
using System.Collections;
using System;
using packet.rank;
using packet.mj;

public class InningRankSub : InningRankSubBase
{
    public void SetValue(int score,int InningRank)
    {
        detail.rank.text = InningRank.ToString();
        detail.score.text = score.ToString();

        if(score >=0)
        {
            detail.score.color = Color.green;
        }
        else
        {
            detail.score.color = Color.red;
        }
    }
}
