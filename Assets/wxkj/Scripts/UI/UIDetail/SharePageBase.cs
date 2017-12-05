using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SharePageBase : UISceneBase
{
    public SharePageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.Icon_wx = transform.Find("Icon_wx").gameObject.GetComponent<Image>();
        detail.Text_wx = transform.Find("Text_wx").gameObject.GetComponent<Text>();
        detail.ShareWX_Button = transform.Find("Icon_wx").gameObject.GetComponent<Button>();

        detail.Icon_peng = transform.Find("Icon_peng").gameObject.GetComponent<Image>();
        detail.Text_peng = transform.Find("Text_peng").gameObject.GetComponent<Text>();
        detail.SharePeng_Button = transform.Find("Icon_peng").gameObject.GetComponent<Button>();
    }
}
