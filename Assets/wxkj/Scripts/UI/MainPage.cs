using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using packet.rank;
using packet.msgbase;
using DG.Tweening;
using System.Collections;

public class MainPage : MainPageBase
{
    private bool isRoundRank = true;
    private bool marquee = false;

    public Image[] flowLightImages;
    public float flowTime = 3;
    public float WaitFlowTime = 4;
    private int curFlowIndex = 0;
    
    public float curFlowTime = 0;

    public override void InitializeScene()
    {
        base.InitializeScene();

        detail.NormalRoomButton_Button.onClick.AddListener(OnClickNormal);
        detail.SingleButton_Button.onClick.AddListener(OnClickSingle);
        detail.CreateRoomButton_Button.onClick.AddListener(OnClickCreate);
        detail.JoinRoomButton_Button.onClick.AddListener(OnClickJoin);
        detail.ShopButton_Button.onClick.AddListener(OnClickShop);
        detail.MailButton_Button.onClick.AddListener(OnClickMail);
        detail.RecordButton_Button.onClick.AddListener(OnClickRecord);
        detail.ActivityButton_Button.onClick.AddListener(OnClickActivity);
        detail.HelpButton_Button.onClick.AddListener(OnClickHelp);
        detail.SettingButton_Button.onClick.AddListener(OnClickSetting);
        detail.ShareButton_Button.onClick.AddListener(OnClickShare);

        detail.GameRankButton_MainRankTabSub.detail.Button_Button.onClick.AddListener(OnClickGameRank);
        detail.WealthRankButton_MainRankTabSub.detail.Button_Button.onClick.AddListener(OnClickWealthRank);
    }

    private void OnClickWealthRank()
    {
        Game.SoundManager.PlayClick();
        isRoundRank = false;
        SetupRank();
    }

    private void OnClickGameRank()
    {
        Game.SoundManager.PlayClick();
        isRoundRank = true;
        SetupRank();
    }

    private void OnClickShare()
    {
        Game.SoundManager.PlayClick();
        Game.AndroidUtil.Share();
    }

    private void OnClickSetting()
    {
        Game.SoundManager.PlayClick();
        Game.UIMgr.PushScene(UIPage.SettingPage);
    }

    private void OnClickHelp()   
    {
        Game.SoundManager.PlayClick();
        Game.UIMgr.PushScene(UIPage.HelpPage);   
    }

    private void OnClickActivity()
    {
        Game.SoundManager.PlayClick();
        if (Game.Instance.ActAndNoticeMsg != null)
        {
            Game.UIMgr.PushScene(UIPage.NoticeActivePage, 1);
        }
    }

    private void OnClickRecord()
    {
        Game.SoundManager.PlayClick();
        Game.SocketHall.DoRoomResult(0, (response) =>
        {
            Game.UIMgr.PushScene(UIPage.TotalRecrodPage, response);
        });
    }

    private void OnClickMail()
    {
        Game.SoundManager.PlayClick();
        Game.UIMgr.PushScene(UIPage.MailPage);
    }

    private void OnClickShop()
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

    private void OnClickJoin()
    {
        Game.SoundManager.PlayClick();

        Game.SocketGame.DoPlayerGamingSynInquire((result) =>
        {
            if (result)
            {
                Game.SocketGame.DoEnterVipRoom("");
            }
            else
            {
                Game.DialogMgr.PushDialog(UIDialog.JoinRoomDialog);
            }
        });
    }

    private void OnClickCreate()
    {
        Game.SoundManager.PlayClick();
        Game.SocketGame.DoLoadVipRoom((result) =>
        {
            // 根据用户权限来决定不同逻辑
            if (Game.Instance.createMultiRoom)
            {
                Game.DialogMgr.PushDialog(UIDialog.RoomListDialog, result);
            }
            else
            {
                if (result.roomList.Count > 0)
                {
                    Game.SocketGame.DoEnterVipRoom(result.roomList[0].code);
                }
                else
                {
                    Game.DialogMgr.PushDialog(UIDialog.CreateRoomDialog);
                }
            }
        });
    }

    public override void OnBackPressed()
    {
        //base.OnBackPressed();
        //Application.Quit();

        Game.Logout();
    }

