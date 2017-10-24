using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankSubBase : UISubBase
{
    public RankSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Nickname_Text = transform.Find("Nickname").gameObject.GetComponent<Text>();
        detail.Nickname_Outline = transform.Find("Nickname").gameObject.GetComponent<Outline>();
        detail.Rank1_Image = transform.Find("Rank1").gameObject.GetComponent<Image>();
        detail.Rank2_Image = transform.Find("Rank2").gameObject.GetComponent<Image>();
        detail.Rank3_Image = transform.Find("Rank3").gameObject.GetComponent<Image>();
        detail.Rank_Text = transform.Find("Rank").gameObject.GetComponent<Text>();
        detail.Rank_Outline = transform.Find("Rank").gameObject.GetComponent<Outline>();
        detail.UserFace_Image = transform.Find("UserFace").gameObject.GetComponent<Image>();
        detail.Info_Text = transform.Find("Info").gameObject.GetComponent<Text>();

    }
}
