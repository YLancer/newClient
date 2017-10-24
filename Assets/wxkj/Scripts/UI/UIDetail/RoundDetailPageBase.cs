using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundDetailPageBase : UISceneBase
{
    public RoundDetailPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.Content_GridLayoutGroup = transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<GridLayoutGroup>();
        detail.Viewport_Mask = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.cache_Image = transform.Find("RoundResult/cache").gameObject.GetComponent<Image>();
        detail.RoundResult_ImageText = transform.Find("RoundResult").gameObject.GetComponent<ImageText>();
        detail.RoundDetailSub_RoundDetailSub = transform.Find("RoundDetailSub").gameObject.GetComponent<RoundDetailSub>();

    }
}
