using UnityEngine;
using System.Collections.Generic;
using System;

public class UserInfoPage : UserInfoPageBase
{
    private bool mdf = false;
    private List<Transform> cardList = new List<Transform>();

    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.ChangeFaceButton_Button.onClick.AddListener(OnClickChangeFace);
        detail.ChangeNameButton_Button.onClick.AddListener(OnClickChangeName);
    }

    private void OnClickChangeFace()
    {
        Game.SoundManager.PlayClick();
        Game.UIMgr.PushScene(UIPage.SelectFacePage);
    }

    private void OnClickChangeName()
    {
        Game.SoundManager.PlayClick();
        if (!mdf)
        {
            mdf = true;
        }
        else
        {
            mdf = false;
            string face = Game.Instance.face;
            string nickName = detail.InputField_InputField.text;
            Game.SocketHall.DoModifyUserInfoRequest(face, nickName);
        }

        detail.InputField_InputField.gameObject.SetActive(mdf);
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);

        EventDispatcher.AddEventListener(MessageCommand.Update_UserInfo, SetupUI);
    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        EventDispatcher.RemoveEventListener(MessageCommand.Update_UserInfo, SetupUI);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        SetupUI();
    }

    private void SetupUI(params object[] objs)
    {
        Game data = Game.Instance;
        //detail.Face_Image.sprite = Game.IconMgr.GetFace(data.face);
        Game.IconMgr.SetFace(detail.Face_Image, data.face);

        detail.Name_Text.text = data.nickname;
        detail.PlayerId_Text.text = "ID:" + Game.Instance.playerId;
        detail.InputField_InputField.text = data.nickname;
        detail.CardNum_Text.text = data.cards.ToString();
        detail.CoinNum_Text.text = data.coins.ToString();
        detail.HighWin_Text.text = data.continueWinCount.ToString();
        detail.TotalRound_Text.text = data.totalGameCount.ToString();
        detail.WinRate_Text.text = data.winRate.ToString("f2")+"%";
        detail.MaxFan_Text.text = data.maxFanType;

        detail.InputField_InputField.gameObject.SetActive(mdf);

        ClearCard();

        foreach (int card in data.downcard)
        {
            int card1 = (card & 0xff);
            int card2 = ((card >> 8) & 0xff);
            int card3 = ((card >> 16) & 0xff);

            SpawnCard(detail.CardGroup_GridLayoutGroup.transform, card1);
            SpawnCard(detail.CardGroup_GridLayoutGroup.transform, card2);
            SpawnCard(detail.CardGroup_GridLayoutGroup.transform, card3);
        }

        SpawnCard(detail.CardGroup_GridLayoutGroup.transform);

        foreach (int card in data.handcard)
        {
            SpawnCard(detail.CardGroup_GridLayoutGroup.transform, card);
        }
    }

    void SpawnCard(Transform parent, int card = -1)
    {
        string sCard = "Dragon_Blank";
        if(-1!= card)
        {
            sCard = card.ToString();
        }
        GameObject card0 = Game.PoolManager.MjPool.Spawn(sCard);
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
