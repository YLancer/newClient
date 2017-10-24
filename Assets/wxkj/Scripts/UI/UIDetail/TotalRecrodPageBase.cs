using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalRecrodPageBase : UISceneBase
{
    public TotalRecrodPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.Content_GridLayoutGroup = transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<GridLayoutGroup>();
        detail.Content_ContentSizeFitter = transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.TotalRecrodSub_TotalRecrodSub = transform.Find("TotalRecrodSub").gameObject.GetComponent<TotalRecrodSub>();

    }
}
