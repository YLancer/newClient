using UnityEngine;
using System.Collections.Generic;

public class TableCardLayout : MonoBehaviour
{
    public float width = 7.1f;
    public float height = 5.02f;
    public int row = 2;      //行
    public int col = 18;     //每行放的总数
    public MJEntity last; 
    public List<int> TableCards = new List<int>();

    public void LineUp()
    {
        for (int j = 0; j < row; j++)
        {
            for (int i = 0; i < col; i++)
            {
                //if (MJUtils.BuGang() == false)
                //{
                    int index = j * col + i;
                    if (index < this.transform.childCount)
                    {
                        Transform trans = this.transform.GetChild(index);
                        trans.localPosition = Vector3.right * width * i + Vector3.forward * height * j;
                        trans.localRotation = Quaternion.identity;
                        trans.localScale = Vector3.one;
                    }
                //}
                //else
                //{
                //    int index_BuGang = j * col + 1;
                //}
            }
        }
    
    }

    //桌面上的牌要进行排列

    public void Clear()
    {
        while (this.transform.childCount > 0)
        {
            Transform child = this.transform.GetChild(0);
            Game.PoolManager.CardPool.Despawn(child.gameObject);
        }

        TableCards.Clear();
    }

    public void AddCard(int card)
    {
        GameObject child = Game.PoolManager.CardPool.Spawn(card.ToString());
        if(null == child)
        {
            Debug.LogWarningFormat("AddCard error card:{0}", card);
            return;
        }
        child.transform.SetParent(this.transform);
        child.transform.localScale = Vector3.one;
        child.transform.localRotation = Quaternion.identity;
        //MJEntity entity = child.GetComponent<MJEntity>();
        LineUp();

        last = child.GetComponent<MJEntity>(); ;

        TableCards.Add(card);
    }
}