using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveBtnSubBase : UISubBase
{
    public ActiveBtnSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Normal_Image = transform.Find("Normal").gameObject.GetComponent<Image>();
        detail.SelectFlag_Image = transform.Find("SelectFlag").gameObject.GetComponent<Image>();
        detail.Title_Text = transform.Find("Title").gameObject.GetComponent<Text>();
        detail.Title_Outline = transform.Find("Title").gameObject.GetComponent<Outline>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
