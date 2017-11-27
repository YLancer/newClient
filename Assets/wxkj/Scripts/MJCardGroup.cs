using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class MJCardGroup : MonoBehaviour {
    public float width = 7.5f;
    public float height = -5.06f;
    public int row = 2;
    public int col = 18;
    private int gangIndex = 1;

    private int headIndex = 0;

    public static int activeGroupIndex = -1;
    public static int firstGroupIndex = -1;
    private static bool firstDrag = false;//首次摸牌的标记，用firstGroupIndex判断会有循环回来到第一组的时候判断出错。

    public int index;
    int LastGroupindex;
    public List<Transform> list = new List<Transform>();

    public static void GetStartGroup()
    {
        int startGroup = 0;
        int DiceNum = Game.MJTable.DiceNum;
        switch (DiceNum)
        {
            case 1:
            case 3:
            case 5:
                {
                    startGroup = Game.MJMgr.Makers.NextPlayer.NextPlayer.index;
                    break;
                }
            case 2:
            case 6:
                {
                    startGroup = Game.MJMgr.Makers.NextPlayer.index;
                    break;
                }
            case 4:
                {
                    startGroup = Game.MJMgr.Makers.PrevPlayer.index;
                    break;
                }
        }

        firstGroupIndex = startGroup;
        activeGroupIndex = startGroup;
        firstDrag = true;
    }

    public bool IsFirstGroup
	{
		get
		{
			return index == firstGroupIndex;
		}
	}

    public void SetActive()
	{
		activeGroupIndex = index;
	}

	public MJCardGroup NextGroup
	{
		get
		{
			int nextIndex = index - 1;
			if (nextIndex == -1)
			{
				nextIndex = 3;
			}
			return Game.MJMgr.cardGroups[nextIndex];
		}
	}

	public MJCardGroup PrevGroup
	{
		get
		{
			int nextIndex = index + 1;
			if (nextIndex == 4)
			{
				nextIndex = 0;
			}
			return Game.MJMgr.cardGroups[nextIndex];
		}
	}

    public void Clear()
    {
        while (this.transform.childCount > 0)
        {
            Transform child = this.transform.GetChild(0);
            Game.PoolManager.CardPool.Despawn(child.gameObject);
        }

        list.Clear();
    }

    private Vector3 GetLocalPos(int index)
    {
        int c = index / row;
        int r = index % row;
        Vector3 pos = Vector3.right * width * c + Vector3.up * height * r;
        return pos;
    }

    public int count;
    public GameObject mj;
    [ContextMenu("LineUp")]
    public void LineUp()
    {
        PrefabUtils.ClearChild(this.transform);
        for(int i = 0; i < count; i++)
        {
            Vector3 toPos = GetLocalPos(this.transform.childCount);
            GameObject child = PrefabUtils.AddChild(this.transform.gameObject, mj);
            
            child.transform.localScale = Vector3.one;
            child.transform.localRotation = Quaternion.identity;
            child.transform.localPosition = toPos;
        }
    }
    public void AddCard()
    {
        Vector3 toPos = GetLocalPos(this.transform.childCount);
        GameObject child = Game.PoolManager.CardPool.Spawn("Dragon_Blank");
        child.transform.SetParent(this.transform);
        child.transform.localScale = Vector3.one;
        child.transform.localRotation = Quaternion.identity;
        child.transform.localPosition = toPos;

        list.Add(child.transform);
    }

    public static void TryDragCard(bool countdown = true) //countdown表示要不要减剩余牌数。
    {
        MJCardGroup group = Game.MJMgr.ActiveGroup;
        if (Game.Instance.Gang == true)
        {
            group.doGangDragCard(countdown);
        }
        else
        {
            group.doTryDragCard(countdown);
        }
    }

    private void doTryDragCard(bool countdown)
    {
        print("  trt drag card  loop  " + countdown + " - " + Game.MJTable.DiceNum + " = " + list.Count + " | " + IsFirstGroup + " - " + firstGroupIndex + " ] ");
        if (firstDrag && IsFirstGroup) //从第一组顺序为diceNum * 2的牌开始摸
        {
            int zhuangNum = Game.MJTable.DiceNum * 2;
            if (list.Count <= zhuangNum) //因为是从zhuangNum开始摸的，所以摸完的条件是list剩余牌数<=zhuangNum。
            {
                NextGroup.SetActive();
                NextGroup.doTryDragCard(countdown);
            }
            else
            {
                Transform card = list[zhuangNum];
                Game.PoolManager.CardPool.Despawn(card.gameObject);
                list.RemoveAt(zhuangNum);
                if (countdown)
                {
                    Game.MJMgr.CardLeft--;
                }
            }
        }
        else
        {
            firstDrag = false;//TODO WXD 时机不准确
            if (list.Count > 0)
            {
                Transform card = list[0];
                Game.PoolManager.CardPool.Despawn(card.gameObject);
                list.RemoveAt(0);
                if (countdown)
                {
                    Game.MJMgr.CardLeft--;
                }
            }
            else
            {
                NextGroup.SetActive();
                NextGroup.doTryDragCard(countdown);
            }
        }
    }
    
    //杠牌的摸牌方式
    private void doGangDragCard(bool countdown)
    {
        var lastGroup = Game.MJMgr.cardGroups[firstGroupIndex];
        for(int i = 0; i < 5; i++) //找到末尾的牌堆。
        {
            if(lastGroup.list.Count > 0)
            {
                break;
            }
            lastGroup = lastGroup.PrevGroup;
        }
        if(lastGroup.list.Count <= 0)//牌摸完了。
        {
            return;
        }

        int len = lastGroup.list.Count;
        int removeIdx = len - (2 - (len & 1));
        Transform card = lastGroup.list[removeIdx];
        Game.PoolManager.CardPool.Despawn(card.gameObject);
        lastGroup.list.RemoveAt(removeIdx);
        
        if (countdown)
        {
            Game.MJMgr.CardLeft--;
        }
        Game.Instance.Gang = false;
    }

    //显示会牌
    public static void ShowHuiCard(int cardPoint)
    {
        MJCardGroup group = Game.MJMgr.ActiveGroup;
        group.DOShowHuiPai(cardPoint);
    }
    private void DOShowHuiPai(int cardPoint)  
    {
        var lastGroup = Game.MJMgr.cardGroups[firstGroupIndex];
        Debug.Assert(lastGroup.list.Count >= 0);
        GameObject child= Game.PoolManager.CardPool.Spawn(cardPoint.ToString());
        child.transform.position = Vector3.zero;
        Transform cardHui = lastGroup.list[lastGroup.list.Count - 2];
        cardHui.GetComponent<MeshFilter>().mesh = child.GetComponent<MeshFilter>().mesh;
        if(System.Math.Abs(this.transform.localPosition.y)== 90)
        {
            cardHui.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }
        else
        {
            cardHui.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
    }

    internal static void DragBaoCard(int dice = -1)
    {
        MJCardGroup group = Game.MJMgr.ActiveGroup;
        
        //if (Game.MJMgr.BaoDize == -1)
        {
            // 第一次确定宝
            group.doDragBaoCard();
        }
        //else
        {
            // 换宝
            //doChangeBaoCard(dice);
        }
    }

    private void doDragBaoCard()
    {
        // 第一次确定宝
        if (list.Count == 0)
        {
            NextGroup.SetActive();
            NextGroup.doDragBaoCard();
        }
        else if (list.Count == 1)
        {
            NextGroup.doDragBaoCard();
        }
        else
        {
            int index = 0;
            if (list.Count % 2 != 0)
            {
                index = 1;
            }
            Transform card = list[index];
            Game.PoolManager.CardPool.Despawn(card.gameObject);
            list.RemoveAt(index);
            Game.MJMgr.CardLeft--;
        }
    }


    private void doChangeBaoCard(int dice)
    {
        // 换宝
        
    }
    //public bool DragGangCard()
    //{
    //    Game.MJMgr.CardLeft--;
    //    return true;

    //    if (list.Count <= 0)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        Transform card = list[gangIndex];
    //        Game.PoolManager.CardPool.Despawn(card.gameObject);
    //        list.RemoveAt(gangIndex);

    //        gangIndex = gangIndex == 1 ? 0 : 1;
    //        Game.MJMgr.CardLeft--;
    //        return true;
    //    }
    //}
}
