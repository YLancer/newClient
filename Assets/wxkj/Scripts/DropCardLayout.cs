using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class DropCardLayout : MonoBehaviour
{
    public float width = 7.1f;
    public float height = 5.02f;
    public int row = 2;
    public int col = 18;
    public GameObject last;
    public int lastCard;
    //public List<int> DropCards = new List<int>();

    public Vector3 GetLocalPos(int index)
    {
        int r = index / col;
        int c = index % col;
        return Vector3.right * width * c + Vector3.forward * height * r;
    }

    public void Clear()
    {
        while (this.transform.childCount > 0)
        {
            Transform child = this.transform.GetChild(0);
            Game.PoolManager.CardPool.Despawn(child.gameObject);
        }

        //DropCards.Clear();
    }

    public void AddCard(int card, GameObject child = null)
    {
        Vector3 toPos = GetLocalPos(this.transform.childCount);

        if(null == child)
        {
            child = Game.PoolManager.CardPool.Spawn(card.ToString());
        }
        
        child.transform.SetParent(this.transform);
        child.transform.localPosition = toPos;
        child.transform.localScale = Vector3.one;
        child.transform.localRotation = Quaternion.identity;
        MJEntity entity = child.GetComponent<MJEntity>();

        last = child;
        lastCard = card;
    }

    public void RemoveLast()
    {
        Game.MJMgr.targetFlag.gameObject.SetActive(false);

        Game.PoolManager.CardPool.Despawn(last);
    }
}