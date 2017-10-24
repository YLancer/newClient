using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TargetSizeFitter : MonoBehaviour
{
    public System.Action<bool> OnMaxSize;
    public bool EveryUpdate = false;
    public RectTransform target;
    public Vector2 sizePlus;

    public Vector2 sizeMax = new Vector2(640, 1138);

    private RectTransform thisRect;
    private bool isMaxSize = false;

    void Start()
    {
        thisRect = this.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        Resize();
    }

    // Update is called once per frame
    void Update()
    {
        if (EveryUpdate)
        {
            Resize();
        }
    }

    public void Resize()
    {
        if (null == target) return;
        if (null == thisRect) return;

        Vector2 s = sizePlus + target.sizeDelta;

        bool isMax = false;
        if (s.x > sizeMax.x)
        {
            s.x = sizeMax.x;
            isMax = true;
        }
        if (s.y > sizeMax.y)
        {
            s.y = sizeMax.y;
            isMax = true;
        }

        if (isMaxSize != isMax)
        {
            isMaxSize = isMax;
            if (null != OnMaxSize)
            {
                OnMaxSize(isMaxSize);
            }
        }
        thisRect.sizeDelta = s;
    }
}
