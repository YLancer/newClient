using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class HttpRequest
{
    public System.Action<Hashtable> OnComplete;
    public System.Action<NetCode, string> OnError;
    public string uri;
    public bool useMask = true;

    public Dictionary<string, string> args;

    public virtual IEnumerator doReauest()
    {
        yield return null;
    }
}
