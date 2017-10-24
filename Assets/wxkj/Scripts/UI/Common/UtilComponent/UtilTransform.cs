using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class UtilTransform : UtilComponent
{
    public Transform from;
    public Transform to;
    protected object obj = new object();
    Vector3 position = Vector3.one * -1;
    Vector3 scale = Vector3.one * -1;
    Quaternion qua = Quaternion.identity;



    void Init(Transform _transform)
    {
        transform.localPosition = _transform.localPosition;
        transform.localScale = _transform.localScale;
        transform.localRotation = _transform.localRotation;
    }

    public override void Play()
    {
        transform.DOKill();
        OperationTransform();
    }

    void OperationTransform()
    {
        int type = isLoop ? -1 : 0;
        Init(from);
        //move
        Tween tweenMove = transform.DOLocalMove(to.localPosition, duration).SetLoops(type, loopType);
        tweenMove.SetEase(animationCure);
        tweenMove.OnComplete(() =>
        {
            NextOperation();
            callBack.Invoke();
        });
        //scale
        Tween tweenScale = transform.DOScale(to.localScale, duration).SetLoops(type, loopType);
        tweenScale.SetEase(animationCure);

        //rotate
        Tween tweenRotate = transform.DORotateQuaternion(to.localRotation, duration).SetLoops(type, loopType);
        tweenRotate.SetEase(animationCure);
    }



}
