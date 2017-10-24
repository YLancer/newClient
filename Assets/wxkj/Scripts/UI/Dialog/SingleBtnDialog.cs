using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class SingleBtnDialog : SingleBtnDialogBase
{
    public Vector2 sizeMax = new Vector2(640, 800);
    private System.Action callback;

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

        detail.OK_Button.onClick.AddListener(OnClickYesBtn);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        string content = (string)sceneData[0];
        string title = sceneData.Length > 1 ? (string)sceneData[1] : "提示";
        Action callback = sceneData.Length > 2 ? (Action)sceneData[2] : null;
        string yesLabel = sceneData.Length > 3 ? (string)sceneData[3] : "确定";

        detail.Title_Text.text = title;
        detail.ContentText_Text.text = content;
        detail.okText_Text.text = yesLabel;
        this.callback = callback;
    }

    public void OnClickYesBtn()
    {
        if (callback != null)
        {
            callback();
        }

        OnBackPressed(true);
    }
}
