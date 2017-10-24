﻿using UnityEngine;
using System.Collections;
using packet.msgbase;
using packet.user;
using packet.game;
using packet.rank;
using System;
using UnityEngine.SceneManagement;

public class SocketHall : MonoBehaviour {
    //public string address = "121.40.177.10";    //老地址
    //public int port = 5000;
    public string address = "112.74.52.173";     //自有新地址
    public int port = 9999;

    public bool NeedAuth = false;

    public SocketNetTools SocketNetTools;

    void Start()
    {
        AddEventListener(PacketType.AuthRequest, OnAuth);
        //AddEventListener(PacketType.RoomResultResponse, OnRoomResultResponse);
        AddEventListener(PacketType.LogoutSyn, OnLogoutSyn);
        AddEventListener(PacketType.ServerChangeSyn, Game.OnServerChangeSyn);

        SocketNetTools.OnConnect -= OnConnect;
        SocketNetTools.OnConnect += OnConnect;
    }

    private void OnDestroy()
    {
        RemoveEventListener(PacketType.AuthRequest, OnAuth);
        //RemoveEventListener(PacketType.RoomResultResponse, OnRoomResultResponse);
        RemoveEventListener(PacketType.LogoutSyn, OnLogoutSyn);
        RemoveEventListener(PacketType.ServerChangeSyn, Game.OnServerChangeSyn);
    }

