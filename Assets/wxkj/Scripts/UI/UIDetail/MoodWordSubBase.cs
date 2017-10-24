using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoodWordSubBase : UISubBase
{
    public MoodWordSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Text_Text = transform.Find("Text").gameObject.GetComponent<Text>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
