using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
}