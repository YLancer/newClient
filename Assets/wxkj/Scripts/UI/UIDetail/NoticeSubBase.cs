using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoticeSubBase : UISubBase
{
    public NoticeSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ContentText_Text = transform.Find("Scroll View (3)/Viewport/ContentText").gameObject.GetComponent<Text>();
        detail.ContentText_ContentSizeFitter = transform.Find("Scroll View (3)/Viewport/ContentText").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View (3)/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View (3)/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView3_ScrollRect = transform.Find("Scroll View (3)").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView3_Image = transform.Find("Scroll View (3)").gameObject.GetComponent<Image>();

    }
}
