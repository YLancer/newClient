using UnityEngine;
using System.Collections;

public class MJAIPlayer : MonoBehaviour {
	private MJPlayer player;
	// Use this for initialization
	void Start () {
		player = this.GetComponent<MJPlayer> ();

		//EventDispatcher.AddEventListener (MessageCommand.MJ_DragCard, OnDragCard);
		//EventDispatcher.AddEventListener (MessageCommand.MJ_DropCard, OnDropCard);
	}

	void OnDestroy(){
		//EventDispatcher.RemoveEventListener (MessageCommand.MJ_DragCard, OnDragCard);
		//EventDispatcher.RemoveEventListener (MessageCommand.MJ_DropCard, OnDropCard);
	}

	//void OnDragCard(params object[] args){
	//	if (player.IsActive) {
	//		Game.Delay (1f, () => {
	//			Game.MJMgr.dropIndex = player.HandCards.Count -1;
	//			player.DropCard ();
	//		});
	//	}
	//}

	void OnDropCard(params object[] args){
		MJPlayer dropCardPlayer = (MJPlayer)args [0];

		if (dropCardPlayer != player) {
			EventDispatcher.DispatchEvent (MessageCommand.MJ_Pass);
		}
	}
}
