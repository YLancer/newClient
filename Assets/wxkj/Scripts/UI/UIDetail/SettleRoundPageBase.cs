using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SettleRoundPageBase : UISceneBase
{
    public SettleRoundPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ShareButton_Image = transform.Find("ShareButton").gameObject.GetComponent<Image>();
        detail.ShareButton_Button = transform.Find("ShareButton").gameObject.GetComponent<Button>();
        detail.ContinueButton_Image = transform.Find("ContinueButton").gameObject.GetComponent<Image>();
        detail.ContinueButton_Button = transform.Find("ContinueButton").gameObject.GetComponent<Button>();
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.Time_Text = transform.Find("Time").gameObject.GetComponent<Text>();
        detail.Grid_GridLayoutGroup = transform.Find("Grid").gameObject.GetComponent<GridLayoutGroup>();
        detail.SingleCard_UIItem = transform.Find("SingleCard").gameObject.GetComponent<UIItem>();
        detail.RoundAccountSub0_RoundAccountSub = transform.Find("RoundAccountSub0").gameObject.GetComponent<RoundAccountSub>();
        detail.RoundAccountSub1_RoundAccountSub = transform.Find("RoundAccountSub1").gameObject.GetComponent<RoundAccountSub>();
        detail.M0_UIItem = transform.Find("ModeGrid/M0").gameObject.GetComponent<UIItem>();
        detail.M1_UIItem = transform.Find("ModeGrid/M1").gameObject.GetComponent<UIItem>();
        detail.M2_UIItem = transform.Find("ModeGrid/M2").gameObject.GetComponent<UIItem>();
        detail.M3_UIItem = transform.Find("ModeGrid/M3").gameObject.GetComponent<UIItem>();
        detail.M4_UIItem = transform.Find("ModeGrid/M4").gameObject.GetComponent<UIItem>();
        detail.M5_UIItem = transform.Find("ModeGrid/M5").gameObject.GetComponent<UIItem>();
        detail.M6_UIItem = transform.Find("ModeGrid/M6").gameObject.GetComponent<UIItem>();
        detail.M7_UIItem = transform.Find("ModeGrid/M7").gameObject.GetComponent<UIItem>();
        detail.ModeGrid_GridLayoutGroup = transform.Find("ModeGrid").gameObject.GetComponent<GridLayoutGroup>();
        detail.Title_TextMeshProUGUI = transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();

    }
}
