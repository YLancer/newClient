using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DialogPage : DialogPageBase
{
    public UtilComponent EnterAnimation;
    public Vector2 sizeMax = new Vector2(640, 800);
    private System.Action<bool> callback;

    void Start()
    {
        detail.Image_TargetSizeFitter.sizeMax = sizeMax;
        detail.Image_TargetSizeFitter.OnMaxSize = OnMaxSize;

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

    void OnMaxSize(bool isMaxSize)
    {
        detail.ScrollView_UnityEngineUIScrollRect.vertical = isMaxSize;
    }

    public void InitializeScene()
    {
        Game.UIMgr.SetUseBlackMask(gameObject);
        Game.UIMgr.SetUseBoxCollider(gameObject);

        detail.OK_UnityEngineUIButton.onClick.AddListener(OnClickYesBtn);
        detail.YES_UnityEngineUIButton.onClick.AddListener(OnClickYesBtn);
        detail.NO_UnityEngineUIButton.onClick.AddListener(OnClickNoBtn);
    }

    public void Show(bool isSingleBtn,string content, string title = "提示", System.Action<bool> callback = null, string yesLabel = "确定", string noLabel = "取消")
    {
        if (EnterAnimation != null)
        {
            EnterAnimation.Play();
        }

        detail.Title_UnityEngineUIText.text = title;
        detail.ContentText_UnityEngineUIText.text = content;

        detail.yesText_UnityEngineUIText.text = yesLabel;
        detail.noText_UnityEngineUIText.text = noLabel;
        detail.okText_UnityEngineUIText.text = yesLabel;

        if (isSingleBtn)
        {
            UIUtils.SetActive(detail.YES, false);
            UIUtils.SetActive(detail.NO, false);
            UIUtils.SetActive(detail.OK, true);
        }
        else
        {
            UIUtils.SetActive(detail.YES, true);
            UIUtils.SetActive(detail.NO, true);
            UIUtils.SetActive(detail.OK, false);
        }

        this.callback = callback;
    }

    public void OnClickYesBtn()
    {
        Game.SoundManager.PlayClick();
        UIUtils.SetActive(this.gameObject, false);
        if (callback != null)
        {
            callback.Invoke(true);
        }
    }

    public void OnClickNoBtn()
    {
        Game.SoundManager.PlayClick();
        UIUtils.SetActive(this.gameObject, false);
        if (callback != null)
        {
            callback.Invoke(false);
        }
    }

    public void OnBackPressed()
    {
        Game.SoundManager.PlayClose();
        OnClickNoBtn();
    }
}
