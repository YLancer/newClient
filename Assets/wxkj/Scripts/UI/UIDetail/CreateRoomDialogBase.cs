using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CreateRoomDialogBase : UIDialogBase    
{
    public CreateRoomDialogDetail detail;
    public override void SetAllMemberValue()
    {
       
        detail.Root_UtilScale = transform.Find("Root").gameObject.GetComponent<UtilScale>();
        //原界面层显示
        detail.CloseButton_Image = transform.Find("Root/CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("Root/CloseButton").gameObject.GetComponent<Button>();
        detail.CreateButton_Image = transform.Find("Root/CreateButton").gameObject.GetComponent<Image>();
        detail.CreateButton_Button = transform.Find("Root/CreateButton").gameObject.GetComponent<Button>();
        detail.PlayerNum2_CheckBoxSub = transform.Find("Root/PlayerNum2").gameObject.GetComponent<CheckBoxSub>();
        detail.PlayerNum4_CheckBoxSub = transform.Find("Root/PlayerNum4").gameObject.GetComponent<CheckBoxSub>();
        detail.Round4_CheckBoxSub = transform.Find("Root/Round4").gameObject.GetComponent<CheckBoxSub>();
        detail.Round8_CheckBoxSub = transform.Find("Root/Round8").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode0_CheckBoxSub = transform.Find("Root/ModeGrid/Mode0").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode1_CheckBoxSub = transform.Find("Root/ModeGrid/Mode1").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode2_CheckBoxSub = transform.Find("Root/ModeGrid/Mode2").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode3_CheckBoxSub = transform.Find("Root/ModeGrid/Mode3").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode4_CheckBoxSub = transform.Find("Root/ModeGrid/Mode4").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode5_CheckBoxSub = transform.Find("Root/ModeGrid/Mode5").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode6_CheckBoxSub = transform.Find("Root/ModeGrid/Mode6").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode7_CheckBoxSub = transform.Find("Root/ModeGrid/Mode7").gameObject.GetComponent<CheckBoxSub>();
        detail.ModeGrid_GridLayoutGroup = transform.Find("Root/ModeGrid").gameObject.GetComponent<GridLayoutGroup>();



        detail.CloseButton_XiliangImage = transform.Find("change_root/CloseButton_xiliang").gameObject.GetComponent<Image>();
        detail.CloseButton_XiliangButton = transform.Find("change_root/CloseButton_xiliang").gameObject.GetComponent<Button>();
        //金昌麻将界面显示
        detail.PlayerNum2_Jinchang = transform.Find("change_root/PlayerNum2_jinchang").gameObject.GetComponent<CheckBoxSub>();
        detail.PlayerNum4_Jinchang= transform.Find("change_root/PlayerNum4_jinchang").gameObject.GetComponent<CheckBoxSub>();
        detail.Round4_Jinchang = transform.Find("change_root/Round4_jinchang").gameObject.GetComponent<CheckBoxSub>();
        detail.Round8_Jinchang = transform.Find("change_root/Round8_jinchang").gameObject.GetComponent<CheckBoxSub>();
        detail.ModeGrid_Jinchang = transform.Find("change_root/ModeGrid_jinchang").gameObject.GetComponent<GridLayoutGroup>();
        detail.Mode0_Fengpai_Jinchang = transform.Find("change_root/Mode_Fengpai").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode1_Sevenpair_Jinchang = transform.Find("change_root/Mode1_Sevenpair").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode2_Baoting_Jinchang = transform.Find("change_root/Mode2_Baoting").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode3_Zimohu_Jinchang = transform.Find("change_root/Mode3_Zimohu").gameObject.GetComponent<CheckBoxSub>();
        detail.CreateButton_JinchangImage = transform.Find("change_root/CreateButton_jinchang").gameObject.GetComponent<Image>();
        detail.CreateButton_JinchangButton = transform.Find("change_root/CreateButton_jinchang").gameObject.GetComponent<Button>();
        //九幺麻将界面
        detail.PlayerNum2_Jiuyao = transform.Find("change_root/PlayerNum2_shuaijiuyao").gameObject.GetComponent<CheckBoxSub>();
        detail.PlayerNum4_Jiuyao = transform.Find("change_root/PlayerNum4_shuaijiuyao").gameObject.GetComponent<CheckBoxSub>();
        detail.Round4_Jiuyao = transform.Find("change_root/Round4_shuaijiuyao").gameObject.GetComponent<CheckBoxSub>();
        detail.Round8_Jiuyao = transform.Find("change_root/Round8_shuaijiuyao").gameObject.GetComponent<CheckBoxSub>();
        detail.ModeGrid_Jiuyao = transform.Find("change_root/ModeGrid_shuaijiuyao").gameObject.GetComponent<GridLayoutGroup>();
        detail.Mode0_OneColorTrain_Jiuyao = transform.Find("change_root/Mode_qing&long").gameObject.GetComponent<CheckBoxSub>();
        detail.CreateButton_JiuyaoImage = transform.Find("change_root/CreateButton_shuaijiuyao").gameObject.GetComponent<Image>();
        detail.CreateButton_JiuyaoButton= transform.Find("change_root/CreateButton_shuaijiuyao").gameObject.GetComponent<Button>();
        //推倒胡麻将界面
        detail.PlayerNum2_Tuidaohu = transform.Find("change_root/PlayerNum2_tuidaohu").gameObject.GetComponent<CheckBoxSub>();
        detail.PlayerNum4_Tuidaohu = transform.Find("change_root/PlayerNum4_tuidaohu").gameObject.GetComponent<CheckBoxSub>();
        detail.Round4_Tuidaohu = transform.Find("change_root/Round4_tuidaohu").gameObject.GetComponent<CheckBoxSub>();
        detail.Round8_Tuidaohu = transform.Find("change_root/Round8_tuidaohu").gameObject.GetComponent<CheckBoxSub>();
        detail.ModeGrid_Tuidaohu = transform.Find("change_root/ModeGrid_tuidaohu").gameObject.GetComponent<GridLayoutGroup>();
        detail.Mode0_Fengpai_Tuidaohu = transform.Find("change_root/Mode0_daifeng").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode1_Daihui_Tuidaohu = transform.Find("change_root/Mode1_daihui").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode2_Baoting_Tuidaohu = transform.Find("change_root/Mode2_baoting").gameObject.GetComponent<CheckBoxSub>();
        detail.Mode3_Zimohu_Tuidaohu = transform.Find("change_root/Mode3_zimo").gameObject.GetComponent<CheckBoxSub>();
        detail.CreateButton_TuidaohuImage = transform.Find("change_root/CreateButton_tuidaohu").gameObject.GetComponent<Image>();
        detail.CreateButton_TuidaohuButton = transform.Find("change_root/CreateButton_tuidaohu").gameObject.GetComponent<Button>();

    }
}
