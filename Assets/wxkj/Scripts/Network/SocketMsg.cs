using UnityEngine;
using System.Collections;
using packet.msgbase;
using packet.user;
using packet.game;
using System;

public class SocketMsg : MonoBehaviour
{
    public string address = "121.40.177.10";
    public int port = 5000;
    //public string address = "112.74.52.173";     //自有新地址
    //public int port = 5000;
    public SocketNetTools SocketNetTools;

    void Start()
    {
        //EventDispatcher.AddEventListener(MessageCommand.LoginSucess, OnLogin);
        AddEventListener(PacketType.AuthRequest, OnAuth);
        AddEventListener(PacketType.MarqueeMsgSyn, OnMarqueeMsgSyn);
        AddEventListener(PacketType.NewMailMsgSyn, OnNewMailMsgSyn);
        AddEventListener(PacketType.ActAndNoticeMsgSyn, OnActAndNoticeMsgSyn);
        AddEventListener(PacketType.ServerChangeSyn, Game.OnServerChangeSyn);

        SocketNetTools.OnConnect -= OnConnect;
        SocketNetTools.OnConnect += OnConnect;
    }

    private void OnDestroy()
    {
        //EventDispatcher.RemoveEventListener(MessageCommand.LoginSucess, OnLogin);
        RemoveEventListener(PacketType.AuthRequest, OnAuth);
        RemoveEventListener(PacketType.MarqueeMsgSyn, OnMarqueeMsgSyn);
        RemoveEventListener(PacketType.NewMailMsgSyn, OnNewMailMsgSyn);
        RemoveEventListener(PacketType.ActAndNoticeMsgSyn, OnActAndNoticeMsgSyn);
        RemoveEventListener(PacketType.ServerChangeSyn, Game.OnServerChangeSyn);
    }

    //void OnLogin(params object[] args)
    //{
    //    if (SocketNetTools.Connected)
    //    {
    //        Auth(Game.Instance.playerId, Game.Instance.token);
    //    }
    //    else
    //    {
    //        Connect();
    //        SocketNetTools.OnConnect -= OnConnect;
    //        SocketNetTools.OnConnect += OnConnect;
    //    }
    //}

    public void AddEventListener(PacketType cmd, System.Action<PacketBase> callback)
    {
        SocketNetTools.AddEventListener((int)cmd, callback);
    }

    public void RemoveEventListener(PacketType cmd, System.Action<PacketBase> callback)
    {
        SocketNetTools.RemoveEventListener((int)cmd, callback);
    }

    void OnConnect()
    {
        if (SocketNetTools.Connected)
        {
            if (Game.Instance.playerId != -1 && Game.Instance.token != "")
            {
                Auth(Game.Instance.playerId, Game.Instance.token);
            }
            //Auth(Game.Instance.playerId, Game.Instance.token);
        }
        else
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "连接消息服务器失败！");
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
        print("OnAuth Msg");
    }

    public void DoReadMsgRequest(long msgId)
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.ReadMailMsgRequest };
        ReadMailMsgRequest request = new ReadMailMsgRequest() { msgId = msgId };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    private void OnMarqueeMsgSyn(PacketBase msg)
    {
        if(msg.code == 0)
        {
            MarqueeMsgSyn response = NetSerilizer.DeSerialize<MarqueeMsgSyn>(msg.data);
            Game.Instance.SetMarqueeMsg(response);
        }
    }

    private void OnNewMailMsgSyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            NewMailMsgSyn response = NetSerilizer.DeSerialize<NewMailMsgSyn>(msg.data);
            Game.Instance.SetNewMailMsg(response);
        }
    }

    private void OnActAndNoticeMsgSyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            ActAndNoticeMsgSyn response = NetSerilizer.DeSerialize<ActAndNoticeMsgSyn>(msg.data);
            Game.Instance.SetActAndNoticeMsg(response);
        }
    }
}

