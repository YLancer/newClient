
public class SettingPage : SettingPageBase
{
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.LogoutButton_Button.onClick.AddListener(OnClickLogout);
        detail.MusicBtn_SwitchSub.OnChange = OnSwitchMusic;
        detail.ShakeBtn_SwitchSub.OnChange = OnSwitchShake;
        detail.SoundBtn_SwitchSub.OnChange = OnSwitchSound;
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);

        SetupUI();
    }

    void SetupUI()
    {
        //detail.Icon_Image.sprite = Game.IconMgr.GetFace(Game.Instance.face);
        Game.IconMgr.SetFace(detail.Icon_Image, Game.Instance.face);

        detail.Name_Text.text = Game.Instance.nickname;

        GameData data = GDM.getSaveAbleData<GameData>();
        detail.MusicBtn_SwitchSub.IsSelected = data.Music;
        detail.SoundBtn_SwitchSub.IsSelected = data.Sound;
        detail.ShakeBtn_SwitchSub.IsSelected = data.Shake;
    }

    void OnClickLogout()
    {
        Game.SoundManager.PlayClick();
        Game.Logout();
    }

    void OnSwitchMusic(bool isOn)
    {
        GameData data = GDM.getSaveAbleData<GameData>();
        data.Music = isOn;
        GDM.Save(SAVE_DATA_TYPE.GameData);

        Game.SoundManager.MuteMusic(isOn);
        Game.SoundManager.PlayClick();
    }

    void OnSwitchShake(bool isOn)
    {
        GameData data = GDM.getSaveAbleData<GameData>();
        data.Shake = isOn;
        GDM.Save(SAVE_DATA_TYPE.GameData);
        Game.SoundManager.PlayClick();
    }

    void OnSwitchSound(bool isOn)
    {
        GameData data = GDM.getSaveAbleData<GameData>();
        data.Sound = isOn;
        GDM.Save(SAVE_DATA_TYPE.GameData);

        Game.SoundManager.MuteSound(isOn);
        Game.SoundManager.PlayClick();
    }
}
