using UnityEngine;
using System.Collections;

public class PlayState : BaseState {
	private SubState InitState;
	private SubState DiceState;
	private SubState LicensingState;
	private SubState TurnState;
	SubState ActiveState;
	SubState NextState;

	private MJMgr MJMgr {
		get {
			return Game.MJMgr;
		}
	}

	public override void OnStateStart()
	{
		base.OnStateStart();

		// 注册子状态机
		//InitState = new SubState (OnInitStart);
		//DiceState = new SubState (OnDiceStart);
		//LicensingState = new SubState (OnLicensingStart);
		//TurnState = new SubState (OnTurnStart, OnTurnUpdate);

		//// 设置初始子状态
		//SetSubNext (InitState);

		//Game.SoundManager.PlayPlayingBackground ();
	}

	public override void OnStateEnd ()
	{
		base.OnStateEnd ();
		MJMgr.Clear ();
	}

	public override void OnStateUpdate()
	{
		base.OnStateUpdate();

		// 子状态机流程
		if (null != NextState)
		{
			if (null != ActiveState && null != ActiveState.OnEnd)
			{
				ActiveState.OnEnd();
			}

			ActiveState = NextState;
			NextState = null;

			ActiveState.RealTimeOfStateStart = Time.realtimeSinceStartup;

			if (null != ActiveState.OnStart) {
				ActiveState.OnStart ();
			}
		}

		if (null != ActiveState && null != ActiveState.OnUpdate)
		{
			ActiveState.OnUpdate();
		}
	}

	#region Sub State
	public void SetSubNext(SubState state)
	{
		if (null == state)
		{
			print("warn! State is null !");
		}
		this.NextState = state;
	}

	public bool IsState(SubState state){
		return ActiveState == state;
	}

	void OnInitStart(){
		//MJMgr.Shuffle ();
		SetSubNext (DiceState);
        //洗牌动画
	}

	void OnDiceStart(){
		//MJMgr.RandomMakers ();
        //Game.MJTable.Dice();


        //Game.MJTable.PlayShuffle();
        //Game.Delay (0.5f, () => {
        //    SetSubNext (LicensingState);
        //});

        //EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
    }

//	void OnLicensingStart(){
////		MJMgr.Licensing ();
//		int remain = Game.MJTable.DiceNum * 2;
//		int dragNum = 3;

//		int index = 0;
//		Game.DelayLoop (dragNum, 0.05f, (i) => {
//            MJCardGroup aGroup = MJMgr.ActiveGroup;
//            int count = aGroup.TryDragCard(4);
//            if (count < 4)
//            {
//                count = 4 - count;
//                aGroup = aGroup.NextGroup;
//                aGroup.SetActive();
//                aGroup.TryDragCard(count);
//            }

//            SendCard(MJMgr.players[index], MJMgr.players[index].HandCards[index]);
//            SendCard(MJMgr.players[index], MJMgr.players[index].HandCards[index]);
//            SendCard(MJMgr.players[index], MJMgr.players[index].HandCards[index]);
//            SendCard(MJMgr.players[index], MJMgr.players[index].HandCards[index]);

//            index++;
//			if(index>=4)index = 0;
//		},()=>{
//            for(int i = 0; i < 4; i++)
//            {
//                SendCard(MJMgr.players[i], MJMgr.players[index].HandCards[index]);
//                if (!MJMgr.ActiveGroup.DragCard())
//                {
//                    MJMgr.ActiveGroup.NextGroup.SetActive();
//                    MJMgr.ActiveGroup.DragCard();
//                }
//            }

//            MJMgr.SortPlayerCard();
//			SetSubNext(TurnState);
//		});
//	}

	//void SendCard(MJPlayer player,int card){
	//	//int card = MJMgr.All [0];

	//	player.HandCards.Add(card);
	//	//MJMgr.All.RemoveAt(0);

 //       player.handCardLayout.AddCard(card);
 //   }

	void OnTurnStart(){
		//MJMgr.ActivePlayer.DragCard ();
	}

	void OnTurnUpdate(){

	}
	#endregion

}
