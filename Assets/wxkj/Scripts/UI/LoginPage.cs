using UnityEngine;
using System.Collections;
using packet.msgbase;
using packet.user;
using packet.game;
using cn.sharesdk.unity3d;
public class LoginPage : LoginPageBase {
    //登录类型 1 游客  2 用户名密码 3微信 4QQ
    private int loginType = 3;
    private WeChatLoginInfo loginInfo = null;
    public ShareSDK shareSdk = null;

    public void OnEnable()
    {
        if (shareSdk != null)
        {
            shareSdk.showUserHandler = getUserInforCallback;
        }
        ////wx 2017.7.22*****
        //if (detail.ShareSDK!=null)
        //{
        //    detail.ShareSDK.showUserHandler = getUserInforCallback;
        //    Debug.Log ("注册微信回调函数");
        //}

    }
    public override void InitializeScene ()
	{
		base.InitializeScene ();

		detail.WxButton_Button.onClick.AddListener (OnClickWX);  //微信登录点击事件
        detail.AccountButton_Button.onClick.AddListener(OnClickAccount);  //账号登录点击事件
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);
        Game.SocketHall.AddEventListener(PacketType.LoginRequest, OnLogin);  //回调登录信息
       // Game.AndroidUtil.m_kActOnWeChatLogin += OnWeChatLogin;  //注册微信登录方法
      
    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        Game.SocketHall.RemoveEventListener(PacketType.LoginRequest, OnLogin);
       // Game.AndroidUtil.m_kActOnWeChatLogin -= OnWeChatLogin;
    }

    public override void OnBackPressed()
    {
        base.OnBackPressed();
        Application.Quit();
    }

    void OnClickWX(){
        loginType = 3;
        Game.SoundManager.PlayClick();
        
        shareSdk.GetUserInfo(PlatformType.WeChat);
        Debug.Log("OnClickWeChatLogin.."+"发送账号"+ loginInfo.nickname + "发送密码" + loginInfo.openid);
    }

    //微信成功登陆回调
    void getUserInforCallback(int reqID, ResponseState state, PlatformType type, Hashtable data) 
    {
        if (data != null)
        {
            Debug.Log("微信回调成功 data: [" + data.toJson() + "]");
            loginInfo = new WeChatLoginInfo();
            try
            {
                loginInfo.openid = (string)data["openid"];
                loginInfo.nickname = (string)data["nickname"];
                loginInfo.headimgurl = (string)data["headimgurl"];
                loginInfo.unionid = (string)data["unionid"];
                loginInfo.province = (string)data["province"];
                loginInfo.city = (string)data["city"];
                loginInfo.sex = int.Parse(data["sex"].ToString());
                Debug.Log("Wechatloginfo.openid" + loginInfo.openid + "Wechatloginfo.nickName" + loginInfo.nickname + "city" + loginInfo.city);
                doWXLogin();
            }
            catch (System.Exception e)
            {
                Debug.Log("微信接口有变动！" + e.Message);
                return;
            }
        }
        else
        {
            Debug.Log("微信date数据为空");
        }
    }

	void OnClickQQ(){
        //loginType = 4;
        //doLogin();
        Game.SoundManager.PlayClick();
    }

    void OnClickAccount()
    {
        Game.SoundManager.PlayClick();
        //loginType = 4;
        //doLogin();
        Game.UIMgr.PushScene(UIPage.AccountLoginPage); //弹出账号登录面板

    }

    void doWXLogin()
    {
        print("   doWXLogin  " + loginInfo.nickname + "  connect " + Game.SocketHall.SocketNetTools.Connected);
        if (Game.SocketHall.SocketNetTools.Connected && loginInfo != null)
        {
            Game.SocketHall.WXLoginMsg(loginInfo, loginType);
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
        print("   on connect  ????  " + Game.SocketHall.SocketNetTools.Connected);
        Game.SocketHall.SocketNetTools.OnConnect -= OnConnect;

        if (loginInfo == null)
        {
            print(" WXlogin error no data "); //TODO wxd log bug
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "微信数据出错！");
            return;
        }
        if (Game.SocketHall.SocketNetTools.Connected)
        {
            Game.SocketHall.WXLoginMsg(loginInfo, loginType);
        }
        else
        {
            //Game.LoadingPage.Hide();
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "连接失败！");
        }
    }

    void OnLogin(PacketBase msg)
    {
        //Game.LoadingPage.Hide();
        if (msg.code == 0)
        {
            //GDM.Save(SAVE_DATA_TYPE.GameData);

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
