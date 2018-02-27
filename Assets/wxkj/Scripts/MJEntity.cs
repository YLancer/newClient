using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class MJEntity : MonoBehaviour {
    public bool isCardUp = false;//控制OnMouseDown启用特殊功能
    private bool isSelect = false;
    public bool Select { get { return isSelect; } }
    private bool isEnable = true;
    private static int id = 0;
    private int cardId;
    public int CardId { get { return cardId; } }

    public int Card
    {
        get
        {
            int card = cardId / 100000;
            return card;
        }

        set
        {
            id++;
            cardId = value * 100000 + id;
        }
    }

    public bool IsMine()
    {
        MJPlayer player = this.GetComponentInParent<MJPlayer>();
        return (null != player && player.index == 0);
    }

    public bool IsHandCard()
    {
        HandCardLayout layout = this.GetComponentInParent<HandCardLayout>();
        return (null != layout);
    }
    //  麻将上下可选择  TODO
    //public delegate void EventHandler(MJEntity card);
    //public event EventHandler reSetPoisiton;
    //public event EventHandler onSendMessage;
    //public EventHandler tingLiangSendMessage;//TODO 因为不知道如何备份清空onSendMessage，将onSendMessage垄断成我的专属方法，所以用个新事件来独断这个event。

    void OnMouseDown()                                //按下
    {
        if (Game.MJMgr.HangUp)
        {
            return;
        }
        if (!(IsMine() && IsHandCard()))
        {
            return;
        }
        if (!isEnable)
        {
            return;
        }

        if (isCardUp)
        {
            SetSelect(!isSelect);
            return;
        }

        if (isSelect == false)
        {
            foreach (MJEntity me in Game.MJMgr.MyPlayer.handCardLayout.list)
            {
                if (me.isSelect)
                {
                    me.SetSelect(false);
                }
            }
            //if (reSetPoisiton != null)
            //{
            //    reSetPoisiton(this.GetComponent<MJEntity>());
            //isSelect = true;
            SetSelect(true);
            //}
        }
        else
        {
            if (MJUtils.DropCard())
            {
                Debug.Log("  drop card----------------  " + Card);
                //if(tingLiangSendMessage != null && MJUtils.TingLiang())
                //{
                //    tingLiangSendMessage(this.GetComponent<MJEntity>());
                //} else if (onSendMessage != null)     //发送消息
                //{
                //    onSendMessage(this.GetComponent<MJEntity>());
                //} else {
                    OnClickDrop();
                //}
            }
            if(MJUtils.TingLiang())
            {
                Debug.Log("  tingliang card----------------  " + Card);
                Game.SoundManager.PlayClick();
                //Game.MJMgr.MyPlayer.TingLiang(Card);
                Game.SocketGame.DoTingLiang(Card);
                EventDispatcher.DispatchEvent(MessageCommand.TingLiangEnd);
            }
            isSelect = false;
            //SetSelect(false);

        }
    }

    public void SetSelect(bool isSelect)
    {
        this.isSelect = isSelect;
        if (isSelect)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.015f);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        }
    }

    public void SetEnable(bool enable)
    {
        this.isEnable = enable;

        MaterialManager cardMaterial = GameObject.FindObjectOfType<MaterialManager>();
        if(enable)
        {
            GetComponent<Renderer>().material = cardMaterial.myCardMatOn;
        }
        else
        {
            GetComponent<Renderer>().material = cardMaterial.myCardMatOff;
        }
    }

    public void OnClickDrop()
    {
        if (RoomMgr.actionNotify.tingList.Count <= 0 || RoomMgr.IsTingDropCard(Card))
        {
            Game.MJMgr.MyDropMJEntity = this;

            //Game.MJMgr.MyPlayer.DropCard(Card);
            Game.SocketGame.DoDropCard(Card);
            Game.MaterialManager.TurnOnHandCard();

            EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
        }
    }
}
