using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using packet.msgbase;
using packet.user;
using System.Collections.Generic;
using packet.game;
using packet.rank;
using DG.Tweening;

public enum GameState
{
    Null = 0,
    Login = 1,
    Hall = 2,
    Waitting = 3,
    Playing = 4,
    Finish = 5,
}
public class Game : MonoBehaviour
{
    #region Public Reference
    public static Game Instance;
    [SerializeField]
    private UIMgr uiMgr = null;
    public static UIMgr UIMgr
    {
        get
        {
            return Instance.uiMgr;
        }
    }

    [SerializeField]
    private DialogMgr dialogMgr = null;
    public static DialogMgr DialogMgr
    {
        get
        {
            return Instance.dialogMgr;
        }
    }

    [SerializeField]
    private FlyTipsMgr flyTipsMgr = null;
    public static FlyTipsMgr FlyTipsMgr
    {
        get
        {
            return Instance.flyTipsMgr;
        }
    }

    //[SerializeField]
    //private StateMachine stateMachine = null;
    //public static StateMachine StateMachine
    //{
    //    get
    //    {
    //        return Instance.stateMachine;
    //    }
    //}

    [SerializeField]
    private InputManager inputManager = null;
    public static InputManager InputManager
    {
        get
        {
            return Instance.inputManager;
        }
    }

    [SerializeField]
    private MyPoolManager poolManager = null;
    public static MyPoolManager PoolManager
    {
        get
        {
            return Instance.poolManager;
        }
    }

    private static LoadingPage loadingPage;
    public static LoadingPage LoadingPage
    {
        get
        {
            if (null == loadingPage)
            {
                loadingPage = (LoadingPage)UIMgr.LoadScene(UIPage.LoadingPage);
            }
            return loadingPage;
        }
    }

    [SerializeField]
    private MJMgr mjMgr = null;
    public static MJMgr MJMgr
    {
        get
        {
            return Instance.mjMgr;
        }
    }

    [SerializeField]
    private MJTable mjTable = null;
    public static MJTable MJTable
    {
        get
        {
            return Instance.mjTable;
        }
    }

    [SerializeField]
    private SoundManager soundManager = null;
    public static SoundManager SoundManager
    {
        get
        {
            return Instance.soundManager;
        }
    }

    [SerializeField]
    private SocketHall socketHall = null;
    public static SocketHall SocketHall
    {
        get
        {
            return Instance.socketHall;
        }
    }

    [SerializeField]
    private SocketMsg socketMsg = null;
    public static SocketMsg SocketMsg
    {
        get
        {
            return Instance.socketMsg;
        }
    }

    [SerializeField]
    private SocketGame socketGame = null;
    public static SocketGame SocketGame
    {
        get
        {
            return Instance.socketGame;
        }
    }

    [SerializeField]
    private AndroidUtil androidUtil = null;
    public static AndroidUtil AndroidUtil
    {
        get
        {
            return Instance.androidUtil;
        }
    }

    public Queue<string> marquee = new Queue<string>();
    public MarqueeMsgSyn MarqueeMsg;
    public void SetMarqueeMsg(MarqueeMsgSyn response)
    {
        this.MarqueeMsg = response;

        int speed = 10;
        int num = 1;
        if(null != response.playerSetting)
        {
            string[] settings = response.playerSetting.Split('x');
            if (settings.Length > 1)
            {
                int.TryParse(settings[0], out speed);
                int.TryParse(settings[1], out num);
            }
        }

        for(int i = 0; i < num; i++)
        {
            marquee.Enqueue(response.content);
        }
    }

    [SerializeField]
    private MaterialManager materialManager = null;

    public List<MailMsgModel> Mails = new List<MailMsgModel>();
    public void SetNewMailMsg(NewMailMsgSyn response)
    {
        Mails.AddRange(response.mails);
        EventDispatcher.DispatchEvent(MessageCommand.Update_Mail);
    }

