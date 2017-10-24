using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MailSubBase : UISubBase
{
    public MailSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.SelectFlag_Image = transform.Find("SelectFlag").gameObject.GetComponent<Image>();
        detail.Title_Text = transform.Find("Title").gameObject.GetComponent<Text>();
        detail.OpenFlag_Image = transform.Find("OpenFlag").gameObject.GetComponent<Image>();
        detail.Time_Text = transform.Find("Time").gameObject.GetComponent<Text>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
