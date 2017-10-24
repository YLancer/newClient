using UnityEngine;
using System.Collections.Generic;
using packet.game;
using packet.msgbase;

public class RoomListDialog : RoomListDialogBase
{
    private VipRoomListSyn result;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnClickBack);
        detail.CreateButton_Button.onClick.AddListener(OnClickCreate);
        detail.CloseBanButton_Button.onClick.AddListener(OnClickCloseBan);
    }

    void OnClickBack()
    {
        OnBackPressed();
    }

    private void OnClickCloseBan()
    {
        Game.SoundManager.PlayClick();
        detail.BanPlayerSub_BanPlayerSub.gameObject.SetActive(false);
        detail.CloseBanButton_Button.gameObject.SetActive(false);
    }

    private void OnClickCreate()
    {
        Game.SoundManager.PlayClick();
        OnClickCloseBan();
        Game.DialogMgr.PushTips(UIDialog.CreateRoomDialog);
    }

    void OnVipRoomListSyn(PacketBase msg)
    {
        if (0 == msg.code)
        {
            result = NetSerilizer.DeSerialize<VipRoomListSyn>(msg.data);
            SetupUI();
        }
        else
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, msg.msg);
        }
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);

        Game.SocketGame.AddEventListener(PacketType.VipRoomListSyn, OnVipRoomListSyn);

        if (null != sceneData && sceneData.Length > 0)
        {
            result = (VipRoomListSyn)sceneData[0];
        }

        detail.BanPlayerSub_BanPlayerSub.gameObject.SetActive(false);
        detail.CloseBanButton_Button.gameObject.SetActive(false);
        SetupUI();
    }

    public override void OnSceneDeactivated()
    {
        base.OnSceneDeactivated();
        Game.SocketGame.RemoveEventListener(PacketType.VipRoomListSyn, OnVipRoomListSyn);
    }

    private void SetupUI()
    {
        PrefabUtils.ClearChild(detail.Content_GridLayoutGroup);

        if (null != result)
        {
            foreach (VipRoomModel room in result.roomList)
            {
                string code = room.code;
                GameObject child = PrefabUtils.AddChild(detail.Content_GridLayoutGroup, detail.RoomListSub_RoomListSub);
                RoomListSub sub = child.GetComponent<RoomListSub>();
                sub.SetValue(room);

                sub.detail.DismissButton_Button.onClick.AddListener(() =>
                {
                    Game.SoundManager.PlayClick();
                    Game.SocketGame.DoDismissVipRoom(code);
                });

                List<PlayerModel> playerList = room.players;
                sub.detail.PlayerButton_Button.onClick.AddListener(() =>
                {
                    Game.SoundManager.PlayClick();
                    detail.CloseBanButton_Button.gameObject.SetActive(true);
                    detail.BanPlayerSub_BanPlayerSub.gameObject.SetActive(true);
                    detail.BanPlayerSub_BanPlayerSub.transform.position = sub.detail.PlayerButton_Button.transform.position;
                    PrefabUtils.ClearChild(detail.BanPlayerSub_BanPlayerSub.detail.Grid_GridLayoutGroup);
                    foreach (PlayerModel player in playerList)
                    {
                        PlayerModel p = player;
                        GameObject child1 = PrefabUtils.AddChild(detail.BanPlayerSub_BanPlayerSub.detail.Grid_GridLayoutGroup, detail.BanPlayerSub_BanPlayerSub.detail.BanSub_BanSub);
                        BanSub sub1 = child1.GetComponent<BanSub>();
                        sub1.SetValue(p);
                        sub1.detail.Button_Button.onClick.AddListener(() =>
                        {
                            Game.SoundManager.PlayClick();
                            Game.SocketGame.DoKickPlayer(code, p.playerId);
                        });
                    }
                });

                sub.detail.WxButton_Button.onClick.AddListener(() =>
                {
                    Game.SoundManager.PlayClick();
                });
            }
        }
    }
}
