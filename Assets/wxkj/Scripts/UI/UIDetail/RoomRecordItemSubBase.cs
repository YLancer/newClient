using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomRecordItemSubBase : UISubBase
{
    public RoomRecordItemSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Name_Text = transform.Find("Name").gameObject.GetComponent<Text>();
        detail.Score_Text = transform.Find("Score").gameObject.GetComponent<Text>();

    }
}
