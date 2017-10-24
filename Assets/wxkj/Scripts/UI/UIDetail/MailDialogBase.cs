using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MailDialogBase : UIDialogBase
{
    public MailDialogDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("Root/CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("Root/CloseButton").gameObject.GetComponent<Button>();
        detail.ListContent_GridLayoutGroup = transform.Find("Root/Scroll View (1)/Viewport/ListContent").gameObject.GetComponent<GridLayoutGroup>();
        detail.ListContent_ContentSizeFitter = transform.Find("Root/Scroll View (1)/Viewport/ListContent").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Root/Scroll View (1)/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Root/Scroll View (1)/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView1_ScrollRect = transform.Find("Root/Scroll View (1)").gameObject.GetComponent<ScrollRect>();
        detail.MailSub_Image = transform.Find("Root/MailSub").gameObject.GetComponent<Image>();
        detail.MailSub_MailSub = transform.Find("Root/MailSub").gameObject.GetComponent<MailSub>();
        detail.ContentText_Text = transform.Find("Root/Right/Scroll View/Viewport/ContentText").gameObject.GetComponent<Text>();
        detail.ContentText_ContentSizeFitter = transform.Find("Root/Right/Scroll View/Viewport/ContentText").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Root/Right/Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Root/Right/Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Root/Right/Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Root/Right/Scroll View").gameObject.GetComponent<Image>();
        detail.ScrollView_LayoutElement = transform.Find("Root/Right/Scroll View").gameObject.GetComponent<LayoutElement>();
        detail.MailAttachSub_MailAttachSub = transform.Find("Root/Right/MailAttachSub").gameObject.GetComponent<MailAttachSub>();
        detail.MailAttachSub_LayoutElement = transform.Find("Root/Right/MailAttachSub").gameObject.GetComponent<LayoutElement>();
        detail.Right_VerticalLayoutGroup = transform.Find("Root/Right").gameObject.GetComponent<VerticalLayoutGroup>();
        detail.Root_UtilScale = transform.Find("Root").gameObject.GetComponent<UtilScale>();

    }
}
