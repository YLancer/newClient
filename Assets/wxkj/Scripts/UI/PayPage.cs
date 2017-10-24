using UnityEngine;
using System.Collections;
using System;
using packet.game;

public class PayPage : PayPageBase
{
    Action<int> callback;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnClickClose);
        detail.WxButton_Button.onClick.AddListener(OnClickWX);
        detail.ZfbButton_Button.onClick.AddListener(OnClickZFB);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);

        callback = (Action<int>)sceneData[0];
        MallProductModel config = (MallProductModel)sceneData[1];
        if (config.category == 1)
        {
            detail.Text_Text.text = string.Format("购买 <color=\"#DC861B\">房卡X{0}</color> 价格 <color=\"#DC861B\">{1}</color> 元", config.itemCount, config.price);
        }
        else
        {
            string shotName = Utils.GetShotName(config.itemCount, 1);
            detail.Text_Text.text = string.Format("购买 <color=\"#DC861B\">{0} 金币</color> 价格 <color=\"#DC861B\">{1}</color> 元", shotName, config.price);
        }
        
    }

    private void OnClickClose()
    {
#if UNITY_EDITOR
        if (null != callback)
        {
            callback(3);
        }
#endif

        OnBackPressed();
    }

    private void OnClickZFB()
    {
        if(null != callback)
        {
            callback(2);
        }

        OnBackPressed();
    }

    private void OnClickWX()
    {
        if (null != callback)
        {
            callback(1);
        }

        OnBackPressed();
    }
}
