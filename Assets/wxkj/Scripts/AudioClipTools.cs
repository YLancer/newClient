using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioClipTools : MonoBehaviour {
    public List<AudioClip> list;
    public AudioSource audioSource;

    [ContextMenu("合并")]
    public void CombineTest()
    {
        audioSource.clip = Combine(2,list.ToArray());
        audioSource.Play();
	}

    [ContextMenu("分割")]
    public void CutupTest()
    {
        audioSource.clip = Cutup(2, list[0]);
        audioSource.Play();
    }

    public static AudioClip Combine(int channels,params AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return null;

        int length = 0;
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i] == null)
                continue;

            
            if (channels == 1 && clips[i].channels == 2)
            {
                length += clips[i].samples;// *clips[i].channels;
            }
            else
            {
                length += clips[i].samples *clips[i].channels;
            }
        }

        float[] data = new float[length];
        length = 0;
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i] == null)
                continue;

            if (channels == 1 && clips[i].channels == 2)
            {
                float[] buffer = new float[clips[i].samples * clips[i].channels];
                float[] buffer2 = new float[clips[i].samples];
                clips[i].GetData(buffer, 0);
                for (int j = 0; j < buffer.Length; j = j + 2)
                {
                    buffer2[j / 2] = buffer[j];
                }
                System.Buffer.BlockCopy(buffer2, 0, data, length*4, buffer2.Length * 4);
                //buffer2.CopyTo(data, length);
                length += buffer2.Length;
            }
            else
            {
                float[] buffer = new float[clips[i].samples * clips[i].channels];
                clips[i].GetData(buffer, 0);
                System.Buffer.BlockCopy(buffer, 0, data, length*4, buffer.Length * 4);// length需要x4
                //buffer.CopyTo(data, length);
                length += buffer.Length;
            }

        }

        if (length == 0)
            return null;

        AudioClip result = AudioClip.Create("Combine", length, channels, 44100, false);
        result.SetData(data, 0);

        return result;
    }

    public static AudioClip Cutup(int subLength, AudioClip clip)
    {
        if (clip == null || clip.length == 0)
            return null;

        int length = clip.samples * clip.channels;
        float[] data = new float[length];
        clip.GetData(data, 0);

        int newLen = Mathf.FloorToInt(subLength / clip.length * length);

        if (newLen == 0)
            return null;

        float[] buffer = new float[newLen];
        System.Buffer.BlockCopy(data, 0, buffer, 0, newLen * 4);

        AudioClip result = AudioClip.Create("Cutup", newLen, clip.channels, clip.frequency, false);
        result.SetData(buffer, 0);

        return result;
    }

    public static AudioClip CutupCombineChannel(int subLength, AudioClip clip)
    {
        if (clip == null || clip.length == 0)
            return null;

        bool combine = (clip.channels == 2);

        int length = clip.samples * clip.channels;
        float[] data = new float[length];
        clip.GetData(data, 0);

        float arg = combine ? 0.5f : 1;
        int newLen = Mathf.FloorToInt(subLength / clip.length * length* arg);

        //Debug.LogFormat("combine{0} {1} {2}",combine,data.Length,newLen);

        if (newLen == 0)
            return null;

        float[] buffer = new float[newLen];
        if (combine)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = data[i * 2];
            }
        }
        else
        {
            System.Buffer.BlockCopy(data, 0, buffer, 0, newLen * 4);
        }

        AudioClip result = AudioClip.Create("Cutup", newLen, clip.channels, clip.frequency, false);
        result.SetData(buffer, 0);

        return result;
    }
}
