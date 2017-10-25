using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayPageBase : UISceneBase
{
    public PlayPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CancelHangUpBtn_Image = transform.Find("HangUp/CancelHangUpBtn").gameObject.GetComponent<Image>();
        detail.CancelHangUpBtn_Button = transform.Find("HangUp/CancelHangUpBtn").gameObject.GetComponent<Button>();
        detail.HangUp_UIItem = transform.Find("HangUp").gameObject.GetComponent<UIItem>();
        detail.DropButton_Image = transform.Find("DropButton").gameObject.GetComponent<Image>();
        detail.DropButton_Button = transform.Find("DropButton").gameObject.GetComponent<Button>();
        detail.ExitButton_Image = transform.Find("ExitButton").gameObject.GetComponent<Image>();
        detail.ExitButton_Button = transform.Find("ExitButton").gameObject.GetComponent<Button>();
        detail.PopupButton_Image = transform.Find("PopupButton").gameObject.GetComponent<Image>();
        detail.PopupButton_Button = transform.Find("PopupButton").gameObject.GetComponent<Button>();
        detail.DismissButton_Image = transform.Find("DismissButton").gameObject.GetComponent<Image>();
        detail.DismissButton_Button = transform.Find("DismissButton").gameObject.GetComponent<Button>();
        detail.Image_Image = transform.Find("Image (1)/Image").gameObject.GetComponent<Image>();
        detail.CardNum_Text = transform.Find("Image (1)/CardNum").gameObject.GetComponent<Text>();
        detail.Time_Text = transform.Find("Image (1)/Time").gameObject.GetComponent<Text>();
        detail.Image1_Image = transform.Find("Image (1)").gameObject.GetComponent<Image>();
        detail.PassButton_Image = transform.Find("CtrlPanel/PassButton").gameObject.GetComponent<Image>();
        detail.PassButton_Button = transform.Find("CtrlPanel/PassButton").gameObject.GetComponent<Button>();
        detail.GangButton_Image = transform.Find("CtrlPanel/Root/GangButton").gameObject.GetComponent<Image>();
        detail.GangButton_Button = transform.Find("CtrlPanel/Root/GangButton").gameObject.GetComponent<Button>();
        detail.GangButton_LayoutElement = transform.Find("CtrlPanel/Root/GangButton").gameObject.GetComponent<LayoutElement>();
        detail.PengButton_Image = transform.Find("CtrlPanel/Root/PengButton").gameObject.GetComponent<Image>();
        detail.PengButton_Button = transform.Find("CtrlPanel/Root/PengButton").gameObject.GetComponent<Button>();
        detail.PengButton_LayoutElement = transform.Find("CtrlPanel/Root/PengButton").gameObject.GetComponent<LayoutElement>();
        detail.ChiButton_Image = transform.Find("CtrlPanel/Root/ChiButton").gameObject.GetComponent<Image>();
        detail.ChiButton_Button = transform.Find("CtrlPanel/Root/ChiButton").gameObject.GetComponent<Button>();
        detail.ChiButton_LayoutElement = transform.Find("CtrlPanel/Root/ChiButton").gameObject.GetComponent<LayoutElement>();
        detail.HuButton_Image = transform.Find("CtrlPanel/Root/HuButton").gameObject.GetComponent<Image>();
        detail.HuButton_Button = transform.Find("CtrlPanel/Root/HuButton").gameObject.GetComponent<Button>();
        detail.HuButton_LayoutElement = transform.Find("CtrlPanel/Root/HuButton").gameObject.GetComponent<LayoutElement>();
        detail.TingButton_Image = transform.Find("CtrlPanel/Root/TingButton").gameObject.GetComponent<Image>();
        detail.TingButton_Button = transform.Find("CtrlPanel/Root/TingButton").gameObject.GetComponent<Button>();
        detail.TingButton_LayoutElement = transform.Find("CtrlPanel/Root/TingButton").gameObject.GetComponent<LayoutElement>();
        detail.TingPengButton_Image = transform.Find("CtrlPanel/Root/TingPengButton").gameObject.GetComponent<Image>();
        detail.TingPengButton_Button = transform.Find("CtrlPanel/Root/TingPengButton").gameObject.GetComponent<Button>();
        detail.TingPengButton_LayoutElement = transform.Find("CtrlPanel/Root/TingPengButton").gameObject.GetComponent<LayoutElement>();
        detail.TingChiButton_Image = transform.Find("CtrlPanel/Root/TingChiButton").gameObject.GetComponent<Image>();
        detail.TingChiButton_Button = transform.Find("CtrlPanel/Root/TingChiButton").gameObject.GetComponent<Button>();
        detail.TingChiButton_LayoutElement = transform.Find("CtrlPanel/Root/TingChiButton").gameObject.GetComponent<LayoutElement>();
        detail.ZhiduiButton_Image = transform.Find("CtrlPanel/Root/ZhiduiButton").gameObject.GetComponent<Image>();
        detail.ZhiduiButton_Button = transform.Find("CtrlPanel/Root/ZhiduiButton").gameObject.GetComponent<Button>();
        detail.ZhiduiButton_LayoutElement = transform.Find("CtrlPanel/Root/ZhiduiButton").gameObject.GetComponent<LayoutElement>();
        detail.Root_HorizontalLayoutGroup = transform.Find("CtrlPanel/Root").gameObject.GetComponent<HorizontalLayoutGroup>();
        detail.CtrlPanel_UIItem = transform.Find("CtrlPanel").gameObject.GetComponent<UIItem>();
        detail.SelectRoot_HorizontalLayoutGroup = transform.Find("SelectPanel/SelectRoot").gameObject.GetComponent<HorizontalLayoutGroup>();
        detail.CancelButton_Image = transform.Find("SelectPanel/CancelButton").gameObject.GetComponent<Image>();
        detail.CancelButton_Button = transform.Find("SelectPanel/CancelButton").gameObject.GetComponent<Button>();
        detail.SelectPanel_UIItem = transform.Find("SelectPanel").gameObject.GetComponent<UIItem>();
        detail.GroupButton_Image = transform.Find("GroupButton").gameObject.GetComponent<Image>();
        detail.GroupButton_Button = transform.Find("GroupButton").gameObject.GetComponent<Button>();
        detail.GroupButton_GridLayoutGroup = transform.Find("GroupButton").gameObject.GetComponent<GridLayoutGroup>();
        detail.PlayerSub0_PlayerSub = transform.Find("PlayerSub0").gameObject.GetComponent<PlayerSub>();
        detail.PlayerSub1_PlayerSub = transform.Find("PlayerSub1").gameObject.GetComponent<PlayerSub>();
        detail.PlayerSub2_PlayerSub = transform.Find("PlayerSub2").gameObject.GetComponent<PlayerSub>();
        detail.PlayerSub3_PlayerSub = transform.Find("PlayerSub3").gameObject.GetComponent<PlayerSub>();
        detail.EffectPos_EffectPos = transform.Find("EffectPos").gameObject.GetComponent<EffectPos>();
        detail.VoiceButton_Image = transform.Find("RightBottom/VoiceButton").gameObject.GetComponent<Image>();
        detail.VoiceButton_Button = transform.Find("RightBottom/VoiceButton").gameObject.GetComponent<Button>();
        detail.ChatButton_Image = transform.Find("RightBottom/ChatButton").gameObject.GetComponent<Image>();
        detail.ChatButton_Button = transform.Find("RightBottom/ChatButton").gameObject.GetComponent<Button>();
        detail.DetailButton_Image = transform.Find("RightBottom/DetailButton").gameObject.GetComponent<Image>();
        detail.DetailButton_Button = transform.Find("RightBottom/DetailButton").gameObject.GetComponent<Button>();
        detail.GameRoundText_Text = transform.Find("GameRoundButton/GameRoundText").gameObject.GetComponent<Text>();
        detail.GameRoundButton_Image = transform.Find("GameRoundButton").gameObject.GetComponent<Image>();
        detail.GameRoundButton_Button = transform.Find("GameRoundButton").gameObject.GetComponent<Button>();
        detail.WXButton_Image = transform.Find("WXButton").gameObject.GetComponent<Image>();
        detail.WXButton_Button = transform.Find("WXButton").gameObject.GetComponent<Button>();
        detail.AudioSource_MicrophoneInput = transform.Find("AudioSource").gameObject.GetComponent<MicrophoneInput>();
        detail.ClosePopButton_Image = transform.Find("PopupMenu/ClosePopButton").gameObject.GetComponent<Image>();
        detail.ClosePopButton_Button = transform.Find("PopupMenu/ClosePopButton").gameObject.GetComponent<Button>();
        detail.Image_Image = transform.Find("PopupMenu/HostedButton/Image").gameObject.GetComponent<Image>();
        detail.HostedButton_Image = transform.Find("PopupMenu/HostedButton").gameObject.GetComponent<Image>();
        detail.HostedButton_Button = transform.Find("PopupMenu/HostedButton").gameObject.GetComponent<Button>();
        detail.Image_Image = transform.Find("PopupMenu/SettingButton/Image").gameObject.GetComponent<Image>();
        detail.SettingButton_Image = transform.Find("PopupMenu/SettingButton").gameObject.GetComponent<Image>();
        detail.SettingButton_Button = transform.Find("PopupMenu/SettingButton").gameObject.GetComponent<Button>();
        detail.PopupMenu_UIItem = transform.Find("PopupMenu").gameObject.GetComponent<UIItem>();
        detail.CountDown_Text = transform.Find("RecodState/CountDown").gameObject.GetComponent<Text>();
        detail.CountDown_Shadow = transform.Find("RecodState/CountDown").gameObject.GetComponent<Shadow>();
        detail.RecodState_UIItem = transform.Find("RecodState").gameObject.GetComponent<UIItem>();
        detail.Text_Text = transform.Find("DumpBtn/Text").gameObject.GetComponent<Text>();
        detail.DumpBtn_Image = transform.Find("DumpBtn").gameObject.GetComponent<Image>();
        detail.DumpBtn_Button = transform.Find("DumpBtn").gameObject.GetComponent<Button>();

        //À¶æ≈Á€ΩÁ√Ê
        detail.ShuaiJiuYao_panel = transform.Find("CtrlPanel/ShuaiJiuYao_panel").gameObject.GetComponent<UIItem>();
        detail.Text_tishi = transform.Find("CtrlPanel/ShuaiJiuYao_panel/Text_tishi").gameObject.GetComponent<Text>();
        detail.marksure_Btn = transform.Find("CtrlPanel/ShuaiJiuYao_panel/marksure_Btn").gameObject.GetComponent<Button>();
    }
}
