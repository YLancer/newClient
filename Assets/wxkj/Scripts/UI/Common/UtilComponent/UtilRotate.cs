using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;


/// <summary>
/// 主要处理对象旋转
/// </summary>
public class UtilRotate : UtilComponent
{
    public Vector3 from;
    public Vector3 to;
    public override void OperationAxisWithXY(Vector3 end, float duration, bool isLoop)
    {
        int type = isLoop ? -1 : 0;
        transform.localEulerAngles = from;
        tween = transform.DORotate(end, duration).SetLoops(type, loopType);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            callBack.Invoke();
        });
    }

    public override void OperationAxisWithXYByAction(Vector3 end, float duration, Action action)
    {
        transform.localEulerAngles = from;
        tween = transform.DORotate(end, duration);
        tween.SetEase(animationCure);
        if (action != null)
            tween.OnComplete(() => { action(); });
    }

    public override void Play()
    {
        OperationAxisWithXY(to, duration, isLoop);

    }

}
