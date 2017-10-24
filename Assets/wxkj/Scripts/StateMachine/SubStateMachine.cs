using UnityEngine;
using System;
using System.Collections;

public class SubStateMachine {
	
}

public class SubState {
	public Action OnStart;
	public Action OnUpdate;
	public Action OnEnd;

	public SubState(Action OnStart,Action OnUpdate = null,Action OnEnd = null){
		this.OnStart = OnStart;
		this.OnUpdate = OnUpdate;
		this.OnEnd = OnEnd;
	}

	public float TimeSinceStateStart
	{
		get
		{
			float time = Time.realtimeSinceStartup - RealTimeOfStateStart;
			return time;
		}
	}
	public float RealTimeOfStateStart = 0;
}