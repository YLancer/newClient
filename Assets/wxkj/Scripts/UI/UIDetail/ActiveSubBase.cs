using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveSubBase : UISubBase
{
    public ActiveSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ListContent_GridLayoutGroup = transform.Find("Scroll View/Viewport/ListContent").gameObject.GetComponent<GridLayoutGroup>();
        detail.ListContent_ContentSizeFitter = transform.Find("Scroll View/Viewport/ListContent").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ActiveBtnSub_ActiveBtnSub = transform.Find("ActiveBtnSub").gameObject.GetComponent<ActiveBtnSub>();
        detail.ContentText_Text = transform.Find("Scroll View (2)/Viewport/ContentText").gameObject.GetComponent<Text>();
        detail.ContentText_ContentSizeFitter = transform.Find("Scroll View (2)/Viewport/ContentText").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View (2)/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View (2)/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView2_ScrollRect = transform.Find("Scroll View (2)").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView2_Image = transform.Find("Scroll View (2)").gameObject.GetComponent<Image>();

    }
}
