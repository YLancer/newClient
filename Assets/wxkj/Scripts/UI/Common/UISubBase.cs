using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UISubBase : MonoBehaviour
{
    public virtual void SetAllMemberValue()
    {

    }

    public GameObject AddChild(MonoBehaviour parent, MonoBehaviour prefab)
    {
        return PrefabUtils.AddChild(parent, prefab);
    }

}
