using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MJEntity : MonoBehaviour {   
    public bool isCardUp = false;//控制OnMouseDown
    private static int id = 0;
    private int cardId;
    public int CardId
    {
        get
        {
            return cardId;
        }
    }

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
    PlayPage playPage;
    private List<MJEntity> throwlist =new List<MJEntity>();
    private List<int> throwcardPointlist = new List<int>();

    void OnMouseDown()
    {
        playPage = FindObjectOfType<PlayPage>();
        throwlist = GameObject.Find("Player0").GetComponentInChildren<HandCardLayout>().list;
        throwcardPointlist= GameObject.Find("Player0").GetComponentInChildren<HandCardLayout>().HandCards;

        if (Game.MJMgr.HangUp)
        {
            return;
        }

        if (!(IsMine() && IsHandCard()))
        {
            return;
        }
        print("  Game.MJMgr.isShuaiJiuYao>>>>>>>>>>>>>>>>> " + Game.MJMgr.isShuaiJiuYao);
        print("  Game.MJMgr.isShuaiJiuYao<<<<<<<<<<<<<<<<<< " + MJUtils.DropCard());
        if (Game.MJMgr.isShuaiJiuYao)
        {
            if(!isCardUp)
            {
                return;
            }
            if (playPage.CardList.Count > 2) 
            {
                playPage.throwCount = 1;
                playPage.throwCardList.Add(this.Card);              
                Destroy(this.gameObject, 0.1f);
                throwlist.Remove(this);
                throwcardPointlist.Remove(this.Card);
                playPage.CalThrowZongShu();
            }
        }  
        else if (MJUtils.DropCard())
        {
            playPage.throwCount = 0;
            OnClickDrop();
        }
    }

    public bool IsMine()
    {
        MJPlayer player = this.GetComponentInParent<MJPlayer>();
        if (null != player && player.index == 0)
        {
            return true;
        }
        return false;
    }

    public bool IsHandCard()
    {
        HandCardLayout layout = this.GetComponentInParent<HandCardLayout>();
        if (null != layout)
        {
            return true;
        }
        return false;
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
