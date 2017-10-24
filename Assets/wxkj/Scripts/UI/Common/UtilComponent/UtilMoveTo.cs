using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class UtilMoveTo : UtilComponent
{
    public Vector3 to;
    public override void OperationAxisWithXY(Vector3 end, float duration, bool isLoop)
    {
        int type = isLoop ? -1 : 0;
        tween = transform.DOLocalMove(to, duration).SetLoops(type, loopType);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            callBack.Invoke();
        });

    }


    public override void OperationAxisWithXYByAction(Vector3 end, float duration, Action action)
    {
        tween = transform.DOLocalMove(end, duration);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            if (action != null)
                action();
        });
    }

    public override void Play()
    {
        OperationAxisWithXY(to, duration, isLoop);
    }


}
