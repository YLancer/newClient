using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class CreateRoomDialogDetail  
{
    public UtilScale Root_UtilScale;
    public Image CloseButton_Image;
    public Button CloseButton_Button;
    public Image CreateButton_Image;
    public Button CreateButton_Button;
    public CheckBoxSub PlayerNum2_CheckBoxSub;
    public CheckBoxSub PlayerNum4_CheckBoxSub;
    public CheckBoxSub Round4_CheckBoxSub;
    public CheckBoxSub Round8_CheckBoxSub;
    public GridLayoutGroup ModeGrid_GridLayoutGroup;
    public CheckBoxSub Mode0_CheckBoxSub;
    public CheckBoxSub Mode1_CheckBoxSub;
    public CheckBoxSub Mode2_CheckBoxSub;
    public CheckBoxSub Mode3_CheckBoxSub;
    public CheckBoxSub Mode4_CheckBoxSub;
    public CheckBoxSub Mode5_CheckBoxSub;
    public CheckBoxSub Mode6_CheckBoxSub;
    public CheckBoxSub Mode7_CheckBoxSub;

    public Image CloseButton_XiliangImage;
    public Button CloseButton_XiliangButton;
    //金昌麻将可选择项
    public CheckBoxSub PlayerNum2_Jinchang;
    public CheckBoxSub PlayerNum4_Jinchang;
    public CheckBoxSub Round4_Jinchang;
    public CheckBoxSub Round8_Jinchang;
    public GridLayoutGroup ModeGrid_Jinchang;
    public CheckBoxSub Mode0_Fengpai_Jinchang;
    public CheckBoxSub Mode1_Sevenpair_Jinchang;    //七小对
    public CheckBoxSub Mode2_Baoting_Jinchang;
    public CheckBoxSub Mode3_Zimohu_Jinchang;
    public Image CreateButton_JinchangImage;
    public Button CreateButton_JinchangButton;
    //九幺麻将可选择项
    public CheckBoxSub PlayerNum2_Jiuyao;
    public CheckBoxSub PlayerNum4_Jiuyao;
    public CheckBoxSub Round4_Jiuyao;
    public CheckBoxSub Round8_Jiuyao;
    public GridLayoutGroup ModeGrid_Jiuyao;
    public CheckBoxSub Mode0_OneColorTrain_Jiuyao;    //清一色、一条龙 ---->  改成自摸胡
    public Image CreateButton_JiuyaoImage;
    public Button CreateButton_JiuyaoButton;
    //推倒胡麻将可选择项
    public CheckBoxSub PlayerNum2_Tuidaohu;
    public CheckBoxSub PlayerNum4_Tuidaohu;
    public CheckBoxSub Round4_Tuidaohu;
    public CheckBoxSub Round8_Tuidaohu;
    public GridLayoutGroup ModeGrid_Tuidaohu;
    public CheckBoxSub Mode0_Fengpai_Tuidaohu;      //风牌
    public CheckBoxSub Mode1_Daihui_Tuidaohu;       //带会
    public CheckBoxSub Mode2_Baoting_Tuidaohu;      //报听
    public CheckBoxSub Mode3_Zimohu_Tuidaohu;       //自摸胡
    public Image CreateButton_TuidaohuImage;
    public Button CreateButton_TuidaohuButton;


    //客户另类创建房间需求的选择项
    //金昌麻将
    public ToggleGroup newBG_jinchang;
    public Toggle Toggle_2;
    public Image Background_Toggle_2;
    public Image Checkmark_Toggle_2;
    public Text name_Toggle_2;
    public ToggleGroup Select_2;
    public Text name_Toggle_Round8;
    public Toggle Toggle2_Round8;
    public Image Background_Toggle_Round8;
    public Image Checkmark_Toggle_Round8;
    public Text name_Toggle_Round16;
    public Toggle Toggle2_Round16;
    public Image Background_Toggle_Round16;
    public Image Checkmark_Toggle_Round16;

    public Toggle Toggle_4;
    public Image Background_Toggle_4;
    public Image Checkmark_Toggle_4;
    public Text name_Toggle_4;
    public ToggleGroup Select_4;
    public Text name_Toggle4_Round8;
    public Toggle Toggle4_Round8;
    public Image Background_Toggle4_Round8;
    public Image Checkmark_Toggle4_Round8;
    public Text name_Toggle4_Round16;
    public Toggle Toggle4_Round16;
    public Image Background_Toggle4_Round16;
    public Image Checkmark_Toggle4_Round16;

    //九幺麻将
    public ToggleGroup newBG_shuaijiuyao;
    public Toggle Toggle2_jiuyaoToggle;
    public Image Background_Toggle2_jiuyaoToggle;
    public Image Checkmark_Toggle2_jiuyaoToggle;
    public Text name_Toggle2_jiuyao;
    public ToggleGroup Select_2_jiuyao;
    public Text name_Round8_shuaijiuyao;
    public Toggle Round8_shuaijiuyao;
    public Image Background_Round8_shuaijiuyao;
    public Image Checkmark_Round8_shuaijiuyao;
    public Text name_Round16_shuaijiuyao;
    public Toggle Round16_shuaijiuyao;
    public Image Background_Round16_shuaijiuyao;
    public Image Checkmark_Round16_shuaijiuyao;

    public Toggle Toggle4_jiuyaoToggle;
    public Image Background_Toggle4_jiuyaoToggle;
    public Image Checkmark_Toggle4_jiuyaoToggle;
    public Text name_Toggle4_jiuyao;
    public ToggleGroup Select_4_jiuyao;
    public Text name4_Round8_shuaijiuyao;
    public Toggle Toggle4_Round8_shuaijiuyao;
    public Image Background4_Round8_shuaijiuyao;
    public Image Checkmark4_Round8_shuaijiuyao;
    public Text name16_Round8_shuaijiuyao;
    public Toggle Toggle4_Round16_shuaijiuyao;
    public Image Background16_Round8_shuaijiuyao;
    public Image Checkmark16_Round8_shuaijiuyao;

    //推倒胡
    public ToggleGroup newBG_tuidaohu;
    public Toggle Toggle2_tuidaohu;
    public Image Background_Toggle2_tuidaohu;
    public Image Checkmark_Toggle2_tuidaohu;
    public Text name_Toggle2_tuidaohu;
    public ToggleGroup Select_2_tuidaohu;
    public Text name_Round8_tuidaohu;
    public Toggle Round8_tuidaohu;
    public Image Background_Round8_tuidaohu;
    public Image Checkmark_Round8_tuidaohu;
    public Text name_Round16_tuidaohu;
    public Toggle Round16_tuidaohu;
    public Image Background_Round16_tuidaohu;
    public Image Checkmark_Round16_tuidaohu;

    public Toggle Toggle4_tuidaohuToggle;
    public Image Background_Toggle4_tuidaohuToggle;
    public Image Checkmark_Toggle4_tuidaohuToggle;
    public Text name_Toggle4_tuidaohu;
    public ToggleGroup Select_4_tuidaohu;
    public Text name4_Round8_tuidaohu;
    public Toggle Toggle4_Round8_tuidaohu;
    public Image Background4_Round8_tuidaohu;
    public Image Checkmark4_Round8_tuidaohu;
    public Text name16_Round8_tuidaohu;
    public Toggle Toggle4_Round16_tuidaohu;
    public Image Background16_Round8_tuidaohu;
    public Image Checkmark16_Round8_tuidaohu;
}