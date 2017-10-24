using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SingleBtnDialogBase : UIDialogBase
{
    public SingleBtnDialogDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ContentText_Text = transform.Find("Image/Scroll View/Viewport/ContentText").gameObject.GetComponent<Text>();
        detail.ContentText_ContentSizeFitter = transform.Find("Image/Scroll View/Viewport/ContentText").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Image/Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Image/Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Image/Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Image/Scroll View").gameObject.GetComponent<Image>();
        detail.Title_Text = transform.Find("Image/Title").gameObject.GetComponent<Text>();
        detail.okText_Text = transform.Find("Image/OK/okText").gameObject.GetComponent<Text>();
        detail.OK_Image = transform.Find("Image/OK").gameObject.GetComponent<Image>();
        detail.OK_Button = transform.Find("Image/OK").gameObject.GetComponent<Button>();
        detail.Image_Image = transform.Find("Image").gameObject.GetComponent<Image>();
        detail.Image_TargetSizeFitter = transform.Find("Image").gameObject.GetComponent<TargetSizeFitter>();
        detail.Image_UtilScale = transform.Find("Image").gameObject.GetComponent<UtilScale>();

    }
}
