using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomListSubBase : UISubBase
{
    public RoomListSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.PlayerButton_Image = transform.Find("PlayerButton").gameObject.GetComponent<Image>();
        detail.PlayerButton_Button = transform.Find("PlayerButton").gameObject.GetComponent<Button>();
        detail.WxButton_Image = transform.Find("WxButton").gameObject.GetComponent<Image>();
        detail.WxButton_Button = transform.Find("WxButton").gameObject.GetComponent<Button>();
        detail.DismissButton_Image = transform.Find("DismissButton").gameObject.GetComponent<Image>();
        detail.DismissButton_Button = transform.Find("DismissButton").gameObject.GetComponent<Button>();
        detail.RoomName_Text = transform.Find("RoomName").gameObject.GetComponent<Text>();
        detail.PlayerNum_Text = transform.Find("PlayerNum").gameObject.GetComponent<Text>();

    }
}
