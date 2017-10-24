using UnityEngine;
using System.Collections;

public class MJEntity : MonoBehaviour {
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


    void OnMouseDown()
    {
        if (!Game.MJMgr.HangUp)
        {
            if (MJUtils.DropCard())
            {
                if (IsMine() && IsHandCard())
                {
                    OnClickDrop();
                }
            }
        }
    }

    bool IsMine()
    {
        MJPlayer player = this.GetComponentInParent<MJPlayer>();
        if (null != player && player.index == 0)
        {
            return true;
        }
        return false;
    }

    bool IsHandCard()
    {
        HandCardLayout layout = this.GetComponentInParent<HandCardLayout>();
        if (null != layout)
        {
            return true;
        }
        return false;
    }

    void OnClickDrop()
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
