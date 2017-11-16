using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class JiuYaoPageBase : UISceneBase
{
    public JiuYaoPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Text_tishi = transform.Find("Text_tishi").gameObject.GetComponent<Text>();
        detail.marksure_Btn = transform.Find("marksure_Btn").gameObject.GetComponent<Button>();
    }
}