    public MailMsgModel GetMail(long mailId)
    {
        foreach (MailMsgModel mail in Mails)
        {
            if (mail.mailId == mailId)
            {
                return mail;
            }
        }

        return null;
    }

    public static MaterialManager MaterialManager
    {
        get
        {
            return Instance.materialManager;
        }
    }

    public ActAndNoticeMsgSyn ActAndNoticeMsg;
    public void SetActAndNoticeMsg(ActAndNoticeMsgSyn response)
    {
        this.ActAndNoticeMsg = response;
        // TODO 要提示红点吗？  好像登录要弹出？
    }

    [SerializeField]
    private IconManager iconManager = null;
    public static IconManager IconMgr
    {
        get
        {
            return Instance.iconManager;
        }
    }

    #endregion
    //public int FrameRate = 40;
    #region property
    public float score;
    public int cards;
    public int coins;
    public string nickname;
    public string face;
    public int sex;

    public GameState state = GameState.Null;
    public bool Ting = false;   // 是否已经听
    public bool Gang = false;   //是否杠了
    public int allGangCount=0 ;  //杠的次数

    public int playerId = -1;
    public static bool IsSelf(int playerId)
    {
        if (null == Instance)
        {
            return false;
        }
        else
        {
            return Instance.playerId == playerId;
        }
    }

    public string token;
    public int continueWinCount;
    public int totalGameCount;
    public float winRate;
    public string ip;
    public string maxFanType;
    public List<int> handcard;
    public List<int> downcard;
    public bool createMultiRoom;
    public MallProductResponse MallProduct;
    public RankSyn RankSyn;
    public RoomConfigResponse RoomConfig;
    //public static bool IsBusy = false;

    // 获取房间信息
    public RoomConfigModel GetRoomInfo(string matchType)
    {
        if (RoomConfig != null)
        {
            foreach (RoomConfigModel config in RoomConfig.roomList)
            {
                if(config.matchType == matchType)
                {
                    return config;
                }
            }
        }

        return null;
    }

    #endregion

    public static void Logout()
    {
        Game.Instance.playerId = 0;
        Game.Instance.token = "";
        Game.SocketGame.SocketNetTools.StopClient();
        Game.SocketHall.SocketNetTools.StopClient();
        Game.SocketMsg.SocketNetTools.StopClient();

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public static void ShowLogin()
    {
        //Game.LoadingPage.Show(LoadPageType.ProgressBar);
        //Game.LoadingPage.UpdateLoading(0.05f, "加载中。。。");
        //Game.DelayLoop(10, 0.1f, (index) =>
        //{
        //    Game.LoadingPage.UpdateLoading(index / 10f, "加载中。。。");

        //    if (index >= 9)
        //    {
        //        Game.LoadingPage.Hide();
        //        Game.UIMgr.PushScene(UIPage.LoginPage);
        //    }
        //});
        Game.UIMgr.PushScene(UIPage.LoginPage);
    }

    protected virtual void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Screen.SetResolution(978, 550, false);
        //Screen.SetResolution(640, 360, false);
#endif
        //Application.targetFrameRate = FrameRate;
        Instance = this;
        EventDispatcher.ClearAll();
        UIMgr.InitializeUISystem();

        //Game.StateMachine.SetNext (Game.StateMachine.InitState);
        ShowLogin();

        GDM.LoadAll(OnLoadGDMFinished);
    }

    void Start()
    {
        Game.SocketHall.AddEventListener(PacketType.UserInfoSyn, OnUserInfoSyn);
        Game.SocketHall.AddEventListener(PacketType.GlobalMsgSyn, OnGlobalMsgSyn);
        Game.SocketGame.AddEventListener(PacketType.GlobalMsgSyn, OnGlobalMsgSyn);
        Game.SocketMsg.AddEventListener(PacketType.GlobalMsgSyn, OnGlobalMsgSyn);
    }

