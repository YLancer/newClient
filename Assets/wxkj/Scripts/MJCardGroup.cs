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

    public static int activeGroupIndex = -1;
    public static int firstGroupIndex = -1;

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
    //由第一牌组得知最后一个牌组   确定下来！！！
    public MJCardGroup LastGroup
    {
        get
        {
            int lastIndex = firstGroupIndex;
            //int lastIndex = firstGroupIndex - 3;
            //if (lastIndex == -3)
            //{
            //    lastIndex = 3;
            //}
            //else
            //{
            //    lastIndex += 2;
            //}
            return Game.MJMgr.cardGroups[lastIndex];
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

    public static void TryDragCard(bool countdown = true)
    {
        MJCardGroup group = Game.MJMgr.ActiveGroup;
        print(" Gang Gang Gang Gang" + MJUtils.ACT_AN_GANG + MJUtils.ACT_BU_GANG + MJUtils.ACT_ZHI_GANG);
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
        if (IsFirstGroup)
        {
            int zhuangNum = Game.MJTable.DiceNum * 2;
            if (list.Count <= zhuangNum)
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
        int gangCount =  FindObjectOfType<PlayPage>().allGangCount;
        int index=0;
        if (LastGroup.list.Count>=0 && LastGroup.list.Count - gangCount >= 0)
        {
            for (int i = 1; i <= gangCount; i++)
            {
                if (i % 2 == 0)
                {
                    index = i - 1;
                }
                else
                {
                    index = i + 1;
                }
            }
            LastGroup.SetActive();
            //PrevGroup.doGangDragCard(countdown);
            Transform card = LastGroup.list[LastGroup.list.Count - index];
            Game.PoolManager.CardPool.Despawn(card.gameObject);
            LastGroup.list.RemoveAt(LastGroup.list.Count - index);
        }
        else
        {
            LastGroup.PrevGroup.SetActive();
            //PrevGroup.PrevGroup.doGangDragCard(countdown);
            for (int j = 1;j <= gangCount- LastGroup.list.Count; j++)
            {
                if (j % 2 == 0)
                {
                    index = j - 1;
                }
                else
                {
                    index = j + 1;
                }
            }
            Transform card = LastGroup.PrevGroup.list[LastGroup.PrevGroup.list.Count - index];
            Game.PoolManager.CardPool.Despawn(card.gameObject);
            LastGroup.PrevGroup.list.RemoveAt(LastGroup.PrevGroup.list.Count - index);
        }    
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
    private  void  DOShowHuiPai(int cardPoint)  
    {
        print(" >>>>>>>>>>>>> 展示会牌 <<<<<<<<<<<<<<<" + Game.PoolManager.CardPool.Spawn(cardPoint.ToString()));
        if (MJUtils.MODE_DAIHUI !=0)   
        {
            if (LastGroup.list.Count >= 0)
            {
                Transform cardHui = LastGroup.list[LastGroup.list.Count - 2];
                cardHui.GetComponent<MeshFilter>().mesh = Game.PoolManager.CardPool.Spawn(cardPoint.ToString()).GetComponent<MeshFilter>().mesh;
                //cardHui.transform.Rotate(new Vector3(-180, 0, 0));
                cardHui.transform.rotation = Quaternion.Euler(new Vector3(-180, 0, 0));
            }
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
