using UnityEngine;
using System.Collections.Generic;
using packet.msgbase;
using packet.game;
using packet.mj;
using packet.user;
using System;

public partial class SocketGame : MonoBehaviour {

    void StartRoom()
    {
        //AddEventListener(PacketType.VipRoomListSyn, OnVipRoomListSyn);
        AddEventListener(PacketType.GameChatMsgSyn, OnGameChatMsgSyn);
        AddEventListener(PacketType.HangupSyn, OnHangupSyn);
        AddEventListener(PacketType.DissmissVoteSyn, OnDissmissVoteSyn);
        AddEventListener(PacketType.PlayerAwaySyn, OnPlayerAwaySyn);
        AddEventListener(PacketType.PlayerComebackSyn, OnPLAYER_COMEBACK);

        AddEventListener(PacketType.PlayerOfflineSyn, OnPLAYER_OFFLINE_SYN);
        AddEventListener(PacketType.PlayerReconnectSyn, OnPlayerReconnectSyn);

        AddEventListener(PacketType.PlayerExitSyn, OnPlayerExitSyn);
    }

    private void DestroyRoom()
    {
        //RemoveEventListener(PacketType.VipRoomListSyn, OnVipRoomListSyn);
        RemoveEventListener(PacketType.GameChatMsgSyn, OnGameChatMsgSyn);
        RemoveEventListener(PacketType.HangupSyn, OnHangupSyn);
        RemoveEventListener(PacketType.DissmissVoteSyn, OnDissmissVoteSyn);
        RemoveEventListener(PacketType.PlayerAwaySyn, OnPlayerAwaySyn);
        RemoveEventListener(PacketType.PlayerComebackSyn, OnPLAYER_COMEBACK);

        RemoveEventListener(PacketType.PlayerOfflineSyn, OnPLAYER_OFFLINE_SYN);
        RemoveEventListener(PacketType.PlayerReconnectSyn, OnPlayerReconnectSyn);

        RemoveEventListener(PacketType.PlayerExitSyn, OnPlayerExitSyn);
    }

