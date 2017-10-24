using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoodPageBase : UISceneBase
{
    public MoodPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.Image_Image = transform.Find("MoodButton/Image").gameObject.GetComponent<Image>();
        detail.MoodButton_Image = transform.Find("MoodButton").gameObject.GetComponent<Image>();
        detail.MoodButton_Button = transform.Find("MoodButton").gameObject.GetComponent<Button>();
        detail.Image_Image = transform.Find("WordButton/Image").gameObject.GetComponent<Image>();
        detail.WordButton_Image = transform.Find("WordButton").gameObject.GetComponent<Image>();
        detail.WordButton_Button = transform.Find("WordButton").gameObject.GetComponent<Button>();
        detail.SendButton_Image = transform.Find("SendButton").gameObject.GetComponent<Image>();
        detail.SendButton_Button = transform.Find("SendButton").gameObject.GetComponent<Button>();
        detail.Placeholder_Text = transform.Find("InputField/Placeholder").gameObject.GetComponent<Text>();
        detail.Text_Text = transform.Find("InputField/Text").gameObject.GetComponent<Text>();
        detail.InputField_Image = transform.Find("InputField").gameObject.GetComponent<Image>();
        detail.InputField_InputField = transform.Find("InputField").gameObject.GetComponent<InputField>();
        detail.MoodGrid_GridLayoutGroup = transform.Find("MoodGrid").gameObject.GetComponent<GridLayoutGroup>();
        detail.Content_VerticalLayoutGroup = transform.Find("WordGrid/Viewport/Content").gameObject.GetComponent<VerticalLayoutGroup>();
        detail.Content_ContentSizeFitter = transform.Find("WordGrid/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("WordGrid/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("WordGrid/Viewport").gameObject.GetComponent<Image>();
        detail.WordGrid_ScrollRect = transform.Find("WordGrid").gameObject.GetComponent<ScrollRect>();
        detail.WordGrid_Image = transform.Find("WordGrid").gameObject.GetComponent<Image>();
        detail.MoodWordSub_LayoutElement = transform.Find("MoodWordSub").gameObject.GetComponent<LayoutElement>();
        detail.MoodWordSub_MoodWordSub = transform.Find("MoodWordSub").gameObject.GetComponent<MoodWordSub>();

    }
}
