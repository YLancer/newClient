using UnityEngine;
using System.Collections;

public class HandAnima : MonoBehaviour {
    public System.Action OnDropCallback;
    public System.Action OnDropSoundCallback;
    public System.Action OnPutTableCallback;
    public System.Action OnDiceCallback;
    public System.Action OnPutBaoCallback;
    public static bool IsBusy = false;

    public void OnStart()
    {
        IsBusy = true;
    }

    public void OnDrop()
    {
        if(null != OnDropCallback)
        {
            OnDropCallback();
            //OnDropCallback = null;
        }
        //IsBusy = false;
        //EventDispatcher.DispatchEvent(MessageCommand.AnimExit_DropCard);
    }

    public void OnDropSound()
    {
        if (null != OnDropSoundCallback)
        {
            OnDropSoundCallback();
            OnDropSoundCallback = null;
        }
        //EventDispatcher.DispatchEvent(MessageCommand.AnimExit_DropCard);
    }

    public void OnPutTable()
    {
        //IsBusy = false;
        if (null != OnPutTableCallback)
        {
            OnPutTableCallback();
            OnPutTableCallback = null;
        }
    }

    public void OnFinish()
    {
        IsBusy = false;
        this.gameObject.SetActive(false);
    }

    public void OnDice()
    {
        if (null != OnDiceCallback)
        {
            OnDiceCallback();
            OnDiceCallback = null;
        }
    }

    public void OnPutBao()
    {
        if (null != OnPutBaoCallback)
        {
            OnPutBaoCallback();
            OnPutBaoCallback = null;
        }
    }
}
