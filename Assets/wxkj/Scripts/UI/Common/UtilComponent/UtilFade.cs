using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UtilFade : UtilComponent
{
    public Color32 to = Color.white;
    public ObjType objType = ObjType.CanvasGroup;

    public enum ObjType
    {
        Image = 0,
        Text = 1,
        Material = 2,
        CanvasGroup = 3,
    }


    /// <summary>
    /// image
    /// </summary>
    /// <param name="color"></param>
    /// <param name="duration"></param>
    /// <param name="isLoop"></param>
    /// <param name="action"></param>
    public void OperationFadeColorByImage(Image img, Color32 color, float duration, bool isLoop, Action action = null)
    {
        if (img != null)
        {
            int type = isLoop ? -1 : 0;
            tween = img.DOColor(color, duration).SetLoops(type, loopType);
            OperationCore(action, tween);
        }
    }

    /// <summary>
    /// text
    /// </summary>
    /// <param name="color"></param>
    /// <param name="duration"></param>
    /// <param name="isLoop"></param>
    /// <param name="action"></param>
    public void OperationFadeColorByText(Text text, Color32 color, float duration, bool isLoop, Action action = null)
    {
        if (text != null)
        {
            int type = isLoop ? -1 : 0;
            tween = text.DOColor(color, duration).SetLoops(type, loopType);
            OperationCore(action, tween);
        }
    }

    /// <summary>
    /// material
    /// </summary>
    /// <param name="color"></param>
    /// <param name="duration"></param>
    /// <param name="isLoop"></param>
    /// <param name="action"></param>
    public void OperationFadeColorByMaterial(Material mat, Color32 color, float duration, bool isLoop, Action action = null)
    {
        if (mat != null)
        {
            int type = isLoop ? -1 : 0;
            tween = mat.DOColor(color, duration).SetLoops(type, loopType);
            OperationCore(action, tween);
        }
    }

    /// <summary>
    /// 核心处理代码
    /// </summary>
    /// <param name="action"></param>
    /// <param name="tween"></param>
    private void OperationCore(Action action, Tween tween)
    {
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            if (action != null)
                action();
        });
    }


    public override void Play()
    {
        switch (objType)
        {
            case ObjType.Image:
                Image img = transform.GetComponent(typeof(Image)) as Image;
                OperationFadeColorByImage(img, to, duration, isLoop);
                break;
            case ObjType.Text:
                Text text = transform.GetComponent(typeof(Text)) as Text;
                OperationFadeColorByText(text, to, duration, isLoop);
                break;
            case ObjType.Material:
                MeshRenderer mr = transform.GetComponent<MeshRenderer>();
                if (mr != null)
                    OperationFadeColorByMaterial(mr.sharedMaterial, to, duration, isLoop);
                break;
            case ObjType.CanvasGroup:
                SetAlphaForAllChildren();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置该节点的所有子节点的对象的透明度
    /// </summary>
    /// <param name="action"></param>
    public void SetAlphaForAllChildren()
    {
        CanvasGroup cg = transform.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = transform.gameObject.AddComponent<CanvasGroup>();

        int type = isLoop ? -1 : 0;
        Tweener tween = cg.DOFade(to.a, duration).SetLoops(type, loopType); ;
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            base.NextOperation();
            callBack.Invoke();
        });
    }
}
