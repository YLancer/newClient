using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using packet.mj;

public class MjData
{
    public Player player;
    //public List<int> HandCards = new List<int>();
    //public List<int> TableCards = new List<int>();
    //public List<int> DropCards = new List<int>();
}
public class MJMgr : MonoBehaviour {
	public List<MJCardGroup> cardGroups = new List<MJCardGroup> ();
    private MjData[] mjData;
    public MjData[] MjData
    {
        get
        {
            if(null == mjData)
            {
                mjData = new MjData[4];
                mjData[0] = new global::MjData();
                mjData[1] = new global::MjData();
                mjData[2] = new global::MjData();
                mjData[3] = new global::MjData();
            }
            return mjData;
        }
    }

    //public List<int> All = new List<int>();
    public List<MJPlayer> players = new List<MJPlayer>();
    public int makersIndex = -1;
    private int makersPosition = -1;
    public int MakersPosition
    {
        get
        {
            return makersPosition;
        }
        set
        {
            makersPosition = value;
            makersIndex = GetIndexByPosition(makersPosition);
        }
    }

    public int ActivePosition;
    
    public int CardLeft = 112;   //136
    public int BaoDize = -1;
    public bool HangUp = false;

    public int[] position = new int[4];
    public int[] indexs = new int[4];

    // 玩家自己真实的位置
    public void IntPosition(int myPosition)
    {
        position[0] = -1;
        position[1] = -1;
        position[2] = -1;
        position[3] = -1;

        indexs[0] = -1;
        indexs[1] = -1;
        indexs[2] = -1;
        indexs[3] = -1;

        if (RoomMgr.IsVip2Room())
        {
            if (myPosition == 0)
            {
                position[0] = 0;
                position[2] = 1;
                indexs[0] = 0;
                indexs[1] = 2;
            }
            else
            {
                position[0] = 1;
                position[2] = 0;
                indexs[1] = 0;
                indexs[0] = 2;
            }
        }
        else
        {
            position[0] = myPosition;
            indexs[myPosition] = 0;

            int next = NextPosition(myPosition);
            position[1] = next;
            indexs[next] = 1;

            next = NextPosition(next);
            position[2] = next;
            indexs[next] = 2;

            next = NextPosition(next);
            position[3] = next;
            indexs[next] = 3;
        }
    }

    public int GetPositionByIndex(int index)
    {
        return position[index];
    }

    public int GetIndexByPosition(int pos)
    {
        return indexs[pos];
    }

    int NextPosition(int position)
    {
        position++;
        if (position > 3)
        {
            return 0;
        }else
        {
            return position;
        }
    }

    public Transform targetFlag;

	[ContextMenu("清理")]
	public void Clear(){
		//All.Clear();
        CardLeft = 0;

        cardGroups[0].Clear();
        cardGroups[1].Clear();
        cardGroups[2].Clear();
        cardGroups[3].Clear();

		players [0].Clear ();
		players [1].Clear ();
		players [2].Clear ();
		players [3].Clear ();

        players[0].MJHand.Clear();
        players[1].MJHand.Clear();
        players[2].MJHand.Clear();
        players[3].MJHand.Clear();

        players[0].MJHand.HideBaoEffect();
        players[1].MJHand.HideBaoEffect();
        players[2].MJHand.HideBaoEffect();
        players[3].MJHand.HideBaoEffect();

        players[0].MJHand.IsBusy = false;
        players[1].MJHand.IsBusy = false;
        players[2].MJHand.IsBusy = false;
        players[3].MJHand.IsBusy = false;

        BaoDize = -1;
    }

 //   [ContextMenu("初始化")]
	//public void Init()
	//{
 //       CardLeft = 112;

 //       players[0].index = 0;
	//	players[1].index = 1;
	//	players[2].index = 2;
	//	players[3].index = 3;

 //       //MakersIndex = -1;
 //       //RandomMakers();
 //   }

	[ContextMenu("洗牌")]
	public void Shuffle()
	{
        int index = 0;
        for (int i = 0; i < CardLeft; i = i + 2)
        {
            cardGroups[index].AddCard();
            cardGroups[index].AddCard();

            index++;
            if (index >= 4) index = 0;
        }
    }

    public void Licensing(List<GameOperHandCardSyn> handCards)
    {
        StartCoroutine(doLicensing(handCards));
    }

    IEnumerator doLicensing(List<GameOperHandCardSyn> handCards)
    {
        float waitTime = 0.3f;

        foreach (GameOperHandCardSyn hc in handCards)
        {
            MJPlayer player = Game.MJMgr.GetPlayerByPosition(hc.position);
            player.handCardLayout.PlayDefault();
        }

        yield return new WaitForEndOfFrame();
        foreach (GameOperHandCardSyn hc in handCards)
        {
            MJPlayer player = Game.MJMgr.GetPlayerByPosition(hc.position);

            for (int i = 0; i < 6; i++)
            {
                int sCard = hc.handCards[i];
                player.handCardLayout.AddCard(sCard, true);
                MJCardGroup.TryDragCard();
            }

            Game.SoundManager.PlayGet4Card();
        }

        yield return new WaitForSeconds(waitTime);
        foreach (GameOperHandCardSyn hc in handCards)
        {
            MJPlayer player = Game.MJMgr.GetPlayerByPosition(hc.position);

            for (int i = 6; i < 12; i++)
            {
                int sCard = hc.handCards[i];
                player.handCardLayout.AddCard(sCard, true);
                MJCardGroup.TryDragCard();
            }

            Game.SoundManager.PlayGet4Card();
        }

        yield return new WaitForSeconds(waitTime);
        foreach (GameOperHandCardSyn hc in handCards)
        {
            MJPlayer player = Game.MJMgr.GetPlayerByPosition(hc.position);
            int sCard = hc.handCards[12];
            player.handCardLayout.AddCard(sCard, true);

            MJCardGroup.TryDragCard();
        }
        //TODO WXD Add ShuaiJiuYao
    }

	public MJEntity MyDropMJEntity = null;
    //public MJPlayer LastDropCardPlayer;
    public MJEntity LastDropCard;

	/// <summary>
	/// 随机庄家
	/// </summary>
	//public void RandomMakers()
	//{
	//	makersIndex = Random.Range(0, 3);
	//	activeIndex = makersIndex;
	//}

	public MJPlayer Makers
	{
		get
		{
			return players[makersIndex];
		}
	}

    public MJPlayer MyPlayer
    {
        get
        {
            return players[0];
        }
    }

    public MJCardGroup ActiveGroup {
		get {
			return cardGroups [MJCardGroup.activeGroupIndex];
		}
	}

    public Player GetPlayerById(int playerId)
    {
        foreach(MjData data in mjData)
        {
            Player player = data.player;
            if (null != player && player.playerId == playerId)
            {
                return player;
            }
        }
        return null;
    }

    public MJPlayer GetPlayerByPosition(int position)
    {
        int index = Game.MJMgr.GetIndexByPosition(position);
        MJPlayer player = Game.MJMgr.players[index];
        return player;
    }
}