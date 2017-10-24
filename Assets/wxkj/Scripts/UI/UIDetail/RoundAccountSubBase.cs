using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RoundAccountSubBase : UISubBase
{
    public RoundAccountSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ID_Text = transform.Find("ID").gameObject.GetComponent<Text>();
        detail.Name_Text = transform.Find("Name").gameObject.GetComponent<Text>();
        detail.TingInfo_Text = transform.Find("TingInfo").gameObject.GetComponent<Text>();
        detail.Fan_TextMeshProUGUI = transform.Find("Fan").gameObject.GetComponent<TextMeshProUGUI>();
        detail.TableCards_GridLayoutGroup = transform.Find("CardGroup/TableCards").gameObject.GetComponent<GridLayoutGroup>();
        detail.Handcards_GridLayoutGroup = transform.Find("CardGroup/Handcards").gameObject.GetComponent<GridLayoutGroup>();
        detail.CardGroup_HorizontalLayoutGroup = transform.Find("CardGroup").gameObject.GetComponent<HorizontalLayoutGroup>();
        detail.ZhuangFlag_Image = transform.Find("ZhuangFlag").gameObject.GetComponent<Image>();
        detail.ResultType_Text = transform.Find("ResultType").gameObject.GetComponent<Text>();

    }
}
