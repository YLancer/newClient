using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJson;

public static class MiniJsonTRExtensions
{
    public static string toJson(this List<object> obj)
    {
        return Json.Serialize(obj);
    }

    public static string toJson(this Dictionary<string, object> obj)
    {
        return Json.Serialize(obj);
    }

    public static List<object> listFromJson(this string json)
    {
        return (List<object>)Json.Deserialize(json);
    }

    public static Dictionary<string, object> dictionaryFromJson(this string json)
    {
        return (Dictionary<string, object>)Json.Deserialize(json);
    }
}

public static class PNGTool
{
    public static bool LoadFromBytes(ref Texture2D destination, string pngPath)
    {
        if (string.IsNullOrEmpty(pngPath) == true)
        {
            return false;
        }

        if (destination == null)
        {
            destination = new Texture2D(0, 0, TextureFormat.RGBA32, false);
        }

        //-- pathPath must end in .bytes in the resource bundle.
        TextAsset rawpng = Resources.Load(pngPath, typeof(TextAsset)) as TextAsset;
        destination.LoadImage(rawpng.bytes);
        Resources.UnloadAsset(rawpng);
        return true;
    }
}
