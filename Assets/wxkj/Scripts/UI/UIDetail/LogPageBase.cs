using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LogPageBase : UISceneBase
{
    public LogPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Text_Text = transform.Find("HideButton/Text").gameObject.GetComponent<Text>();
        detail.HideButton_Image = transform.Find("HideButton").gameObject.GetComponent<Image>();
        detail.HideButton_Button = transform.Find("HideButton").gameObject.GetComponent<Button>();
        detail.Text_Text = transform.Find("ShowButton/Text").gameObject.GetComponent<Text>();
        detail.ShowButton_Image = transform.Find("ShowButton").gameObject.GetComponent<Image>();
        detail.ShowButton_Button = transform.Find("ShowButton").gameObject.GetComponent<Button>();
        detail.Text_Text = transform.Find("ClearButton/Text").gameObject.GetComponent<Text>();
        detail.ClearButton_Image = transform.Find("ClearButton").gameObject.GetComponent<Image>();
        detail.ClearButton_Button = transform.Find("ClearButton").gameObject.GetComponent<Button>();
        detail.ContentText_Text = transform.Find("Scroll View/Viewport/ContentText").gameObject.GetComponent<Text>();
        detail.ContentText_ContentSizeFitter = transform.Find("Scroll View/Viewport/ContentText").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.Handle_Image = transform.Find("Scroll View/Scrollbar Vertical/Sliding Area/Handle").gameObject.GetComponent<Image>();
        detail.ScrollbarVertical_Image = transform.Find("Scroll View/Scrollbar Vertical").gameObject.GetComponent<Image>();
        detail.ScrollbarVertical_Scrollbar = transform.Find("Scroll View/Scrollbar Vertical").gameObject.GetComponent<Scrollbar>();
        detail.ScrollView_ScrollRect = transform.Find("Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Scroll View").gameObject.GetComponent<Image>();
        detail.Text_Text = transform.Find("DumpButton/Text").gameObject.GetComponent<Text>();
        detail.DumpButton_Image = transform.Find("DumpButton").gameObject.GetComponent<Image>();
        detail.DumpButton_Button = transform.Find("DumpButton").gameObject.GetComponent<Button>();
        detail.Placeholder_Text = transform.Find("InputField/Placeholder").gameObject.GetComponent<Text>();
        detail.Text_Text = transform.Find("InputField/Text").gameObject.GetComponent<Text>();
        detail.InputField_Image = transform.Find("InputField").gameObject.GetComponent<Image>();
        detail.InputField_InputField = transform.Find("InputField").gameObject.GetComponent<InputField>();
        detail.Text_Text = transform.Find("AuthButton/Text").gameObject.GetComponent<Text>();
        detail.AuthButton_Image = transform.Find("AuthButton").gameObject.GetComponent<Image>();
        detail.AuthButton_Button = transform.Find("AuthButton").gameObject.GetComponent<Button>();

    }
}