    private void OnDestroy()
    {
        Game.SocketHall.RemoveEventListener(PacketType.UserInfoSyn, OnUserInfoSyn);
        Game.SocketHall.RemoveEventListener(PacketType.GlobalMsgSyn, OnGlobalMsgSyn);
        Game.SocketGame.RemoveEventListener(PacketType.GlobalMsgSyn, OnGlobalMsgSyn);
        Game.SocketMsg.RemoveEventListener(PacketType.GlobalMsgSyn, OnGlobalMsgSyn);
    }

    private void OnUserInfoSyn(PacketBase msg)
    {
        if (msg.code == 0)
        {
            UserInfoSyn response = NetSerilizer.DeSerialize<UserInfoSyn>(msg.data);
            this.playerId = response.userId;
            this.nickname = response.nickName;
            this.coins = response.coin;
            this.cards = response.fanka;
            this.face = response.headImg;
            //this.sex = response.sex;//性别 值为1时是男性，值为2时是女性，值为0时是未知
            this.sex = IconManager.GetSexByFace(response.sex, response.headImg);
            this.continueWinCount = response.continueWinCount;
            this.totalGameCount = response.totalGameCount;
            this.winRate = (float)response.winRate;
            this.ip = response.ip;
            this.maxFanType = response.maxFanType;
            this.handcard = response.handcard;
            this.downcard = response.downcard;
            this.createMultiRoom = response.createMultiRoom;

            Debug.LogFormat("OnUserInfoSyn playerId:{0};nickname:{1};face:{2};", playerId, nickname, face);

            EventDispatcher.DispatchEvent(MessageCommand.Update_UserInfo);
        }
    }

    private void OnGlobalMsgSyn(PacketBase msg)
    {
        Game.DialogMgr.PushDialogImmediately(UIDialog.SingleBtnDialog, msg.msg);
    }


    void OnLoadGDMFinished(bool sucess)
    {
        if (sucess)
        {
            GameData data = GDM.getSaveAbleData<GameData>();
            Game.SoundManager.MuteMusic(data.Music);
            Game.SoundManager.MuteSound(data.Sound);
        }
    }

    // Use this for initialization
    //	void Start () {
    //        Game.UIMgr.ResetToHomeScreen();
    //	}

    void OnApplicationQuit()
    {
        GDM.SaveAll();
    }

    //void OnApplicationFocus(bool hasFocus)
    //{
    //    print("====>><OnApplicationFocus>" + hasFocus);
    //}

    void OnApplicationPause(bool pauseStatus)
    {
        if (Game.Instance.state == GameState.Waitting || Game.Instance.state == GameState.Playing)
        {
            //print("====>><OnApplicationPause>" + pauseStatus);
            if (pauseStatus)
            {
                Game.SocketGame.DoAwayGameRequest();
            }
            else
            {
                Game.SocketGame.DoBackGameRequest();
            }
        }
    }

    public static void DelayWaitForEndOfFrame(System.Action callback)
    {
        if (null != Instance)
        {
            Instance.StartCoroutine(DelayFrame(callback));
        }
    }

    public static void Delay(float delayTime, System.Action callback)
    {
        if (null != Instance)
        {
            Instance.StartCoroutine(DelayTime(delayTime, callback));
        }
    }

    public static Coroutine DelayLoop(int loop, float delayTime, System.Action<int> callback, System.Action onFinish = null)
    {
        if (null == Instance)
        {
            return null;
        }
        else
        {
            return Instance.StartCoroutine(DelayLoopTime(loop, delayTime, callback, onFinish));
        }
    }

    public static Coroutine DelayLoop(float delayTime, System.Action callback, System.Action onFinish = null)
    {
        if (null == Instance)
        {
            return null;
        }
        else
        {
            return Instance.StartCoroutine(DelayLoopTime(delayTime, callback, onFinish));
        }
    }

