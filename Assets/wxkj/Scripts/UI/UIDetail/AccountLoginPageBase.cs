using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccountLoginPageBase : UISceneBase
{
    public AccountLoginPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.SavePassword_CheckBoxSub = transform.Find("SavePassword").gameObject.GetComponent<CheckBoxSub>();
        detail.Agree_CheckBoxSub = transform.Find("Agree").gameObject.GetComponent<CheckBoxSub>();
        detail.Username_Image = transform.Find("Username").gameObject.GetComponent<Image>();
        detail.Username_InputField = transform.Find("Username").gameObject.GetComponent<InputField>();
        detail.Password_Image = transform.Find("Password").gameObject.GetComponent<Image>();
        detail.Password_InputField = transform.Find("Password").gameObject.GetComponent<InputField>();
        detail.LoginButton_Image = transform.Find("LoginButton").gameObject.GetComponent<Image>();
        detail.LoginButton_Button = transform.Find("LoginButton").gameObject.GetComponent<Button>();
        detail.RegistButton_Image = transform.Find("RegistButton").gameObject.GetComponent<Image>();
        detail.RegistButton_Button = transform.Find("RegistButton").gameObject.GetComponent<Button>();
        detail.VisitorButton_Image = transform.Find("VisitorButton").gameObject.GetComponent<Image>();
        detail.VisitorButton_Button = transform.Find("VisitorButton").gameObject.GetComponent<Button>();
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();

    }
}
