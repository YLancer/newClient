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
    //����齫��ѡ����
    public CheckBoxSub PlayerNum2_Jinchang;
    public CheckBoxSub PlayerNum4_Jinchang;
    public CheckBoxSub Round4_Jinchang;
    public CheckBoxSub Round8_Jinchang;
    public GridLayoutGroup ModeGrid_Jinchang;
    public CheckBoxSub Mode0_Fengpai_Jinchang;
    public CheckBoxSub Mode1_Sevenpair_Jinchang;    //��С��
    public CheckBoxSub Mode2_Baoting_Jinchang;
    public CheckBoxSub Mode3_Zimohu_Jinchang;
    public Image CreateButton_JinchangImage;
    public Button CreateButton_JinchangButton;
    //�����齫��ѡ����
    public CheckBoxSub PlayerNum2_Jiuyao;
    public CheckBoxSub PlayerNum4_Jiuyao;
    public CheckBoxSub Round4_Jiuyao;
    public CheckBoxSub Round8_Jiuyao;
    public GridLayoutGroup ModeGrid_Jiuyao;
    public CheckBoxSub Mode0_OneColorTrain_Jiuyao;    //��һɫ��һ���� ---->  �ĳ�������
    public Image CreateButton_JiuyaoImage;
    public Button CreateButton_JiuyaoButton;
    //�Ƶ����齫��ѡ����
    public CheckBoxSub PlayerNum2_Tuidaohu;
    public CheckBoxSub PlayerNum4_Tuidaohu;
    public CheckBoxSub Round4_Tuidaohu;
    public CheckBoxSub Round8_Tuidaohu;
    public GridLayoutGroup ModeGrid_Tuidaohu;
    public CheckBoxSub Mode0_Fengpai_Tuidaohu;      //����
    public CheckBoxSub Mode1_Daihui_Tuidaohu;       //����
    public CheckBoxSub Mode2_Baoting_Tuidaohu;      //����
    public CheckBoxSub Mode3_Zimohu_Tuidaohu;       //������
    public Image CreateButton_TuidaohuImage;
    public Button CreateButton_TuidaohuButton;


    //�ͻ����ഴ�����������ѡ����
    //����齫
    public CheckBoxSub Num2_jinchang;
    public CheckBoxSub Num4_jinchang;
    public GameObject Num2_jinchang_Select_2;
    public CheckBoxSub Num2_Round8_jinchang;
    public CheckBoxSub Num2_Round16_jinchang;
    public GameObject Num4_jinchang_Select_4;
    public CheckBoxSub Num4_Round8_jinchang;
    public CheckBoxSub Num4_Round16_jinchang;

    //�����齫
    public CheckBoxSub Num2_jiuyao;
    public CheckBoxSub Num4_jiuyao;
    public GameObject Num2_jiuyao_Select_2;
    public CheckBoxSub Num2_Round8_jiuyao;
    public CheckBoxSub Num2_Round16_jiuyao;
    public GameObject Num4_jiuyao_Select_4;
    public CheckBoxSub Num4_Round8_jiuyao;
    public CheckBoxSub Num4_Round16_jiuyao;

    //�Ƶ���
    public CheckBoxSub Num2_tuidaohu;
    public CheckBoxSub Num4_tuidaohu;
    public GameObject Num2_tuidaohu_Select_2;
    public CheckBoxSub Num2_Round8_tuidaohu;
    public CheckBoxSub Num2_Round16_tuidaohu;
    public GameObject Num4_tuidaohu_Select_4;
    public CheckBoxSub Num4_Round8_tuidaohu;
    public CheckBoxSub Num4_Round16_tuidaohu;
}