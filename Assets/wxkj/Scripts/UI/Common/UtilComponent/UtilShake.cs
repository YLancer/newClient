using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

[ExecuteInEditMode]
public class UtilShake : UtilComponent
{
    public Vector3 to;
    public enum ShakeType
    {
        Position = 0,
        Scale = 1,
        Rotate = 2,
    }
    public ShakeType shakeType;

    /// <summary>
    /// 对象抖动
    /// </summary>
    public void OperationShakePosition()
    {
        int type = isLoop ? -1 : 0;
        tween = transform.DOShakePosition(duration, to, 4, 1f).SetLoops(type, loopType);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            callBack.Invoke();
        });
    }

    /// <summary>
    /// x,y的值最好是在-1到1之间 这是比较合理的  也可以根据具体的调节
    /// </summary>
    public void OperationShakeScale()
    {
        int type = isLoop ? -1 : 0;
        tween = transform.DOShakeScale(duration, to, 1, 0f).SetLoops(type, loopType);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            callBack.Invoke();
        });
    }
    /// <summary>
    /// 可用于做抖动之类的
    /// </summary>
    public void OperationShakeRotate()
    {
        int type = isLoop ? -1 : 0;
        tween = transform.DOShakeRotation(duration, to, 1, 0f).SetLoops(type, loopType);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            callBack.Invoke();
        });
    }

    public override void Play()
    {
        switch (shakeType)
        {
            case ShakeType.Position:
                OperationShakePosition();
                break;
            case ShakeType.Rotate:
                OperationShakeRotate();
                break;
            case ShakeType.Scale:
                OperationShakeScale();
                break;
            default:
                break;
        }
    }

}
