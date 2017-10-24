using UnityEngine;
using System.Collections;
using packet.game;

public class ShopPage : ShopPageBase
{
    int type = 0;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.CardsButton_Button.onClick.AddListener(OnClickCardTab);
        detail.CoinsButton_Button.onClick.AddListener(OnClickCoinTab);
    }

    void Update()
    {
        detail.CardNum_Text.text = Game.Instance.cards.ToString();
        detail.CoinNum_Text.text = Utils.GetShotName(Game.Instance.coins, 1);
    }

    private void OnClickCoinTab()
    {
        Game.SoundManager.PlayClick();
        type = 0;
        OnClickTab();
    }

    private void OnClickCardTab()
    {
        Game.SoundManager.PlayClick();
        type = 1;
        OnClickTab();
    }

    private void OnClickTab()
    {
        bool isCoinTab = type == 0;
        detail.CardsButton_Button.gameObject.SetActive(isCoinTab);
        detail.CoinsButton_Button.gameObject.SetActive(!isCoinTab);
        detail.CardBtnSelectFlag_Image.gameObject.SetActive(!isCoinTab);
        detail.CoinBtnSelectFlag_Image.gameObject.SetActive(isCoinTab);
        detail.CardsGrid_GridLayoutGroup.gameObject.SetActive(!isCoinTab);
        detail.CoinsGrid_GridLayoutGroup.gameObject.SetActive(isCoinTab);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        if(null != sceneData && sceneData.Length > 0)
        {
            type = (int)sceneData[0];
        }

        SetupUI();

        OnClickTab();
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);
        Game.SoundManager.PlayShopBackground();
    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        Game.SoundManager.PlayMenuBackground();
    }

    private void SetupUI()
    {
        MallProductResponse resp = Game.Instance.MallProduct;
        if(null != resp)
        {
            PrefabUtils.ClearChild(detail.CardsGrid_GridLayoutGroup);
            PrefabUtils.ClearChild(detail.CoinsGrid_GridLayoutGroup);
            foreach (MallProductModel model in resp.products)
            {
                if (model.category == 1)
                {
                    GameObject child = PrefabUtils.AddChild(detail.CardsGrid_GridLayoutGroup, detail.ShopCardSub_ShopCardSub);
                    ShopCardSub sub = child.GetComponent<ShopCardSub>();
                    sub.SetValue(model);
                }
                else
                {
                    GameObject child = PrefabUtils.AddChild(detail.CoinsGrid_GridLayoutGroup, detail.ShopCoinsSub_ShopCoinsSub);
                    ShopCoinsSub sub = child.GetComponent<ShopCoinsSub>();
                    sub.SetValue(model);
                }
            }
        }
        //foreach (ConfigShop config in ConfigShop.datas)
        //{
        //    if(config.ShopType == 1)
        //    {
        //        GameObject child = PrefabUtils.AddChild(detail.CardsGrid_GridLayoutGroup, detail.ShopCardSub_ShopCardSub);
        //        ShopCardSub sub = child.GetComponent<ShopCardSub>();
        //        sub.SetValue(config);
        //    }else
        //    {
        //        GameObject child = PrefabUtils.AddChild(detail.CoinsGrid_GridLayoutGroup, detail.ShopCoinsSub_ShopCoinsSub);
        //        ShopCoinsSub sub = child.GetComponent<ShopCoinsSub>();
        //        sub.SetValue(config);
        //    }
        //}
    }
}
