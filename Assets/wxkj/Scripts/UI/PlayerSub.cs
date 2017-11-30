using UnityEngine;
using System.Collections;

public class PlayerSub : PlayerSubBase
{
    private MjData data;
    private GameObject eff;
    public void SetValue(MjData data)
    {
        this.data = data;
        
        Player player = data.player;
        if (player != null)
        {
            gameObject.SetActive(true);
            //detail.Ready_Text.text = player.isReady[0] ? "已准备" : "未准备";

            if (player.offline)
            {
                detail.AwayFlag_Image.gameObject.SetActive(false);
                detail.OfflineFlag_Image.gameObject.SetActive(true);
            }
            else
            {
                detail.AwayFlag_Image.gameObject.SetActive(player.leave);
                detail.OfflineFlag_Image.gameObject.SetActive(false);
            }
            
            //players[p.index].detail.Zhuang_Text.gameObject.SetActive(Game.MJMgr.makersIndex == p.playerInfo.Index);
            detail.Ting_Image.gameObject.SetActive(player.ting);
            bool IsMakers = Game.MJMgr.MakersPosition == player.position;
            detail.Zhuang_Image.gameObject.SetActive(IsMakers);

            //detail.Icon_Image.sprite = Game.IconMgr.GetFace(player.headImg);
            Game.IconMgr.SetFace(detail.Icon_Image, player.headImg);

            detail.Name_Text.text = player.nickName;
            if(RoomMgr.IsVipRoom() || RoomMgr.IsSingeRoom())
            {
                detail.Coins_Text.text = player.score.ToString();
            }
            else
            {
                detail.Coins_Text.text = player.coin.ToString();
            }
            detail.TalkingFlag_Image.gameObject.SetActive(showVoice);
            detail.WordRoot_Text.gameObject.SetActive(showWord);
            detail.MoodRoot_UIItem.gameObject.SetActive(showMood);

            detail.Icon_Button.onClick.AddListener(showUserInfo);
            detail.userInfo_Button.onClick.AddListener(closeUserInfo);

            bool isActivePlayer = Game.MJMgr.ActivePosition == player.position;
            //
            if(null == eff)
            {
                eff = Game.PoolManager.EffectPool.Spawn("FrameEffect");
                eff.transform.SetParent(this.transform);
                eff.transform.localPosition = Vector3.zero;
                eff.transform.localScale = Vector3.one;
            }
            eff.SetActive(isActivePlayer);
        }
        else
        {
            gameObject.SetActive(false);
            //players[p.index].detail.Coins_Text.text = "100";
            //players[p.index].detail.Ready_Text.text = "";
            //players[p.index].detail.OfflineFlag_Text.text = "";
        }
    }

    private bool showVoice = false;
    private bool showMood = false;
    private bool showWord = false;

    public void ShowVoice(byte[] data, MicrophoneInput mic)
    {
        showVoice = true;
        detail.TalkingFlag_Image.gameObject.SetActive(showVoice);
        float length = mic.Play(data);
        Game.Delay(length, () =>
        {
            showVoice = false;
            detail.TalkingFlag_Image.gameObject.SetActive(showVoice);
        });
    }

    public void ShowMood(int index)
    {
        showMood = true;
        detail.MoodRoot_UIItem.gameObject.SetActive(showMood);
        PrefabUtils.ClearChild(detail.MoodRoot_UIItem);
        GameObject go = PrefabUtils.AddChild(detail.MoodRoot_UIItem.gameObject, Game.IconMgr.moods[index]);
        Game.Delay(3, () => {
            showMood = false;
            detail.MoodRoot_UIItem.gameObject.SetActive(showMood);
            Destroy(go);
        });
    }

    public void ShowWord(string str)
    {
        showWord = true;
        detail.WordRoot_Text.gameObject.SetActive(showWord);
        detail.WordRoot_Text.text = str;
        detail.Word_Text.text = str;

        Game.Delay(3, () =>
        {
            showWord = false;
            detail.WordRoot_Text.gameObject.SetActive(showWord);
        });
    }

     void  showUserInfo()
    {
        Player player =this.data.player;
        detail.userInfo_image.gameObject.SetActive(true);
        detail.Text_IP.text = "IP:" + player.ip;
        detail.Text_uuid.text = "ID:"+ player.playerId ;
        detail.Text_nickname.text = "昵称:" +  player.nickName;
    }

    void closeUserInfo()
    {
        detail.userInfo_image.gameObject.SetActive(false);
    }
}
