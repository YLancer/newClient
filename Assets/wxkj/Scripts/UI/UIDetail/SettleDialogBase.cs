using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SettleDialogBase : UIDialogBase
{
    public SettleDialogDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ShareButton_Image = transform.Find("Root/ShareButton").gameObject.GetComponent<Image>();
        detail.ShareButton_Button = transform.Find("Root/ShareButton").gameObject.GetComponent<Button>();
        detail.BackButton_Image = transform.Find("Root/BackButton").gameObject.GetComponent<Image>();
        detail.BackButton_Button = transform.Find("Root/BackButton").gameObject.GetComponent<Button>();
        detail.Grid_GridLayoutGroup = transform.Find("Root/Grid").gameObject.GetComponent<GridLayoutGroup>();
        detail.AccountSub_AccountSub = transform.Find("Root/AccountSub").gameObject.GetComponent<AccountSub>();
        detail.Time_Text = transform.Find("Root/Time").gameObject.GetComponent<Text>();
        detail.Root_UtilScale = transform.Find("Root").gameObject.GetComponent<UtilScale>();

    }
}
