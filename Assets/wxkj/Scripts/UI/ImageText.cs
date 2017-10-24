using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum FitMode{
    Null,Width,Height,Both
}

[ExecuteInEditMode]
public class ImageText : MonoBehaviour
{
    [SerializeField]
    private string text;
    public string Text
    {
        get
        {
            return text;
        }

        set
        {
            text = value;
            SetupUI();
        }
    }

    public Color32 color = Color.white;
    public TextAlignment alignment;
    public FitMode fitMode;
    public FontAtlas fontAtlas;
    public float spacing = 0;
    public float IconScale = 1;

    private float lastPos = 0;
    private float parentWidth = 0;
    private float parentHeight = 0;
    private float childWidth = 0;
    private float childHeight = 0;

    private List<Image> childs = new List<Image>();
    private List<Image> caches = new List<Image>();
    private Vector3 pos = new Vector3();

    private Image GetFromCache(string name,Sprite sprite)
    {
        if (caches.Count > 0)
        {
            Image img = caches[0];
            caches.RemoveAt(0);
            img.gameObject.name = name;
            img.gameObject.SetActive(true);
            img.color = color;
            img.sprite = sprite;
            img.SetNativeSize();
            return img;
        }
        else
        {
            GameObject child = new GameObject(name);
            child.transform.SetParent(this.transform);
            child.transform.localScale = Vector3.one;
            Image img = child.AddComponent<Image>();
            img.color = color;
            img.sprite = sprite;
            img.SetNativeSize();
            print("Create");
            return img;
        }
    }

    private void CreateOneChar(char c)
    {
        if (null != fontAtlas)
        {
            Sprite s = fontAtlas.GetChar(c);
            if (null != s)
            {
                Image img = GetFromCache(c.ToString(),s);

                float width = img.rectTransform.rect.width;
                childWidth += width + spacing;
                float height = img.rectTransform.rect.height;
                if (height > childHeight)
                {
                    childHeight = height;
                }

                childs.Add(img);
            }
        }
    }

    private void CreateOneChar(int index)
    {
        if (null != fontAtlas)
        {
            Sprite s = fontAtlas.GetIcon(index);
            if (null != s)
            {
                Image img = GetFromCache(s.name, s);
                img.color = Color.white;

                Rect rect = img.rectTransform.rect;
                rect.width *= IconScale;
                rect.height *= IconScale;

                float width = rect.width;
                float height = rect.height;

                img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

                childWidth += width + spacing;
                
                if (height > childHeight)
                {
                    childHeight = height;
                }

                childs.Add(img);
            }
        }
    }

    private void SetupUI()
    {
        lastPos = 0;
        parentWidth = 0;
        parentHeight = 0;
        childWidth = 0;
        childHeight = 0;

        while (childs.Count > 0)
        {
            Image img = childs[0];
            img.gameObject.name = "cache";
            img.gameObject.SetActive(false);
            caches.Add(img);
            childs.RemoveAt(0);
        }

        if (string.IsNullOrEmpty(text) == false)
        {
            RectTransform rectTrans = this.GetComponent<RectTransform>();
            parentWidth = rectTrans.rect.width;
            parentHeight = rectTrans.rect.height;

            int i = 0;
            while (i < text.Length)
            {
                char c = text[i];
                if (c == '\\')
                {
                    if (i + 2 < text.Length)
                    {
                        string s = text.Substring(i + 1, 2);
                        int index = 0;
                        if (int.TryParse(s, out index))
                        {
                            CreateOneChar(index);
                            i = i + 2;
                        }
                        else
                        {
                            CreateOneChar(c);
                        }
                    }
                    else
                    {
                        CreateOneChar(c);
                    }
                }
                else
                {
                    CreateOneChar(c);
                }

                i++;
            }

            Align();
        }
    }

    private float GetScale()
    {
        float scale = 1;
        float widthScale = parentWidth / childWidth;
        float heightScale = parentHeight / childHeight;

        if (fitMode == FitMode.Width)
        {
            scale = widthScale;
        }
        else if (fitMode == FitMode.Height)
        {
            scale = heightScale;
        }
        else if (fitMode == FitMode.Both)
        {
            scale = Mathf.Min(widthScale, heightScale);
        }
        else
        {
            scale = 1;
        }
        
        return scale;
    }

    private void Align()
    {
        float scale = GetScale();

        Vector3 offset = Vector3.zero;
        if (alignment == TextAlignment.Left)
        {
            offset = -Vector3.right * parentWidth * 0.5f;
        }
        else if (alignment == TextAlignment.Right)
        {
            offset = Vector3.right * (parentWidth * 0.5f - childWidth * scale);
        }
        else
        {
            offset = -Vector3.right * childWidth * 0.5f * scale;
        }

        for (int i = 0; i < childs.Count; i++)
        {
            Image img = childs[i];
            float width = (img.rectTransform.rect.width + spacing) * scale;
            pos.x = lastPos + width * 0.5f;
            img.rectTransform.localPosition = pos + offset;
            lastPos = pos.x + width * 0.5f;
            img.transform.localScale = Vector3.one * scale;
        }
    }

    void Start()
    {
        SetupUI();
    }

    void OnDrawGizmosSelected()
    {
        SetupUI();
    }
}
