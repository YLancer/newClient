using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingPageBase : UISceneBase
{
    public LoadingPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Background_Image = transform.Find("LoadingSlider/Background").gameObject.GetComponent<Image>();
        detail.Fill_Image = transform.Find("LoadingSlider/Fill Area/Fill").gameObject.GetComponent<Image>();
        detail.Text_Text = transform.Find("LoadingSlider/Text").gameObject.GetComponent<Text>();
        detail.Text_Outline = transform.Find("LoadingSlider/Text").gameObject.GetComponent<Outline>();
        detail.LoadingSlider_Slider = transform.Find("LoadingSlider").gameObject.GetComponent<Slider>();
        detail.LoadingCircle_UIItem = transform.Find("LoadingCircle").gameObject.GetComponent<UIItem>();

    }
}
