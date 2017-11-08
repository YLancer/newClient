using UnityEngine;
using System.Collections.Generic;
using packet.msgbase;
using packet.game;
using packet.mj;
using packet.user;
using System;

public partial class SocketGame : MonoBehaviour {
    public string address = "121.40.177.10";
    public int port = 5000;
    //public string address = "112.74.52.173";     //自有新地址
    //public int port = 5000;
    public SocketNetTools SocketNetTools;
    private List<Player> tempPlayer = new List<Player>();
    private Queue<GameOperation> queue = new Queue<GameOperation>();

    void Start()
    {
        //EventDispatcher.AddEventListener(MessageCommand.LoginSucess, OnLogin);
        AddEventListener(PacketType.AuthRequest, OnAuth);
        AddEventListener(PacketType.PlayerSitSyn, OnPLAYER_SIT_SYN);
        
        AddEventListener(PacketType.KickOutSyn, OnKickOutSyn);
        
        AddEventListener(PacketType.PlayerGamingSyn, OnGAMING_SYN);
        AddEventListener(PacketType.EnrollRequest, OnENROLL);
        AddEventListener(PacketType.ReadySyn, OnREADY_SYN);
        AddEventListener(PacketType.GameStartSyn, OnAllReadySyn);
        AddEventListener(PacketType.GameStartDealCardSyn, OnStartDealSyn);
        AddEventListener(PacketType.GameStartPlaySyn, OnStartGamePlaySyn);
        AddEventListener(PacketType.GameOperation, OnGameOperation);
        AddEventListener(PacketType.DeskDestorySyn, OnDeskDestorySyn);
        AddEventListener(PacketType.ServerChangeSyn, Game.OnServerChangeSyn);
        AddEventListener(PacketType.LogoutSyn, OnLogoutSyn);

        //EventDispatcher.AddEventListener(MessageCommand.PopQueue, OnPopQueue);

        SocketNetTools.OnConnect -= OnConnect;
        SocketNetTools.OnConnect += OnConnect;

        StartRoom();

        Game.DelayLoop(0.03f, () =>
        {
            bool IsBusy = Game.MJMgr.players[0].MJHand.IsBusy 
            || Game.MJMgr.players[1].MJHand.IsBusy 
            || Game.MJMgr.players[2].MJHand.IsBusy 
            || Game.MJMgr.players[3].MJHand.IsBusy;
            if (!IsBusy && queue.Count > 0)
            {
                GameOperation actionSyn = queue.Dequeue();
                _OnGameOperation(actionSyn);
            }
        });
    }

    private void OnDestroy()
    {
        //EventDispatcher.RemoveEventListener(MessageCommand.LoginSucess, OnLogin);
        RemoveEventListener(PacketType.AuthRequest, OnAuth);
        RemoveEventListener(PacketType.PlayerSitSyn, OnPLAYER_SIT_SYN);
        
        RemoveEventListener(PacketType.KickOutSyn, OnKickOutSyn);
        
        RemoveEventListener(PacketType.PlayerGamingSyn, OnGAMING_SYN);
        RemoveEventListener(PacketType.EnrollRequest, OnENROLL);
        RemoveEventListener(PacketType.ReadySyn, OnREADY_SYN);
        RemoveEventListener(PacketType.GameStartSyn, OnAllReadySyn);
        RemoveEventListener(PacketType.GameStartDealCardSyn, OnStartDealSyn);
        RemoveEventListener(PacketType.GameStartPlaySyn, OnStartGamePlaySyn);
        RemoveEventListener(PacketType.GameOperation, OnGameOperation);
        RemoveEventListener(PacketType.DeskDestorySyn, OnDeskDestorySyn);
        RemoveEventListener(PacketType.ServerChangeSyn, Game.OnServerChangeSyn);
        RemoveEventListener(PacketType.LogoutSyn, OnLogoutSyn);

        //EventDispatcher.RemoveEventListener(MessageCommand.PopQueue, OnPopQueue);

        DestroyRoom();
    }

    private void OnLogoutSyn(PacketBase msg)
    {
        LogoutSyn response = NetSerilizer.DeSerialize<LogoutSyn>(msg.data);
        Game.DialogMgr.PushDialogImmediately(UIDialog.SingleBtnDialog, response.reason, new System.Action(() =>
        {
            Game.Logout();
        }));
    }

    //void OnPopQueue(params object[] args)
    //{
    //    if (queue.Count > 0)
    //    {
    //        GameOperation actionSyn = queue.Dequeue();
    //        _OnGameOperation(actionSyn);
    //    }
    //}

