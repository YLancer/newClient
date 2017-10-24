using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class UtilBase : BaseMonoBehaviour
{
    public float duration = 0.2f;
    public bool playOnStart = false;
    public List<UtilBase> childNodes;
    public UnityEvent callBack = new UnityEvent();

    public virtual void Play() { }

    public virtual void OnEnable() { }

    public virtual void NextOperation() { }
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
