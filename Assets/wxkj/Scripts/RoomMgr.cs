using UnityEngine;
using System.Collections.Generic;
using packet.game;
using packet.mj;
using System;

public class RoomMgr {
    public static PlayerGamingSyn playerGamingSyn;
    public static GameOperStartSyn gameOperStartSyn;
    public static GameOperPlayerActionNotify actionNotify;
    public static GameOperPlayerHuSyn huSyn;
    public static GameOperFinalSettleSyn finalSettleSyn;

    public static bool IsSingeRoom()
    {
        if(playerGamingSyn == null)
        {
            Debug.LogWarning("playerGamingSyn == null");
            return true;
        }
        return (playerGamingSyn.matchId == MatchType.G_DQMJ_MATCH_SINGLE.ToString());
    }

    public static bool IsVipRoom()
    {
        if (playerGamingSyn == null)
        {
 //           Debug.LogWarning("playerGamingSyn == null");
            return false;
        }

        return playerGamingSyn.matchId == MatchType.G_DQMJ_MATCH_4VIP.ToString() || playerGamingSyn.matchId == MatchType.G_DQMJ_MATCH_2VIP.ToString();
    }

    public static bool IsNormalRoom()
    {
        if (IsVipRoom())
        {
            return false;
        }

        if (IsSingeRoom())
        {
            return false;
        }

        return true;
    }

    public static bool IsVip2Room()
    {
        if (playerGamingSyn == null)
        {
            return false;
        }

        return playerGamingSyn.matchId == MatchType.G_DQMJ_MATCH_2VIP.ToString();
    }

    public static void Reset()
    {
        playerGamingSyn = null;
        gameOperStartSyn = null;
        actionNotify = null;
        huSyn = null;
        finalSettleSyn = null;
}

    public static ConfigRooms GetRoomByMatchId(MatchType matchType)
    {
        foreach (ConfigRooms config in ConfigRooms.datas)
        {
            if (config.MatchType == matchType)
            {
                return config;
            }
        }
        return null;
    }

    // 是否是打了可以听的牌
    public static bool IsTingDropCard(int card)
    {
        if(null != actionNotify)
        {
            List<int> tingList = actionNotify.tingList;
            if(null != tingList && tingList.Contains(card))
            {
                return true;
            }
        }
        return false;
    }

    public static GameOperPlayerSettle FindHu()
    {
        if(null == huSyn)
        {
            return null;
        }

        foreach(GameOperPlayerSettle hu in huSyn.detail)
        {
            if(hu.position == huSyn.position)
            {
                return hu;
            }
        }

        return null;
    }

    internal static int GetTotalQuan()
    {
        if(null == playerGamingSyn)
        {
            return 0;
        }

        return playerGamingSyn.totalQuan;
    }

    internal static int GetQuanNum()
    {
        if (null == gameOperStartSyn)
        {
            return 0;
        }

        return gameOperStartSyn.quanNum;
    }
}