    void OnConnect()
    {
        if (SocketNetTools.Connected)
        {
            if (NeedAuth)
            {
                NeedAuth = false;
                Auth(Game.Instance.playerId, Game.Instance.token);
            }
        }
        else
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "连接游戏服务器失败！");
        }
    }

    public void Auth(int userId, string token)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.AuthRequest };
        AuthRequest request = new AuthRequest() { userId = userId, token = token, version = GlobalConfig.GetVersion };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    void OnAuth(PacketBase msg)
    {
        print("OnAuth Game");
        DoHeartBeat();
    }

    private Coroutine heartBeatCoroutine = null;
    void DoHeartBeat()
    {
        Game.StopDelay(heartBeatCoroutine);
		heartBeatCoroutine = Game.DelayLoop(GlobalConfig.HeartBeatTime, () =>
        {
            PacketBase msg = new PacketBase() { packetType = PacketType.HEARTBEAT };
            SocketNetTools.SendMsg(msg);
        });
    }


    private void OnLogoutSyn(PacketBase msg)
    {
        LogoutSyn response = NetSerilizer.DeSerialize<LogoutSyn>(msg.data);
        Action callback = () =>
        {
            Game.Logout();
        };
        Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, response.reason, "提示", callback);
    }

    public void AddEventListener(PacketType cmd,System.Action<PacketBase> callback)
    {
        SocketNetTools.AddEventListener((int)cmd, callback);
    }

    public void RemoveEventListener(PacketType cmd, System.Action<PacketBase> callback)
    {
        SocketNetTools.RemoveEventListener((int)cmd, callback);
    }

    public void LoginMsg(string username,string password,int type)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.LoginRequest };
        //设备号  1:ios 2:android 3:winphon 4:other
        int deviceFlag = GlobalConfig.GetPlatformId;
        LoginRequest request = new LoginRequest() { username = username, passward = password, type = type, version = GlobalConfig.GetVersion, deviceFlag= deviceFlag };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoRegistVistor()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.VistorRegisterRequest };
        int deviceFlag = GlobalConfig.GetPlatformId; //设备号 1:ios 2:android 3:winphon 4:other
        string deviceId = GlobalConfig.DeviceId;
        VistorRegisterRequest request = new VistorRegisterRequest() { deviceFlag = deviceFlag, deviceId = deviceId };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoRegist(string account,string nickname,string password)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.RegisterRequest };
        int deviceFlag = GlobalConfig.GetPlatformId; //设备号 1:ios 2:android 3:winphon 4:other
        string deviceId = GlobalConfig.DeviceId;
        RegisterRequest request = new RegisterRequest() { account = account, nickname = nickname, password = password,  deviceFlag = deviceFlag};
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void SendMsg()
    {
        PacketBase msg = new PacketBase() {packetType = PacketType.ChatRequest };
        //ChatRequest request = new ChatRequest() { context = msgText.text };
        //msg.data = NetSerilizer.Serialize(request);

        //// 不带回调，返回值预先注册监听
        ////SocketNetTools.SendMsg(msg);

        //// 或者下面这样，直接带回调
        //SocketNetTools.SendMsg(msg, MsgType.MSG_CHAT_RES, (m) =>
        //{
        //    ChatResponse response = NetSerilizer.DeSerialize<ChatResponse>(m.data);
        //    chatText.text += response.name + "222:" + response.context + "\n";
        //});
    }

    void OnChat(PacketBase msg)
    {
        //ChatResponse response = NetSerilizer.DeSerialize<ChatResponse>(msg.data);
        //chatText.text += response.name + ":" + response.context + "\n";
    }

    public void DoRoomResult(long roomId,Action<RoomResultResponse> callback)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.RoomResultRequest };
        RoomResultRequest request = new RoomResultRequest() { roomId = roomId };
        msg.data = NetSerilizer.Serialize(request);

        SocketNetTools.SendMsg(msg, PacketType.RoomResultResponse, (data) =>
        {
            RoomResultResponse response = NetSerilizer.DeSerialize<RoomResultResponse>(data.data);
            callback(response);
        });
    }

    private void OnRoomResultResponse(PacketBase msg)
    {
        throw new NotImplementedException();
    }

    public void DoMallProductRequest(Action callback)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.MallProductRequest };
        SocketNetTools.SendMsg(msg, PacketType.MallProductResponse, (data) =>
        {
            MallProductResponse response = NetSerilizer.DeSerialize<MallProductResponse>(data.data);
            Game.Instance.MallProduct = response;
            callback();
        });
    }

    public void DoGenOrderRequest(int platformId,string productId, Action<GenOrderResponse> callback)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.GenOrderRequest };
        GenOrderRequest request = new GenOrderRequest() { platformId = platformId, productId = productId };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg, PacketType.GenOrderResponse, (data) =>
        {
            GenOrderResponse response = NetSerilizer.DeSerialize<GenOrderResponse>(data.data);
            callback(response);
        });
    }

    public void DoConfirmOrderRequest(string orderId,int platformId, int result,byte[] data)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.ConfirmOrderRequest };
        ConfirmOrderRequest request = new ConfirmOrderRequest();
        request.orderId = orderId;
        request.platformId = platformId;
        request.result = result;
        request.data = data;

        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoRankRequest()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.RankRequest };
        SocketNetTools.SendMsg(msg, PacketType.RankSyn, (data) =>
        {
            RankSyn response = NetSerilizer.DeSerialize<RankSyn>(data.data);
            Game.Instance.RankSyn = response;

            EventDispatcher.DispatchEvent(MessageCommand.Update_Rank);
        });
    }

    public void DoModifyUserInfoRequest(string headImg, string nickName)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.ModifyUserInfoRequest };
        ModifyUserInfoRequest request = new ModifyUserInfoRequest() { headImg = headImg, nickName = nickName };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoRoomConfigRequest()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.RoomConfigRequest };
        SocketNetTools.SendMsg(msg, PacketType.RoomConfigResponse, (data) =>
        {
            RoomConfigResponse response = NetSerilizer.DeSerialize<RoomConfigResponse>(data.data);
            Game.Instance.RoomConfig = response;
        });
    }

    public void DoReceiveMailAttachRequest(long msgId)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.ReceiveMailAttachRequest };
        ReadMailMsgRequest request = new ReadMailMsgRequest() { msgId = msgId };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }
}
