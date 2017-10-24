using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomSubBase : UISubBase
{
    public RoomSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Icon_Image = transform.Find("Icon").gameObject.GetComponent<Image>();
        detail.Icon_Button = transform.Find("Icon").gameObject.GetComponent<Button>();
        detail.BaseScore_Text = transform.Find("BaseScore").gameObject.GetComponent<Text>();
        detail.BaseScore_Outline = transform.Find("BaseScore").gameObject.GetComponent<Outline>();
        detail.PlayerNum_Text = transform.Find("PlayerNum").gameObject.GetComponent<Text>();
        detail.CoinsRange_Text = transform.Find("CoinsRange").gameObject.GetComponent<Text>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
