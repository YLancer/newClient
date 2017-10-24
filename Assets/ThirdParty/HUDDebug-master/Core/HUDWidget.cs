using UnityEngine;
using System.Collections;
using Playflock.Log;

public abstract class HUDWidget : MonoBehaviour
{
    [HideInInspector]
    public HUDNode ParentNode;

    #region UNITY_EVENTS

    private void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
        Initialize();
    }

    private void Update()
    {
        Draw();
    }

    private void OnDestroy()
    {
        OnDismiss();
    }

    #endregion

    public abstract void Initialize();
    public abstract void Draw();

    public virtual void OnDismiss()
    {
    }
}
