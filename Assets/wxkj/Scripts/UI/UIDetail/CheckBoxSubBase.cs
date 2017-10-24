using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckBoxSubBase : UISubBase
{
    public CheckBoxSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Name_Text = transform.Find("Name").gameObject.GetComponent<Text>();
        detail.SelectFlag_Image = transform.Find("SelectFlag").gameObject.GetComponent<Image>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
