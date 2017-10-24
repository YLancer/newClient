using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomListPageBase : UISceneBase
{
    public RoomListPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Content_GridLayoutGroup = transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<GridLayoutGroup>();
        detail.Content_ContentSizeFitter = transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Scroll View").gameObject.GetComponent<Image>();
        detail.CloseBanButton_Image = transform.Find("CloseBanButton").gameObject.GetComponent<Image>();
        detail.CloseBanButton_Button = transform.Find("CloseBanButton").gameObject.GetComponent<Button>();
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.CreateButton_Image = transform.Find("CreateButton").gameObject.GetComponent<Image>();
        detail.CreateButton_Button = transform.Find("CreateButton").gameObject.GetComponent<Button>();
        detail.RoomListSub_RoomListSub = transform.Find("RoomListSub").gameObject.GetComponent<RoomListSub>();
        detail.BanPlayerSub_BanPlayerSub = transform.Find("BanPlayerSub").gameObject.GetComponent<BanPlayerSub>();

    }
}
