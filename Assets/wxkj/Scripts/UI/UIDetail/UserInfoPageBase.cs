using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UserInfoPageBase : UISceneBase
{
    public UserInfoPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("Root/CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("Root/CloseButton").gameObject.GetComponent<Button>();
        detail.ChangeFaceButton_Image = transform.Find("Root/ChangeFaceButton").gameObject.GetComponent<Image>();
        detail.ChangeFaceButton_Button = transform.Find("Root/ChangeFaceButton").gameObject.GetComponent<Button>();
        detail.ChangeNameButton_Image = transform.Find("Root/ChangeNameButton").gameObject.GetComponent<Image>();
        detail.ChangeNameButton_Button = transform.Find("Root/ChangeNameButton").gameObject.GetComponent<Button>();
        detail.Face_Image = transform.Find("Root/Face").gameObject.GetComponent<Image>();
        detail.WinRate_Text = transform.Find("Root/WinRate").gameObject.GetComponent<Text>();
        detail.Name_Text = transform.Find("Root/Name").gameObject.GetComponent<Text>();
        detail.HighWin_Text = transform.Find("Root/HighWin").gameObject.GetComponent<Text>();
        detail.IP_Text = transform.Find("Root/IP").gameObject.GetComponent<Text>();
        detail.CardNum_Text = transform.Find("Root/CardNum").gameObject.GetComponent<Text>();
        detail.CoinNum_Text = transform.Find("Root/CoinNum").gameObject.GetComponent<Text>();
        detail.TotalRound_Text = transform.Find("Root/TotalRound").gameObject.GetComponent<Text>();
        detail.CardGroup_GridLayoutGroup = transform.Find("Root/CardGroup").gameObject.GetComponent<GridLayoutGroup>();
        detail.CardTingGroup_GridLayoutGroup = transform.Find("Root/CardTingGroup").gameObject.GetComponent<GridLayoutGroup>();
        detail.MaxFan_Text = transform.Find("Root/MaxFan").gameObject.GetComponent<Text>();
        detail.Placeholder_Text = transform.Find("Root/InputField/Placeholder").gameObject.GetComponent<Text>();
        detail.Text_Text = transform.Find("Root/InputField/Text").gameObject.GetComponent<Text>();
        detail.InputField_Image = transform.Find("Root/InputField").gameObject.GetComponent<Image>();
        detail.InputField_InputField = transform.Find("Root/InputField").gameObject.GetComponent<InputField>();
        detail.PlayerId_Text = transform.Find("Root/PlayerId").gameObject.GetComponent<Text>();
        detail.Root_UtilScale = transform.Find("Root").gameObject.GetComponent<UtilScale>();

    }
}
