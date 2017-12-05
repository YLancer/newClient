using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InningRankSubBase : UISubBase
{
    public InningRankSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.rank = transform.Find("InningRankSub/rank").gameObject.GetComponent<Text>();
        detail.score = transform.Find("InningRankSub/score").gameObject.GetComponent<Text>();
    }
}
