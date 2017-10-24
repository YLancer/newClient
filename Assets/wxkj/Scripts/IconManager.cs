using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IconManager : MonoBehaviour {
    public List<Sprite> FaceList = new List<Sprite>();
    public List<Sprite> IconList = new List<Sprite>();
    public List<GameObject> moods;

    public Sprite Get(string name)
    {
        foreach(Sprite s in IconList)
        {
            if(s.name == name)
            {
                return s;
            }
        }
        return null;
    }

    public Sprite GetFace(string name)
    {
        foreach (Sprite s in FaceList)
        {
            if (s.name == name)
            {
                return s;
            }
        }
        return FaceList[0];
    }

    public void SetFace(Image image, string playerHeadImg)
    {
        if (playerHeadImg.StartsWith("http"))
        {
            ImageExtends wwwImage = image.GetComponent<ImageExtends>();
            if(null == wwwImage)
            {
                wwwImage = image.gameObject.AddComponent<ImageExtends>();
            }
            wwwImage.SetSprite(image, playerHeadImg);
        }
        else
        {
            image.sprite = GetFace(playerHeadImg);
        }
    }

    //性别 值为1时是男性，值为2时是女性，值为0时是未知（根据头像来判断）
    public static int GetSexByFace(int sex,string headImg)
    {
        if (sex == 0)
        {
            if (!string.IsNullOrEmpty(headImg))
            {
                //portrait_img_02
                string sid = headImg.Replace("portrait_img_", "");
                int id = 1;
                int.TryParse(sid, out id);
                sex = id >= 5 ? 2 : 1;
            }
        }

        return sex;
    }
}
