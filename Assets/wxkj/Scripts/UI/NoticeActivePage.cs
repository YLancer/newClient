using UnityEngine;
using System.Collections;
using packet.game;

public class NoticeActivePage : NoticeActivePageBase
{
    private int tab = 0;
    private ActiveBtnSub selectSub = null;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.ActiveButton_NoticeActiveTabSub.detail.Button_Button.onClick.AddListener(OnClickAciveTab);
        detail.NoticeButton_NoticeActiveTabSub.detail.Button_Button.onClick.AddListener(OnClickNoticeTab);
    }

    private void OnClickNoticeTab()
    {
        Game.SoundManager.PlayClick();
        tab = 0;
        SetupUI();
    }

    private void OnClickAciveTab()
    {
        Game.SoundManager.PlayClick();
        tab = 1;
        SetupUI();
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        if(null != sceneData && sceneData.Length > 0)
        {
            tab = (int)sceneData[0];
        }

        SetupUI();
    }

    private void SetupUI()
    {
        ActAndNoticeMsgSyn msg = Game.Instance.ActAndNoticeMsg;

        bool isActive = tab == 1;
        detail.ActiveButton_NoticeActiveTabSub.detail.SelectFlag_Image.gameObject.SetActive(isActive);
        detail.NoticeButton_NoticeActiveTabSub.detail.SelectFlag_Image.gameObject.SetActive(!isActive);
        detail.ActiveSub_ActiveSub.gameObject.SetActive(isActive);
        detail.NoticeSub_NoticeSub.gameObject.SetActive(!isActive);

        if(null != msg)
        {
            if (isActive)
            {
                PrefabUtils.ClearChild(detail.ActiveSub_ActiveSub.detail.ListContent_GridLayoutGroup);
                detail.ActiveSub_ActiveSub.detail.ContentText_Text.text = "";

                foreach (ActMsgModel item in msg.acts)
                {
                    string ctt = item.content;

                    GameObject go = PrefabUtils.AddChild(detail.ActiveSub_ActiveSub.detail.ListContent_GridLayoutGroup, detail.ActiveSub_ActiveSub.detail.ActiveBtnSub_ActiveBtnSub);
                    ActiveBtnSub sub = go.GetComponent<ActiveBtnSub>();
                    sub.detail.Title_Text.text = item.title;
                    sub.detail.Normal_Image.gameObject.SetActive(true);
                    sub.detail.SelectFlag_Image.gameObject.SetActive(false);

                    sub.detail.Button_Button.onClick.AddListener(()=> {
                        Game.SoundManager.PlayClick();
                        if (null != selectSub)
                        {
                            selectSub.detail.Normal_Image.gameObject.SetActive(true);
                            selectSub.detail.SelectFlag_Image.gameObject.SetActive(false);
                        }
                        selectSub = sub;

                        selectSub.detail.Normal_Image.gameObject.SetActive(false);
                        selectSub.detail.SelectFlag_Image.gameObject.SetActive(true);
                        detail.ActiveSub_ActiveSub.detail.ContentText_Text.text = ctt;
                    });

                    if (null == selectSub)
                    {
                        selectSub = sub;

                        selectSub.detail.Normal_Image.gameObject.SetActive(false);
                        selectSub.detail.SelectFlag_Image.gameObject.SetActive(true);
                        detail.ActiveSub_ActiveSub.detail.ContentText_Text.text = ctt;
                    }
                }
            }
            else
            {
                detail.NoticeSub_NoticeSub.detail.ContentText_Text.text = msg.notice;
                //detail.NoticeSub_NoticeSub.detail.ContentText_Text.text ="西凉3D麻将已上线";
            }
        }
    }
}
