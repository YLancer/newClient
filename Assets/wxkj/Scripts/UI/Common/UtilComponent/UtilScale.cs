using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

/// <summary>
/// 主要处理对象的缩放
/// </summary>
public class UtilScale : UtilComponent
{
    public Vector3 from;
    public Vector3 to;
    public override void OperationAxisWithXY(Vector3 end, float duration, bool isLoop)
    {
        int type = isLoop ? -1 : 0;
        transform.localScale = from;
        tween = transform.DOScale(to, duration).SetLoops(type, loopType);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            callBack.Invoke();
        });
    }

    public override void OperationAxisWithXYByAction(Vector3 end, float duration, Action action)
    {
        tween = transform.DOScale(end, duration);
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
