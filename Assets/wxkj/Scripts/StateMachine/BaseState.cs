using UnityEngine;
using System.Collections;

public class BaseState : MonoBehaviour
{
    public float TimeSinceStateStart
    {
        get
        {
            float time = Time.realtimeSinceStartup - RealTimeOfStateStart;
            return time;
        }
    }
    [HideInInspector]
    public float RealTimeOfStateStart = 0;
    [HideInInspector]
    public StateMachine Owner;

    public virtual void OnStateStart()
    {

    }

    public virtual void OnStateUpdate()
    {

    }

    public virtual void OnStateFixedUpdate()
    {

    }

    public virtual void OnStateLateUpdate()
    {

    }

    public virtual void OnStateEnd()
    {

    }

    public void SetNext(BaseState state)
    {
        Owner.SetNext(state);
    }
}
