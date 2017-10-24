using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegistPageBase : UISceneBase
{
    public RegistPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.RegistButton_Image = transform.Find("RegistButton").gameObject.GetComponent<Image>();
        detail.RegistButton_Button = transform.Find("RegistButton").gameObject.GetComponent<Button>();
        detail.Nickname_Image = transform.Find("Nickname").gameObject.GetComponent<Image>();
        detail.Nickname_InputField = transform.Find("Nickname").gameObject.GetComponent<InputField>();
        detail.Password_Image = transform.Find("Password").gameObject.GetComponent<Image>();
        detail.Password_InputField = transform.Find("Password").gameObject.GetComponent<InputField>();
        detail.Username_Image = transform.Find("Username").gameObject.GetComponent<Image>();
        detail.Username_InputField = transform.Find("Username").gameObject.GetComponent<InputField>();
        detail.Password2_Image = transform.Find("Password2").gameObject.GetComponent<Image>();
        detail.Password2_InputField = transform.Find("Password2").gameObject.GetComponent<InputField>();

    }
}
