using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomRecordSubBase : UISubBase
{
    public RoomRecordSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.RoomName_Text = transform.Find("RoomName").gameObject.GetComponent<Text>();
        detail.Time_Text = transform.Find("Time").gameObject.GetComponent<Text>();
        detail.Rank_ImageText = transform.Find("Rank").gameObject.GetComponent<ImageText>();
        detail.Grid_GridLayoutGroup = transform.Find("Grid").gameObject.GetComponent<GridLayoutGroup>();
        detail.RoomRecordItemSub_RoomRecordItemSub = transform.Find("RoomRecordItemSub").gameObject.GetComponent<RoomRecordItemSub>();

    }
}
