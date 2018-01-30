using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using System.Collections.Generic;

public enum AnimationAct
{
    Idle = 0,
    DragCard = 1,
    PutHandCard = 2,
    DropCard = 3,
    PutTable = 4,
    Hu = 5,
}
public class MJHand : MonoBehaviour
{
    public MJPlayer player;
    public GameObject hand;
    public Animation anim;
    public Transform handMJRoot;
    public HandAnima handAnima;
    public Transform dizePos;

    private GameObject baoEffect;
    public bool IsBusy = false;

    public void Clear()
    {
        while (handMJRoot.childCount > 0)
        {
            Transform trans = handMJRoot.GetChild(0);
            Game.PoolManager.CardPool.Despawn(trans.gameObject);
        }
    }

    private int position
    {
        get
        {
            return player.postion;
        }
    }

    public void PlayDropCard(int card, bool isMy)
    {
        IsBusy = true;
        MjData data = Game.MJMgr.MjData[position];

        Game.SoundManager.PlayCardSound(position, card);

        Transform dropCLTrans = player.dropCardLayout.transform;
        int childCount = dropCLTrans.childCount;

        if (isMy)
        {
            if (null == Game.MJMgr.MyDropMJEntity)
            {
                player.handCardLayout.DropCard(card);
            }
            else
            {
                player.handCardLayout.DropCard();
            }

            Game.MJMgr.MyDropMJEntity = null;
            //player.handCardLayout.Sort();
        }
        else
        {
            //int position = Game.MJMgr.GetPositionByIndex(player.index);

            //MjData data = Game.MJMgr.MjData[position];
            //if (data.player.ting)
            //{
            //    int Count = player.handCardLayout.HandCards.Count;
            //    player.handCardLayout.RemoveCardAt(Count);
            //}
            //else
            {
                int count = player.handCardLayout.HandCards.Count;
                if (data.player.ting)
                {
                    player.handCardLayout.RemoveCardAt(count - 1);
                }
                else
                {
                    int index = UnityEngine.Random.Range(2, count);
                    player.handCardLayout.RemoveCardAt(index);
                }
            }

            //player.HandCards.RemoveAt(index);
            // TODO  这里需要做细节
        }

        anim.gameObject.SetActive(true);

        Vector3 toPos = player.dropCardLayout.GetLocalPos(childCount);
        Vector3 endPos = dropCLTrans.TransformPoint(toPos);

        hand.transform.position = endPos;

        GameObject child = Game.PoolManager.CardPool.Spawn(card.ToString());
        Game.MJMgr.LastDropCard = child.GetComponent<MJEntity>();
        Game.MJMgr.LastDropCard.Card = card;

        Transform cardTrans = child.transform;
        cardTrans.SetParent(handMJRoot);
        cardTrans.localPosition = Vector3.zero;
        cardTrans.localRotation = Quaternion.identity;
        cardTrans.localScale = Vector3.one;

        if (UnityEngine.Random.Range(0f, 1f) < 0.8f)
        {
            anim.Play("Drop");
            handAnima.OnDropCallback = () =>
            {
                Game.SoundManager.PlayDropCard();
                Game.MJMgr.targetFlag.gameObject.SetActive(true);
                Game.MJMgr.targetFlag.position = hand.transform.position;

                player.dropCardLayout.AddCard(card, child);
                //hand.SetActive(false);
                player.handCardLayout.LineUp(isMy);
                IsBusy = false;
            };
            //Game.Delay(0.13f, () =>{
            //});
        }
        else
        {
            anim.Play("Drop1");
            handAnima.OnDropSoundCallback = () =>
            {
                Game.SoundManager.PlayDropCard();
            };

            handAnima.OnDropCallback = () =>
            {
                Game.MJMgr.targetFlag.gameObject.SetActive(true);
                Game.MJMgr.targetFlag.position = hand.transform.position;

                player.dropCardLayout.AddCard(card, child);
                //hand.SetActive(false);
                player.handCardLayout.LineUp(isMy);
                IsBusy = false;
            };
            //Game.Delay(0.25f, () => {

            //});
        }
    }

