using UnityEngine;
using System.Collections;
using System;
using packet.game;
using TMPro;

public class ShopCoinsSub : ShopCoinsSubBase
{
    MallProductModel config;
    GenOrderResponse response;
    int platformId;
    public void SetValue(MallProductModel config)
    {
        this.config = config;
        detail.CoinNum_Text.text = Utils.GetShotName(config.itemCount,1)+ "<size=26>金币</size>";
        detail.Price_TextMeshProUGUI.text = "￥" + config.price.ToString();
        detail.Image_Image.sprite = Game.IconMgr.Get(config.image);
        detail.Image_Image.SetNativeSize();
        detail.Button_Button.onClick.AddListener(() =>
        {
            Game.SoundManager.PlayClick();
            System.Action<int> callback = OnSelectPayType;
            Game.UIMgr.PushScene(UIPage.PayPage, callback, config);
        });
    }

    void OnSelectPayType(int platformId)
    {
        this.platformId = platformId;
        print("platformId:" + platformId);
        Game.SocketHall.DoGenOrderRequest(platformId, config.id, (response) =>
        {
            this.response = response;
            Game.AndroidUtil.m_kActPurchaseSuccess += OnPaySuccess;
            Game.AndroidUtil.m_kActPurchaseFailures += OnPayFailure;
            string info = System.Text.Encoding.Default.GetString(response.data);
            print("DoGenOrderRequest info:" + info);
            if (this.platformId == 1)
            {
                Game.AndroidUtil.AndroidJavaObject.Call("purchaseProductWX", new object[] { info });
            }
            else if (this.platformId == 2)
            {
                Game.AndroidUtil.AndroidJavaObject.Call("purchaseProduct", new object[] { info });
            }
            else if (this.platformId == 3)
            {
                string data = "";
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
                int result = 1;////结果 1 支付成功  2 支付失败 3 放弃
                Game.SocketHall.DoConfirmOrderRequest(response.orderId, platformId, result, byteArray);
            }
        });
    }

    void OnPaySuccess(string info)
    {
        Debug.LogFormat("OnPaySuccess platformId: {0}; orderId:{1}; info: {2}", platformId, response.orderId, info);

        Game.SoundManager.PlayBuy();

        Game.AndroidUtil.m_kActPurchaseSuccess -= OnPaySuccess;
        Game.AndroidUtil.m_kActPurchaseFailures -= OnPayFailure;

        string data = "";
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
        int result = 1;////结果 1 支付成功  2 支付失败 3 放弃
        Game.SocketHall.DoConfirmOrderRequest(response.orderId, platformId, result, byteArray);
    }

    void OnPayFailure(string info)
    {
        Debug.LogFormat("OnPayFailure platformId: {0}; orderId:{1}; info: {2}", platformId, response.orderId, info);

        Game.AndroidUtil.m_kActPurchaseSuccess -= OnPaySuccess;
        Game.AndroidUtil.m_kActPurchaseFailures -= OnPayFailure;
        string data = "";
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
        int result = 2;////结果 1 支付成功  2 支付失败 3 放弃
        Game.SocketHall.DoConfirmOrderRequest(response.orderId, platformId, result, byteArray);
    }
}
