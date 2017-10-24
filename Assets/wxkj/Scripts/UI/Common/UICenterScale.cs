using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICenterScale : MonoBehaviour
{
    public ScrollRect ScrollRect;
    public Mask mask;
    public AnimationCurve curve;
    LayoutElement element;
    Vector2 defaultSize = new Vector2();

    private RectTransform maskRect;
    private RectTransform thisRect;

    public enum Arrangement
    {
        Horizontal,
        Vertical,
    }
    // Use this for initialization
    void Start()
    {
        if (ScrollRect == null)
        {
            ScrollRect = GetComponentInParent <ScrollRect>();
        }
        if (mask == null)
        {
            mask = GetComponentInParent<Mask>();
        }
        maskRect = mask.GetComponent<RectTransform>();
        thisRect = GetComponent<RectTransform>();

        element = GetComponent<LayoutElement>();
        defaultSize.x = element.preferredWidth;
        defaultSize.y = element.preferredHeight;
    }
    // Update is called once per frame
    void Update()
    {
        float rate = 0;
        if (ScrollRect.horizontal && ScrollRect.vertical)
        {
        }
        else if (ScrollRect.horizontal)
        {
            float distance = Mathf.Abs(thisRect.position.x - maskRect.position.x);
            rate = distance / maskRect.rect.width;
        }
        else if (ScrollRect.vertical)
        {
            float distance = Mathf.Abs(thisRect.position.y - maskRect.position.y);
            rate = distance / maskRect.rect.height;
        }
        float scale = curve.Evaluate(rate);// (1 - rate);

        element.preferredWidth = scale * defaultSize.x;
        element.preferredHeight = scale * defaultSize.y;
    }
}
