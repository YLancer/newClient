using UnityEngine;
using System.Collections;

public class DialogPageBase : MonoBehaviour
{
    public DialogPageDetail detail;

    public void SetAllMemberValue()
    {
        detail.ContentText = GameObject.Find("DialogPage/Image/Scroll View/Viewport/ContentText");
        detail.ContentText_UnityEngineUIText = detail.ContentText.GetComponent<UnityEngine.UI.Text>();
        detail.ContentText_UnityEngineUIContentSizeFitter = detail.ContentText.GetComponent<UnityEngine.UI.ContentSizeFitter>();
        detail.Viewport = GameObject.Find("DialogPage/Image/Scroll View/Viewport");
        detail.Viewport_UnityEngineUIMask = detail.Viewport.GetComponent<UnityEngine.UI.Mask>();
        detail.Viewport_UnityEngineUIImage = detail.Viewport.GetComponent<UnityEngine.UI.Image>();
        detail.ScrollView = GameObject.Find("DialogPage/Image/Scroll View");
        detail.ScrollView_UnityEngineUIScrollRect = detail.ScrollView.GetComponent<UnityEngine.UI.ScrollRect>();
        detail.ScrollView_UnityEngineUIImage = detail.ScrollView.GetComponent<UnityEngine.UI.Image>();
        detail.Title = GameObject.Find("DialogPage/Image/Title");
        detail.Title_UnityEngineUIText = detail.Title.GetComponent<UnityEngine.UI.Text>();
        detail.okText = GameObject.Find("DialogPage/Image/OK/okText");
        detail.okText_UnityEngineUIText = detail.okText.GetComponent<UnityEngine.UI.Text>();
        detail.OK = GameObject.Find("DialogPage/Image/OK");
        detail.OK_UnityEngineUIImage = detail.OK.GetComponent<UnityEngine.UI.Image>();
        detail.OK_UnityEngineUIButton = detail.OK.GetComponent<UnityEngine.UI.Button>();
        detail.yesText = GameObject.Find("DialogPage/Image/YES/yesText");
        detail.yesText_UnityEngineUIText = detail.yesText.GetComponent<UnityEngine.UI.Text>();
        detail.YES = GameObject.Find("DialogPage/Image/YES");
        detail.YES_UnityEngineUIImage = detail.YES.GetComponent<UnityEngine.UI.Image>();
        detail.YES_UnityEngineUIButton = detail.YES.GetComponent<UnityEngine.UI.Button>();
        detail.noText = GameObject.Find("DialogPage/Image/NO/noText");
        detail.noText_UnityEngineUIText = detail.noText.GetComponent<UnityEngine.UI.Text>();
        detail.NO = GameObject.Find("DialogPage/Image/NO");
        detail.NO_UnityEngineUIImage = detail.NO.GetComponent<UnityEngine.UI.Image>();
        detail.NO_UnityEngineUIButton = detail.NO.GetComponent<UnityEngine.UI.Button>();
        detail.Image = GameObject.Find("DialogPage/Image");
        detail.Image_UnityEngineUIImage = detail.Image.GetComponent<UnityEngine.UI.Image>();
        detail.Image_TargetSizeFitter = detail.Image.GetComponent<TargetSizeFitter>();
        detail.DialogPage = GameObject.Find("DialogPage");
        detail.DialogPage_UnityEngineUICanvasScaler = detail.DialogPage.GetComponent<UnityEngine.UI.CanvasScaler>();
        detail.DialogPage_UnityEngineUIGraphicRaycaster = detail.DialogPage.GetComponent<UnityEngine.UI.GraphicRaycaster>();

    }
}
