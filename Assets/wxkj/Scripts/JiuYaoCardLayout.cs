using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class JiuYaoCardLayout : MonoBehaviour
{
    public float width = 7.1f;
    public float height = 5.02f;
    public int row = 2;
    public int col = 18;
    public GameObject last;
    public List<int> JiuYaoCards = new List<int>();
    //public List<int> DropCards = new List<int>();

    private void LineUp()
    {
        for (int j = 0; j < row; j++)
        {
            for (int i = 0; i < col; i++)
            {

                int index = j * col + i;
                if (index < this.transform.childCount)
                {
                    Transform trans = this.transform.GetChild(index);
                    trans.localPosition = Vector3.left * width * i + Vector3.forward * height * j;
                    trans.localRotation = Quaternion.identity;
                    trans.localScale = Vector3.one;
                }
            }
        }
    }

    public void Clear()
    {
        while (this.transform.childCount > 0)
        {
            Transform child = this.transform.GetChild(0);
            Game.PoolManager.CardPool.Despawn(child.gameObject);
        }
        JiuYaoCards.Clear();
    }


    public void AddCard(int card)
    {
        GameObject child = Game.PoolManager.CardPool.Spawn(card.ToString());
        if (null == child)
        {
            Debug.LogWarningFormat("AddCard error card:{0}", card);
            return;
        }
        child.transform.SetParent(this.transform);
        child.transform.localScale = Vector3.one;
        child.transform.localRotation = Quaternion.identity;
        //MJEntity entity = child.GetComponent<MJEntity>();
        LineUp();

        last = child;

        JiuYaoCards.Add(card);
    }

    public void RemoveLast()
    {
        Game.MJMgr.targetFlag.gameObject.SetActive(false);

        Game.PoolManager.CardPool.Despawn(last);
    }
}