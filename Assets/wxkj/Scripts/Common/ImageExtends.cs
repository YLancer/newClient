using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageExtends : MonoBehaviour
{
    private static System.Collections.Generic.Dictionary<string, Sprite> cache = new System.Collections.Generic.Dictionary<string, Sprite>();
    private string url;
    private Coroutine coroutine;
    public void SetSprite(Image image, string url)
    {
        if (string.IsNullOrEmpty(url)){
            return;
        }

        if (url.EndsWith("0"))
        {
            url = url.Substring(0,url.Length - 1) + "64";
        }
        if (cache.ContainsKey(url))
        {
            image.sprite = cache[url];
        }
        else
        {
            if(this.url != url)
            {
                this.url = url;
                if(null != coroutine)
                {
                    Game.StopDelay(coroutine);
                }

                coroutine = Game.Start(DownloadImg(image));
            }
        }
    }

    IEnumerator DownloadImg(Image image)
    {
        Debug.Log("downloading url:" + url);
        WWW www = new WWW(url);
        yield return www;
        while (!www.isDone)
        {
            yield return 0;
        }

        if (www.error == null)
        {
            Texture2D tex2d = www.texture;
            ////将图片保存至缓存路径  
            //byte[] pngData = tex2d.EncodeToPNG();                         //将材质压缩成byte流  
            //File.WriteAllBytes(path + url.GetHashCode(), pngData);        //然后保存到本地  

            Sprite m_sprite = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), new Vector2(0, 0));
            image.sprite = m_sprite;
            cache.Add(url, m_sprite);
        }
    }
}
