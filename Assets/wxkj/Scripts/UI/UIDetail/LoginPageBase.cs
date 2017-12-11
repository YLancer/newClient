using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using cn.sharesdk.unity3d; //wx 2017.7.22
public class LoginPageBase : UISceneBase
{
    public LoginPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.WxButton_Image = transform.Find("WxButton").gameObject.GetComponent<Image>();
        detail.WxButton_Button = transform.Find("WxButton").gameObject.GetComponent<Button>();
        detail.AccountButton_Image = transform.Find("AccountButton").gameObject.GetComponent<Image>();
        detail.AccountButton_Button = transform.Find("AccountButton").gameObject.GetComponent<Button>();

        detail.WXToggle = transform.Find("Toggle").gameObject.GetComponent<Toggle>();
        detail.Background= transform.Find("Toggle/Background").gameObject.GetComponent<Image>();
        detail.Checkmark= transform.Find("Toggle/Background/Checkmark").gameObject.GetComponent<Image>();

        detail.Deal= transform.Find("Deal").gameObject.GetComponent<Button>();
        detail.text_image = transform.Find("Deal/text_image").gameObject.GetComponent<Image>();
        detail.text_uesr = transform.Find("Deal/text_uesr").gameObject.GetComponent<Text>();
        detail.Deal_image = transform.Find("Deal_image").gameObject.GetComponent<Image>();
        detail.BG= transform.Find("Deal_image/BG").gameObject.GetComponent<Image>();
        detail.close =transform.Find("Deal_image/close").gameObject.GetComponent<Button>();
        detail.ScrollView= transform.Find("Deal_image/Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.Viewport= transform.Find("Deal_image/Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.Context= transform.Find("Deal_image/Scroll View/Viewport/Content").gameObject.GetComponent<Text>();
    }
}
