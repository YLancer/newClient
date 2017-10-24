using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalRecrodSubBase : UISubBase
{
    public TotalRecrodSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.cache_Image = transform.Find("Rank/cache").gameObject.GetComponent<Image>();
        detail.cache_Image = transform.Find("Rank/cache").gameObject.GetComponent<Image>();
        detail.Rank_ImageText = transform.Find("Rank").gameObject.GetComponent<ImageText>();
        detail.RoomName_Text = transform.Find("RoomName").gameObject.GetComponent<Text>();
        detail.Time_Text = transform.Find("Time").gameObject.GetComponent<Text>();
        detail.Grid_GridLayoutGroup = transform.Find("Grid").gameObject.GetComponent<GridLayoutGroup>();
        detail.RoomRecordItemSub_RoomRecordItemSub = transform.Find("RoomRecordItemSub").gameObject.GetComponent<RoomRecordItemSub>();
        detail.Button_Image = transform.Find("Button").gameObject.GetComponent<Image>();
        detail.Button_Button = transform.Find("Button").gameObject.GetComponent<Button>();

    }
}
