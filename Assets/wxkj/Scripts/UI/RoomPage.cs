using UnityEngine;
using System.Collections.Generic;
using packet.game;
using System;

public class RoomPage : RoomPageBase {
	public override void InitializeScene ()
	{
		base.InitializeScene ();

		detail.QuikStartButton_Button.onClick.AddListener (OnClickQuikStart);
	}

    public override void OnBackPressed()
    {
        base.OnBackPressed();
        Game.UIMgr.PushScene(UIPage.MainPage);
    }

    private void OnClickQuikStart()
    {
        Game.SoundManager.PlayClick();

        if (Game.Instance.RoomConfig != null)
        {
            int coins = Game.Instance.coins;
            foreach (RoomConfigModel config in Game.Instance.RoomConfig.roomList)
            {
                if (config.roomType == RoomType.Normal.ToString())
                {
                    string matchType = config.matchType;
                    if ((config.minCoinLimit <= coins && coins <= config.maxCoinLimit)
                        || config.minCoinLimit <= coins && config.maxCoinLimit <= 0)
                    {
                        Game.SocketGame.DoENROLL(matchType, null);
                        return;
                    }
                }
            }
        }

        Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "没有你可以进去的游戏场！");
    }

    public override void OnSceneActivated (params object[] sceneData)
	{
		base.OnSceneActivated (sceneData);

        SetupUI();
	}

    //public override void OnSceneOpened(params object[] sceneData)
    //{
    //    base.OnSceneOpened(sceneData);
    //    EventDispatcher.AddEventListener(MessageCommand.OnEnterRoom, OnEnterRoom);
    //}

    //public override void OnSceneClosed()
    //{
    //    base.OnSceneClosed();
    //    EventDispatcher.RemoveEventListener(MessageCommand.OnEnterRoom, OnEnterRoom);
    //}

    //void OnEnterRoom(params object[] args)
    //{
    //    Game.UIMgr.PushScene(UIPage.PlayPage);
    //}

    void SetupUI()
    {
        PrefabUtils.ClearChild(detail.Content_GridLayoutGroup);

        if(Game.Instance.RoomConfig != null)
        {
            foreach (RoomConfigModel config in Game.Instance.RoomConfig.roomList)
            {
                string matchType = config.matchType;
                if (config.roomType == RoomType.Normal.ToString())
                {
                    GameObject child = PrefabUtils.AddChild(detail.Content_GridLayoutGroup, detail.RoomSub_RoomSub.gameObject);
                    child.SetActive(true);
                    RoomSub sub = child.GetComponent<RoomSub>();
                    sub.SetValue(config);
                    sub.detail.Button_Button.onClick.AddListener(() =>
                    {
                        Game.SoundManager.PlayClick();
                        List<int> list = null;
#if UNITY_EDITOR
                        if (null != LogPage.Instance)
                        {
                            list = LogPage.Instance.GetTestCard();
                        }
#endif
                        Game.SocketGame.DoENROLL(matchType, list);
                    });
                }
            }
        }
    }
}
