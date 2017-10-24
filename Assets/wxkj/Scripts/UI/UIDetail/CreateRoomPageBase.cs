using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateRoomPageBase : UISceneBase
{
    public CreateRoomPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.CreateButton_Image = transform.Find("CreateButton").gameObject.GetComponent<Image>();
        detail.CreateButton_Button = transform.Find("CreateButton").gameObject.GetComponent<Button>();
        detail.PlayerNum2_CheckBoxSub = transform.Find("PlayerNum2").gameObject.GetComponent<CheckBoxSub>();
        detail.PlayerNum4_CheckBoxSub = transform.Find("PlayerNum4").gameObject.GetComponent<CheckBoxSub>();
        detail.Round4_CheckBoxSub = transform.Find("Round4").gameObject.GetComponent<CheckBoxSub>();
        detail.Round8_CheckBoxSub = transform.Find("Round8").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode0_CheckBoxSub = transform.Find("ModeGrid/Mode0").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode1_CheckBoxSub = transform.Find("ModeGrid/Mode1").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode2_CheckBoxSub = transform.Find("ModeGrid/Mode2").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode3_CheckBoxSub = transform.Find("ModeGrid/Mode3").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode4_CheckBoxSub = transform.Find("ModeGrid/Mode4").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode5_CheckBoxSub = transform.Find("ModeGrid/Mode5").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode6_CheckBoxSub = transform.Find("ModeGrid/Mode6").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode7_CheckBoxSub = transform.Find("ModeGrid/Mode7").gameObject.GetComponent<CheckBoxSub>();
        detail.ModeGrid_GridLayoutGroup = transform.Find("ModeGrid").gameObject.GetComponent<GridLayoutGroup>();

    }
}
