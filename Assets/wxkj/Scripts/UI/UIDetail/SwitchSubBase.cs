using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchSubBase : UISubBase
{
    public SwitchSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.On_Image = transform.Find("On").gameObject.GetComponent<Image>();
        detail.Off_Image = transform.Find("Off").gameObject.GetComponent<Image>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
