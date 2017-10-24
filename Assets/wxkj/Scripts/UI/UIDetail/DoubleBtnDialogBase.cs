using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DoubleBtnDialogBase : UIDialogBase
{
    public DoubleBtnDialogDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ContentText_Text = transform.Find("Image/Scroll View/Viewport/ContentText").gameObject.GetComponent<Text>();
        detail.ContentText_ContentSizeFitter = transform.Find("Image/Scroll View/Viewport/ContentText").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Image/Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Image/Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Image/Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Image/Scroll View").gameObject.GetComponent<Image>();
        detail.Title_Text = transform.Find("Image/Title").gameObject.GetComponent<Text>();
        detail.yesText_Text = transform.Find("Image/YES/yesText").gameObject.GetComponent<Text>();
        detail.YES_Image = transform.Find("Image/YES").gameObject.GetComponent<Image>();
        detail.YES_Button = transform.Find("Image/YES").gameObject.GetComponent<Button>();
        detail.noText_Text = transform.Find("Image/NO/noText").gameObject.GetComponent<Text>();
        detail.NO_Image = transform.Find("Image/NO").gameObject.GetComponent<Image>();
        detail.NO_Button = transform.Find("Image/NO").gameObject.GetComponent<Button>();
        detail.Image_Image = transform.Find("Image").gameObject.GetComponent<Image>();
        detail.Image_TargetSizeFitter = transform.Find("Image").gameObject.GetComponent<TargetSizeFitter>();
        detail.Image_UtilScale = transform.Find("Image").gameObject.GetComponent<UtilScale>();

    }
}
