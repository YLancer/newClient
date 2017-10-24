using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[ExecuteInEditMode]
public class UISceneBase : MonoBehaviour
{
    [HideInInspector]
    public bool ShowNav = true;
    [HideInInspector]
    public bool UseBoxCollider = true;
    [HideInInspector]
    public bool UseBlackMask = false;
    /* Whether or not to show the scene below this one in the stack */
    //是否要在堆栈下面显示这个场景
    [HideInInspector]
    public bool HideOldScenes = true;
    [HideInInspector]
    public bool BackPopPreScenes = false;

    public UIPage Page;

    public bool IsScreenActivated = false;

    public UtilComponent EnterAnimation;

    protected GameObject BlackMask;
    protected GameObject BoxCollider;
    protected GameObject Nav;
    public GameObject NavRoot;
    void Start()
    {
#if UNITY_EDITOR
        SetParent();
#endif
    }

    void SetParent()
    {
        UIMgr root = GameObject.FindObjectOfType<UIMgr>();
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
    //在此场景创建后立即调用的初始化函数
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

        if (ShowNav)
        {
            Nav = Game.UIMgr.SetUseNav(NavRoot==null?gameObject: NavRoot);
        }

        Button[] allBtns = GetComponentsInChildren<Button>();
        for(int i = 0; i < allBtns.Length; i++)
        {
            Button button = allBtns[i];
            button.transition = Selectable.Transition.None;

            GameObject btn = button.gameObject;
            EventTriggerListener.Get(btn).onDown = (bt) =>
            {
                bt.transform.DOKill();
                bt.transform.DOScale(Vector3.one * 1.1f, 0.3f);
            };

            EventTriggerListener.Get(btn).onUp = (bt) =>
            {
                bt.transform.DOKill();
                bt.transform.DOScale(Vector3.one, 0.3f);
            };
        }
    }

    /* Function called when this scene is to be destroyed (allows for deallocation of memory or whatnot) */
    //当这个场景被销毁时函数被调用(允许存储内存或其他东西)
    public virtual void DestroyScene()
    {
    }

    /* Function called every time this scene is opened */
    //每当这个场景被打开时函数就会被调用
    public virtual void OnSceneOpened(params object[] sceneData)
    {
        ShowScene();
    }

    /* Function called every time this scene is closed */
    //每当这个场景被关闭时函数就会被调用
    public virtual void OnSceneClosed()
    {
        HideScene();
    }

    /* Function called every time this scene becomes the active scene */
    //每当这个场景变成活动场景时函数就会被调用
    public virtual void OnSceneActivated(params object[] sceneData)
    {
        IsScreenActivated = true;

        ShowScene();

        if(EnterAnimation != null)
        {
            EnterAnimation.Play();
        }
    }

    /* Function called every time this scene becomes deactivated (no longer the active scene) */
    //每当这个场景被禁用时函数就会被调用(不再是活动场景)
    public virtual void OnSceneDeactivated(bool hideScene)
    {
        IsScreenActivated = false;

        if (hideScene == true)
        {
            HideScene();
        }
    }

    /* Function that hides this scene */
    //隐藏这个场景的函数
    public virtual void HideScene()
    {
        gameObject.SetActive(false);
    }

    /* Function that unhides this scene */
    //将这个场景隐藏起来的函数
    public virtual void ShowScene()
    {
        gameObject.SetActive(true);

        //RectTransform rect = GetComponent<RectTransform>();
        //rect.SetAsFirstSibling();
    }

    public virtual void OnBackPressed()
    {
        Game.SoundManager.PlayClose();
        Game.UIMgr.PopScene();
    }

    public virtual void SetAllMemberValue()
    {

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
