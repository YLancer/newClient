using UnityEngine;
using System.Collections.Generic;
using System;
using packet.msgbase;
using packet.mj;

public class SettleRoundDialog : SettleRoundDialogBase
{

    private List<Transform> cardList = new List<Transform>();

    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnClickBack);
        detail.ContinueButton_Button.onClick.AddListener(OnClickContinue);
        detail.RoundAccountSub1_RoundAccountSub.gameObject.SetActive(false);

        detail.ShareButton_Button.onClick.AddListener(() =>
        {
            Game.AndroidUtil.ShareImage();
        });
    }

    void OnClickBack()
    {
        Game.SoundManager.PlayClose();
        if (RoomMgr.IsSingeRoom())
        {
            if (Game.UIMgr.IsSceneActive(UIPage.PlayPage))
            {
                Game.SocketGame.DoREADYL(1, 0);
            }
        }

        //if (RoomMgr.IsSingeRoom())
        //{
        //    Game.SocketGame.DoLeaveGame();
        //    Game.Instance.state = GameState.Hall;
        //    OnBackPressed();
        //}
        //else
        //{
        //    if (Game.Instance.state == GameState.Playing)
        //    {
        //        Game.SocketGame.DoBack2HallRequest();
        //    }
        //    else
        //    {
        //        OnBackPressed();
        //        Game.UIMgr.ResetToHomeScreen();
        //    }
        //}

        //Game.MJMgr.Clear();

        OnBackPressed();
    }

    private void OnClickContinue()
    {
        Game.SoundManager.PlayClick();

        //Game.MJMgr.Clear();

        if (!RoomMgr.IsVipRoom())
        {
            Game.SocketGame.DoREADYL(1, 0);
        }
        //Game.MJMgr.Clear();
        //if (Game.Instance.state != GameState.Playing)
        //{
        //    Game.SocketGame.DoENROLL(RoomMgr.playerGamingSyn.matchId);
        //}
        //else
        //{
        //    Game.SocketGame.DoREADYL();
        //}

        OnBackPressed();
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        SetupUI();
    }

    private void SetupUI()
    {
        detail.Time_Text.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        ClearCard();
        PrefabUtils.ClearChild(detail.Grid_GridLayoutGroup);

        if (null != RoomMgr.huSyn)
        {
            detail.Title_TextMeshProUGUI.text = MJUtils.GetHuType(RoomMgr.huSyn.resultType);

            if (RoomMgr.huSyn.resultType == MJUtils.HU_LiuJu)
            {
                foreach (GameOperPlayerSettle settle in RoomMgr.huSyn.detail)
                {
                    if (settle.position == Game.MJMgr.MyPlayer.postion)
                    {
                        SetupSub(detail.RoundAccountSub0_RoundAccountSub, settle, RoomMgr.huSyn.card);
                    }
                    else
                    {
                        GameObject child = PrefabUtils.AddChild(detail.Grid_GridLayoutGroup, detail.RoundAccountSub1_RoundAccountSub);
                        RoundAccountSub sub = child.GetComponent<RoundAccountSub>();
                        SetupSub(sub, settle);
                    }
                }
            }
            else
            {
                foreach (GameOperPlayerSettle settle in RoomMgr.huSyn.detail)
                {
                    if (settle.position == RoomMgr.huSyn.position)
                    {
                        SetupSub(detail.RoundAccountSub0_RoundAccountSub, settle, RoomMgr.huSyn.card);
                    }
                    else
                    {
                        GameObject child = PrefabUtils.AddChild(detail.Grid_GridLayoutGroup, detail.RoundAccountSub1_RoundAccountSub);
                        RoundAccountSub sub = child.GetComponent<RoundAccountSub>();
                        SetupSub(sub, settle);
                    }
                }
            }
        }

        PrefabUtils.ClearChild(detail.SingleCard_UIItem);
        SpawnCard(detail.SingleCard_UIItem.transform, RoomMgr.huSyn.bao);

        detail.M0_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_ZHA));
        detail.M1_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_ZHIDUI));
        detail.M2_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_37JIA));
        detail.M3_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_DANDIAOJIA));

        detail.M4_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_DAFENG));
        detail.M5_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_HONGZHONG));
        detail.M6_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_DAILOU));
        detail.M7_UIItem.gameObject.SetActive(MJUtils.HasWanfa(MJUtils.MODE_JIAHU));
    }

    void SetupSub(RoundAccountSub sub, GameOperPlayerSettle settle, int huCard = -1)
    {
        MJPlayer mjPlayer = Game.MJMgr.GetPlayerByPosition(settle.position);
        MjData data = Game.MJMgr.MjData[settle.position];
        sub.detail.Name_Text.text = data.player.nickName;
        sub.detail.ID_Text.text = "ID:" + data.player.playerId;
        if (settle.fanNum > 0)
        {
            sub.detail.Fan_TextMeshProUGUI.text = string.Format("+{0}分", settle.fanNum);
        }
        else
        {
            sub.detail.Fan_TextMeshProUGUI.text = string.Format("{0}分", settle.fanNum);
        }

        //sub.detail.ResultType_Text.text = settle.resultType.ToString();

        string fans = "";
        foreach (string fan in settle.fanDetail)
        {
            //if (fan.Contains("庄家")) continue;
            fans += fan.Trim() + " ";
        }
        sub.detail.TingInfo_Text.text = fans;

        bool IsMakers = Game.MJMgr.MakersPosition == settle.position;
        sub.detail.ZhuangFlag_Image.gameObject.SetActive(IsMakers);

        foreach (int card in mjPlayer.tableCardLayout.TableCards)
        {
            SpawnCard(sub.detail.TableCards_GridLayoutGroup.transform, card);
        }

        foreach (int card in settle.handcard)
        {
            SpawnCard(sub.detail.Handcards_GridLayoutGroup.transform, card);
        }

        if (huCard > 0)
        {
            SpawnCard(sub.detail.Handcards_GridLayoutGroup.transform, huCard);
        }
    }

    void SpawnCard(Transform parent, int card)
    {
        GameObject card0 = Game.PoolManager.MjPool.Spawn(card.ToString());
        if(null != card0)
        {
            card0.transform.SetParent(parent);
            card0.transform.localScale = Vector3.one;
            card0.transform.localRotation = Quaternion.identity;
            card0.transform.localPosition = Vector3.zero;
            cardList.Add(card0.transform);
        }
    }

    void ClearCard()
    {
        while (cardList.Count > 0)
        {
            Game.PoolManager.MjPool.Despawn(cardList[0]);
            cardList.RemoveAt(0);
        }
    }
}
