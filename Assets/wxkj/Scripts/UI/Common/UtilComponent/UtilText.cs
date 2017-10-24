using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UtilText : UtilComponent
{
    public int target = 2000;
    public float FlyCount = 1;
    Text text;
    [SerializeField]
    Text unitText;

    IEnumerator doFly()
    {
        int curValue = 0;
        int.TryParse(text.text, out curValue);
        int left = 0;
        if (FlyCount != 0 && target != 0)
        {
            int avg = (int)(target / FlyCount);
            float delay = duration / FlyCount;
            for (int i = 0; i < FlyCount; i++)
            {
                yield return new WaitForSeconds(delay);
                int rd = UnityEngine.Random.Range(1, avg);
                int add = avg - rd + left;
                left = avg - rd;
                GenerateTextToShow(add);
                curValue += add;
                text.text = i == FlyCount - 1 ? target.ToString() : curValue.ToString();
            }
        }
    }

    public override void Play()
    {
        if (text == null)
        {
            text = transform.GetComponent<Text>();
            text.text = "";
        }
        StartCoroutine(doFly());
    }

    void GenerateTextToShow(int showNumber)
    {
        if (null != unitText)
        {
            GameObject textGameObject = PrefabUtils.AddChild(this.gameObject, unitText.gameObject);
            Text text = textGameObject.GetComponent<Text>();
            text.text = showNumber.ToString();
        }
    }
}
