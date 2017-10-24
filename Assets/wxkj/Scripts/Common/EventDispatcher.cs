using UnityEngine;
using System.Collections.Generic;

public enum MessageCommand
{
    //LoginSucess = 1001,

    Update_Score = 4001,
    Update_UserInfo = 4002,
    Update_Rank = 4003,
    Update_Mail = 4004,

    MJ_DragCard = 5001,
	MJ_DropCard = 5002,
	MJ_Chi = 5003,
	MJ_Peng = 5004,
	MJ_Gang = 5005,
	MJ_Ting = 5006,
	MJ_Hu = 5007,
	MJ_Pass = 5008,

    MJ_UpdateCtrlPanel = 5101,
    MJ_UpdatePlayPage = 5102,

    OnEnterRoom = 6001,
    AnimExit_DropCard = 6002,
    AnimExit_DropCard_Finish = 6003,

    Chat = 7001,
    PlayEffect = 8001,
    //PopQueue = 8101,
}

public delegate void MyEventHandler(params object[] objs);

public class EventDispatcher
{
    private static Dictionary<string, MyEventHandler> listeners = new Dictionary<string, MyEventHandler>();
    private static Dictionary<string, MyEventHandler> oneOfflisteners = new Dictionary<string, MyEventHandler>();
    public EventDispatcher()
    {
    }

    public static void AddEventOneOffListener(MessageCommand type, MyEventHandler handler)
    {
        AddEventOneOffListener(type.ToString(), handler);
    }

    public static void AddEventOneOffListener(string type, MyEventHandler handler)
    {
        if (handler == null)
            return;

        if (oneOfflisteners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]+=..
            oneOfflisteners[type] += handler;
        }
        else
        {
            oneOfflisteners.Add(type, handler);
        }
    }

    public static void AddEventListener(MessageCommand type, MyEventHandler handler)
    {
        AddEventListener(type.ToString(), handler);
    }

    public static void AddEventListener(string type, MyEventHandler handler)
    {
        if (handler == null)
            return;

        if (listeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]+=..
            listeners[type] += handler;
        }
        else
        {
            listeners.Add(type, handler);
        }
    }

    public static void RemoveEventListener(MessageCommand type, MyEventHandler handler)
    {
        RemoveEventListener(type.ToString(), handler);
    }

    public static void RemoveEventListener(string type, MyEventHandler handler)
    {
        if (handler == null)
            return;

        if (listeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]-=..
            listeners[type] -= handler;
            if (listeners[type] == null)
            {
                //已经没有监听者了，移除.
                listeners.Remove(type);
            }
        }
    }

    private static readonly string szErrorMessage = "DispatchEvent Error, Event:{0}, Error:{1}, {2}";

    public static void DispatchEvent(MessageCommand evt, params object[] objs)
    {
        DispatchEvent(evt.ToString(), objs);
    }
    public static void DispatchEvent(string evt, params object[] objs)
    {
        try
        {
            if (listeners.ContainsKey(evt))
            {
                MyEventHandler handler = listeners[evt];
                if (handler != null)
                    handler(objs);
            }

            if (oneOfflisteners.ContainsKey(evt))
            {
                MyEventHandler handler = oneOfflisteners[evt];
                if (handler != null)
                {
                    handler(objs);
                }
                //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]-=..
                oneOfflisteners[evt] -= handler;
                if (oneOfflisteners[evt] == null)
                {
                    //已经没有监听者了，移除.
                    oneOfflisteners.Remove(evt);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format(szErrorMessage, evt, ex.Message, ex.StackTrace));
        }
    }

    public static void Regist(string type, MyEventHandler handler)
    {
        AddEventListener(type, handler);
    }

    public static void UnRegist(string type, MyEventHandler handler)
    {
        RemoveEventListener(type, handler);
    }

    public static void ClearAll()
    {
        listeners.Clear();
    }

    public static void ClearEvents(string key)
    {
        if (listeners.ContainsKey(key))
        {
            listeners.Remove(key);
        }
    }

    static EventDispatcher GameWorldEventDispatcher = new EventDispatcher();
}