    public void PlayPeng(int card, bool isMy)
    {
        Game.SoundManager.PlayPeng(position);

        player.tableCardLayout.AddCard(card);
        player.tableCardLayout.AddCard(card);
        player.tableCardLayout.AddCard(card);

        if (isMy)
        {
            player.handCardLayout.RemoveCard(card);
            player.handCardLayout.RemoveCard(card);
        }
        else
        {
            int count = player.handCardLayout.HandCards.Count;
            int index = UnityEngine.Random.Range(0, count);
            int index2 = index - 1;
            if (index <= 0)
            {
                index2 = index + 1;
            }
            player.handCardLayout.RemoveCardAt(index);
            player.handCardLayout.RemoveCardAt(index2);
        }

        player.handCardLayout.LineUp();

        Game.MJMgr.targetFlag.gameObject.SetActive(false);
        Game.PoolManager.CardPool.Despawn(Game.MJMgr.LastDropCard.gameObject);
        //Game.MJMgr.LastDropCardPlayer.dropCardLayout.RemoveLast();

        EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, position, "pengUI_EF");

        Transform tableCLTrans = player.tableCardLayout.transform;
        int childCount = tableCLTrans.childCount;
        Transform lastChild = tableCLTrans.GetChild(childCount - 1);
        Vector3 endPos = tableCLTrans.TransformPoint(lastChild.localPosition);
        hand.transform.position = endPos;
        anim.gameObject.SetActive(true);

        GameObject eff = Game.PoolManager.EffectPool.Spawn("peng_EF");
        eff.transform.position = endPos;
        Game.PoolManager.EffectPool.Despawn(eff, 2);

