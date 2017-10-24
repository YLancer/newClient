using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MailPageBase : UISceneBase
{
    public MailPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.ListContent_GridLayoutGroup = transform.Find("Scroll View (1)/Viewport/ListContent").gameObject.GetComponent<GridLayoutGroup>();
        detail.ListContent_ContentSizeFitter = transform.Find("Scroll View (1)/Viewport/ListContent").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View (1)/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View (1)/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView1_ScrollRect = transform.Find("Scroll View (1)").gameObject.GetComponent<ScrollRect>();
        detail.MailSub_Image = transform.Find("MailSub").gameObject.GetComponent<Image>();
        detail.MailSub_MailSub = transform.Find("MailSub").gameObject.GetComponent<MailSub>();
        detail.ContentText_Text = transform.Find("Right/Scroll View/Viewport/ContentText").gameObject.GetComponent<Text>();
        detail.ContentText_ContentSizeFitter = transform.Find("Right/Scroll View/Viewport/ContentText").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Right/Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Right/Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Right/Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Right/Scroll View").gameObject.GetComponent<Image>();
        detail.ScrollView_LayoutElement = transform.Find("Right/Scroll View").gameObject.GetComponent<LayoutElement>();
        detail.MailAttachSub_MailAttachSub = transform.Find("Right/MailAttachSub").gameObject.GetComponent<MailAttachSub>();
        detail.MailAttachSub_LayoutElement = transform.Find("Right/MailAttachSub").gameObject.GetComponent<LayoutElement>();
        detail.Right_VerticalLayoutGroup = transform.Find("Right").gameObject.GetComponent<VerticalLayoutGroup>();

    }
}
