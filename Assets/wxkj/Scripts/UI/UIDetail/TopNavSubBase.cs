using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TopNavSubBase : UISubBase
{
    public TopNavSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.BackButton_Image = transform.Find("BackButton").gameObject.GetComponent<Image>();
        detail.BackButton_Button = transform.Find("BackButton").gameObject.GetComponent<Button>();
        detail.BackButton_ButtonEffect = transform.Find("BackButton").gameObject.GetComponent<ButtonEffect>();
        detail.UserIcon_Image = transform.Find("UserInfoButton/UserIcon").gameObject.GetComponent<Image>();
        detail.Nickname_Text = transform.Find("UserInfoButton/Nickname").gameObject.GetComponent<Text>();
        detail.PlayerId_Text = transform.Find("UserInfoButton/PlayerId").gameObject.GetComponent<Text>();
        detail.PlayerId_Shadow = transform.Find("UserInfoButton/PlayerId").gameObject.GetComponent<Shadow>();
        detail.UserInfoButton_Image = transform.Find("UserInfoButton").gameObject.GetComponent<Image>();
        detail.UserInfoButton_Button = transform.Find("UserInfoButton").gameObject.GetComponent<Button>();
        detail.CardNum_Text = transform.Find("CardButton/CardNum").gameObject.GetComponent<Text>();
        detail.CardButton_Image = transform.Find("CardButton").gameObject.GetComponent<Image>();
        detail.CardButton_Button = transform.Find("CardButton").gameObject.GetComponent<Button>();
        detail.CoinsNum_Text = transform.Find("CoinsButton/CoinsNum").gameObject.GetComponent<Text>();
        detail.CoinsButton_Image = transform.Find("CoinsButton").gameObject.GetComponent<Image>();
        detail.CoinsButton_Button = transform.Find("CoinsButton").gameObject.GetComponent<Button>();

    }
}
