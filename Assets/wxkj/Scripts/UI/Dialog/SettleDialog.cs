using UnityEngine;
using System.Collections;
using System;
using packet.mj;

public class SettleDialog : SettleDialogBase
{
    public override void InitializeScene()
    {
        base.InitializeScene();

        detail.ShareButton_Button.onClick.AddListener(()=> {
            Game.AndroidUtil.ShareImage();
        });

        detail.BackButton_Button.onClick.AddListener(() =>
        {
            Game.SoundManager.PlayClose();
            Game.Instance.state = GameState.Hall;
            OnBackPressed();
        });
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);

        SetupUI();
    }

    private void SetupUI()
    {
        Clear(detail.Grid_GridLayoutGroup);

        if (null != RoomMgr.finalSettleSyn)
        {
            detail.Time_Text.text = RoomMgr.finalSettleSyn.settleDate;

            int maxHu = 0;
            int maxPao = 0;
            foreach (PlayerFinalResult result in RoomMgr.finalSettleSyn.detail)
            {
                maxHu = Mathf.Max(maxHu, result.huCount);
                maxPao = Mathf.Max(maxPao, result.paoCount);
            }

            foreach (PlayerFinalResult result in RoomMgr.finalSettleSyn.detail)
            {
                GameObject child = AddChild(detail.Grid_GridLayoutGroup, detail.AccountSub_AccountSub);
                AccountSub sub = child.GetComponent<AccountSub>();
                sub.SetValue(result, maxHu, maxPao);
            }
        }
    }
}
