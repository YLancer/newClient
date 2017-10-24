using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class HandCardLayout : MonoBehaviour {
    public Animator anim;
    [SerializeField]
    public List<MJEntity> list = new List<MJEntity>();
    public List<int> HandCards = new List<int>();

    public float width = 7.4f;
    private MJEntity last;

    public void PlayHu()
    {
        anim.SetTrigger("ToHuPos");
    }

    public void PlayDefault()
    {
        anim.SetTrigger("ToPlayPos");
    }

    public void Clear()
    {
        while (list.Count > 0)
        {
            MJEntity trans = list[0];
            list.RemoveAt(0);
            Game.PoolManager.CardPool.Despawn(trans.gameObject);
        }

        HandCards.Clear();
    }

    public void AddCard(int card,bool withTween = false)
    {
        GameObject child = null;
        if (-1== card)
        {
            child = Game.PoolManager.CardPool.Spawn("Dragon_Blank");
        }
        else
        {
            child = Game.PoolManager.CardPool.Spawn(card.ToString());
        }

        if(null == child)
        {
            Debug.LogWarningFormat("没有找到牌模型 card：{0}",card);
        }

        child.transform.SetParent(this.transform);
        child.transform.localScale = Vector3.one;
        child.transform.localRotation = Quaternion.identity;

        Transform newPos = FindRightCard();
        Vector3 pos = Vector3.zero;
        if(null != newPos)
        {
            pos = newPos.localPosition;
        }
        //child.transform.localPosition = pos + Vector3.right * width;
        child.transform.localPosition = Vector3.zero;

        MJEntity entity = child.GetComponent<MJEntity>();
        entity.Card = card;
        list.Add(entity);
        HandCards.Add(card);

        LineUpNoAnimation();

        if (withTween)
        {
            entity.transform.DOLocalRotate(new Vector3(90, 0, 0), 0.3f).From();
        }
    }

    public Vector3 DragCard(int card, GameObject child)
    {
        child.transform.SetParent(this.transform);
        child.transform.localScale = Vector3.one;
        child.transform.localRotation = Quaternion.identity;
        Transform newPos = FindRightCard();
        Vector3 pos = Vector3.zero;
        if (null != newPos)
        {
            pos = newPos.localPosition;
        }
        child.transform.localPosition = pos + Vector3.right * width * 1.2f;

        last = child.GetComponent<MJEntity>();
        last.Card = card;
        list.Add(last);

        HandCards.Add(card);

        MJCardGroup.TryDragCard(true);
        return child.transform.position;
    }

    private void doSort()
    {
        list.Sort(delegate (MJEntity a, MJEntity b) {
            return a.CardId.CompareTo(b.CardId);
        });

        HandCards.Sort(delegate (int a, int b)
        {
            return a.CompareTo(b);
        });
    }

    //public void Sort()
    //{
    //    doSort();

    //    LineUpNoAnimation();
    //}

    public void DropCard(int card)
    {
        for(int i=list.Count - 1; i >= 0; i--)
        {
            MJEntity trans = list[i];
            if (card == trans.Card)
            {
                list.Remove(trans);

                Game.PoolManager.CardPool.Despawn(trans.gameObject);
                HandCards.RemoveAt(i);
                return;
            }
        }
        //LineUp();
    }

    public void DropCard()
    {
        MJEntity entity = Game.MJMgr.MyDropMJEntity;
        if (!list.Contains(entity))
        {
            Debug.LogWarning("DropCard error not Contains");
            return;
        }
        
        list.Remove(entity);

        Game.PoolManager.CardPool.Despawn(entity.gameObject);
        HandCards.Remove(entity.Card);
        //LineUp();
    }

    public void RemoveCard(int card)
    {
        MJEntity ent = Find(card);
        if(null != ent)
        {
            list.Remove(ent);
            Game.PoolManager.CardPool.Despawn(ent.gameObject);
            HandCards.Remove(card);
        }
    }

    public void RemoveCardAt(int index)
    {
        if(index<0|| index >= list.Count)
        {
            Debug.LogError("RemoveCardAt index:" + index);
        }
        MJEntity trans = list[index];
        list.Remove(trans);
        Game.PoolManager.CardPool.Despawn(trans.gameObject);
        HandCards.RemoveAt(index);
    }

    public void LineUp(bool isMy = true) {
        doSort();

        for (int i = 0; i < list.Count; i++) {
            MJEntity trans = list[i];

            //trans.localPosition = Vector3.right * width * (i-count * 0.5f);
            Vector3 toPos = Vector3.right * width * (i - list.Count * 0.5f);

            if (trans == last && list.IndexOf(trans) != list.Count -1)
            {
                Vector3 lcPos = trans.transform.localPosition;
                Vector3 upPos = lcPos + Vector3.forward * 0.0405f;
                Vector3 leftUpPos = toPos + Vector3.forward * 0.0405f;

                Vector3[] path = new Vector3[] { lcPos , upPos , leftUpPos ,toPos};
                //trans.trans.DOLocalPath(path,1);
                trans.transform.DOLocalMove(upPos, 0.3f).OnComplete(()=> {
                    trans.transform.DOLocalMove(leftUpPos, 0.3f).OnComplete(()=> {
                        trans.transform.DOLocalMove(toPos, 0.5f);
                    });
                });
            }
            else
            {
                //trans.transform.DOLocalMove(toPos, 0.3f).SetDelay(0.5f);
                trans.transform.DOLocalMoveX(toPos.x, 0.3f).SetDelay(0.5f);
            }
        }

        last = null;
    }

    [ContextMenu("LineUp")]
    public void LineUpNoAnimation()
    {
        for (int i = 0; i < list.Count; i++)
        {
            MJEntity trans = list[i];
            Vector3 pos = trans.transform.localPosition;
            pos.x = width * (i - list.Count * 0.5f);
            trans.transform.localPosition = pos;
            //trans.transform.localPosition = Vector3.right * width * (i - list.Count * 0.5f);
            //Vector3 toPos = Vector3.right * width * (i - list.Count * 0.5f);
            //trans.trans.DOLocalMove(toPos, 0.3f);
        }
    }

    private Transform FindRightCard()
    {
        if (list.Count <= 0)
        {
            return null;
        }
        MJEntity trans = list[list.Count -1];
        return trans.transform;
    }

    public void Refresh(List<int> handCards)
    {
        Clear();

        foreach(int card in handCards)
        {
            AddCard(card);
        }
    }

    public MJEntity Find(int card)
    {
        foreach (MJEntity trans in list)
        {
            if (trans.Card == card)
            {
                return trans;
            }
        }

        Debug.LogErrorFormat("Can't Find MJEntity card:{0}", card);
        return null;
    }

    public int FindIndex(int card)
    {
        for(int i=0;i<list.Count;i++)
        {
            MJEntity trans = list[i];
            if (trans.Card == card)
            {
                return i;
            }
        }

        Debug.LogErrorFormat("Can't Find MJEntity card:{0}", card);
        return -1;
    }

    public void PlayZhidui(int card,bool isMy)
    {
        if (isMy)
        {
            int index = FindIndex(card);
            Game.MaterialManager.TurnOffAllHandCard();
            Game.MaterialManager.TurnOnCard(list[index].transform);
            Game.MaterialManager.TurnOnCard(list[index + 1].transform);
        }
        else
        {
            list[0].transform.localRotation = Quaternion.Euler(90, 0, 0);
            list[1].transform.localRotation = Quaternion.Euler(90, 0, 0);

            list[0].transform.localPosition += new Vector3(0, 0, -0.009f);
            list[1].transform.localPosition += new Vector3(0, 0, -0.009f);
        }
    }
}
