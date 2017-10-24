using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour
{
    #region State
    public BaseState InitState;
	public BaseState MenuState;
	public BaseState PlayState;
    #endregion

    BaseState ActiveState;
    BaseState NextState;
	// Use this for initialization
//	void Start () {
//        SetNext(MenuState);
//	}

    // Update is called once per frame
    void Update()
    {
        if (null != NextState)
        {
            if (null != ActiveState)
            {
                ActiveState.OnStateEnd();
                ActiveState.enabled = false;
            }

            ActiveState = NextState;
            NextState = null;

            ActiveState.enabled = true;
            ActiveState.RealTimeOfStateStart = Time.realtimeSinceStartup;
            ActiveState.Owner = this;

            ActiveState.OnStateStart();
        }

        if (null != ActiveState)
        {
            ActiveState.OnStateUpdate();
        }
	}

    void FixedUpdate()
    {
        if (null != ActiveState)
        {
            ActiveState.OnStateFixedUpdate();
        }
    }

    void LateUpdate()
    {
        if (null != ActiveState)
        {
            ActiveState.OnStateLateUpdate();
        }
    }

    public void SetNext(BaseState state)
    {
        if (null == state)
        {
            print("warn! State is null !");
        }
        this.NextState = state;
    }

    public BaseState GetActiveState() 
    {
        return ActiveState;
    }

	public bool IsState(BaseState state){
		return ActiveState == state;
	}
}
