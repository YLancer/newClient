using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AccountSubBase : UISubBase
{
    public AccountSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Face_Image = transform.Find("Face").gameObject.GetComponent<Image>();
        detail.Name_Text = transform.Find("Name").gameObject.GetComponent<Text>();
        detail.ZhuangTime_Text = transform.Find("ZhuangTime").gameObject.GetComponent<Text>();
        detail.HuTime_Text = transform.Find("HuTime").gameObject.GetComponent<Text>();
        detail.PaoTime_Text = transform.Find("PaoTime").gameObject.GetComponent<Text>();
        detail.BaoTime_Text = transform.Find("BaoTime").gameObject.GetComponent<Text>();
        detail.Bao2Time_Text = transform.Find("Bao2Time").gameObject.GetComponent<Text>();
        detail.ZhaTime_Text = transform.Find("ZhaTime").gameObject.GetComponent<Text>();
        detail.ID_Text = transform.Find("ID").gameObject.GetComponent<Text>();
        detail.WinFlag_Image = transform.Find("WinFlag").gameObject.GetComponent<Image>();
        detail.PaoFlag_Image = transform.Find("PaoFlag").gameObject.GetComponent<Image>();
        detail.RoomOwnerFlag_Image = transform.Find("RoomOwnerFlag").gameObject.GetComponent<Image>();
        detail.Score_TextMeshProUGUI = transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>();

    }
}
