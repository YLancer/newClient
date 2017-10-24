using UnityEngine;
using System.Collections;

public class MJHMPlayer : MonoBehaviour {
	private MJPlayer player;
	// Use this for initialization
	//void Start () {
	//	player = this.GetComponent<MJPlayer> ();

	//	EventDispatcher.AddEventListener (MessageCommand.MJ_DragCard, OnDragCard);
	//	EventDispatcher.AddEventListener (MessageCommand.MJ_DropCard, OnDropCard);
	//}

	//void OnDestroy(){
	//	EventDispatcher.RemoveEventListener (MessageCommand.MJ_DragCard, OnDragCard);
	//	EventDispatcher.RemoveEventListener (MessageCommand.MJ_DropCard, OnDropCard);
	//}

	//void OnDragCard(params object[] args){
	//	MJPlayer dragCardPlayer = (MJPlayer)args [0];
	//	int card = (int)args [1];

	//	if (dragCardPlayer == player) {
 //           // 可能同时存在多个可以杠的，跟吃一样
 //           bool gang = false;// player.CanAnGang (card) || player.CanAddGang (card);
	//		bool hu = player.CanHu (card);

	//		if (gang || hu) {
	//			EventDispatcher.DispatchEvent (MessageCommand.MJ_UpdateCtrlPanel,false,false,gang,hu);
	//		}
	//	}
	//}

	void OnDropCard(params object[] args){
		MJPlayer dropCardPlayer = (MJPlayer)args [0];

		//if (dropCardPlayer != player) {
		//	// 判断其他玩家 吃 碰 杠 胡
		//	bool chi = player.CanChi (card);
		//	bool peng = player.CanPeng (card);
  //          bool gang = false;// player.CanGang (card);
		//	bool hu = player.CanHu (card);

		//	if (chi || peng || gang || hu) {
		//		//					player.turnState = PlayerTurnState.NeedCard;
		//		EventDispatcher.DispatchEvent (MessageCommand.MJ_UpdateCtrlPanel,chi,peng,gang,hu);
		//	} else {
		//		EventDispatcher.DispatchEvent (MessageCommand.MJ_Pass);
		//	}
		//}
	}
}
