using UnityEngine;
using System.Collections;
using System;
using packet.msgbase;
using packet.user;
using packet.game;

public class RegistPage : RegistPageBase
{
    string nickname = "";
    string password = "";
    string username = "";
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.RegistButton_Button.onClick.AddListener(OnClickRegist);
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);
        Game.SocketHall.AddEventListener(PacketType.RegisterResponse, OnRegister);
    }

    private void OnRegister(PacketBase msg)
    {
        Game.LoadingPage.Hide();

        if (msg.code == 0)
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "注册成功！");
        }
        else
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, msg.msg);
        }
    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        Game.SocketHall.RemoveEventListener(PacketType.RegisterResponse, OnRegister);
    }

    private void OnClickRegist()
    {
        Game.SoundManager.PlayClick();
        nickname = detail.Nickname_InputField.text;
        password = detail.Password_InputField.text;
        username = detail.Username_InputField.text;
        string password2 = detail.Password2_InputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "昵称不能为空！");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "密码不能为空！");
            return;
        }

        if (string.IsNullOrEmpty(username))
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "账号不能为空！");
            return;
        }

        if (password.Equals(password2)== false)
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "两次输入的密码不一致！");
            return;
        }

        doRegist();
    }

    void doRegist()
    {
        Game.LoadingPage.Show(LoadPageType.LoopCircle);

        if (Game.SocketHall.SocketNetTools.Connected)
        {
            Game.SocketHall.DoRegist(username, nickname, password);
        }
        else
        {
            Game.InitHallSocket(GlobalConfig.address);
            Game.SocketHall.SocketNetTools.OnConnect -= OnConnect;
            Game.SocketHall.SocketNetTools.OnConnect += OnConnect;
        }
    }
    void OnConnect()
    {
        Game.SocketHall.SocketNetTools.OnConnect -= OnConnect;

        if (Game.SocketHall.SocketNetTools.Connected)
        {
            Game.SocketHall.DoRegist(username, nickname, password);
        }
        else
        {
            Game.LoadingPage.Hide();
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "连接失败！");
        }
    }
}
