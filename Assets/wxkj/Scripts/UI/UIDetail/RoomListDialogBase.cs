using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RoomListDialogBase : UIDialogBase
{
    public RoomListDialogDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Content_GridLayoutGroup = transform.Find("Root/Scroll View/Viewport/Content").gameObject.GetComponent<GridLayoutGroup>();
        detail.Content_ContentSizeFitter = transform.Find("Root/Scroll View/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Root/Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Root/Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Root/Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Root/Scroll View").gameObject.GetComponent<Image>();
        detail.CloseBanButton_Image = transform.Find("Root/CloseBanButton").gameObject.GetComponent<Image>();
        detail.CloseBanButton_Button = transform.Find("Root/CloseBanButton").gameObject.GetComponent<Button>();
        detail.CloseButton_Image = transform.Find("Root/CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("Root/CloseButton").gameObject.GetComponent<Button>();
        detail.CreateButton_Image = transform.Find("Root/CreateButton").gameObject.GetComponent<Image>();
        detail.CreateButton_Button = transform.Find("Root/CreateButton").gameObject.GetComponent<Button>();
        detail.RoomListSub_RoomListSub = transform.Find("Root/RoomListSub").gameObject.GetComponent<RoomListSub>();
        detail.BanPlayerSub_BanPlayerSub = transform.Find("Root/BanPlayerSub").gameObject.GetComponent<BanPlayerSub>();
        detail.Root_UtilScale = transform.Find("Root").gameObject.GetComponent<UtilScale>();

    }
}
