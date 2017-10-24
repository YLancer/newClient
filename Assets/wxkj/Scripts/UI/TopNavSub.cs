using UnityEngine;
using System.Collections;
using System;

public class TopNavSub : TopNavSubBase {
	public System.Action BackButtonClickAction;

    private string headImage;
	void Start()
	{
		this.detail.BackButton_Button.onClick.AddListener(OnBackButtonClick);
        detail.UserInfoButton_Button.onClick.AddListener(OnClickUserInfo);
        detail.CardButton_Button.onClick.AddListener(OnClickCard);
        detail.CoinsButton_Button.onClick.AddListener(OnClickCoin);
    }

    void Update()
    {
        detail.Nickname_Text.text = Game.Instance.nickname;
        detail.PlayerId_Text.text = "ID:"+Game.Instance.playerId;

        //detail.UserIcon_Image.sprite = Game.IconMgr.GetFace(Game.Instance.face);
        if(headImage != Game.Instance.face)
        {
            headImage = Game.Instance.face;
            //头像赋值
            Game.IconMgr.SetFace(detail.UserIcon_Image, headImage);
        }
        
        detail.CardNum_Text.text = Game.Instance.cards.ToString();
        detail.CoinsNum_Text.text = Utils.GetShotName(Game.Instance.coins,1);
    }

    private void OnClickCoin()
    {
        Game.SoundManager.PlayClick();
        if (null != Game.Instance.MallProduct && null != Game.Instance.MallProduct.products && Game.Instance.MallProduct.products.Count > 0)
        {
            Game.UIMgr.PushScene(UIPage.ShopPage, 0);
        }
        else
        {
            Game.SocketHall.DoMallProductRequest(() =>
            {
                Game.UIMgr.PushScene(UIPage.ShopPage, 0);
            });
        }
    }

    private void OnClickCard()
    {
        Game.SoundManager.PlayClick();
        if (null != Game.Instance.MallProduct && null != Game.Instance.MallProduct.products && Game.Instance.MallProduct.products.Count > 0)
        {
            Game.UIMgr.PushScene(UIPage.ShopPage, 1);
        }
        else
        {
            Game.SocketHall.DoMallProductRequest(() =>
            {
                Game.UIMgr.PushScene(UIPage.ShopPage, 1);
            });
        }
    }

    private void OnClickUserInfo()
    {
        Game.SoundManager.PlayClick();
        Game.UIMgr.PushScene(UIPage.UserInfoPage);
    }

	private void OnBackButtonClick()
	{
        if (BackButtonClickAction != null)
		{
			BackButtonClickAction();
		}
	}
}
