using UnityEngine;
using System.Collections;
using packet.msgbase;
using packet.game;
using packet.user;

public class NetworkTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Game.SocketGame.AddEventListener(PacketType.BACK_TO_HALL, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.CHANGE_DESK, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.CHAT, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.ENROLL, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.BACK_TO_HALL, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.BACK_TO_HALL, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.BACK_TO_HALL, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.BACK_TO_HALL, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.BACK_TO_HALL, OnMsgCome);
        //Game.SocketGame.AddEventListener(PacketType.BACK_TO_HALL, OnMsgCome);
    }

    // Update is called once per frame
    void OnDestroy () {
	
	}

    void OnMsgCome(PacketBase msg)
    {

    }
}
