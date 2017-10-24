using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Reflection;

public enum UIDialog
{
    [Description("Prefabs/UI/Null")]
    Null = 0,

    [Description("Prefabs/UI/Dialog/SingleBtnDialog")]
    SingleBtnDialog = 1,

    [Description("Prefabs/UI/Dialog/DoubleBtnDialog")]
    DoubleBtnDialog = 2,

    [Description("Prefabs/UI/Dialog/SettleRoundDialog")]
    SettleRoundDialog = 3,

    [Description("Prefabs/UI/Dialog/SettleDialog")]
    SettleDialog = 4,

    [Description("Prefabs/UI/Dialog/CreateRoomDialog")]
    CreateRoomDialog = 5,

    [Description("Prefabs/UI/Dialog/JoinRoomDialog")]
    JoinRoomDialog = 6,

    [Description("Prefabs/UI/Dialog/RoomListDialog")]
    RoomListDialog = 7,


}

public class DialogMgr : BaseMonoBehaviour {
    private class DialogParameter
    {
        public UIDialog dialog;
        public object[] paras;

        public DialogParameter(UIDialog dialog, params object[] paras)
        {
            this.dialog = dialog;
            this.paras = paras;
        }
    }

    private List<UIDialogBase> cacheList = new List<UIDialogBase>();
    private List<UIDialogBase> openedList = new List<UIDialogBase>();
    private Queue<DialogParameter> queue = new Queue<DialogParameter>();
    private Queue<DialogParameter> tipsQueue = new Queue<DialogParameter>();

    /// <summary>
    /// 回调事件，用于在dialog界面打开时被阻挡的事件处理
    /// </summary>
    public Action<UIDialog,bool> OnDialogCallBack;

    private void Update()
    {
        if (queue.Count != 0 && !IsDialogActive)
        {
            DialogParameter parameter = queue.Dequeue();
            UIDialogBase activeDialog = LoadScene(parameter.dialog);
            openedList.Add(activeDialog);
            activeDialog.transform.SetAsLastSibling();
            activeDialog.OnSceneActivated(parameter.paras);
        }

        if (tipsQueue.Count != 0)
        {
            DialogParameter parameter = tipsQueue.Dequeue();
            UIDialogBase dialog = LoadScene(parameter.dialog);
            dialog.transform.SetAsLastSibling();
            dialog.OnSceneActivated(parameter.paras);
        }
    }

    public bool IsDialogActive
    {
        get
        {
            return (openedList.Count>0);
        }
    }

    public UIDialogBase LoadScene(UIDialog key)
    {
        UIDialogBase dialog = FindInCache(key);

        if (dialog == null)
        {
            string strPrefabeName = Utils.GetEnumDes(key);
            GameObject newSceneGameObject = Resources.Load(strPrefabeName) as GameObject;

            if (newSceneGameObject != null)
            {
                newSceneGameObject = UIUtils.AddChild(this.gameObject, newSceneGameObject);
                if (newSceneGameObject.activeSelf == true)
                {
                    Debug.Log("newSceneGameObject activity true:" + newSceneGameObject.name);
                }
                else
                {
                    Debug.Log("newSceneGameObject activity false:" + newSceneGameObject.name);
                }
                if (newSceneGameObject != null)
                {
                    dialog = newSceneGameObject.GetComponent<UIDialogBase>();
                    dialog.InitializeScene();
                }
                else
                {
                    Debug.LogWarning("UISystem::LoadDialog() Failed to add new scene to parent UISystem with name: " + key);
                }
            }
            else
            {
                Debug.LogWarning("UISystem::LoadDialog() Failed to load new scene with name: " + key);
            }
        }

        return dialog;
    }

    UIDialogBase FindInCache(UIDialog key)
    {
        foreach (UIDialogBase dialog in cacheList)
        {
            if (dialog.dialog == key)
            {
                cacheList.Remove(dialog);
                return dialog;
            }
        }

        return null;
    }

    public void PushDialog( UIDialog dialog, params object[] paras)
    {
        DialogParameter parameter = new DialogParameter(dialog, paras);
        queue.Enqueue(parameter);
    }

    public void PushDialogImmediately(UIDialog dialog, params object[] paras)
    {
        DialogParameter parameter = new DialogParameter(dialog, paras);
        UIDialogBase activeDialog = LoadScene(parameter.dialog);
        openedList.Add(activeDialog);
        activeDialog.transform.SetAsLastSibling();
        activeDialog.OnSceneActivated(parameter.paras);
    }

    public void PushTips(UIDialog dialog, params object[] paras)
    {
        DialogParameter parameter = new DialogParameter(dialog, paras);
        tipsQueue.Enqueue(parameter);
    }

    public void Cache(UIDialogBase dialog,bool isOk)
    {
        if(null != dialog){
            openedList.Remove(dialog);
            cacheList.Add(dialog);

            if (null != OnDialogCallBack)
            {
                OnDialogCallBack(dialog.dialog, isOk);
            }
        }
    }
    
    public void OnBackPressed()
    {
        if (openedList.Count > 0)
        {
            openedList[openedList.Count - 1].OnBackPressed(false);
        }
    }
}
