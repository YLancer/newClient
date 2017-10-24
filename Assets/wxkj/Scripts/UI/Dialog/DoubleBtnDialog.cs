using UnityEngine;
using System.Collections;
using System;

public class DoubleBtnDialog : DoubleBtnDialogBase
{
    public Vector2 sizeMax = new Vector2(640, 800);
    private System.Action<bool> callback;

    void Start()
    {
        base.Start();
        detail.Image_TargetSizeFitter.sizeMax = sizeMax;
        detail.Image_TargetSizeFitter.OnMaxSize = OnMaxSize;
    }

    void OnMaxSize(bool isMaxSize)
    {
        detail.ScrollView_ScrollRect.vertical = isMaxSize;
    }

    public override void InitializeScene()
    {
        base.InitializeScene();

        detail.YES_Button.onClick.AddListener(OnClickYesBtn);
        detail.NO_Button.onClick.AddListener(OnClickNoBtn);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        string content = (string)sceneData[0];
        string title = sceneData.Length > 1 ? (string)sceneData[1] : "提示";
        Action<bool> callback = sceneData.Length > 2 ? (Action<bool>)sceneData[2] : null;
        string yesLabel = sceneData.Length > 3 ? (string)sceneData[3] : "确定";
        string noLabel = sceneData.Length > 4 ? (string)sceneData[4] : "取消";

        detail.Title_Text.text = title;
        detail.ContentText_Text.text = content;
        detail.yesText_Text.text = yesLabel;
        detail.noText_Text.text = noLabel;

        this.callback = callback;
    }

    public void OnClickYesBtn()
    {
        if (callback != null)
        {
            callback.Invoke(true);
        }

        OnBackPressed(true);
    }

    public void OnClickNoBtn()
    {
        if (callback != null)
        {
            callback.Invoke(false);
        }

        OnBackPressed(false);
    }

    public void OnClickCloseBtn()
    {
        if (callback != null)
        {
            callback.Invoke(false);
        }

        OnBackPressed(false);
    }
}