    void OnClickLogout()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //Game.ShowLogin();
    }

    void OnClickNormal()
    {
        Game.SoundManager.PlayClick();
        Game.UIMgr.PushScene(UIPage.RoomPage);
    }

    void OnClickSingle()
    {
        Game.SoundManager.PlayClick();
        List<int> list = null;
#if UNITY_EDITOR
        if (null != LogPage.Instance)
        {
            list = LogPage.Instance.GetTestCard();
        }
#endif

       Game.SocketGame.DoENROLL(MatchType.G_DQMJ_MATCH_SINGLE.ToString(), list);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);

        Game.SocketHall.DoRankRequest();
        Game.SoundManager.PlayMenuBackground();
        //SetupRank();

        //if (Game.Instance.state == GameState.Playing)
        //{
        //    //OnEnterRoom();
        //    Game.UIMgr.PushScene(UIPage.PlayPage);
        //}

        detail.Notice_Text.text = "";
        marquee = false;

        //for (int i = 0; i < 3; i++)
        //{
        //    Game.Instance.marquee.Enqueue("UGUI中RectTransform中Top和Bottom的设置，宽和高的设置，以及postion和锚点的设置");
        //}
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);
        //EventDispatcher.AddEventListener(MessageCommand.OnEnterRoom, OnEnterRoom);
        EventDispatcher.AddEventListener(MessageCommand.Update_Rank, SetupRank);

    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        //EventDispatcher.RemoveEventListener(MessageCommand.OnEnterRoom, OnEnterRoom);
        EventDispatcher.RemoveEventListener(MessageCommand.Update_Rank, SetupRank);
    }

    //void OnEnterRoom(params object[] args)
    //{
    //    //OnBackPressed();
    //    Game.UIMgr.PushScene(UIPage.PlayPage);
    //}

    void SetupRank(params object[] args)
    {
        Image gameRank = detail.GameRankButton_MainRankTabSub.detail.SelectFlag_Image;
        if (null != gameRank && null != gameRank.gameObject)
        {
            gameRank.gameObject.SetActive(isRoundRank);
        }

        Image wealthRank = detail.WealthRankButton_MainRankTabSub.detail.SelectFlag_Image;
        if (null != wealthRank && null != wealthRank.gameObject)
        {
            wealthRank.gameObject.SetActive(!isRoundRank);
        }

        PrefabUtils.ClearChild(detail.Content_GridLayoutGroup);

        RankSyn rank = Game.Instance.RankSyn;
        if (null != rank)
        {
            List<RankItem> list = rank.coinList;
            if (isRoundRank)
            {
                list = rank.gameCountList;
            }

            foreach (RankItem item in list)
            {
                GameObject child = PrefabUtils.AddChild(detail.Content_GridLayoutGroup, detail.RankSub_RankSub);
                RankSub sub = child.GetComponent<RankSub>();
                sub.SetupUI(item, isRoundRank);
            }
        }
    }

    void Update()
    {
        //Update_FlowLight();
        Update_Marquee();
    }

    void Update_Marquee()
    {
        if (!marquee)
        {
            if (Game.Instance.marquee.Count > 0)
            {
                marquee = true;
                string content = Game.Instance.marquee.Dequeue();
                detail.Notice_Text.text = content;
                detail.NoticeSub_Image.gameObject.SetActive(true);

                Game.DelayWaitForEndOfFrame(() =>
                {
                    RectTransform rectTrans = detail.Notice_Text.GetComponent<RectTransform>();
                    Vector2 pos = rectTrans.anchoredPosition;
                    pos.x = 300;
                    rectTrans.anchoredPosition = pos;
                    float s = 300 + rectTrans.sizeDelta.x;
                    float t = s / 100;

                    rectTrans.DOKill();
                    rectTrans.DOLocalMoveX(-s, t).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        marquee = false;
                    });
                });
            }
            else
            {
                detail.NoticeSub_Image.gameObject.SetActive(false);
            }
        }
    }

    void Update_FlowLight()
    {
        curFlowTime += Time.deltaTime;
        if (curFlowTime >= flowTime)
        {
            for (int i = 0; i < flowLightImages.Length; i++)
            {
                flowLightImages[i].material = null;
            }
        }

        if (curFlowTime >= WaitFlowTime)
        {
            curFlowTime = 0;

            flowLightImages[curFlowIndex].material = Game.MaterialManager.Flowlight_Fast;

            curFlowIndex++;
            if (curFlowIndex >= flowLightImages.Length)
            {
                curFlowIndex = 0;
            }
        }
    }
}
