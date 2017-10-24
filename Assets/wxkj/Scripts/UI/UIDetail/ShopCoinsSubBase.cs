using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ShopCoinsSubBase : UISubBase
{
    public ShopCoinsSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Image_Image = transform.Find("Image").gameObject.GetComponent<Image>();
        detail.Price_TextMeshProUGUI = transform.Find("Price").gameObject.GetComponent<TextMeshProUGUI>();
        detail.CoinNum_Text = transform.Find("CoinNum").gameObject.GetComponent<Text>();
        detail.CoinNum_TextGradientColor = transform.Find("CoinNum").gameObject.GetComponent<TextGradientColor>();
        detail.CoinNum_Outline = transform.Find("CoinNum").gameObject.GetComponent<Outline>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
