using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MyPoolManager : MonoBehaviour
{
	public PrefabsPool EffectPool;
	public PrefabsPool CardPool;
	public PrefabsPool MjPool;

    void Awake()
    {
//        RoadItemPool.InitPool(RoadItemResetAction);
		EffectPool.InitPool();
		CardPool.InitPool(ResetCard);
		MjPool.InitPool ();
    }

    private void ResetCard(GameObject card)
    {
        if(card.tag != "Dragon_Blank")
        {
            Game.MaterialManager.TurnOnCard(card.transform);
        }
    }

    //    void RoadItemResetAction(GameObject go)
    //    {
    //        RoadItem item = go.GetComponent<RoadItem>();
    //        item.Reset();
    //    }
}
