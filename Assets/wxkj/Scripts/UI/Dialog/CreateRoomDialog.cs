using UnityEngine;
using System.Collections;

public class CreateRoomDialog : CreateRoomDialogBase
{
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.CreateButton_Button.onClick.AddListener(OnClickCreate);

        detail.CloseButton_XiliangButton.onClick.AddListener(OnBackPressed);
        detail.CreateButton_JinchangButton.onClick.AddListener(OnJinChangClickCreate);
        detail.CreateButton_JiuyaoButton.onClick.AddListener(OnJiuYaoClickCreate);
        detail.CreateButton_TuidaohuButton.onClick.AddListener(OnTuiDaoHuClickCreate);
    }

    private void OnClickCreate()  
    {
        Game.SoundManager.PlayClick();
        bool is2Player = detail.PlayerNum2_CheckBoxSub.IsSelected;
        int vipRoomType = is2Player ? 2 : 4;
        int quanNum = detail.Round4_CheckBoxSub.IsSelected ? 4 : 8;
        int wanfa = 0;
        if (detail.Mode0_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_ZHA;
        }
        if (detail.Mode1_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_37JIA;
        }
        if (detail.Mode2_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_ZHIDUI;
        }
        if (detail.Mode3_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_DANDIAOJIA;
        }
        if (detail.Mode4_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_DAFENG;
        }
        if (detail.Mode5_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_HONGZHONG;
        }
        if (detail.Mode6_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_DAILOU;
        }
        if (detail.Mode7_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_JIAHU;
        }

        Game.SocketGame.DoCreateVipRoom(vipRoomType, quanNum, wanfa, (result) =>
        {
            OnBackPressed();
            // 根据用户权限来决定不同逻辑
            if (!Game.Instance.createMultiRoom)
            {
                if (result.roomList.Count > 0)
                {
                    Game.SocketGame.DoEnterVipRoom(result.roomList[0].code);
                }
            }
        });
    }

    private void OnJinChangClickCreate()  // 创建金昌麻将房间   
    {
        Game.SoundManager.PlayClick();
        bool is2Player = detail.PlayerNum2_Jinchang.IsSelected;
        int vipRoomType = is2Player ? 2 : 4;
        int quanNum = detail.Round4_Jinchang.IsSelected ? 4 : 8;
        int wanfa = 0;
        if (detail.Mode0_Fengpai_Jinchang.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_FENGPAI;
        }
        if (detail.Mode1_Sevenpair_Jinchang.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_SEVENPAIR;
        }
        if (detail.Mode2_Baoting_Jinchang.IsSelected)
        {
            wanfa = wanfa | MJUtils.ACT_TING;
        }
        if (detail.Mode3_CheckBoxSub.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_ZIMOHU;
        }

        Game.SocketGame.DoCreateVipRoom(vipRoomType, quanNum, wanfa, (result) =>{
            OnBackPressed();
            // 根据用户权限来决定不同逻辑
            if (!Game.Instance.createMultiRoom){
                if (result.roomList.Count > 0){
                    Game.SocketGame.DoEnterVipRoom(result.roomList[0].code);
                }
            }
        } );
    }

    private void OnJiuYaoClickCreate()  // 创建九幺麻将房间   
    {
        Game.SoundManager.PlayClick();
        bool is2Player = detail.PlayerNum2_Jiuyao.IsSelected;
        int vipRoomType = is2Player ? 2 : 4;
        int quanNum = detail.Round4_Jiuyao.IsSelected ? 4 : 8;
        int wanfa = 0;
        if (detail.Mode0_OneColorTrain_Jiuyao.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_ONECOLORTRAIN;
        }

        Game.SocketGame.DoCreateVipRoom(vipRoomType, quanNum, wanfa, (result) => {
            OnBackPressed();
            // 根据用户权限来决定不同逻辑
            if (!Game.Instance.createMultiRoom)
            {
                if (result.roomList.Count > 0)
                {
                    Game.SocketGame.DoEnterVipRoom(result.roomList[0].code);
                }
            }
        });
    }

    private void OnTuiDaoHuClickCreate()  // 创建推倒胡麻将房间   
    {
        Game.SoundManager.PlayClick();
        bool is2Player = detail.PlayerNum2_Tuidaohu.IsSelected;
        int vipRoomType = is2Player ? 2 : 4;
        int quanNum = detail.Round4_Tuidaohu.IsSelected ? 4 : 8;
        int wanfa = 0;
        if (detail.Mode0_Fengpai_Tuidaohu.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_FENGPAI;
        }
        if (detail.Mode1_Daihui_Tuidaohu.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_DAIHUI;
        }
        if (detail.Mode2_Baoting_Tuidaohu.IsSelected)
        {
            wanfa = wanfa | MJUtils.ACT_TING;
        }
        if (detail.Mode3_Zimohu_Tuidaohu.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_ZIMOHU;
        }

        Game.SocketGame.DoCreateVipRoom(vipRoomType, quanNum, wanfa, (result) => {
            OnBackPressed();
            // 根据用户权限来决定不同逻辑
            if (!Game.Instance.createMultiRoom)
            {
                if (result.roomList.Count > 0)
                {
                    Game.SocketGame.DoEnterVipRoom(result.roomList[0].code);
                }
            }
        });
    }
}
