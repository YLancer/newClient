using UnityEngine;
using System.Collections;

public class RoundDetailPage : RoundDetailPageBase
{
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
    }
}
