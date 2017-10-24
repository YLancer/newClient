using System.Collections;
using System.IO.Compression;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioClip clip;

    private System.Action<byte[]> OnEndRecord;

    //通常的无损音质的采样率是44100，即每秒音频用44100个float数据表示，但是语音只需8000（通常移动电话是8000）就够了  
    //不然音频数据太大，不利于传输和存储  
    public const int SamplingRate = 8000;
    public const int RECORD_TIME = 10;
    public int curTime = 0;

    public void StartRecord(System.Action<byte[]> OnEndRecord)
    {
        this.OnEndRecord = OnEndRecord;

        curTime = 0;

        Microphone.End(null);//这句可以不需要，但是在开始录音以前调用一次是好习惯  
        clip = Microphone.Start(null, false, RECORD_TIME, SamplingRate);

        //倒计时   
        //StopCoroutine("TimeDown");
        //StartCoroutine("TimeDown");
    }

    public static int samples;
    public void EndRecord()
    {
        //StopCoroutine("TimeDown");

        if (!Microphone.IsRecording(null))
        {
            return;
        }

        Microphone.End(null);//此时录音结束，clip已可以播放了  

        if (curTime < 0.1f) return;//录音小于1秒就不处理了  

        //clip = AudioClipTools.CutupCombineChannel(curTime, clip);
        //如果要便于传输还需要进行压缩，压缩后的recordData就可以用于网络传输了 
        //byte[] data = clip.GetData();

        byte[] data = MoPhoGames.USpeak.Core.USpeakAudioClipCompressor.CompressAudioClip(clip, out samples, BandMode.Narrow);
        print("CompressAudioClip samples[" + samples + "] :" + data.Length);
        OnEndRecord(data);
    }

    void Update()
    {
        if (Microphone.IsRecording(null))
        {
            int lastPos = Microphone.GetPosition(null);
            curTime = lastPos / SamplingRate;//录音时长 

            if (curTime >= RECORD_TIME)
            {
                EndRecord();
            }
        }
    }

    //IEnumerator TimeDown()
    //{
    //    yield return new WaitForEndOfFrame();

    //    while (Microphone.IsRecording(null))
    //    {
    //        Debug.Log("yield return new WaitForSeconds " + curTime);
    //        yield return new WaitForSeconds(1);
    //        int lastPos = Microphone.GetPosition(null);
    //        curTime = lastPos / SamplingRate;//录音时长 

    //        if(curTime>= RECORD_TIME)
    //        {
    //            EndRecord();
    //        }
    //    }
    //}

    public float Play(byte[] data)
    {
        //AudioClip myClip = AudioClip.Create("voice", data.Length, 1, SamplingRate, false);
        //myClip.SetData(data);
        //data = AudioClipExt.unzip(data);
        AudioClip clip = MoPhoGames.USpeak.Core.USpeakAudioClipCompressor.DecompressAudioClip(data, 0, 1, false, BandMode.Narrow, 1f);
        audioSource.clip = clip;
        audioSource.Play();

        return clip.length;
    }
}

public static class AudioClipExt
{
    //public static byte[] GetData(this AudioClip clip)
    //{
    //    //float[] data = new float[clip.samples * clip.channels];
        
    //    //clip.GetData(data, 0);
    //    //byte[] bytes = new byte[data.Length];
    //    //for (int i = 0; i < data.Length; i++)
    //    //{
    //    //    bytes[i] = (byte)((1 + data[i]) * 127);
    //    //}

    //    byte[] bytes = MoPhoGames.USpeak.Core.USpeakAudioClipConverter.AudioClipToBytes(clip);
    //    byte[] z = zip(bytes);

    //    Debug.LogFormat("CompressAudioClip>>>{0}:{1}", bytes.Length,z.Length);
    //    return z;
    //}

    //public static void SetData(this AudioClip clip, byte[] bytes)
    //{
    //    float[] data = new float[bytes.Length];
    //    for (int i = 0; i < data.Length; i++)
    //    {
    //        data[i] = bytes[i] / 127f - 1;
    //    }
    //    clip.SetData(data, 0);
    //}

    //private static byte[] zip(byte[] data)
    //{
    //    MemoryStream memstream = new MemoryStream(data);
    //    MemoryStream outstream = new MemoryStream();

    //    using (GZipStream encoder = new GZipStream(outstream, CompressionMode.Compress))
    //    {
    //        memstream.WriteTo(encoder);
    //    }

    //    return outstream.ToArray();
    //}

    //public static byte[] unzip(byte[] data)
    //{
    //    GZipStream decoder = new GZipStream(new MemoryStream(data), CompressionMode.Decompress);
    //    MemoryStream outstream = new MemoryStream();
    //    CopyStream(decoder, outstream);
    //    return outstream.ToArray();
    //}

    //private static void CopyStream(Stream input, Stream output)
    //{
    //    byte[] buffer = new byte[32768];
    //    //long TempPos = input.Position;
    //    while (true)
    //    {
    //        int read = input.Read(buffer, 0, buffer.Length);
    //        if (read <= 0) break;
    //        output.Write(buffer, 0, read);
    //    }
    //    //input.Position = TempPos;// or you make Position = 0 to set it at the start 
    //}
}