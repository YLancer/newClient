using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettlePageBase : UISceneBase
{
    public SettlePageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ShareButton_Image = transform.Find("ShareButton").gameObject.GetComponent<Image>();
        detail.ShareButton_Button = transform.Find("ShareButton").gameObject.GetComponent<Button>();
        detail.BackButton_Image = transform.Find("BackButton").gameObject.GetComponent<Image>();
        detail.BackButton_Button = transform.Find("BackButton").gameObject.GetComponent<Button>();
        detail.Grid_GridLayoutGroup = transform.Find("Grid").gameObject.GetComponent<GridLayoutGroup>();
        detail.AccountSub_AccountSub = transform.Find("AccountSub").gameObject.GetComponent<AccountSub>();
        detail.Time_Text = transform.Find("Time").gameObject.GetComponent<Text>();

    }
}
