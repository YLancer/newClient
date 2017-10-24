using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingPageBase : UISceneBase
{
    public SettingPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Icon_Image = transform.Find("Icon").gameObject.GetComponent<Image>();
        detail.Name_Text = transform.Find("Name").gameObject.GetComponent<Text>();
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.CheckUpdateButton_Image = transform.Find("CheckUpdateButton").gameObject.GetComponent<Image>();
        detail.CheckUpdateButton_Button = transform.Find("CheckUpdateButton").gameObject.GetComponent<Button>();
        detail.AboutButton_Image = transform.Find("AboutButton").gameObject.GetComponent<Image>();
        detail.AboutButton_Button = transform.Find("AboutButton").gameObject.GetComponent<Button>();
        detail.LogoutButton_Image = transform.Find("LogoutButton").gameObject.GetComponent<Image>();
        detail.LogoutButton_Button = transform.Find("LogoutButton").gameObject.GetComponent<Button>();
        detail.MusicBtn_SwitchSub = transform.Find("MusicBtn").gameObject.GetComponent<SwitchSub>();
        detail.SoundBtn_SwitchSub = transform.Find("SoundBtn").gameObject.GetComponent<SwitchSub>();
        detail.ShakeBtn_SwitchSub = transform.Find("ShakeBtn").gameObject.GetComponent<SwitchSub>();

    }
}
