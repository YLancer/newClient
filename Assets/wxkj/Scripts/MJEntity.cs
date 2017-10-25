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
    //牌位置上升
    public void SelectCardForJiuYao()
    {
        if (IsMine() && IsHandCard())
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.01f);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
