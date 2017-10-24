using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MoodPage : MoodPageBase
{
    private DateTime lastPostMsgTime;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.SendButton_Button.onClick.AddListener(OnClickSend);
        detail.WordButton_Button.onClick.AddListener(OnClickWord);
        detail.MoodButton_Button.onClick.AddListener(OnClickMood);

        SetupUI();

        OnClickMood();
    }

    private void OnClickMood()
    {
        Game.SoundManager.PlayClick();
        detail.WordButton_Image.color = Color.white * 0.01f;//.enabled = false;
        detail.MoodButton_Image.color = Color.white;//.enabled = true;
        detail.MoodGrid_GridLayoutGroup.gameObject.SetActive(true);
        detail.WordGrid_ScrollRect.gameObject.SetActive(false);
    }

    private void OnClickWord()
    {
        Game.SoundManager.PlayClick();
        detail.WordButton_Image.color = Color.white;//.enabled = false;
        detail.MoodButton_Image.color = Color.white * 0.01f;//.enabled = true;

        //detail.WordButton_Image.enabled = true;
        //detail.MoodButton_Image.enabled = false;
        detail.MoodGrid_GridLayoutGroup.gameObject.SetActive(false);
        detail.WordGrid_ScrollRect.gameObject.SetActive(true);
    }

    void PostMood(int id)
    {
        Game.SoundManager.PlayClick();
        TimeSpan span = DateTime.Now - lastPostMsgTime;
        if (span.TotalSeconds < 3)
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "发言太快了，休息下吧！");
            return;
        }
        lastPostMsgTime = DateTime.Now;

        Game.SocketGame.DoGameChatMsgRequest(string.Format("[{0}]", id));
        OnBackPressed();
    }

    private void SetupUI()
    {
        for (int i = 0; i < Game.IconMgr.moods.Count; i++)
        {
            int index = i;
            GameObject go = PrefabUtils.AddChild(detail.MoodGrid_GridLayoutGroup.gameObject, Game.IconMgr.moods[i]);
            Button btn = go.GetComponent<Button>();
            btn.onClick.AddListener(()=> {
                PostMood(index);
            });
        }

        foreach(ConfigWord word in ConfigWord.datas)
        {
            int id = word.Id;
            GameObject go = PrefabUtils.AddChild(detail.Content_VerticalLayoutGroup, detail.MoodWordSub_MoodWordSub);
            MoodWordSub sub = go.GetComponent<MoodWordSub>();
            sub.detail.Text_Text.text = word.TextContent;
            sub.detail.Button_Button.onClick.AddListener(()=> {
                PostMood(id);
            });
        }
    }

    void OnClickSend()
    {
        Game.SoundManager.PlayClick();
        TimeSpan span = DateTime.Now - lastPostMsgTime;
        if (span.TotalSeconds < 3)
        {
            Game.DialogMgr.PushDialog(UIDialog.SingleBtnDialog, "发言太快了，休息下吧！");
            return;
        }
        lastPostMsgTime = DateTime.Now;

        string txt = detail.InputField_InputField.text;
        if (string.IsNullOrEmpty(txt) == false)
        {
            Game.SocketGame.DoGameChatMsgRequest(txt);
            OnBackPressed();
        }
    }
}
