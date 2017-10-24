using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class UtilDelay : UtilBase
{
    public override void OnEnable()
    {
        if (playOnStart)
            Play();
    }

    public override void NextOperation()
    {
        foreach (var next in childNodes)
        {
            next.enabled = true;
            next.Play();
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(duration);
        NextOperation();
        callBack.Invoke();
    }

    public override void Play()
    {
        StartCoroutine(Delay());
    }
}
