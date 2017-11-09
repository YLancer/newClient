using UnityEngine;
using System.Collections;
using packet.msgbase;
using packet.user;
using packet.game;
using cn.sharesdk.unity3d;
public class LoginPage : LoginPageBase {
    //登录类型 1 游客  2 用户名密码 3微信 4QQ
    private int loginType = 3;
    private string username = "kane";
    private string password = "";


    public void OnEnable() 
    {
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
     //   Game.AndroidUtil.AndroidJavaObject.Call("RequestLogin");
        //wx 2017.7.22***** 微信登录 单例里返回token和openid
        MyShareSDK.MyshareSDK.shareSdk.GetUserInfo(PlatformType.WeChat);
        Hashtable authinfo = MyShareSDK.MyshareSDK.shareSdk.GetAuthInfo(PlatformType.WeChat);
        print("GetAuthInfo" + MiniJSON.jsonEncode(authinfo));
      
        username = MyShareSDK.MyshareSDK.Username();
       // password = MyShareSDK.MyshareSDK.Password();
        password = (string)authinfo["token"];
        doLogin();
        Debug.Log("OnClickWeChatLogin.."+"发送账号"+username+"发送密码"+password);
    }
    //wx 2017.7.22*****
    //微信成功登陆回调
    void getUserInforCallback(int reqID, ResponseState state, PlatformType type, Hashtable data) 
    {
            if (data != null)
            {
                Debug.Log(data.toJson());
                WeChatLoginInfo Wechatloginfo = new WeChatLoginInfo();
                try
                {
                    Wechatloginfo.openid = (string)data["openid"];
                    Wechatloginfo.nickname = (string)data["nickname"];
                    Wechatloginfo.headimgurl = (string)data["headimgurl"];
                    Wechatloginfo.language = (string)data["unionid"];
                    Wechatloginfo.province = (string)data["province"];
                    Wechatloginfo.city = (string)data["city"];
                    Wechatloginfo.sex = int.Parse(data["sex"].ToString());
                    Debug.Log("Wechatloginfo.openid" + Wechatloginfo.openid + "Wechatloginfo.nickName" + Wechatloginfo.nickname + "city" + Wechatloginfo.city);
                    //  username=(string)data["openid"]; //oppenid
                    //  password=(string)data["unionid"]; //token
                    //  print("ranger****username" + username+"password"+password);
                    // //发送到服务器
                    //// Game.SocketHall.LoginMsg((string)data["openid"], (string)data["unionid"], loginType);
                    // //服务器回调 

                    // //跳转界面
                    //  Game.UIMgr.PushScene(UIPage.MainPage);
                    //  doLogin();
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
         Debug.Log("微信回调成功");
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

    void doLogin()
    {
        if (Game.SocketHall.SocketNetTools.Connected)
        {
            Game.SocketHall.LoginMsg(username, password, loginType);
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
            Game.SocketHall.LoginMsg(username, password, loginType);
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

    #region SDK
    public void OnWeChatLogin(string openid, string access_token)
    {
        username = openid;
        password = access_token;
        doLogin();
    }

    public void OnClickWXShare()
    {
        Game.AndroidUtil.AndroidJavaObject.Call("WeChatShare", new object[] { "http://www.daqingxinshikong.com/WebServer/?from=singlemessage&isappinstalled=1", "测试主题11", "测试内容22" });
    }
    #endregion
}
