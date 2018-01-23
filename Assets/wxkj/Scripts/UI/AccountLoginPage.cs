using UnityEngine;
using System.Collections;
using System;
using packet.msgbase;
using packet.user;
using packet.game;

public class AccountLoginPage : AccountLoginPageBase
{
    //登录类型 1 游客  2 用户名密码 3微信 4QQ
    private int loginType = 2;
    private string username = "kane";
    private string password = "";
    private string ip = "";
    private bool isLogin = true;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.LoginButton_Button.onClick.AddListener(OnClickLogin);  // 账号登录   
        detail.RegistButton_Button.onClick.AddListener(OnClickRegist); // 注册账号
        detail.VisitorButton_Button.onClick.AddListener(OnClickVisitor); // 游客登录
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);
        Game.SocketHall.AddEventListener(PacketType.LoginRequest, OnLogin);
        Game.SocketHall.AddEventListener(PacketType.VistorRegisterResponse, OnVistorRegister);
    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        Game.SocketHall.RemoveEventListener(PacketType.LoginRequest, OnLogin);
        Game.SocketHall.RemoveEventListener(PacketType.VistorRegisterResponse, OnVistorRegister);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        GameData data = GDM.getSaveAbleData<GameData>();
        detail.Username_InputField.text = data.username;
        detail.Password_InputField.text = data.password;
        detail.SavePassword_CheckBoxSub.IsSelected = data.savePswd;
        detail.Agree_CheckBoxSub.IsSelected = data.agreement;
    }

    private void OnClickLogin()
    {
        Game.SoundManager.PlayClick();
        isLogin = true;
        loginType = 2;
        username = detail.Username_InputField.text;
        password = detail.Password_InputField.text;
        SavePassword();
        doLogin();
    }

    private void OnClickVisitor()
    {
        Game.SoundManager.PlayClick();
        GameData data = GDM.getSaveAbleData<GameData>();

        if (string.IsNullOrEmpty(data.usernameVisitor))
        {
            //regist
            isLogin = false;
            doRegist();
        }
        else
        {
            // login
            loginType = 1;
            username = data.usernameVisitor;// GlobalConfig.DeviceId;
            print("username:" + username);
            password = data.passwordVisitor;
            doLogin();
        }
    }

    void doRegist()
    {
        if (Game.SocketHall.SocketNetTools.Connected)
        {
            Game.SocketHall.DoRegistVistor();
        }
        else
        {
            Game.InitHallSocket(GlobalConfig.address);
            Game.SocketHall.SocketNetTools.OnConnect -= OnConnect;
            Game.SocketHall.SocketNetTools.OnConnect += OnConnect;
        }
    }

    private void OnVistorRegister(PacketBase msg)
    {
        if (msg.code == 0)
        {
            GameData data = GDM.getSaveAbleData<GameData>();

            VistorRegisterResponse response = NetSerilizer.DeSerialize<VistorRegisterResponse>(msg.data);
            data.usernameVisitor = response.account;
            data.passwordVisitor = response.password;
            GDM.SaveAll();

            loginType = 1;
            username = data.usernameVisitor;// GlobalConfig.DeviceId;
            print("username:" + username);
            password = data.passwordVisitor;
            doLogin();
        }
        else
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, msg.msg);
        }
    }

    private void OnClickRegist()
    {
        Game.SoundManager.PlayClick();
        Game.UIMgr.PushScene(UIPage.RegistPage);
    }

    void doLogin()
    {
        //Game.LoadingPage.Show(LoadPageType.LoopCircle);
        ip = Game.SocketHall.getIpAddress();
        //GameData data = GDM.getSaveAbleData<GameData>();
        //data.agreement = detail.Agree_CheckBoxSub.IsSelected;
        if (Game.SocketHall.SocketNetTools.Connected)   // &&  data.agreement==true   同意协议才能登录游戏
        {
            Game.SocketHall.LoginMsg(username, password, ip, loginType);
        }
        else
        {
            Game.SocketHall.SocketNetTools.OnConnect -= OnConnect;
            Game.SocketHall.SocketNetTools.OnConnect += OnConnect;
            Game.InitHallSocket(GlobalConfig.address);
        }
    }

    void OnConnect()
    {
        Game.SocketHall.SocketNetTools.OnConnect -= OnConnect;

        if (Game.SocketHall.SocketNetTools.Connected)
        {
            if (isLogin)
            {
                Game.SocketHall.LoginMsg(username, password, ip, loginType);
            }
            else
            {
                doRegist();
            }
        }
        else
        {
            //Game.LoadingPage.Hide();
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "连接失败！");
        }
    }


    private void SavePassword()
    {
        GameData data = GDM.getSaveAbleData<GameData>();
        data.savePswd = detail.SavePassword_CheckBoxSub.IsSelected;
        data.agreement = detail.Agree_CheckBoxSub.IsSelected;

        if (data.savePswd)
        {
            data.username = username;
            data.password = password;
        }
        else
        {
            data.password = "";
        }
    }

    void OnLogin(PacketBase msg)
    {
        //Game.LoadingPage.Hide();
        if (msg.code == 0)
        {
            GDM.Save(SAVE_DATA_TYPE.GameData);

            LoginResponse response = NetSerilizer.DeSerialize<LoginResponse>(msg.data);
            Game.Instance.playerId = response.userId;
            Game.Instance.token = response.token;

            Game.InitHallSocket(response.hallServerAddr);
            Game.InitMsgSocket(response.msgServerAddr);
            Game.InitGameSocket(response.gameServerAddr);

            //EventDispatcher.DispatchEvent(MessageCommand.LoginSucess);

            //Game.StateMachine.SetNext(Game.StateMachine.MenuState);
            Game.Reset();

            Game.SocketHall.DoRoomConfigRequest();
            //Game.SocketHall.DoRankRequest();

            Game.UIMgr.PushScene(UIPage.MainPage);
        }
        else
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, msg.msg);
        }
    }
}
