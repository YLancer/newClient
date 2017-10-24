using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoticeActiveTabSubBase : UISubBase
{
    public NoticeActiveTabSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.SelectFlag_Image = transform.Find("SelectFlag").gameObject.GetComponent<Image>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
