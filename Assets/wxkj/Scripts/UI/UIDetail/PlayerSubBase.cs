using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerSubBase : UISubBase
{
    public PlayerSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Icon_Image = transform.Find("Icon").gameObject.GetComponent<Image>();
        detail.OfflineFlag_Image = transform.Find("OfflineFlag").gameObject.GetComponent<Image>();
        detail.Ting_Image = transform.Find("Ting").gameObject.GetComponent<Image>();
        detail.Coins_Text = transform.Find("Coins").gameObject.GetComponent<Text>();
        detail.Ready_Text = transform.Find("Ready").gameObject.GetComponent<Text>();
        detail.Ready_Shadow = transform.Find("Ready").gameObject.GetComponent<Shadow>();
        detail.Zhuang_Image = transform.Find("Zhuang").gameObject.GetComponent<Image>();
        detail.Name_Text = transform.Find("Name").gameObject.GetComponent<Text>();
        detail.TalkingFlag_Image = transform.Find("TalkingFlag").gameObject.GetComponent<Image>();
        detail.MoodRoot_UIItem = transform.Find("MoodRoot").gameObject.GetComponent<UIItem>();
        detail.MoodRoot_GridLayoutGroup = transform.Find("MoodRoot").gameObject.GetComponent<GridLayoutGroup>();
        detail.Word_Text = transform.Find("WordRoot/Word").gameObject.GetComponent<Text>();
        detail.Word_ContentSizeFitter = transform.Find("WordRoot/Word").gameObject.GetComponent<ContentSizeFitter>();
        detail.WordRoot_Text = transform.Find("WordRoot").gameObject.GetComponent<Text>();
        detail.WordRoot_ContentSizeFitter = transform.Find("WordRoot").gameObject.GetComponent<ContentSizeFitter>();
        detail.AwayFlag_Image = transform.Find("AwayFlag").gameObject.GetComponent<Image>();

    }
}
