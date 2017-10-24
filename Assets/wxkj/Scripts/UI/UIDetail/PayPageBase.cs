using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PayPageBase : UISceneBase
{
    public PayPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.WxButton_Image = transform.Find("WxButton").gameObject.GetComponent<Image>();
        detail.WxButton_Button = transform.Find("WxButton").gameObject.GetComponent<Button>();
        detail.ZfbButton_Image = transform.Find("ZfbButton").gameObject.GetComponent<Image>();
        detail.ZfbButton_Button = transform.Find("ZfbButton").gameObject.GetComponent<Button>();
        detail.Text_Text = transform.Find("Text").gameObject.GetComponent<Text>();

    }
}
