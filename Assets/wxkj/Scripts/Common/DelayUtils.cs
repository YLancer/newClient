using UnityEngine;
using System.Collections;

public class DelayUtils : ISingleton<DelayUtils>
{
    public static Coroutine Start(IEnumerator callback)
    {
        return Instance.StartCoroutine(callback);
    }
}
