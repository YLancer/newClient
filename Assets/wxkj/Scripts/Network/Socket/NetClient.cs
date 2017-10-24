using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using packet.msgbase;
using System.Reflection;

public class NetClient
{
	public string address = "";
    public int port = 5000;
    private Socket client;
    //用于存放接收数据
    public byte[] buffer;
    //每次接受和发送数据的大小
    private const int size = 1024;
    //接收数据池
    private List<byte> receiveCache;
    private bool isReceiving;
    // 连接成功
    public Action connectCallBack;
    //接收到消息之后的回调
    public Action<PacketBase> receiveCallBack;

    public void StartClient()
    {
        buffer = new byte[size];
        receiveCache = new List<byte>();

        IPAddress[] adds = Dns.GetHostAddresses(address);

        if (adds[0].AddressFamily == AddressFamily.InterNetworkV6)
        {
            Debug.Log("Connect InterNetworkV6");
            client = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
        }
        else
        {
            Debug.Log("Connect InterNetwork");
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        IAsyncResult result = client.BeginConnect(adds, port, AsyncAccept2, client);
        //IAsyncResult result = client.BeginConnect(IPAddress.Parse(address), port, AsyncAccept2, client);

        DelayUtils.Start(waitConnection(result));
        Debug.Log(string.Format("StartClient {0}:{1}", address, port));
    }

    private IEnumerator waitConnection(IAsyncResult result)
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);
        while (!result.IsCompleted)
        {
            yield return wait;
        }
        AsyncAccept(result);
    }

    private void AsyncAccept2(IAsyncResult reuslt)
    {
        Debug.Log("AsnycAccept2");
    }

    public void StopClient()
    {
        if (client == null)
            return;

        if (!client.Connected)
            return;

        try
        {
            client.Shutdown(SocketShutdown.Both);
            Debug.Log("Shutdown Socket");
        }
        catch
        {
        }

        try
        {
            client.Close();
            Debug.Log("Close Socket");
        }
        catch
        {
        }
    }

    public bool Connected
    {
        get
        {
            return null != client && client.Connected;
        }
    }

    public void SendMsg(PacketBase msg)
    {
        if (Connected)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            string dataStr = Utils.ToStr(Utils.DeSerialize(msg));
            if(msg.packetType != PacketType.HEARTBEAT)
            {
                Debug.LogFormat("=>>:{0}; code:{1}; msg:{2};data:[{3}]", msg.packetType, msg.code, msg.msg, dataStr);
                //Debug.LogFormat("=>>:{0}; code:{1}; msg:{2};data:[{3}]", msg.packetType, msg.code, msg.msg, dataStr);
            }
#endif

            byte[] datas = NetSerilizer.Serialize(msg);

            byte[] result = NetEncode.Encode(datas);// Encoding.UTF8.GetBytes(msg);
            Debug.Log(result.Length);
            client.Send(result);
        }
        else
        {
            Debug.LogWarning("SendMsg not Connected : " + msg.packetType);
        }
    }

    //回调函数， 有客户端连接的时候会自动调用此方法
    private void AsyncAccept(IAsyncResult result)
    {
        if (null != connectCallBack)
        {
            connectCallBack();
        }

        try
        {
            Socket client = result.AsyncState as Socket;
            Debug.Log("AsyncAccept");
            BeginReceive(client);
            //尾递归，再次监听是否还有其他客户端连入
            //tl.BeginAcceptSocket(AsyncAccept, null);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    //异步监听消息
    private void BeginReceive(Socket client)
    {
        try
        {
            //Debug.Log("BeginReceive");
            //异步方法
            //client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, EndReceive, client);
            IAsyncResult result = client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, EndReceive2, client);
            DelayUtils.Start(waitReceive(result));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    private IEnumerator waitReceive(IAsyncResult result)
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);
        while (!result.IsCompleted)
        {
            yield return wait;
        }
        EndReceive(result);
    }

    private void EndReceive2(IAsyncResult result)
    {
        //Debug.Log("EndReceive2");
    }

    //监听到消息之后调用的函数
    private void EndReceive(IAsyncResult result)
    {
        try
        {
            Socket client = result.AsyncState as Socket;
            //获取消息的长度
            int len = client.EndReceive(result);
            if (len > 0)
            {
                byte[] data = new byte[len];
                Buffer.BlockCopy(buffer, 0, data, 0, len);
                //用户接受消息
                Receive(data);
                //尾递归，再次监听消息
                BeginReceive(client);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    // 服务器接受客户端发送的消息
    public void Receive(byte[] data)
    {
        //UnityEngine.Debug.Log("接收到数据");
        //将接收到的数据放入数据池中
        receiveCache.AddRange(data);
        //如果没在读数据
        if (!isReceiving)
        {
            isReceiving = true;
            ReadData();
        }
    }

    // 读取数据
    private void ReadData()
    {
        byte[] data = NetEncode.Decode(ref receiveCache);
        //说明获取到一条完整数据
        if (data != null)
        {
            PacketBase msg = NetSerilizer.DeSerialize<PacketBase>(data);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            string dataStr = Utils.ToStr(Utils.DeSerialize(msg));
            Debug.LogFormat("<<=:{0}; code:{1}; msg:{2};data:[{3}]", msg.packetType, msg.code, msg.msg, dataStr);
            //Debug.LogFormat("<<=:{0}; code:{1}; msg:{2};data:[{3}]", msg.packetType, msg.code, msg.msg, dataStr);
#endif

            //if(msg.code != 0 && string.IsNullOrEmpty(msg.msg) == false)
            //{
            //    Game.DialogMgr.ShowDialog1Btn(msg.msg);
            //}

            if (receiveCallBack != null)
            {
                receiveCallBack(msg);
            }
            //尾递归，继续读取数据
            ReadData();
        }
        else
        {
            isReceiving = false;
        }
    }
}
