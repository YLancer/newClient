using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public AudioClip[] Backgrounds;
    public AudioClip[] EffectSounds;

    public SoundsFiles female;
    public SoundsFiles man;

    public AudioSource bgSource;
    public AudioSource voiceSource;
    public AudioSource aSource;

    public void MuteSound(bool mute)
    {
        voiceSource.mute = !mute;
        aSource.mute = !mute;
    }

    public void MuteMusic(bool mute)
    {
        bgSource.mute = !mute;
    }

    private SoundsFiles GetSoundsFiles(int position)
    {
        MjData data = Game.MJMgr.MjData[position];
        if (data.player.sex == 1)
        {
            return man;
        }
        else
        {
            return female;
        }
    }

    public void PlayCardSound(int position, int card){
        int clientCard = Utils.CardS2C(card);
        
		AudioClip ac = GetSoundsFiles(position).CardSounds [clientCard];
        voiceSource.PlayOneShot (ac);
	}

	public void PlayMenuBackground(){
		AudioClip ac = Backgrounds [0];
        if(bgSource.isPlaying == false || bgSource.clip != ac)
        {
            bgSource.clip = ac;
            bgSource.Play();
        }
	}

	public void PlayShopBackground(){
		AudioClip ac = Backgrounds [1];
        if (bgSource.isPlaying == false || bgSource.clip != ac)
        {
            bgSource.clip = ac;
            bgSource.Play();
        }
    }

    public void PlayVoice(int position, int index)
    {
        voiceSource.PlayOneShot(GetSoundsFiles(position).Voices[index]);
    }

    private void PlayRandom(AudioClip[] sounds)
    {
        if(null == sounds || sounds.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, sounds.Length);
        voiceSource.PlayOneShot(sounds[index]);
    }
	public void PlayChi(int position)
    {
        PlayRandom(GetSoundsFiles(position).chiSounds);
    }

	public void PlayPeng(int position)
    {
        PlayRandom(GetSoundsFiles(position).pengSounds);
    }

	public void PlayGang(){
        //PlayRandom(PlayGang);
    }

	public void PlayTing(int position)
    {
        PlayRandom(GetSoundsFiles(position).tingSounds);
    }

    public void PlayZhidui(int position)
    {
        PlayRandom(GetSoundsFiles(position).zhiduiSounds);
    }

    public void PlayZimo(int position)
    {
        PlayRandom(GetSoundsFiles(position).zimoSounds);
    }

    public void PlayHu(int position)
    {
        PlayRandom(GetSoundsFiles(position).huSounds);
    }

    public void PlayHuanBao(int position)
    {
        PlayRandom(GetSoundsFiles(position).huanBaoSounds);
    }

    public void PlayClick()
    {
        aSource.PlayOneShot(EffectSounds[0]);
    }

    public void PlayClose()
    {
        aSource.PlayOneShot(EffectSounds[1]);
    }

    public void PlayRoundStartSound()
    {
        //aSource.PlayOneShot(EffectSounds[4]);
    }

    public void PlayShuffle()
    {
        aSource.PlayOneShot(EffectSounds[5]);
    }

    public void PlayDizeSound()
    {
        aSource.PlayOneShot(EffectSounds[6]);
    }

    public void PlayGet4Card()
    {
        aSource.PlayOneShot(EffectSounds[7]);
    }

    public void PlayDropCard()
    {
        aSource.PlayOneShot(EffectSounds[8]);
    }

    public void PlaySelectCard()
    {
        aSource.PlayOneShot(EffectSounds[9]);
    }

    public void PlayRoundEndSound()
    {
        aSource.PlayOneShot(EffectSounds[11]);
    }

    public void PlaySettleSound()
    {
        aSource.PlayOneShot(EffectSounds[12]);
    }

    public void PlayHuSound()
    {
        aSource.PlayOneShot(EffectSounds[13]);
    }

    public void PlayTingSound()
    {
        aSource.PlayOneShot(EffectSounds[15]);
    }

    public void PlayGetCard()
    {
        aSource.PlayOneShot(EffectSounds[16]);
    }

    public void PlayFall()
    {
        aSource.PlayOneShot(EffectSounds[17]);
    }

    public void PlayBuy()
    {
        aSource.PlayOneShot(EffectSounds[18]);
    }

    public void PlayLose()
    {
        aSource.PlayOneShot(EffectSounds[19]);
    }

    public void PlayWin()
    {
        aSource.PlayOneShot(EffectSounds[20]);
    }

    public void PlayEffect(int index)
    {
        aSource.PlayOneShot(EffectSounds[index]);
    }

}