        anim.Play("PutTable");
    }

    internal void PlayChi(int card0, int card1, bool isMy)
    {
        Game.SoundManager.PlayChi(position);

        int card2 = Game.MJMgr.LastDropCard.Card;

        if (isMy)
        {
            player.handCardLayout.RemoveCard(card0);
            player.handCardLayout.RemoveCard(card1);
        }
        else
        {
            int count = player.handCardLayout.HandCards.Count;
            int index = UnityEngine.Random.Range(0, count);
            int index2 = index - 1;
            if (index <= 0)
            {
                index2 = index + 1;
            }
            player.handCardLayout.RemoveCardAt(index);
            player.handCardLayout.RemoveCardAt(index2);
        }

        player.handCardLayout.LineUp();

        player.tableCardLayout.AddCard(card0);
        player.tableCardLayout.AddCard(card2);
        player.tableCardLayout.AddCard(card1);

        Game.MJMgr.targetFlag.gameObject.SetActive(false);
        Game.PoolManager.CardPool.Despawn(Game.MJMgr.LastDropCard.gameObject);
        //Game.MJMgr.LastDropCardPlayer.dropCardLayout.RemoveLast();
        EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, position, "chiUI_EF");

        Transform tableCLTrans = player.tableCardLayout.transform;
        int childCount = tableCLTrans.childCount;
        Transform lastChild = tableCLTrans.GetChild(childCount - 1);
        Vector3 endPos = tableCLTrans.TransformPoint(lastChild.localPosition);

        hand.transform.position = endPos;
        anim.gameObject.SetActive(true);

        GameObject eff = Game.PoolManager.EffectPool.Spawn("peng_EF");
        eff.transform.position = endPos;
        Game.PoolManager.EffectPool.Despawn(eff, 2);

        anim.Play("PutTable");
    }

    internal void PlayTing(bool isMy)
    {
        Game.SoundManager.PlayTing(position);
        Game.SoundManager.PlayTingSound();
        Game.MJMgr.MjData[position].player.ting = true;
        EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
        EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, position, "tingUI_EF");
    }

    internal void PlayTingLiang(int card, bool isMy) //tingliang #3 收到亮牌同步协议，展示亮的牌。
    {
        print("   show  ting  liang  " + card);
        
        player.liangCardLayout.AddCard(card);
    }

    /// <summary>
    /// 杠牌桌面上卡牌的动作和播放动画  TODO
    /// </summary>
    /// <param name="cardG">常规自己抓四张牌杠cardG</param>
    /// <param name="isMy">是不是我杠了</param>
    /// <param name="type">杠类型 1-暗杠 2-补杠 3-直杠</param>
    internal void PlayGang(int cardG, bool isMy, int type)
    {
        Game.SoundManager.PlayGang(position);

        if ( type==1 )
        {
            player.tableCardLayout.AddCard(cardG, true);
            player.tableCardLayout.AddCard(cardG, true);
            player.tableCardLayout.AddCard(cardG, true);
            player.tableCardLayout.AddCard(cardG, true);

            if (!isMy)
            {
                int count = player.handCardLayout.HandCards.Count;
                int index = UnityEngine.Random.Range(0, count);
                if (index < 3)
                {
                    index = 3;
                }
                player.handCardLayout.RemoveCardAt(index);
                player.handCardLayout.RemoveCardAt(index - 1);
                player.handCardLayout.RemoveCardAt(index - 2);
                player.handCardLayout.RemoveCardAt(index - 3);
            }
            else
            {
                player.handCardLayout.RemoveCard(cardG);
                player.handCardLayout.RemoveCard(cardG);
                player.handCardLayout.RemoveCard(cardG);
                player.handCardLayout.RemoveCard(cardG);
            }
        }
        else if(type == 2)
        {
            //检验手中有没有card牌
            bool isHandCard = false;
            List<int> pengGangCard  = player.handCardLayout.HandCards;
            for (int i = 0; i < pengGangCard.Count; i++)
            {
                if (pengGangCard[i] == cardG)
                {
                    isHandCard = true;
                    break;
                }
            }
            //if(!isHandCard)
            //{
            //    return;
            //}

            //检验门前牌中有没有card碰牌
            //int cnt = 0;
            List<int> pengCard = player.tableCardLayout.TableCards;
            //for (int i = 0; i < pengCard.Count; i++)
            //{
            //    if (pengCard[i] == cardG)
            //    {
            //        cnt++;
            //    }
            //}
            //if (cnt != 3)
            //{
            //    return;
            //}

            //往碰牌里补杠 补杠牌的位置
            int index = 0;
            for (int i = 0; i < pengCard.Count; i++)
            {
                if (pengCard[i] == cardG)
                {
                    index = i;
                    break;
                }
            }
            player.tableCardLayout.InsertCard(index, cardG);
            //player.tableCardLayout.AddCard(cardG);
            if (isMy)
            {
                //player.tableCardLayout.AddCard(cardG);
                player.handCardLayout.RemoveCard(cardG);
            }
            else
            {
                //player.tableCardLayout.AddCard(cardG);
                player.handCardLayout.DropCard();
            }
 
        }
        else if (type == 3)
        {
            player.tableCardLayout.AddCard(cardG);
            player.tableCardLayout.AddCard(cardG);
            player.tableCardLayout.AddCard(cardG);
            player.tableCardLayout.AddCard(cardG);

            if (isMy)
            {
                player.handCardLayout.RemoveCard(cardG);
                player.handCardLayout.RemoveCard(cardG);
                player.handCardLayout.RemoveCard(cardG);
            }
            else
            {
                player.dropCardLayout.RemoveLast();

                int count = player.handCardLayout.HandCards.Count;
                int index = UnityEngine.Random.Range(0, count);
                int index2 = index - 1;
                int index3 = index - 2;
                if (index <=1 )
                {
                    index2 = index + 1;
                    index3 = index + 2;
                }
                player.handCardLayout.RemoveCardAt(index);
                player.handCardLayout.RemoveCardAt(index2);
                player.handCardLayout.RemoveCardAt(index3);
            }
            Game.PoolManager.CardPool.Despawn(Game.MJMgr.LastDropCard.gameObject);
        }

        player.handCardLayout.LineUp();
        Game.MJMgr.targetFlag.gameObject.SetActive(false);
        EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, position, "gangUI_EF");

        Transform tableCLTrans = player.tableCardLayout.transform;
        int childCount = tableCLTrans.childCount;
        Transform lastChild = tableCLTrans.GetChild(childCount - 1);
        Vector3 endPos = tableCLTrans.TransformPoint(lastChild.localPosition);

        hand.transform.position = endPos;
        anim.gameObject.SetActive(true);

        GameObject eff = Game.PoolManager.EffectPool.Spawn("peng_EF");
        eff.transform.position = endPos;
        Game.PoolManager.EffectPool.Despawn(eff, 2);

        anim.Play("PutTable");
        Game.Instance.Gang = true;
    }
    

    // 玩家收炮阶段，桌面上卡牌的动作   TODO  修改声音
    internal void PlayShouPao(int cardSP, bool isMy)
    {
        Game.SoundManager.PlayDianPao(position);
        if (isMy)
        {// ture
            player.shouPaoCardLayout.AddCard(cardSP);
        }
        else
        {//false
            player.dropCardLayout.RemoveLast();
            player.shouPaoCardLayout.AddCard(cardSP);
        }
            
        Game.MJMgr.targetFlag.gameObject.SetActive(false);
        Game.PoolManager.CardPool.Despawn(Game.MJMgr.LastDropCard.gameObject);
        //Game.MJMgr.LastDropCardPlayer.dropCardLayout.RemoveLast();

        EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, position, "huUI_EF");
        Transform shouPaoCLTrans = player.shouPaoCardLayout.transform;
        int childCount = shouPaoCLTrans.childCount;
        Transform lastChild = shouPaoCLTrans.GetChild(childCount - 1);
        Vector3 endPos = shouPaoCLTrans.TransformPoint(lastChild.localPosition);
        hand.transform.position = endPos;
        anim.gameObject.SetActive(true);

        GameObject eff = Game.PoolManager.EffectPool.Spawn("hu_EF");
        eff.transform.position = endPos;
        Game.PoolManager.EffectPool.Despawn(eff, 5);

        anim.Play("PutTable");
    }
    //胡的界面上显示的动作
    internal void PlayHU(int cardHu,bool  isMy)
    {   
        Game.MaterialManager.TurnOnHandCard();
        player.handCardLayout.PlayHu();

        Game.SoundManager.PlayHu(position);
        Game.SoundManager.PlayWin();
        EventDispatcher.DispatchEvent(MessageCommand.PlayEffect, position, "huUI_EF");
        Vector3 pos = player.handCardLayout.DragCard(cardHu, Game.MJMgr.LastDropCard.gameObject);
        GameObject eff = Game.PoolManager.EffectPool.Spawn("hu_EF");
        eff.transform.position = pos;
        Game.PoolManager.EffectPool.Despawn(eff, 3);
    }

    // 甩九幺牌消失
    internal void PlayShuaiJiuYao(int[] list, bool isMy)
    {
        if (isMy)
        {
            for (int i = 0; i < list.Length; i++) //可以利用有序数组优化成一次for
            {
                for (int j = 0; j < player.handCardLayout.list.Count; j++)
                {
                    var card = player.handCardLayout.list[j];
                    if (card.Card == list[i])
                    {
                        player.handCardLayout.RemoveCard(card.Card);
                        GameObject child = Game.PoolManager.CardPool.Spawn(card.Card.ToString());
                        child.transform.position = Vector3.zero;
                        player.jiuYaoCardLayout.AddCard(card.Card);
                        Destroy(card.gameObject);//需参照出牌的地方，做这里的删除。
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < list.Length; i++)
            {
                int index = UnityEngine.Random.Range(1, player.handCardLayout.list.Count);
                player.handCardLayout.RemoveCardAt(index);
                GameObject child = Game.PoolManager.CardPool.Spawn(list[i].ToString());
                child.transform.position = Vector3.zero;
                player.jiuYaoCardLayout.AddCard(list[i]);
            }
        }
        player.handCardLayout.LineUp();
    }

    public void Bao(int dice, int oldBao)
    {
        IsBusy = true;
        MJPlayer player = Game.MJMgr.GetPlayerByPosition(position);
        //Time.timeScale = 0.1f;
        bool hasOldBao = (oldBao != -1);
        if (hasOldBao)
        {
            HideBaoEffect();
            Game.SoundManager.PlayHuanBao(player.postion);
            int card = oldBao;
            //Game.SoundManager.PlayCardSound(card);
            if (player.baoRoot.childCount > 0)
            {
                Transform old = player.baoRoot.GetChild(0);
                Game.PoolManager.CardPool.Despawn(old.gameObject);
            }

            Transform dropCLTrans = player.dropCardLayout.transform;
            int childCount = dropCLTrans.childCount;

            anim.gameObject.SetActive(true);

            Vector3 toPos = player.dropCardLayout.GetLocalPos(childCount);
            Vector3 endPos = dropCLTrans.TransformPoint(toPos);

            hand.transform.position = endPos;

            GameObject child = Game.PoolManager.CardPool.Spawn(card.ToString());
            //Game.MJMgr.LastDropCard = child.GetComponent<MJEntity>();
            //Game.MJMgr.LastDropCard.Card = card;
            Transform cardTrans = child.transform;
            cardTrans.SetParent(handMJRoot);
            cardTrans.localPosition = Vector3.zero;
            cardTrans.localRotation = Quaternion.identity;
            cardTrans.localScale = Vector3.one;

            anim.Play("Drop");
            handAnima.OnDropCallback = () =>
            {
                Game.SoundManager.PlayDropCard();
                player.dropCardLayout.AddCard(card, child);
                MJCardGroup.TryDragCard();
                //MJCardGroup.DragBaoCard(dice);
                Game.Delay(0.5f, () => { PutBao(); });
            };
        }
        else
        {
            MJCardGroup.TryDragCard();
            //MJCardGroup.DragBaoCard();
            PutBao();
        }
    }

    void PutBao()
    {
        anim.gameObject.SetActive(true);
        Vector3 endPos = player.baoRoot.position;
        hand.transform.position = endPos;

        GameObject child = Game.PoolManager.CardPool.Spawn("Dragon_Blank");
        Transform cardTrans = child.transform;
        cardTrans.SetParent(handMJRoot);
        cardTrans.localPosition = Vector3.zero;
        cardTrans.localRotation = Quaternion.Euler(0, 0, 180);
        cardTrans.localScale = Vector3.one;

        anim.Play("PutBao");
        handAnima.OnPutBaoCallback = () =>
        {
            Game.SoundManager.PlayDropCard();
            child.transform.SetParent(player.baoRoot);
            child.transform.localPosition = Vector3.zero;
            child.transform.localRotation = Quaternion.identity;
            child.transform.localScale = Vector3.one;
            IsBusy = false;

            ShowBaoEffect();
        };
    }

    public void HideBaoEffect()
    {
        if (null != baoEffect)
        {
            baoEffect.SetActive(false);
        }
    }

    public void ShowBaoEffect()
    {
        if (null == baoEffect)
        {
            baoEffect = Game.PoolManager.EffectPool.Spawn("BaoEffect");
        }
        baoEffect.transform.position = player.baoRoot.position;
        baoEffect.SetActive(true);
    }

    public void PlayDize(int dice1, int dice2)
    {
        Game.MJTable.HideCountdown();

        anim.gameObject.SetActive(true);

        hand.transform.position = dizePos.position;
        anim.Play("Dize");
        handAnima.OnDiceCallback = () =>
        {
            Game.SoundManager.PlayDizeSound();
            Game.MJTable.Dice(dice1, dice2);
        };

    }
}