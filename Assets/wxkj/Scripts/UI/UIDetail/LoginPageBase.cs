using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using cn.sharesdk.unity3d; //wx 2017.7.22
public class LoginPageBase : UISceneBase
{
    public LoginPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.WxButton_Image = transform.Find("WxButton").gameObject.GetComponent<Image>();
        detail.WxButton_Button = transform.Find("WxButton").gameObject.GetComponent<Button>();
        detail.AccountButton_Image = transform.Find("AccountButton").gameObject.GetComponent<Image>();
        detail.AccountButton_Button = transform.Find("AccountButton").gameObject.GetComponent<Button>();
        
        //detail.ShareSDK =  GameObject .Find("ShardSDK").GetComponent<ShareSDK>();
    }
}
