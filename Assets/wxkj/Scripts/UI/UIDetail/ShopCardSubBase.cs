using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ShopCardSubBase : UISubBase
{
    public ShopCardSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Icon_Image = transform.Find("Icon").gameObject.GetComponent<Image>();
        detail.Price_TextMeshProUGUI = transform.Find("Price").gameObject.GetComponent<TextMeshProUGUI>();
        detail.CardNum_TextMeshProUGUI = transform.Find("CardNum").gameObject.GetComponent<TextMeshProUGUI>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
