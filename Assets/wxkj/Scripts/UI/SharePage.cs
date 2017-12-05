
public class SharePage : SharePageBase
{
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.ShareWX_Button.onClick.AddListener(OnShareWeChat);
        detail.SharePeng_Button.onClick.AddListener(OnShareWeChatMoments);
    }

    //public override void OnSceneActivated(params object[] sceneData)
    //{
    //    base.OnSceneActivated(sceneData);
    //    SetupUI();
    //}

    //void SetupUI()
    //{
    //    //detail.Icon_Image.sprite = Game.IconMgr.GetFace(Game.Instance.face);
    //    GameData data = GDM.getSaveAbleData<GameData>();
    //}

    void OnShareWeChat()
    {
        Game.SoundManager.PlayClick();
        Game.AndroidUtil.OnShareClick();
    }

    void OnShareWeChatMoments()
    {
        Game.SoundManager.PlayClick();
        Game.AndroidUtil.OnShareWeChatMoments();
    }

}
