using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundDetailSubBase : UISubBase
{
    public RoundDetailSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.WinName_Text = transform.Find("WinName").gameObject.GetComponent<Text>();
        detail.Multi_Text = transform.Find("Multi").gameObject.GetComponent<Text>();
        detail.Score_Text = transform.Find("Score").gameObject.GetComponent<Text>();
        detail.PlayerPos_Text = transform.Find("PlayerPos").gameObject.GetComponent<Text>();

    }
}
