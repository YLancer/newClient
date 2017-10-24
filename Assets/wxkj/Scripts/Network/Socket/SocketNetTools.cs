using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using packet.msgbase;

public class SocketNetTools : MonoBehaviour
{
    private NetClient client;
    // 连接成功
    public System.Action OnConnect;
    public Queue<PacketBase> pools = new Queue<PacketBase>();
    private Dictionary<int, System.Action<PacketBase>> listeners = new Dictionary<int, System.Action<PacketBase>>();
    private Dictionary<int, System.Action<PacketBase>> onceListeners = new Dictionary<int, System.Action<PacketBase>>();
    private bool connectFinish = false;

    public void StartClient(string address,int port)
    {
        client = new NetClient();
        client.address = address;
        client.port = port;
        client.receiveCallBack -= OnReceive;
        client.receiveCallBack += OnReceive;
        client.connectCallBack -= connectCallBack;
        client.connectCallBack += connectCallBack;
        client.StartClient();
        connectFinish = false;
    }

    public void StopClient()
    {
        if (null != client)
        {
            client.receiveCallBack -= OnReceive;
            client.connectCallBack -= connectCallBack;
            client.StopClient();
        }
    }

    public bool Connected
    {
        get
        {
            return null != client && client.Connected;
        }
    }

    void connectCallBack()
    {
        connectFinish = true;
    }

    void Update()
    {
        if (connectFinish)
        {
            connectFinish = false;
            if(null != OnConnect)
            {
                OnConnect();
            }
        }

        if (pools.Count > 0)
        {
            PacketBase msg = pools.Dequeue();
            if (null != msg)
            {
                DispatchEvent((int)msg.packetType, msg);
                DispatchOnceEvent((int)msg.packetType, msg);
            }
        }
    }

    public void SendMsg(PacketBase msg)
    {
        client.SendMsg(msg);
    }

    public void SendMsg(PacketBase msg, PacketType cmd, System.Action<PacketBase> callback)
    {
        AddEventOnceListener((int)cmd, callback);
        client.SendMsg(msg);
    }

    void OnDestroy()
    {
        StopClient();
    }

    void OnReceive(PacketBase msg)
    {
        pools.Enqueue(msg);
    }

#region Event
    public void AddEventListener(int type, System.Action<PacketBase> handler)
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

    private void AddEventOnceListener(int type, System.Action<PacketBase> handler)
    {
        if (handler == null)
            return;

        if (onceListeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]+=..
            onceListeners[type] += handler;
        }
        else
        {
            onceListeners.Add(type, handler);
        }
    }

    public void RemoveEventListener(int type, System.Action<PacketBase> handler)
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

    private void RemoveEventOnceListener(int type, System.Action<PacketBase> handler)
    {
        if (handler == null)
            return;

        if (onceListeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]-=..
            onceListeners[type] -= handler;
            if (onceListeners[type] == null)
            {
                //已经没有监听者了，移除.
                onceListeners.Remove(type);
            }
        }
    }

    private static readonly string szErrorMessage = "NetworkManager Error, Event:{0}, Error:{1}, {2}";

    public void DispatchEvent(int evt, PacketBase msg)
    {
        try
        {
            if (listeners.ContainsKey(evt))
            {
                System.Action<PacketBase> handler = listeners[evt];
                if (handler != null)
                    handler(msg);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format(szErrorMessage, evt, ex.Message, ex.StackTrace));
        }
    }

    private void DispatchOnceEvent(int evt, PacketBase msg)
    {
        try
        {
            if (onceListeners.ContainsKey(evt))
            {
                System.Action<PacketBase> handler = onceListeners[evt];
                if (handler != null)
                {
                    handler(msg);
                    RemoveEventOnceListener(evt, handler);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format(szErrorMessage, evt, ex.Message, ex.StackTrace));
        }
    }

    public void ClearAll()
    {
        listeners.Clear();
    }

    public void ClearEvents(int key)
    {
        if (listeners.ContainsKey(key))
        {
            listeners.Remove(key);
        }
    }
#endregion
}