    public void DoPlayerGamingSynInquire(Action<bool> callback)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.PlayerGamingSynInquire };

        SocketNetTools.SendMsg(msg, PacketType.PlayerGamingSynInquire, (data) =>
        {
            PlayerGamingSynInquire result = NetSerilizer.DeSerialize<PlayerGamingSynInquire>(data.data);
            callback(result.isGaming);
        });
    }

    private void OnDissmissVoteSyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            DissmissVoteSyn response = NetSerilizer.DeSerialize<DissmissVoteSyn>(msg.data);
            MjData player = Game.MJMgr.MjData[response.position];
            Action<bool> callback = (ok) => {
                DoDissmissVoteSyn(ok);
            };
            Game.DialogMgr.PushDialog(UIDialog.DoubleBtnDialog, player.player.nickName+"申请解散房间", "提示", callback);
        }
    }

    public void DoDissmissVoteSyn(bool ok)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.DissmissVoteSyn };
        DissmissVoteSyn request = new DissmissVoteSyn() {agree=ok };

        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    private void OnHangupSyn(PacketBase msg)
    {
        HangupSyn response = NetSerilizer.DeSerialize<HangupSyn>(msg.data);
        Game.MJMgr.HangUp = response.status == 1;// 1 托管  2 正常
        EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
    }

    public void DoCreateVipRoom(int vipRoomType, int quanNum,int wangfa, Action<VipRoomListSyn> callback)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.CreateVipRoomRequest };
        CreateVipRoomRequest request = new CreateVipRoomRequest();
        request.vipRoomType = vipRoomType;
        request.quanNum = quanNum;
        //request.vipRoomType = 0;
        request.wangfa = wangfa;

        msg.data = NetSerilizer.Serialize(request);

        SocketNetTools.SendMsg(msg, PacketType.VipRoomListSyn, (data) => {
            VipRoomListSyn result = NetSerilizer.DeSerialize<VipRoomListSyn>(data.data);
            callback(result);
        });
    }

    public void DoLoadVipRoom(Action<VipRoomListSyn> callback)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.VipRoomListReuqest };
        SocketNetTools.SendMsg(msg,PacketType.VipRoomListSyn,(data)=> {
            VipRoomListSyn result = NetSerilizer.DeSerialize<VipRoomListSyn>(data.data);
            callback(result);
        });
    }

    public void DoDismissVipRoom(string code)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.DismissVipRoomRequest };
        DismissVipRoomRequest request = new DismissVipRoomRequest() { code = code };

        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoEnterVipRoom(string psw)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.EnrollRequest };
        EnrollRequest request = new EnrollRequest() { roomCode = psw, gameId = "G_DQMJ"};

        msg.data = NetSerilizer.Serialize(request);

        SocketNetTools.SendMsg(msg);
    }

    public void DoKickPlayer(string code, int playerId)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.KickPlayerRequest };
        KickPlayerRequest request = new KickPlayerRequest() { code = code, playerId= playerId };

        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoGameChatMsgRequest(string content)
    {
        int contentType = 1;//类型 1 文字 2图片 3语音
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(content);

        PacketBase msg = new PacketBase() { packetType = PacketType.GameChatMsgRequest };
        GameChatMsgRequest request = new GameChatMsgRequest() { contentType = contentType, content = byteArray };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoGameChatMsgRequest(byte[] content)
    {
        int contentType = 3;//类型 1 文字 2图片 3语音
        PacketBase msg = new PacketBase() { packetType = PacketType.GameChatMsgRequest };
        GameChatMsgRequest request = new GameChatMsgRequest() { contentType = contentType, content = content };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    private void OnGameChatMsgSyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            GameChatMsgSyn response = NetSerilizer.DeSerialize<GameChatMsgSyn>(msg.data);
            Debug.LogFormat("===OnGameChatMsgSyn:" + Utils.ToStr(response));
            //类型 1 文字 2图片 3语音
            if (response.contentType == 1)
            {
                string str = System.Text.Encoding.Default.GetString(response.data);
                if (null != str && str.StartsWith("[") && str.EndsWith("]"))
                {
                    string sId = str.Substring(1, str.Length - 2);
                    int id = 0;
                    int.TryParse(sId, out id);
                    if (id < 100)
                    {
                        // 表情
                        EventDispatcher.DispatchEvent(MessageCommand.Chat, response.position, 0, id);
                    }
                    else
                    {
                        // 短语
                        EventDispatcher.DispatchEvent(MessageCommand.Chat, response.position, 1, id);
                    }
                    print(id);
                }
                else
                {
                    // 文字
                    print(str);
                    EventDispatcher.DispatchEvent(MessageCommand.Chat, response.position, 2, str);
                }
            }
            else
            {
                // 语音
                EventDispatcher.DispatchEvent(MessageCommand.Chat, response.position, 3, response.data);
            }
        }
    }

    public void DoHangUpRequest(bool hangUp)
    {
        PacketBase msg = new PacketBase() { packetType = hangUp? PacketType.HangupRequest: PacketType.CancelHangupRequest };
        SocketNetTools.SendMsg(msg);
    }
    //   AwayGameRequest = 2009; //离开房间
    //PlayerAwaySyn = 2010; //有玩家离开桌子
    //PlayerComebackSyn = 2011; //玩家回来

    //PlayerOfflineSyn = 2012; //有玩家掉线
    //PlayerReconnectSyn = 2013; //有玩家重连	

    //ExitGameRequest = 2014; //退出游戏
    //PlayerExitSyn = 2015;//有玩家退出游戏
    public void DoAwayGameRequest()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.AwayGameRequest};
        SocketNetTools.SendMsg(msg);
    }

    public void DoBackGameRequest()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.BackGameRequest };
        SocketNetTools.SendMsg(msg);
    }

    public void DoExitGameRequest()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.ExitGameRequest };
        SocketNetTools.SendMsg(msg);
    }

    void OnPlayerAwaySyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            PlayerAwaySyn response = NetSerilizer.DeSerialize<PlayerAwaySyn>(msg.data);
            if (Game.IsSelf(response.playerId))
            {
                Game.Instance.state = GameState.Hall;
                Game.UIMgr.PushScene(UIPage.MainPage);
                return;
            }
            else
            {

                Player player = Game.MJMgr.GetPlayerById(response.playerId);
                if (null != player)
                {
                    Game.MJMgr.MjData[player.position].player.leave = true;
                    EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
                }
                else
                {
                    Debug.LogWarningFormat("Game.MJMgr.GetPlayerById == null! playerId={0}", response.playerId);
                }
            }
        }
    }

    void OnPLAYER_COMEBACK(PacketBase msg)
    {
        if (msg.code == 0)
        {
            PlayerComebackSyn response = NetSerilizer.DeSerialize<PlayerComebackSyn>(msg.data);
            Player player = Game.MJMgr.GetPlayerById(response.playerId);
            if (null != player)
            {
                Game.MJMgr.MjData[player.position].player.leave = false;
                Game.MJMgr.MjData[player.position].player.offline = false;
                EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
            }
            else
            {
                Debug.LogWarningFormat("Game.MJMgr.GetPlayerById == null! playerId={0}", response.playerId);
            }
        }
    }

    void OnPLAYER_OFFLINE_SYN(PacketBase msg)
    {
        if (msg.code == 0)
        {
            PlayerOfflineSyn response = NetSerilizer.DeSerialize<PlayerOfflineSyn>(msg.data);
            Player player = Game.MJMgr.GetPlayerById(response.playerId);

            if (null != player)
            {
                Game.MJMgr.MjData[player.position].player.offline = true;
                EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
            }
            else
            {
                Debug.LogWarningFormat("Game.MJMgr.GetPlayerById == null! playerId={0}", response.playerId);
            }
        }
    }

    void OnPlayerReconnectSyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            PlayerReconnectSyn response = NetSerilizer.DeSerialize<PlayerReconnectSyn>(msg.data);
            Player player = Game.MJMgr.GetPlayerById(response.playerId);

            if (null != player)
            {
                Game.MJMgr.MjData[player.position].player.offline = false;
                EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
            }
            else
            {
                Debug.LogWarningFormat("Game.MJMgr.GetPlayerById == null! playerId={0}", response.playerId);
            }
        }
    }

    void OnPlayerExitSyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            PlayerExitSyn response = NetSerilizer.DeSerialize<PlayerExitSyn>(msg.data);
            if (Game.IsSelf(response.playerId))
            {
                Game.Instance.state = GameState.Hall;
                Game.UIMgr.PushScene(UIPage.MainPage);
                return;
            }
            else
            {
                Player player = Game.MJMgr.GetPlayerById(response.playerId);
                if (null != player)
                {
                    Game.MJMgr.MjData[player.position].player = null;
                    EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
                }
                else
                {
                    Debug.LogWarningFormat("Game.MJMgr.GetPlayerById == null! playerId={0}", response.playerId);
                }
            }
        }
    }
}
