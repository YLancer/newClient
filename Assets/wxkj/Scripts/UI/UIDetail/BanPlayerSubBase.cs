using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BanPlayerSubBase : UISubBase
{
    public BanPlayerSubDetail detail;

    public override void SetAllMemberValue()
    {
        detail.BanSub_BanSub = transform.Find("BanSub").gameObject.GetComponent<BanSub>();
        detail.Grid_GridLayoutGroup = transform.Find("Grid").gameObject.GetComponent<GridLayoutGroup>();

    }
}
