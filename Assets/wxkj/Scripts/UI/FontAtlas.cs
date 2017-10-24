using UnityEngine;
using System.Collections;

public class FontAtlas : MonoBehaviour {
    [SerializeField]
    private string text;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Sprite[] spriteIcons;

    public Sprite GetChar(char c)
    {
        if (string.IsNullOrEmpty(text) || sprites.Length<=0)
        {
            return null;
        }

        int index = text.IndexOf(c);
        if (index < 0 || index >= sprites.Length)
        {
            return null;
        }

        return sprites[index];
    }

    public Sprite GetIcon(int index)
    {
        if (index < 0 || index >= spriteIcons.Length)
        {
            return null;
        }

        return spriteIcons[index];
    }
}
