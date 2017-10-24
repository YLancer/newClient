using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SettleRoundDialogBase : UIDialogBase
{
    public SettleRoundDialogDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ShareButton_Image = transform.Find("Root/ShareButton").gameObject.GetComponent<Image>();
        detail.ShareButton_Button = transform.Find("Root/ShareButton").gameObject.GetComponent<Button>();
        detail.ContinueButton_Image = transform.Find("Root/ContinueButton").gameObject.GetComponent<Image>();
        detail.ContinueButton_Button = transform.Find("Root/ContinueButton").gameObject.GetComponent<Button>();
        detail.CloseButton_Image = transform.Find("Root/CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("Root/CloseButton").gameObject.GetComponent<Button>();
        detail.Time_Text = transform.Find("Root/Time").gameObject.GetComponent<Text>();
        detail.Grid_GridLayoutGroup = transform.Find("Root/Grid").gameObject.GetComponent<GridLayoutGroup>();
        detail.SingleCard_UIItem = transform.Find("Root/SingleCard").gameObject.GetComponent<UIItem>();
        detail.RoundAccountSub0_RoundAccountSub = transform.Find("Root/RoundAccountSub0").gameObject.GetComponent<RoundAccountSub>();
        detail.RoundAccountSub1_RoundAccountSub = transform.Find("Root/RoundAccountSub1").gameObject.GetComponent<RoundAccountSub>();
        detail.M0_UIItem = transform.Find("Root/ModeGrid/M0").gameObject.GetComponent<UIItem>();
        detail.M1_UIItem = transform.Find("Root/ModeGrid/M1").gameObject.GetComponent<UIItem>();
        detail.M2_UIItem = transform.Find("Root/ModeGrid/M2").gameObject.GetComponent<UIItem>();
        detail.M3_UIItem = transform.Find("Root/ModeGrid/M3").gameObject.GetComponent<UIItem>();
        detail.M4_UIItem = transform.Find("Root/ModeGrid/M4").gameObject.GetComponent<UIItem>();
        detail.M5_UIItem = transform.Find("Root/ModeGrid/M5").gameObject.GetComponent<UIItem>();
        detail.M6_UIItem = transform.Find("Root/ModeGrid/M6").gameObject.GetComponent<UIItem>();
        detail.M7_UIItem = transform.Find("Root/ModeGrid/M7").gameObject.GetComponent<UIItem>();
        detail.ModeGrid_GridLayoutGroup = transform.Find("Root/ModeGrid").gameObject.GetComponent<GridLayoutGroup>();
        detail.Title_TextMeshProUGUI = transform.Find("Root/Title").gameObject.GetComponent<TextMeshProUGUI>();
        detail.Root_UtilScale = transform.Find("Root").gameObject.GetComponent<UtilScale>();

    }
}
