using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopPageBase : UISceneBase
{
    public ShopPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.CoinsImage_Image = transform.Find("CoinsTab/CoinsImage").gameObject.GetComponent<Image>();
        detail.CoinBtnSelectFlag_Image = transform.Find("CoinsTab/CoinBtnSelectFlag").gameObject.GetComponent<Image>();
        detail.CoinsButton_Image = transform.Find("CoinsTab/CoinsButton").gameObject.GetComponent<Image>();
        detail.CoinsButton_Button = transform.Find("CoinsTab/CoinsButton").gameObject.GetComponent<Button>();
        detail.CardsImage_Image = transform.Find("CardsTab/CardsImage").gameObject.GetComponent<Image>();
        detail.CardBtnSelectFlag_Image = transform.Find("CardsTab/CardBtnSelectFlag").gameObject.GetComponent<Image>();
        detail.CardsButton_Image = transform.Find("CardsTab/CardsButton").gameObject.GetComponent<Image>();
        detail.CardsButton_Button = transform.Find("CardsTab/CardsButton").gameObject.GetComponent<Button>();
        detail.CardNum_Text = transform.Find("CardNum").gameObject.GetComponent<Text>();
        detail.CoinNum_Text = transform.Find("CoinNum").gameObject.GetComponent<Text>();
        detail.CoinsGrid_GridLayoutGroup = transform.Find("CoinsGrid").gameObject.GetComponent<GridLayoutGroup>();
        detail.ShopCoinsSub_ShopCoinsSub = transform.Find("ShopCoinsSub").gameObject.GetComponent<ShopCoinsSub>();
        detail.ShopCardSub_ShopCardSub = transform.Find("ShopCardSub").gameObject.GetComponent<ShopCardSub>();
        detail.CardsGrid_GridLayoutGroup = transform.Find("CardsGrid").gameObject.GetComponent<GridLayoutGroup>();

    }
}
