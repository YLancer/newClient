using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MailAttachSubBase : UISubBase
{
    public MailAttachSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.GoldIcon_Image = transform.Find("GoldIcon").gameObject.GetComponent<Image>();
        detail.CardIcon_Image = transform.Find("CardIcon").gameObject.GetComponent<Image>();
        detail.GetButton_Image = transform.Find("GetButton").gameObject.GetComponent<Image>();
        detail.GetButton_Button = transform.Find("GetButton").gameObject.GetComponent<Button>();
        detail.Num_Text = transform.Find("Num").gameObject.GetComponent<Text>();
        detail.Num_Outline = transform.Find("Num").gameObject.GetComponent<Outline>();
        detail.Num_Shadow = transform.Find("Num").gameObject.GetComponent<Shadow>();

    }
}
