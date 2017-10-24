using UnityEngine;
using System.Collections;
using packet.game;
using packet.msgbase;

public class MailPage : MailPageBase
{
    private long selectId = 0;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.MailAttachSub_MailAttachSub.detail.GetButton_Button.onClick.AddListener(OnClickGetBtn);
    }

    public override void OnSceneOpened(params object[] sceneData)
    {
        base.OnSceneOpened(sceneData);
        EventDispatcher.AddEventListener(MessageCommand.Update_Mail, SetupUI);
    }

    public override void OnSceneClosed()
    {
        base.OnSceneClosed();
        EventDispatcher.RemoveEventListener(MessageCommand.Update_Mail, SetupUI);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        SetupUI();
    }

    void SetupUI(params object[] args)
    {
        Clear(detail.ListContent_GridLayoutGroup);
        detail.MailAttachSub_MailAttachSub.gameObject.SetActive(false);

        if (null != Game.Instance.Mails)
        {
            foreach (MailMsgModel mail in Game.Instance.Mails)
            {
                long mailId = mail.mailId;

                GameObject child = AddChild(detail.ListContent_GridLayoutGroup, detail.MailSub_MailSub);
                MailSub sub = child.GetComponent<MailSub>();
                sub.detail.Title_Text.text = mail.title;
                System.DateTime time = TimeUtils.GetNoralTime(mail.sendTime.ToString());
                sub.detail.Time_Text.text = time.ToString("yyyy-MM-dd");

                bool readFlag = (mail.state > 0);
                bool getFlag = (mail.state > 1);

                if (selectId == 0)
                {
                    selectId = mailId;
                    if (!readFlag)
                    {
                        mail.state = 1;
                        readFlag = true;
                        Game.SocketMsg.DoReadMsgRequest(mailId);
                    }
                }

                bool isSelected = (selectId == mailId);
                sub.detail.OpenFlag_Image.gameObject.SetActive(readFlag);
                sub.detail.SelectFlag_Image.gameObject.SetActive(isSelected);
                if (isSelected)
                {
                    detail.ContentText_Text.text = mail.content;

                    if (mail.attachType == 0)
                    {
                        detail.MailAttachSub_MailAttachSub.gameObject.SetActive(false);
                    }
                    else
                    {
                        detail.MailAttachSub_MailAttachSub.gameObject.SetActive(!getFlag);
                    }

                    detail.MailAttachSub_MailAttachSub.SetValue(mail.attachType,mail.attachNum);
                }
                
                sub.detail.Button_Button.onClick.AddListener(() => {
                    Game.SoundManager.PlayClick();
                    selectId = mailId;
                    if (!readFlag)
                    {
                        mail.state = 1;
                        Game.SocketMsg.DoReadMsgRequest(mailId);
                    }
                    SetupUI();
                });
            }
        }
    }

    void OnClickGetBtn()
    {
        Game.SoundManager.PlayClick();
        MailMsgModel mail = Game.Instance.GetMail(selectId);
        if(null != mail && mail.attachType>0)
        {
            mail.state = 2;
            Game.SocketHall.DoReceiveMailAttachRequest(selectId);
            SetupUI();
        }
    }
}
