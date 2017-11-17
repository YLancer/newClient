using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using packet.mj;
using System;
using UnityEngine.EventSystems;

public class JiuYaoPage : JiuYaoPageBase
{
    private List<MJEntity> handCardList;

    public override void InitializeScene()
    {
        base.InitializeScene();

        detail.marksure_Btn.onClick.AddListener(OnClickShuaiJiuYao_MakeSureBtn);
    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        ResetCard();
        Game.SocketGame.DoREADYL(1, 2);
    }

    public override void OnBackPressed()
    {
        //提示是否取消甩九幺？
        base.OnBackPressed();
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        handCardList = Game.MJMgr.MyPlayer.handCardLayout.list;
        Invoke("OnBackPressed", 12f);//TODO wxd 临时处理
        SetupUI();
    }

    void SetupUI(params object[] args)
    {
        PopingCard();
    }

    private void PopingCard()
    {
        for (int i = 0; i < handCardList.Count; i++)
        {
            MJEntity cardObj = handCardList[i];
            int cardPoint = cardObj.Card;
            if (cardPoint % 8 == 1 || cardPoint > 48)
            {
                cardObj.isCardUp = true;
                cardObj.SetSelect(true);
            }
            else
            {
                cardObj.SetEnable(false);
            }
        }
    }

    private void ResetCard()
    {
        for (int i = 0; i < handCardList.Count; i++)
        {
            MJEntity cardObj = handCardList[i];
            int cardPoint = cardObj.Card;
            if (cardPoint % 8 == 1 || cardPoint > 48)
            {
                cardObj.isCardUp = false;
                cardObj.SetSelect(false);
            }
            else
            {
                cardObj.SetEnable(true);
            }
        }
    }

    // 甩九幺按钮触发的事件
    private void OnClickShuaiJiuYao_MakeSureBtn()
    {
        List<int> throwList = new List<int>();
        for (int i = 0; i < handCardList.Count; i++)
        {
            MJEntity cardObj = handCardList[i];
            if(cardObj.Select)
            {
                throwList.Add(cardObj.Card);
            }
        }

        if (throwList.Count % 3 != 0)
        {
            detail.Text_tishi.text = "选出的牌数不对，请选三张、六张或者九张";
            detail.Text_tishi.color = Color.red;
            return;
        }
        
        if (RoomMgr.actionNotify == null)//避免报错
        {
            RoomMgr.actionNotify = new GameOperPlayerActionNotify();
        }
        Game.SocketGame.DoGameOperPlayerActionSyn(MJUtils.ACT_SHUAIJIUYAO, throwList.ToArray());
        CancelInvoke("OnBackPressed");
        OnBackPressed();
        detail.Text_tishi.text = "";
    }
}
