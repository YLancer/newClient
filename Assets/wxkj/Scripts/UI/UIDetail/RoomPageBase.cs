using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RoomPageBase : UISceneBase
{
    public RoomPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Content_GridLayoutGroup = transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<GridLayoutGroup>();
        detail.Content_ContentSizeFitter = transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Scroll View").gameObject.GetComponent<Image>();
        detail.QuikStartButton_Image = transform.Find("QuikStartButton").gameObject.GetComponent<Image>();
        detail.QuikStartButton_Button = transform.Find("QuikStartButton").gameObject.GetComponent<Button>();
        detail.RoomSub_RoomSub = transform.Find("RoomSub").gameObject.GetComponent<RoomSub>();

    }
}
