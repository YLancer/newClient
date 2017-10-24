using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MainPageBase : UISceneBase
{
    public MainPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.NoticeButton_Image = transform.Find("Top/NoticeButton").gameObject.GetComponent<Image>();
        detail.NoticeButton_Button = transform.Find("Top/NoticeButton").gameObject.GetComponent<Button>();
        detail.Notice_Text = transform.Find("Top/NoticeSub/Notice").gameObject.GetComponent<Text>();
        detail.Notice_ContentSizeFitter = transform.Find("Top/NoticeSub/Notice").gameObject.GetComponent<ContentSizeFitter>();
        detail.NoticeSub_Image = transform.Find("Top/NoticeSub").gameObject.GetComponent<Image>();
        detail.NoticeSub_Mask = transform.Find("Top/NoticeSub").gameObject.GetComponent<Mask>();
        detail.CreateRoomButton_Image = transform.Find("Right/CreateRoomButton").gameObject.GetComponent<Image>();
        detail.CreateRoomButton_Button = transform.Find("Right/CreateRoomButton").gameObject.GetComponent<Button>();
        detail.JoinRoomButton_Image = transform.Find("Right/JoinRoomButton").gameObject.GetComponent<Image>();
        detail.JoinRoomButton_Button = transform.Find("Right/JoinRoomButton").gameObject.GetComponent<Button>();
        detail.NormalRoomButton_Image = transform.Find("Right/NormalRoomButton").gameObject.GetComponent<Image>();
        detail.NormalRoomButton_Button = transform.Find("Right/NormalRoomButton").gameObject.GetComponent<Button>();
        detail.RaceButton_Image = transform.Find("Right/RaceButton").gameObject.GetComponent<Image>();
        detail.RaceButton_Button = transform.Find("Right/RaceButton").gameObject.GetComponent<Button>();
        detail.SingleButton_Image = transform.Find("Right/SingleButton").gameObject.GetComponent<Image>();
        detail.SingleButton_Button = transform.Find("Right/SingleButton").gameObject.GetComponent<Button>();
        detail.ShopButton_Image = transform.Find("Bottom/ShopButton").gameObject.GetComponent<Image>();
        detail.ShopButton_Button = transform.Find("Bottom/ShopButton").gameObject.GetComponent<Button>();
        detail.MailButton_Image = transform.Find("Bottom/MailButton").gameObject.GetComponent<Image>();
        detail.MailButton_Button = transform.Find("Bottom/MailButton").gameObject.GetComponent<Button>();
        detail.RecordButton_Image = transform.Find("Bottom/RecordButton").gameObject.GetComponent<Image>();
        detail.RecordButton_Button = transform.Find("Bottom/RecordButton").gameObject.GetComponent<Button>();
        detail.ActivityButton_Image = transform.Find("Bottom/ActivityButton").gameObject.GetComponent<Image>();
        detail.ActivityButton_Button = transform.Find("Bottom/ActivityButton").gameObject.GetComponent<Button>();
        detail.HelpButton_Image = transform.Find("Bottom/HelpButton").gameObject.GetComponent<Image>();
        detail.HelpButton_Button = transform.Find("Bottom/HelpButton").gameObject.GetComponent<Button>();
        detail.SettingButton_Image = transform.Find("Bottom/SettingButton").gameObject.GetComponent<Image>();
        detail.SettingButton_Button = transform.Find("Bottom/SettingButton").gameObject.GetComponent<Button>();
        detail.ShareButton_Image = transform.Find("Bottom/ShareButton").gameObject.GetComponent<Image>();
        detail.ShareButton_Button = transform.Find("Bottom/ShareButton").gameObject.GetComponent<Button>();
        detail.GameRankButton_MainRankTabSub = transform.Find("Left/GameRankButton").gameObject.GetComponent<MainRankTabSub>();
        detail.WealthRankButton_MainRankTabSub = transform.Find("Left/WealthRankButton").gameObject.GetComponent<MainRankTabSub>();
        detail.Content_GridLayoutGroup = transform.Find("Left/Scroll View/Viewport/Content").gameObject.GetComponent<GridLayoutGroup>();
        detail.Content_ContentSizeFitter = transform.Find("Left/Scroll View/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
        detail.Viewport_Mask = transform.Find("Left/Scroll View/Viewport").gameObject.GetComponent<Mask>();
        detail.Viewport_Image = transform.Find("Left/Scroll View/Viewport").gameObject.GetComponent<Image>();
        detail.ScrollView_ScrollRect = transform.Find("Left/Scroll View").gameObject.GetComponent<ScrollRect>();
        detail.ScrollView_Image = transform.Find("Left/Scroll View").gameObject.GetComponent<Image>();
        detail.RankSub_Image = transform.Find("Left/RankSub").gameObject.GetComponent<Image>();
        detail.RankSub_Button = transform.Find("Left/RankSub").gameObject.GetComponent<Button>();
        detail.RankSub_RankSub = transform.Find("Left/RankSub").gameObject.GetComponent<RankSub>();

    }
}