    private void OnDeskDestorySyn(PacketBase obj)
    {
        Game.MJMgr.Clear();
        RoomMgr.Reset();
        Game.Instance.Ting = false;
        Game.Instance.state = GameState.Hall;
        Game.UIMgr.PushScene(UIPage.MainPage);
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
            Auth(Game.Instance.playerId, Game.Instance.token);
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

    public void DoENROLL(string matchType, List<int> cards = null)
    {
        Game.MJMgr.Clear();

        PacketBase msg = new PacketBase() { packetType = PacketType.EnrollRequest };
        EnrollRequest request = new EnrollRequest() { gameId = "G_DQMJ", matchId = matchType };
        if (null != cards)
        {
            request.cards.AddRange(cards);
        }
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    void OnPLAYER_SIT_SYN(PacketBase msg)
    {
        if (msg.code == 0)
        {
            PlayerSitSyn response = NetSerilizer.DeSerialize<PlayerSitSyn>(msg.data);

            Debug.LogFormat("===OnPLAYER_SIT_SYN:" + Utils.ToStr(response));

            Player player = new Player();
            player.position = response.position;
            player.playerId = response.playerId;
            //player.sex = response.sex;
            player.sex = IconManager.GetSexByFace(response.sex, response.headImg);
            player.nickName = response.nickName;
            player.headImg = response.headImg;
            player.coin = response.coin;
            player.score = response.score;
            player.offline = (response.online == 0);
            player.isReady[0] = (response.state == 1);
            player.leave = (response.away == 1);

            Game.MJMgr.MjData[player.position].player = player;

            if (player.playerId == Game.Instance.playerId)
            {
                Game.MJMgr.IntPosition(player.position);
                Game.MJTable.SetDirection(player.position);
            }

            int index = Game.MJMgr.GetIndexByPosition(response.position);
            Game.MJMgr.players[index].postion = response.position;

            EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
        }
    }

    string GetWanfaOne(int wanfa, int act, string name)
    {
        if ((wanfa & act) > 0)
        {
            return name + "/";
        }
        else
        {
            return "";
        }
    }

    string GetWanfa(int wanfa)
    {
        string str = "";
        str += GetWanfaOne(wanfa, MJUtils.MODE_ZHA, "炸");
        str += GetWanfaOne(wanfa, MJUtils.MODE_ZHIDUI, "支");
        str += GetWanfaOne(wanfa, MJUtils.MODE_37JIA, "三");
        str += GetWanfaOne(wanfa, MJUtils.MODE_DANDIAOJIA, "单");
        str += GetWanfaOne(wanfa, MJUtils.MODE_DAFENG, "风");
        str += GetWanfaOne(wanfa, MJUtils.MODE_HONGZHONG, "中");
        str += GetWanfaOne(wanfa, MJUtils.MODE_DAILOU, "漏");
        str += GetWanfaOne(wanfa, MJUtils.MODE_JIAHU, "夹");

        str += GetWanfaOne(wanfa, MJUtils.MODE_FENGPAI, "带风");
        str += GetWanfaOne(wanfa, MJUtils.MODE_BAOTING, "报听");
        str += GetWanfaOne(wanfa, MJUtils.MODE_SEVENPAIR, "七对");
        str += GetWanfaOne(wanfa, MJUtils.MODE_DAIHUI, "带会");
        str += GetWanfaOne(wanfa, MJUtils.MODE_ZIMOHU, "自摸");
        str += GetWanfaOne(wanfa, MJUtils.MODE_ONECOLORTRAIN, "清一色一条龙");
        str += GetWanfaOne(wanfa, MJUtils.MODE_SHUAIJIUYAO, "甩九幺");
        str += GetWanfaOne(wanfa, MJUtils.MODE_SHOUPAO, "收炮");
        return str;
    }

    void OnGAMING_SYN(PacketBase msg)
    {
        if (msg.code == 0)
        {
            PlayerGamingSyn response = NetSerilizer.DeSerialize<PlayerGamingSyn>(msg.data);

            Game.Instance.state = GameState.Waitting;

            Debug.Log("===OnGAMING_SYN [" + GetWanfa(response.wanfa) + "]");
            Game.MJMgr.targetFlag.gameObject.SetActive(false);
            
            RoomMgr.Reset();
            RoomMgr.playerGamingSyn = response;

            Game.MJTable.roomNum.text = "";
            Game.MJTable.baseScore.text = "";
            if (!RoomMgr.IsSingeRoom())
            {
                if (!RoomMgr.IsVipRoom())
                {
                    RoomConfigModel config = Game.Instance.GetRoomInfo(response.matchId);
                    if (null != config)
                    {
                        Game.MJTable.baseScore.text = string.Format("底分:{0}", config.baseScore);
                    }
                }
                else
                {
                    Game.MJTable.roomNum.text = string.Format("房间号:{0}", response.deskId);
                }
            }

            int col = RoomMgr.IsVip2Room() ? 10 : 6;
            Game.MJMgr.players[0].dropCardLayout.col = col;
            Game.MJMgr.players[1].dropCardLayout.col = col;
            Game.MJMgr.players[2].dropCardLayout.col = col;
            Game.MJMgr.players[3].dropCardLayout.col = col;

            if (Game.UIMgr.IsSceneActive(UIPage.PlayPage))
            {
                EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
            }
            else
            {
                Game.UIMgr.PushScene(UIPage.PlayPage);
            }
        }
    }

    void OnENROLL(PacketBase msg)
    {
        if (msg.code != 0)
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, msg.msg);
        }
        else
        {
            Game.MJMgr.HangUp = false;
            Game.MJMgr.Clear();
            //RoomMgr.Reset();
            Game.Instance.Ting = false;
            Game.Instance.state = GameState.Waitting;
            Game.MJMgr.targetFlag.gameObject.SetActive(false);
        }
    }

