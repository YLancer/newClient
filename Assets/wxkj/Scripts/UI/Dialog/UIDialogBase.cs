using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[ExecuteInEditMode]
public class UIDialogBase : BaseMonoBehaviour {
    public UIDialog dialog = UIDialog.Null;

    [HideInInspector]
    public bool UseBoxCollider = true;
    [HideInInspector]
    public bool UseBlackMask = true;

    public bool IsScreenActivated = false;

    protected GameObject BlackMask;
    protected GameObject BoxCollider;
    //private Action<bool> callback;

    public UtilBase EnterAnimatioin;
    public UtilBase ExitAnimatioin;

    public void Start()
    {
#if UNITY_EDITOR
        SetParent();
#endif
    }

    void SetParent()
    {
        DialogMgr root = GameObject.FindObjectOfType<DialogMgr>();
        if (null != root)
        {
            Transform parent = this.transform.parent;
            if (parent != root.transform)
            {
                this.transform.SetParent(root.transform);
                RectTransform rect = this.GetComponent<RectTransform>();
                rect.offsetMax = Vector2.zero;
                rect.offsetMin = Vector2.zero;
                rect.localScale = Vector3.one;
            }
        }
    }

    /* Initialization function that is called immediately after this scene is created */
    public virtual void InitializeScene()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMax = Vector2.zero;
        rect.offsetMin = Vector2.zero; ;

        if (UseBlackMask)
        {
            BlackMask = Game.UIMgr.SetUseBlackMask(gameObject);
        }

        if (UseBoxCollider)
        {
            BoxCollider = Game.UIMgr.SetUseBoxCollider(gameObject);
        }
    }

    /* Function called every time this scene becomes the active scene */
    public virtual void OnSceneActivated(params object[] sceneData)
    {
        //Action<bool> callback = null, 
        //this.callback = callback;
        IsScreenActivated = true;
        UIUtils.SetActive(this.gameObject, true);

        PlayEnterAnimation();
    }

    /* Function called every time this scene becomes deactivated (no longer the active scene) */
    public virtual void OnSceneDeactivated()
    {
        IsScreenActivated = false;
        UIUtils.SetActive(this.gameObject, false);
    }

    public virtual void OnBackPressed(bool isOk = false)
    {
        PlayExitAnimation(isOk);
    }

    public virtual void SetAllMemberValue()
    {

    }

    public virtual void PlayEnterAnimation()
    {
        if (null != EnterAnimatioin)
        {
            EnterAnimatioin.Play();
        }
    }

    public virtual void PlayExitAnimation(bool isOk = false)
    {
        if (null != ExitAnimatioin)
        {
            //ExitAnimatioin.callBack.AddListener(() => {
            //    OnExitFinish(isOk);
            //});
            ExitAnimatioin.Play();
        }
        else
        {
            OnExitFinish(isOk);
        }
    }

    public void OnExitFinish(bool isOk)
    {
        OnSceneDeactivated();
        Game.DialogMgr.Cache(this, isOk);
    }

    public void Clear(MonoBehaviour mono)
    {
        PrefabUtils.ClearChild(mono);
    }

    public GameObject AddChild(MonoBehaviour parent, MonoBehaviour prefab)
    {
        return PrefabUtils.AddChild(parent, prefab);
    }
}

//[System.Serializable]
//public class DialogAnimation
//{
//    public bool enable = true;
//    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
//    public float duration = 0.2f;
//    public Vector3 scale = new Vector3(0.5f, 0.5f, 1);
//}
