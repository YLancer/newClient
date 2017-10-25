﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using packet.mj;
using System;

public class MJPlayer : MonoBehaviour {
    public HandCardLayout handCardLayout;
	public DropCardLayout dropCardLayout;
	public TableCardLayout tableCardLayout;
    public Transform baoRoot;
    public Transform EffectPos;
    public MJHand MJHand;

    public int postion;
	public int index;
	public int otherPlayerLastDropCard = -1;

	public void Clear()
	{
        handCardLayout.Clear();
        dropCardLayout.Clear();
        tableCardLayout.Clear();

        if (baoRoot.childCount > 0)
        {
            Transform old = baoRoot.GetChild(0);
            Game.PoolManager.CardPool.Despawn(old.gameObject);
        }
    }
    
	public MJPlayer NextPlayer
	{
		get
		{
			int nextIndex = index + 1;
			if (nextIndex == 4)
			{
				nextIndex = 0;
			}
			return Game.MJMgr.players[nextIndex];
		}
	}

	public MJPlayer PrevPlayer
	{
		get
		{
			int nextIndex = index - 1;
			if (nextIndex == -1)
			{
				nextIndex = 3;
			}
			return Game.MJMgr.players[nextIndex];
		}
	}

	public Vector3 DragCard(int card, bool isMy)
	{
        Game.SoundManager.PlayGetCard();

        GameObject child = null;
        if (!isMy)
        {
            child = Game.PoolManager.CardPool.Spawn("Dragon_Blank");
        }
        else
        {
            child = Game.PoolManager.CardPool.Spawn(card.ToString());
        }

        if (null == child)
        {
            Debug.LogWarningFormat("没有找到牌模型 card：{0}", card);
        }

        Vector3 pos = handCardLayout.DragCard(card, child);
        EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
        return pos;
    }

	public void Chi(GameOperChiArg chiArg)
	{
		int card = chiArg.targetCard;
		if(MJUtils.Chi() || MJUtils.TingChi())
        {
            Game.SocketGame.DoChi(chiArg.myCard1, chiArg.myCard2);
        }
	}

	public void Peng()
	{
        if (MJUtils.Peng()|| MJUtils.TingPeng())
        {
            int card = RoomMgr.actionNotify.pengArg;

            Game.SocketGame.DoPeng(card);
        }
    }

	public void Gang()
	{
	}

	public void Hu()
	{

	}

    internal void Zhidui(int card)
    {
        if (MJUtils.TingZhidui())
        {
            Game.SocketGame.DoZhidui(card);
        }
    }
}