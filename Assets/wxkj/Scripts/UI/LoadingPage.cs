using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingPage : LoadingPageBase
{
    private LoadPageType loadPageType;
    #region 封装给外部调用
    public void Show(LoadPageType type = LoadPageType.LoopCircle)
    {

        loadPageType = type;
        Game.UIMgr.PushScene(UIPage.LoadingPage);
    }

    public void UpdateLoading(float value, string msg = null)
    {
        detail.LoadingSlider_Slider.value = value;
        if (null != msg)
        {
            detail.Text_Text.text = msg;
        }
    }

    public void Hide()
    {
        if (Game.UIMgr.IsSceneActive(UIPage.LoadingPage))
        {
            Game.UIMgr.PopScene();
        }
    }

    #endregion
    public override void InitializeScene()
    {
        base.InitializeScene();
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);

        switch (loadPageType)
        {
            case LoadPageType.OnlyMask:
                {
                    this.BlackMask.SetActive(false);
                    detail.LoadingSlider_Slider.gameObject.SetActive(false);
                    detail.LoadingCircle_UIItem.gameObject.SetActive(false);
                    break;
                }
            case LoadPageType.OnlyBlackMask:
                {
                    detail.LoadingSlider_Slider.gameObject.SetActive(true);
                    detail.LoadingCircle_UIItem.gameObject.SetActive(false);
                    break;
                }
            case LoadPageType.LoopCircle:
                {
                    detail.LoadingSlider_Slider.gameObject.SetActive(false);
                    detail.LoadingCircle_UIItem.gameObject.SetActive(true);
                    break;
                }
            case LoadPageType.ProgressBar:
                {
                    detail.LoadingSlider_Slider.gameObject.SetActive(true);
                    detail.LoadingCircle_UIItem.gameObject.SetActive(false);
                    break;
                }
        }
    }
}
