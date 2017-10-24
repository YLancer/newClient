using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoticeActivePageBase : UISceneBase
{
    public NoticeActivePageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.ActiveButton_NoticeActiveTabSub = transform.Find("ActiveButton").gameObject.GetComponent<NoticeActiveTabSub>();
        detail.NoticeButton_NoticeActiveTabSub = transform.Find("NoticeButton").gameObject.GetComponent<NoticeActiveTabSub>();
        detail.ActiveSub_ActiveSub = transform.Find("ActiveSub").gameObject.GetComponent<ActiveSub>();
        detail.NoticeSub_NoticeSub = transform.Find("NoticeSub").gameObject.GetComponent<NoticeSub>();
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();

    }
}
