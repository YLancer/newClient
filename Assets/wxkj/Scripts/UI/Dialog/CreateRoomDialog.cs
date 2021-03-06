﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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

    private void Update()
    {
        OnShow_jinchang();
        OnShow_jiuyao();
        OnShow_tuidaohu();
    }

    private void OnShow_jinchang()
    {
        if(detail.Num2_jinchang.IsSelected)
        {
            detail.Num2_jinchang_Select_2.gameObject.SetActive(true);
            detail.Num4_jinchang_Select_4.gameObject.SetActive(false);
        }
        else if (detail.Num4_jinchang.IsSelected)
        {
            detail.Num2_jinchang_Select_2.gameObject.SetActive(false);
            detail.Num4_jinchang_Select_4.gameObject.SetActive(true);
        }
    }

    private void OnShow_jiuyao()
    {
        if (detail.Num2_jiuyao.IsSelected)
        {
            detail.Num2_jiuyao_Select_2.gameObject.SetActive(true);
            detail.Num4_jiuyao_Select_4.gameObject.SetActive(false);
        }
        else if (detail.Num4_jiuyao.IsSelected)
        {
            detail.Num4_jiuyao_Select_4.gameObject.SetActive(true);
            detail.Num2_jiuyao_Select_2.gameObject.SetActive(false);
        }
    }

    private void OnShow_tuidaohu()
    {
        if (detail.Num2_tuidaohu.IsSelected)
        {
            detail.Num2_tuidaohu_Select_2.gameObject.SetActive(true);
            detail.Num4_tuidaohu_Select_4.gameObject.SetActive(false);
        }
        else if (detail.Num4_tuidaohu.IsSelected)
        {
            detail.Num4_tuidaohu_Select_4.gameObject.SetActive(true);
            detail.Num2_tuidaohu_Select_2.gameObject.SetActive(false);
        }
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
        int vipRoomType = 2;
        int quanNum = 0;
        //bool is2Player = detail.PlayerNum2_Jinchang.IsSelected;
        //int vipRoomType = is2Player ? 2 : 4;
        //int quanNum = detail.Round4_Jinchang.IsSelected ? 1 : 2;  消耗房卡数量
        if (detail.Num2_jinchang.IsSelected)
        {
            vipRoomType = 2;
            if (detail.Num2_Round8_jinchang.IsSelected)
            {
                quanNum = 7;
            }
            else if (detail.Num2_Round16_jinchang.IsSelected){
                quanNum = 14;
            }
        }
        else if (detail.Num4_jinchang.IsSelected){
            vipRoomType = 4;
            if (detail.Num4_Round8_jinchang.IsSelected){
                quanNum = 5;
            }
            else if (detail.Num4_Round16_jinchang.IsSelected){
                quanNum = 10;
            }
        }
        int wanfa = MJUtils.MODE_CHI;
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
            wanfa = wanfa | MJUtils.MODE_BAOTING;
        }
        if (detail.Mode3_Zimohu_Jinchang.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_ZIMOHU;
        }

        print( "  final wanfa " + wanfa + "  btn  " + detail.Mode3_CheckBoxSub.IsSelected);

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
        //bool is2Player = detail.PlayerNum2_Jiuyao.IsSelected;
        //int vipRoomType = is2Player ? 2 : 4;
        //int quanNum = detail.Round4_Jiuyao.IsSelected ? 1 : 2;
        int vipRoomType = 2;
        int quanNum = 0;
        if (detail.Num2_jiuyao.IsSelected)
        {
            vipRoomType = 2;
            if (detail.Num2_Round8_jiuyao.IsSelected)
            {
                quanNum = 7;
            }
            else if (detail.Num2_Round16_jiuyao.IsSelected)
            {
                quanNum = 14;
            }
        }
        else if (detail.Num4_jiuyao.IsSelected)
        {
            vipRoomType = 4;
            if (detail.Num4_Round8_jiuyao.IsSelected)
            {
                quanNum = 5;
            }
            else if (detail.Num4_Round16_jiuyao.IsSelected)
            {
                quanNum = 10;
            }
        }
        int wanfa = MJUtils.MODE_SHUAIJIUYAO | MJUtils.MODE_FENGPAI | MJUtils.MODE_CHI | MJUtils.MODE_SEVENPAIR;
        if (detail.Mode0_OneColorTrain_Jiuyao.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_SHOUPAO;
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
        //bool is2Player = detail.PlayerNum2_Tuidaohu.IsSelected;
        //int vipRoomType = is2Player ? 2 : 4;
        //int quanNum = detail.Round4_Tuidaohu.IsSelected ? 1 : 2;
        int vipRoomType = 2;
        int quanNum = 0;
        if (detail.Num2_tuidaohu.IsSelected)
        {
            vipRoomType = 2;
            if (detail.Num2_Round8_tuidaohu.IsSelected)
            {
                quanNum = 7;
            }
            else if (detail.Num2_Round16_tuidaohu.IsSelected)
            {
                quanNum = 14;
            }
        }
        else if (detail.Num4_tuidaohu.IsSelected)
        {
            vipRoomType = 4;
            if (detail.Num4_Round8_tuidaohu.IsSelected)
            {
                quanNum = 5;
            }
            else if (detail.Num4_Round16_tuidaohu.IsSelected)
            {
                quanNum = 10;
            }
        }
        int wanfa = MJUtils.MODE_SEVENPAIR;
        if (detail.Mode0_Fengpai_Tuidaohu.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_FENGPAI;
        }
        if (detail.Mode1_Daihui_Tuidaohu.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_DAIHUI;
        }
        else //带会的玩法不能吃，不带会的玩法能吃
        {
            wanfa = wanfa | MJUtils.MODE_CHI;
        }
        if (detail.Mode2_Baoting_Tuidaohu.IsSelected)
        {
            wanfa = wanfa | MJUtils.MODE_BAOTING;
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