    public void DoREADYL(int state, int phase) //TODO WXD send ready
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.ReadyRequest };
        ReadyRequest request = new ReadyRequest() { state = state, phase = phase };
        msg.data = NetSerilizer.Serialize(request);
        SocketNetTools.SendMsg(msg);
    }

    void OnREADY_SYN(PacketBase msg)
    {
        if (msg.code == 0)
        {
            ReadySyn response = NetSerilizer.DeSerialize<ReadySyn>(msg.data);
            Player player = Game.MJMgr.GetPlayerById(response.playerId);
            if (null != player)
            {
                player.isReady[response.phase] = (response.state == 1);
                print("    --------------  wxd  ready " + response.state + " , " + response.phase); //TODO WXD get ready
                foreach(var f in player.isReady)
                {
                    print(" for : " + f);
                }
                print("-----------------------------------");
                EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
            }
        }
        else if (msg.code == (int)ErrCode.NotEnoughCoins)
        {
            // TODO 金币不足  无法准备
        }
    }

    void OnAllReadySyn(PacketBase msg)
    {
        DoREADYL(1, 1);
    }

    void OnStartDealSyn(PacketBase msg)
    {
        print("   -------   OnStartDealSyn");
        Game.MJMgr.isShuaiJiuYao = true;
    }

    void OnStartGamePlaySyn(PacketBase msg)
    {
        print("   -------   OnStartGamePlaySyn");
        Game.MJMgr.isShuaiJiuYao = false;
    }

    public void DoBack2HallRequest()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.Back2HallRequest };
        SocketNetTools.SendMsg(msg);
    }

    void OnGameOperation(PacketBase msg)
    {
        if (msg.code == 0)
        {
            GameOperation response = NetSerilizer.DeSerialize<GameOperation>(msg.data);
            //if (string.IsNullOrEmpty(Game.Instance.matchType) || Game.Instance.matchType == MatchType.G_DQMJ_MATCH_SINGLE.ToString())
            //{
            //    queue.Enqueue(response);
            //}else

            //if (HandAnima.IsBusy || queue.Count>0)
            {
                queue.Enqueue(response);
            }
            //else
            //{
            //_OnGameOperation(response);
            //}

        } else
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "错误：code:" + msg.code + "; msg:" + msg.msg);
        }
    }

    void _OnGameOperation(GameOperation response)
    {
        switch (response.operType)
        {
            case GameOperType.GameOperStartSyn:
                OnGameOperStartSyn(NetSerilizer.DeSerialize<GameOperStartSyn>(response.content));
                break;
            case GameOperType.GameOperHandCardSyn:
                //OnGameOperHandCardSyn(NetSerilizer.DeSerialize<GameOperHandCardSyn>(response.content));
                break;
            case GameOperType.GameOperPublicInfoSyn:
                OnGameOperPublicInfoSyn(NetSerilizer.DeSerialize<GameOperPublicInfoSyn>(response.content));
                break;
            case GameOperType.GameOperPlayerHuSyn:
                OnGameOperPlayerHuSyn(NetSerilizer.DeSerialize<GameOperPlayerHuSyn>(response.content));
                break;
            case GameOperType.GameOperPlayerActionNotify:
                OnGameOperPlayerActionNotify(NetSerilizer.DeSerialize<GameOperPlayerActionNotify>(response.content));
                break;
            case GameOperType.GameOperActorSyn:
                OnGameOperActorSyn(NetSerilizer.DeSerialize<GameOperActorSyn>(response.content));
                break;
            case GameOperType.GameOperPlayerActionSyn:
                OnGameOperPlayerActionSyn(NetSerilizer.DeSerialize<GameOperPlayerActionSyn>(response.content));
                break;
            case GameOperType.GameOperBaoChangeSyn:
                OnGameOperBaoChangeSyn(NetSerilizer.DeSerialize<GameOperBaoChangeSyn>(response.content));
                break;
            case GameOperType.GameOperFinalSettleSyn:
                OnGameOperFinalSettleSyn(NetSerilizer.DeSerialize<GameOperFinalSettleSyn>(response.content));
                break;
        }
    }

    void OnGameOperStartSyn(GameOperStartSyn data)
    {
        Debug.LogFormat("===StartSyn(seq[" + data.seq + "]):" + Utils.ToStr(data));

        Game.Instance.state = GameState.Playing;

        Game.SoundManager.PlayRoundStartSound();
        RoomMgr.gameOperStartSyn = data;
        Game.MJMgr.Clear();

        Game.MJMgr.CardLeft = data.cardLeft + 13 * 4;

        Game.MJMgr.cardHui = data.lastCard;        //TODO YC
        //MJCardGroup.ShowHuiCard(data.lastCard);    //TODO YC
        //Game.MJMgr.Init();
        Game.Instance.Ting = false;
        //Game.IsBusy = false;

        //Game.MJMgr.HangUp = false;

        for (int i = 0; i < Game.MJMgr.MjData.Length; i++)
        {
            if (null != Game.MJMgr.MjData[i])
            {
                Player player = Game.MJMgr.MjData[i].player;
                if (null != player)
                {
                    player.ting = false;
                }
            }
        }

        Game.MJMgr.MakersPosition = data.bankerPos;    // 庄家位置
        MJPlayer banker = Game.MJMgr.GetPlayerByPosition(data.bankerPos);

        Game.MJMgr.Shuffle();

        if (data.reconnect)
        {
            Game.MJTable.Dice(data.dice1, data.dice2);
            LicensingOnReconnect(data.playerHandCards);
        }
        else
        {
            banker.MJHand.PlayDize(data.dice1, data.dice2);

            LicensingOnStart(data.playerHandCards);
            Game.MJTable.PlayShuffle();

            Game.Delay(1, () =>
            {
                Game.MJMgr.Licensing(data.playerHandCards);
            });
        }

        if (Game.UIMgr.IsSceneActive(UIPage.PlayPage))
        {
            EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
        }
        else
        {
            Game.UIMgr.PushScene(UIPage.PlayPage);
        }
    }

    void LicensingOnStart(List<GameOperHandCardSyn> handCards)
    {
        foreach (GameOperHandCardSyn hc in handCards)
        {
            Debug.LogFormat("===CardSyn: {0} HandCard[{1}],DropCards[{2}],TableCards[{3}],reconnect:{4}", strs[hc.position], ToStr(hc.handCards), ToStr(hc.cardsBefore), ToStr(hc.downCards, true), false);
        }
    }

    void LicensingOnReconnect(List<GameOperHandCardSyn> handCards)
    {
        foreach (GameOperHandCardSyn hc in handCards)
        {
            Debug.LogFormat("===CardSyn: {0} HandCard[{1}],DropCards[{2}],TableCards[{3}],reconnect:{4}", strs[hc.position], ToStr(hc.handCards), ToStr(hc.cardsBefore), ToStr(hc.downCards, true), true);
            MJPlayer player = Game.MJMgr.GetPlayerByPosition(hc.position);

            player.dropCardLayout.Clear();
            player.tableCardLayout.Clear();
            player.handCardLayout.Clear();
            player.handCardLayout.PlayDefault();

            for (int i = 0; i < hc.handCards.Count; i++)
            {
                int sCard = hc.handCards[i];
                player.handCardLayout.AddCard(sCard);
                MJCardGroup.TryDragCard();
            }

            for (int i = 0; i < hc.cardsBefore.Count; i++)
            {
                int sCard = hc.cardsBefore[i];
                player.dropCardLayout.AddCard(sCard);
                MJCardGroup.TryDragCard();
            }

            for (int i = 0; i < hc.downCards.Count; i++)
            {
                int sCard = hc.downCards[i];
                int card1 = (sCard & 0xff);
                int card2 = ((sCard >> 8) & 0xff);
                int card3 = ((sCard >> 16) & 0xff);

                player.tableCardLayout.AddCard(card1);
                player.tableCardLayout.AddCard(card2);
                player.tableCardLayout.AddCard(card3);

                MJCardGroup.TryDragCard();
                MJCardGroup.TryDragCard();
                MJCardGroup.TryDragCard();
            }
        }
    }

    string[] cardTypes = new string[] { "万", "条", "筒", "风", "花" };
    string GetCardStr(int c)
    {
        if (c <= 0)
        {
            return "空";
        }
        //else if (c == 69)
        //{
        //    return "中";
        //}

        int type = (c >> 4);
        int card = (c & 0x0f);

        return card + cardTypes[type];
    }
    string ToStr(List<int> list, bool is3 = false)
    {
        string str = "";
        foreach (int c in list)
        {
            if (is3)
            {
                int card1 = (c & 0xff);
                int card2 = ((c >> 8) & 0xff);
                int card3 = ((c >> 16) & 0xff);

                str += "[" + GetCardStr(card1) + ",";
                str += GetCardStr(card2) + ",";
                str += GetCardStr(card3) + "],";
            }
            else
            {
                str += GetCardStr(c) + ",";
            }
        }
        return str;
    }

    void OnGameOperPublicInfoSyn(GameOperPublicInfoSyn data)
    {
        Debug.LogFormat("=== 剩余：{0}", data.cardLeft);
        //Game.MJMgr.MakersPosition = data.bankerPos;
        Game.MJMgr.CardLeft = data.cardLeft;
        Game.MJMgr.cardHui = data.cardLeft;   //TODO YC
        //Game.MJMgr.BaoCard = data.baoCard;
        //EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
    }

    string[] strs = new string[] { "北", "东", "南", "西" };
    public void OnGameOperPlayerActionNotify(GameOperPlayerActionNotify data)
    {
        if (RoomMgr.actionNotify != null && ((RoomMgr.actionNotify.actions & MJUtils.ACT_SHUAIJIUYAO) != 0)) //TODO WXD 额外添加甩九幺标记，防止被覆盖。
        {
            data.actions |= MJUtils.ACT_SHUAIJIUYAO;
        }

        RoomMgr.actionNotify = data;

        int position = data.position;
        int actions = data.actions;
        int lastActionPosition = data.lastActionPosition;
        int lastActionCard = data.lastActionCard;

        string pengArg = "";
        if (MJUtils.Peng())
        {
            pengArg = GetCardStr(data.pengArg);
        }

        string chi = "";
        if (MJUtils.Chi())
        {
            int count = data.chiArg.Count;
            for (int i = 0; i < count; i++)
            {
                GameOperChiArg arg = data.chiArg[i];
                chi += "[" + GetCardStr(arg.myCard1) + "," + GetCardStr(arg.myCard2) + "]-" + GetCardStr(arg.targetCard) + " ";
            }
        }

        Debug.LogFormat("===<color=blue>提示</color>,最后操作[{0}]; 牌[{1}]", strs[lastActionPosition], GetCardStr(lastActionCard));
        Debug.LogFormat("===<color=blue>提示</color>,{0} <color=yellow>{1}</color>,[{2}] 支对:[{3}],听列表:[{4}]", strs[data.position], ActionStr(data.actions), pengArg + chi, ToStr(data.tingDzs), ToStr(data.tingList));

        if (lastActionPosition >= 0 && lastActionCard > 0)
        {
            MJPlayer p = Game.MJMgr.GetPlayerByPosition(lastActionPosition);
            GameObject go = p.dropCardLayout.last;
            if (null != go)
            {
                MJEntity et = go.GetComponent<MJEntity>();
                if (null != et)
                {
                    Game.MJMgr.LastDropCard = et;
                    Game.MJMgr.targetFlag.gameObject.SetActive(true);
                    Game.MJMgr.targetFlag.position = go.transform.position;
                }
            }
        }
        else
        {
            Game.MJMgr.targetFlag.gameObject.SetActive(false);
        }

        EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
    }

    string ActionsStrOne(int act, string name, int action)
    {
        if (MJUtils.CanAct(act, action))
        {
            return name + "/";
        } else
        {
            return "";
        }
    }
    string ActionStr(int action)
    {
        string str = "[";
        str += ActionsStrOne(MJUtils.ACT_CHI, "吃", action);
        str += ActionsStrOne(MJUtils.ACT_PENG, "碰", action);
        str += ActionsStrOne(MJUtils.ACT_AN_GANG, "暗杠", action);
        str += ActionsStrOne(MJUtils.ACT_BU_GANG, "补杠", action);
        str += ActionsStrOne(MJUtils.ACT_ZHI_GANG, "直杠", action);
        str += ActionsStrOne(MJUtils.ACT_DROP_CARD, "出", action);
        str += ActionsStrOne(MJUtils.ACT_HU, "胡", action);
        str += ActionsStrOne(MJUtils.ACT_TING, "听", action);
        str += ActionsStrOne(MJUtils.ACT_PASS, "过", action);
        str += ActionsStrOne(MJUtils.ACT_TING_CHI, "吃听", action);
        str += ActionsStrOne(MJUtils.ACT_DRAG_CARD, "摸牌", action);
        str += ActionsStrOne(MJUtils.ACT_TING_PENG, "碰听", action);
        str += ActionsStrOne(MJUtils.ACT_TING_ZHIDUI, "支对听", action);
        if (str.Length > 1)
        {
            str = str.Substring(0, str.Length - 1);
        }
        str += "]";
        return str;
    }

    void OnGameOperPlayerActionSyn(GameOperPlayerActionSyn data)
    {
        Debug.LogFormat("===<color=green>同步</color>[seq({0})]:{1} <color=yellow>{2}</color> 牌:[{3}]", data.seq, strs[data.position], ActionStr(data.action), ToStr(data.cardValue));

        int position = data.position;
        MJPlayer player = Game.MJMgr.GetPlayerByPosition(data.position);
        bool isMy = player.index == 0;

        //MJCardGroup.ShowHuiCard(data.cardValue[0]);  //TODO YC

        if (MJUtils.DragCard(data.action))
        {
            int card = data.cardValue[0];
            player.DragCard(card, isMy);
        }
        else if (MJUtils.DropCard(data.action))
        {
            HandAnima.IsBusy = true;
            int card = data.cardValue[0];
            //Game.MJMgr.LastDropCardPlayer = player;

            player.MJHand.PlayDropCard(card, isMy);
        }
        else if (MJUtils.Peng(data.action))
        {
            player.MJHand.PlayPeng(data.cardValue[0], isMy);
        }
        //胡牌的动画
        else if(MJUtils.Hu(data.action))
        {
            player.MJHand.PlayHU(data.cardValue[0], isMy);
        }
        else if (MJUtils.Chi(data.action))
        {
            player.MJHand.PlayChi(data.cardValue[0], data.cardValue[1], isMy);
        }
        else if (MJUtils.AnGang(data.action))
        {
            player.MJHand.PlayGang(data.cardValue[0], isMy, 1);
        }
        else if (MJUtils.BuGang(data.action))
        {
            player.MJHand.PlayGang(data.cardValue[0], isMy, 2);
        }
        else if (MJUtils.ZhiGang(data.action))
        {
            player.MJHand.PlayGang(data.cardValue[0], isMy, 3);
        }
        else if (MJUtils.Ting(data.action))
        {
            player.MJHand.PlayTing(isMy);
        }
        else if (MJUtils.TingPeng(data.action))
        {
            player.MJHand.PlayPeng(data.cardValue[0], isMy);

            Game.Delay(0.5f, () =>
            {
                player.MJHand.PlayTing(isMy);
            });
        }
        else if (MJUtils.TingChi(data.action))
        {
            player.MJHand.PlayChi(data.cardValue[0], data.cardValue[1], isMy);

            Game.Delay(0.5f, () =>
            {
                player.MJHand.PlayTing(isMy);
            });
        }
        else if (MJUtils.TingZhidui(data.action))
        {
            Game.SoundManager.PlayZhidui(position);

            int card = data.cardValue[0];
            Game.MJMgr.MjData[position].player.ting = true;
            EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
            player.handCardLayout.PlayZhidui(card, isMy);
        }
        else if (MJUtils.ShuaiJiuYao(data.action))
        {
            player.MJHand.PlayShuaiJiuYao(data.cardValue.ToArray(), isMy);
        }
    }

    void OnGameOperActorSyn(GameOperActorSyn data)
    {
        //Debug.LogFormat("===OnActorSyn:{0} timeLeft:{1}", strs[data.position],data.timeLeft);
        int position = data.position;
        int timeLeft = data.timeLeft;
        Game.MJTable.ShowCountdown(timeLeft, position);
    }

    public void DoGameOperPlayerActionSyn(int action, params int[] card)
    {
        Debug.LogFormat("=>DoActionSyn action:{0},card1:{1},card2:{2}", ActionStr(action), card.Length > 0 ? GetCardStr(card[0]) : GetCardStr(-1), card.Length > 1 ? GetCardStr(card[1]) : GetCardStr(-1));
        RoomMgr.actionNotify.actions = 0;

        GameOperPlayerActionSyn content = new GameOperPlayerActionSyn();
        content.action = action;
        content.cardValue.AddRange(card);

        GameOperation request = new GameOperation() { operType = GameOperType.GameOperPlayerActionSyn };
        request.content = NetSerilizer.Serialize<GameOperPlayerActionSyn>(content);

        PacketBase msg = new PacketBase() { packetType = PacketType.GameOperation };
        msg.data = NetSerilizer.Serialize<GameOperation>(request);
        SocketNetTools.SendMsg(msg);
    }

    public void DoDropCard(int card)
    {
        //if (Game.MJMgr.Ting)
        //{
        //    DoGameOperPlayerActionSyn(MJUtils.ACT_DROP_CARD | MJUtils.ACT_TING, card);
        //}
        //else
        {
            DoGameOperPlayerActionSyn(MJUtils.ACT_DROP_CARD, card);
        }
    }

    public void DoChi(int card1, int card2)
    {
        if (Game.Instance.Ting)
        {
            DoGameOperPlayerActionSyn(MJUtils.ACT_TING_CHI, card1, card2);
        }
        else
        {
            DoGameOperPlayerActionSyn(MJUtils.ACT_CHI, card1, card2);
        }
    }

    public void DoPeng(int card)
    {
        if (Game.Instance.Ting)
        {
            DoGameOperPlayerActionSyn(MJUtils.ACT_TING_PENG, card);
        }
        else
        {
            DoGameOperPlayerActionSyn(MJUtils.ACT_PENG, card);
        }
    }
    //胡牌我写的
    public  void DoHu(int card = -1)
    {
        DoGameOperPlayerActionSyn(MJUtils.ACT_HU, card);
    }
    
    public void DoAnGang(int card)
    {
        DoGameOperPlayerActionSyn(MJUtils.ACT_AN_GANG, card);
    }

    public void DoBuGang(int card)
    {
        DoGameOperPlayerActionSyn(MJUtils.ACT_BU_GANG, card);
    }

    public void DoZhiGang(int card)
    {
        DoGameOperPlayerActionSyn(MJUtils.ACT_ZHI_GANG, card);
    }

    public void DoZhidui(int card)
    {
        DoGameOperPlayerActionSyn(MJUtils.ACT_TING_ZHIDUI, card);
    }

    public void DoTing(int card = -1)
    {
        DoGameOperPlayerActionSyn(MJUtils.ACT_TING, card);
    }

    public void DoPass()
    {
        DoGameOperPlayerActionSyn(MJUtils.ACT_PASS, -1);
    }

    void OnGameOperBaoChangeSyn(GameOperBaoChangeSyn data)
    {
        HandAnima.IsBusy = true;
        int dice = data.dice;
        int oldBao = data.oldBao;
        int position = data.position;
        Debug.LogFormat("===换宝:{0} oldBao:{1} dice:[{2}]", strs[data.position], GetCardStr(oldBao), dice);
        //Debug.LogFormat("===OnGameOperBaoChangeSyn:" + Utils.ToStr(data));


        MJPlayer player = Game.MJMgr.GetPlayerByPosition(position);
        player.MJHand.Bao(dice, oldBao);
    }

    public void DoDump()
    {
        PacketBase msg = new PacketBase() { packetType = PacketType.Dump };
        SocketNetTools.SendMsg(msg);
    }

    string GetStrOne(int act, string name, int action)
    {
        if ((action & act) > 0)
        {
            return name + "/";
        }
        else
        {
            return "";
        }
    }
    string GetHuStr(int action)
    {
        string str = "[";
        str += GetStrOne(MJUtils.HU_Hu, "胡牌", action);
        str += GetStrOne(MJUtils.HU_Shu, "输了", action);
        str += GetStrOne(MJUtils.HU_LiuJu, "流局", action);
        str += GetStrOne(MJUtils.HU_Pao, "点炮", action);
        if (str.Length > 1)
        {
            str = str.Substring(0, str.Length - 1);
        }
        str += "]";
        return str;
    }

    void OnGameOperPlayerHuSyn(GameOperPlayerHuSyn data)
    {
        string huInfo = GetHuStr(data.resultType);
        string winInfo = GetHuStr(data.winType);
        Debug.LogFormat("===HuSyn:{0}[炮：{1}] card:{2} bao:[{3}] Hu:{4}/Win:{5}", (data.position < 0) ? "流局" : strs[data.position], (data.paoPosition < 0) ? "无" : strs[data.paoPosition], GetCardStr(data.card), GetCardStr(data.bao), huInfo, winInfo);

        RoomMgr.huSyn = data;

        if (RoomMgr.huSyn.resultType == MJUtils.HU_LiuJu)
        {
            Game.SoundManager.PlayFall();
            if (!data.skipHuSettle)
            {
                Game.Delay(3, () =>
                {
                    Game.SoundManager.PlaySettleSound();
                    Game.DialogMgr.PushDialog(UIDialog.SettleRoundDialog);
                });
            }
            return;
        }

        MJPlayer player = Game.MJMgr.GetPlayerByPosition(data.position);
        MjData pData = Game.MJMgr.MjData[data.position];
        bool isMy = player.index == 0;
        List<GameOperPlayerSettle> list = data.detail;
        foreach (GameOperPlayerSettle s in list)
        {
            Debug.LogFormat("===OnGameOperPlayerHuSyn:{0} fanNum:{1} handcard:[{2}]", strs[s.position], s.fanNum, ToStr(s.handcard));

            if (s.position == data.position)
            {
                player.handCardLayout.Refresh(s.handcard);
            }
        }

        bool zimo = (data.paoPosition == -1 || data.paoPosition == data.position);
        if (zimo)
        {
            PlayZimoHu(player, data);
        }
        else if ((RoomMgr.huSyn.winType & MJUtils.HU_ShouPao) != 0)
        {
            Game.SoundManager.PlayEffect(28);
            GameObject eff = Game.PoolManager.EffectPool.Spawn("shandian_EF");
            eff.transform.position = Game.MJMgr.LastDropCard.transform.position;
            Game.PoolManager.EffectPool.Despawn(eff, 5);           
            player.MJHand.PlayShouPao(data.card, isMy);
        }
        else
        {
            bool isPao = false;
            if (-1 != data.paoPosition)
            {
                Game.SoundManager.PlayEffect(28);
                MJPlayer paoPlayer = Game.MJMgr.GetPlayerByPosition(data.paoPosition);
                GameObject eff = Game.PoolManager.EffectPool.Spawn("shandian_EF");
                eff.transform.position = Game.MJMgr.LastDropCard.transform.position;
                Game.PoolManager.EffectPool.Despawn(eff, 5);

                isPao = paoPlayer.index == 0;
            }

            Game.Delay(1, () =>
            {
                bool isWin = player.index == 0;
                if (isWin)
                {
                    Game.SoundManager.PlayWin();
                }
                else if (isPao)
                {
                    Game.SoundManager.PlayLose();
                }

                PlayHu(player, data);
            });
        }
    }

    void PlayZimoHu(MJPlayer player, GameOperPlayerHuSyn data)
    {
        Game.MaterialManager.TurnOnHandCard();
        player.handCardLayout.PlayHu();

        Game.SoundManager.PlayWin();
        Vector3 pos = player.DragCard(data.card, true);

        if (!PlayHuEffect(data.position))
        {
            Game.SoundManager.PlayEffect(27);
            Game.SoundManager.PlayZimo(data.position);

            GameObject eff = Game.PoolManager.EffectPool.Spawn("zimou _EF");
            eff.transform.position = pos;
            Game.PoolManager.EffectPool.Despawn(eff, 5);
        }

        if (!data.skipHuSettle)
        {
            Game.Delay(3, () =>
            {
                Game.SoundManager.PlaySettleSound();
                Game.DialogMgr.PushDialog(UIDialog.SettleRoundDialog);
            });
        }
    }

    void PlayHu(MJPlayer player, GameOperPlayerHuSyn data)
    {
        Vector3 pos = player.handCardLayout.DragCard(data.card, Game.MJMgr.LastDropCard.gameObject);

        Game.MaterialManager.TurnOnHandCard();
        player.handCardLayout.PlayHu();
        Game.SoundManager.PlayHu(data.position);

        if (!PlayHuEffect(data.position))
        {
            EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, data.position, "huUI_EF");
            GameObject eff = Game.PoolManager.EffectPool.Spawn("hu_EF");
            eff.transform.position = pos;
            Game.PoolManager.EffectPool.Despawn(eff, 3);
        }

        if (!data.skipHuSettle)
        {
            Game.Delay(3, () =>
            {
                Game.SoundManager.PlaySettleSound();
                Game.DialogMgr.PushDialog(UIDialog.SettleRoundDialog);
            });
        }
    }

    bool PlayHuEffect(int position)
    {
        string HuEffectUI = "";
        string HuEffectScene = "";
        switch (RoomMgr.huSyn.winType)
        {
            case MJUtils.HU_BaoZhongBao:
                {
                    Game.SoundManager.PlayEffect(21);
                    HuEffectUI = "bzbUI_EF";
                    HuEffectScene = "baozhongbao_EF";
                    break;
                }
            case MJUtils.HU_DaiLou:
                {
                    Game.SoundManager.PlayEffect(23);
                    HuEffectUI = "louUI_EF";
                    HuEffectScene = "lou_EF";
                    break;
                }
            case MJUtils.HU_GuaDaFeng:
                {
                    Game.SoundManager.PlayEffect(29);
                    HuEffectUI = "ljfUI_EF";
                    HuEffectScene = "longjuanfeng_EF";
                    break;
                }
            case MJUtils.HU_HongZhong:
                {
                    Game.SoundManager.PlayEffect(25);
                    HuEffectUI = "hzmtfUI_EF";
                    HuEffectScene = "hzmtf_EF";
                    break;
                }
            //case MJUtils.HU_Hu:
            //    {
            //        Game.SoundManager.PlayEffect(26);
            //        HuEffectUI = "huUI_EF";
            //        HuEffectScene = "hu_EF";
            //        break;
            //    }
            case MJUtils.HU_KaiPaiZha:
                {
                    Game.SoundManager.PlayEffect(24);
                    HuEffectUI = "zhaUI_EF";
                    HuEffectScene = "zhadang_EF";
                    break;
                }
            case MJUtils.HU_MoBao:
                {
                    Game.SoundManager.PlayEffect(22);
                    HuEffectUI = "mbhUI_EF";
                    HuEffectScene = "mobaohu_EF";
                    break;
                }
            default:
                {
                    return false;
                }
        }

        EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, position, HuEffectUI);
        MJPlayer player = Game.MJMgr.GetPlayerByPosition(position);
        GameObject eff = Game.PoolManager.EffectPool.Spawn(HuEffectScene);
        eff.transform.position = player.EffectPos.position;
        Game.PoolManager.EffectPool.Despawn(eff, 5);
        return true;
    }

    private void OnGameOperFinalSettleSyn(GameOperFinalSettleSyn gameOperFinalSettleSyn)
    {
        RoomMgr.finalSettleSyn = gameOperFinalSettleSyn;
        Game.DialogMgr.PushDialog(UIDialog.SettleDialog);
    }

    private void OnKickOutSyn(PacketBase msg)
    {
        Game.Instance.state = GameState.Hall;
        Game.UIMgr.PushScene(UIPage.MainPage);
    }
}
