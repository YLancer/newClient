using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine.Events;

public class UtilComponent : UtilBase
{
    public AnimationCurve animationCure = AnimationCurve.Linear(0, 0, 1.0f, 1.0f);
    public LoopType loopType = LoopType.Yoyo;

    public bool isLoop = false;
    public Tween tween;

    public override void OnEnable()
    {
        if (null != tween)
            tween.Kill();

        if (playOnStart)
            Play();
    }

    public override void NextOperation()
    {
        foreach (var next in childNodes)
        {
            next.enabled = true;
            next.Play();
        }
    }
    public virtual void OperationAxisWithXY(Vector3 end, float duration, bool isLoop) { }
    public virtual void OperationAxisWithXYByAction(Vector3 end, float duration, Action action) { }
}



