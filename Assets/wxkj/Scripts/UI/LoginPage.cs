using UnityEngine;
using System.Collections;
using packet.msgbase;
using packet.user;
using packet.game;
using cn.sharesdk.unity3d;
using UnityEngine.UI;
using System;
using UnityEngine.Experimental.Networking;
using System.Text;
public class LoginPage : LoginPageBase
{
    //登录类型 1 游客  2 用户名密码 3微信 4QQ
    private int loginType = 3;
    //private WeChatLoginInfo loginInfo = null;
    private ShareSDK shareSdk = null;
    private string username = "";
    private string password = "";
    private string ip = "";

    public void OnEnable()
    {
        //shareSdk = GameObject.Find("ShardSDK").GetComponent<ShareSDK>();
        //text.text = "wj"+ shareSdk;
        //if (shareSdk != null)
        //{
        //    shareSdk.showUserHandler = getUserInforCallback;
        //}
        ////wx 2017.7.22*****
        //if (detail.ShareSDK!=null)
        //{
        //    detail.ShareSDK.showUserHandler = getUserInforCallback;
        //    Debug.Log ("注册微信回调函数");
        //}

    }
    void Start()
    {
        shareSdk = GameObject.Find("Main Camera").GetComponent<ShareSDK>();
        //分享回调事件
        shareSdk.shareHandler += ShareResultHandler;
        //授权回调事件
        shareSdk.authHandler += AuthResultHandler;
        //用户信息事件
        shareSdk.showUserHandler += getUserInforCallback;

    }
    void ShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        //成功
        if (state == ResponseState.Success)
        {
            Game.SocketHall.DoShareRequest();
        }
        //失败
        else if (state == ResponseState.Fail)
        {
        }
        //关闭
        else if (state == ResponseState.Cancel)
        {
        }
    }
    void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            //token = (string)result["token"];
            //授权成功的话，获取用户信息
            shareSdk.GetUserInfo(type);
        }
        else if (state == ResponseState.Fail)
        {
        }
        else if (state == ResponseState.Cancel)
        {
        }
    }
    //获取用户信息
    void GetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            //获取成功的话 可以写一个类放不同平台的结构体，用PlatformType来判断，用户的Json转化成结构体，来做第三方登录。
            switch (type)
            {
                case PlatformType.WeChat:
                    break;
            }
        }
        else if (state == ResponseState.Fail)
        {
        }
        else if (state == ResponseState.Cancel)
        {
        }
    }

    public override void InitializeScene()
    {
        base.InitializeScene();

        detail.WxButton_Button.onClick.AddListener(OnClickWX);  //微信登录点击事件
        detail.AccountButton_Button.onClick.AddListener(OnClickAccount);  //账号登录点击事件
        detail.Deal.onClick.AddListener(OnClickDeal);
        detail.close.onClick.AddListener(OnCloseDeal);
    }

    private void OnClickDeal()
    {
        detail.Deal_image.gameObject.SetActive(true);
    }
    private void OnCloseDeal()
    {
        detail.Deal_image.gameObject.SetActive(false);
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);
        Game.SocketHall.AddEventListener(PacketType.LoginRequest, OnLogin);  //回调登录信息
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

    IEnumerator TestWWW()
    {
        byte[] byteArray = Encoding.UTF8.GetBytes("{\"notifyUrl\":\"www.google.com\"}");
        WWWForm form = new WWWForm();
        form.AddField("amount", 100);
        form.AddField("orderNo", "1");
        form.AddField("channel", 7002);
        form.AddField("timePaid", "100000");
        form.AddField("mchNo", "123456");
        form.AddField("body", "testpay");
        form.AddField("description", "testpay2");
        form.AddField("subject", "testpay3");
        form.AddField("extra", "{\"notifyUrl\":\"www.google.com\"}");
        //form.AddBinaryData("extra", byteArray);
        form.AddField("version", "2.0");
        form.AddField("sign", "00");

        UnityWebRequest ww = new UnityWebRequest();
        WWW www = new WWW("https://tapi.pay.jommytech.com/applyPay", form);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            print(" ERROR: - " + www.error);
        }
        else
        {
            if (www.responseHeaders.ContainsKey("STATUS"))
            {
                print(" HEAD: - " + www.responseHeaders["STATUS"]);
                if(www.responseHeaders["STATUS"] == "HTTP/1.1 200 OK")
                {
                    print(" MSG: - " + www.text);
                }
            }
        }
    }

    void OnClickWX()
    {
        StartCoroutine(TestWWW());
        if (!detail.WXToggle.isOn)  return;
        loginType = 3;
        Game.SoundManager.PlayClick();
        //shareSdk.Authorize(PlatformType.WeChat);
    }

    public void PluginCallBack(string text)
    {
        text.showAsToast();
    }

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
                //Wechatloginfo.unionid = (string)data["unionid"];
                Wechatloginfo.province = (string)data["province"];
                Wechatloginfo.city = (string)data["city"];
                Wechatloginfo.IP = Game.SocketHall.getIpAddress();              //获取ip地址
                Debug.Log("Wechatloginfo.openid" + Wechatloginfo.openid + "Wechatloginfo.nickName" + Wechatloginfo.nickname + "city" + Wechatloginfo.city);
                username = (string)data["openid"]; //oppenid
                Hashtable authinfo = shareSdk.GetAuthInfo(PlatformType.WeChat);
                password = (string)authinfo["token"]; //token
                ip = Game.SocketHall.getIpAddress();              //获取ip地址
                print("ranger****username" + username + "password" + password);
                //发送到服务器
                //Game.SocketHall.LoginMsg((string)data["openid"], (string)data["unionid"], loginType);
                //服务器回调 

                //跳转界面
                //Game.UIMgr.PushScene(UIPage.MainPage);
                doLogin();
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
    void doLogin()
    {
        if (Game.SocketHall.SocketNetTools.Connected)
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


    void OnClickQQ()
    {
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

    void OnConnect()
    {
        Game.SocketHall.SocketNetTools.OnConnect -= OnConnect;

        if (Game.SocketHall.SocketNetTools.Connected)
        {
            Game.SocketHall.LoginMsg(username, password, ip, loginType);
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