    static IEnumerator DelayFrame(System.Action callback)
    {
        yield return new WaitForEndOfFrame();
        callback();
    }

    static IEnumerator DelayTime(float delayTime, System.Action callback)
    {
        yield return new WaitForSeconds(delayTime);
        callback();
    }

    static IEnumerator DelayLoopTime(int loop, float delayTime, System.Action<int> callback, System.Action onFinish)
    {
        for (int i = 0; i < loop; i++)
        {
            yield return new WaitForSeconds(delayTime);
            callback(i);
        }

        if (null != onFinish)
        {
            onFinish();
        }
    }

    static IEnumerator DelayLoopTime(float delayTime, System.Action callback, System.Action onFinish)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            callback();
        }
    }

    internal static void StopDelay(Coroutine coroutine)
    {
        if (null == Instance || null == coroutine)
        {
            return;
        }

        Instance.StopCoroutine(coroutine);
    }

    public static Coroutine Start(IEnumerator coroutine)
    {
        if (null == Instance || null == coroutine)
        {
            return null;
        }

        return Instance.StartCoroutine(coroutine);
    }

    public static void Reset()
    {
        Instance.score = 0;
        Instance.state = GameState.Hall;
        Instance.Ting = false;   // 是否已经听
        Instance.allGangCount = 0;
    }

    public static void OnServerChangeSyn(PacketBase msg)
    {
        ServerChangeSyn response = NetSerilizer.DeSerialize<ServerChangeSyn>(msg.data);
        Game.InitHallSocket(response.hallServerAddr);
        Game.InitMsgSocket(response.msgServerAddr);
        Game.InitGameSocket(response.gameServerAddr);

        Debug.LogFormat("hallServerAddr:{0}; msgServerAddr:{1}; gameServerAddr:{2}", response.hallServerAddr, response.msgServerAddr, response.gameServerAddr);
    }

    public static void InitHallSocket(string serverAddr)
    {
        if (string.IsNullOrEmpty(serverAddr))
        {
            return;
        }

        string[] adds = serverAddr.Split(':');
        string ip = adds[0];
        int port = int.Parse(adds[1]);

        if (Game.SocketHall.SocketNetTools.Connected)
        {
            if (Game.SocketHall.address == ip && Game.SocketHall.port == port)
            {
                return;
            }

            Game.SocketHall.NeedAuth = true;
        }

        Game.SocketHall.address = ip;
        Game.SocketHall.port = port;
        Game.SocketHall.SocketNetTools.StopClient();
        Game.SocketHall.SocketNetTools.StartClient(ip, port);
    }

    public static void InitMsgSocket(string serverAddr)
    {
        if (string.IsNullOrEmpty(serverAddr))
        {
            return;
        }
        string[] adds = serverAddr.Split(':');
        string ip = adds[0];
        int port = int.Parse(adds[1]);

        if (Game.SocketMsg.SocketNetTools.Connected)
        {
            if (Game.SocketMsg.address == ip && Game.SocketMsg.port == port)
            {
                return;
            }
        }

        Game.SocketMsg.address = ip;
        Game.SocketMsg.port = port;
        Game.SocketMsg.SocketNetTools.StopClient();
        Game.SocketMsg.SocketNetTools.StartClient(ip, port);
    }

    public static void InitGameSocket(string serverAddr)
    {
        if (string.IsNullOrEmpty(serverAddr))
        {
            return;
        }

        string[] adds = serverAddr.Split(':');
        string ip = adds[0];
        int port = int.Parse(adds[1]);

        if (Game.SocketGame.SocketNetTools.Connected)
        {
            if (Game.SocketGame.address == ip && Game.SocketGame.port == port)
            {
                return;
            }
        }

        Game.SocketGame.address = ip;
        Game.SocketGame.port = port;
        Game.SocketGame.SocketNetTools.StopClient();
        Game.SocketGame.SocketNetTools.StartClient(ip,port);
    }
}
