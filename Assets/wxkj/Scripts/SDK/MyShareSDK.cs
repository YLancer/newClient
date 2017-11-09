using UnityEngine;
using System.Collections;
using cn.sharesdk.unity3d;
public class MyShareSDK : MonoBehaviour {

    private static MyShareSDK myshareSDK = null;

    public static MyShareSDK MyshareSDK
    {
        get { return MyShareSDK.myshareSDK; }
        set { MyShareSDK.myshareSDK = value; }
    }
  
    public int name = 123;       
    public ShareSDK shareSdk;
	// Use this for initialization
	void Start () {
        myshareSDK = this;
        if (shareSdk != null)
        {
            shareSdk.showUserHandler = getUserInforCallback;
            
        }
        else
        {
            Debug.Log("shareSdk为空");
        }
	}

    private string _token;
    private string _opendid;
    //获取微信个人信息成功回调,登录	  
    public void getUserInforCallback(int reqID, ResponseState state, PlatformType type, Hashtable data)
    {
        Debug.Log("ranger getUserInforCallback Start");
        print("ranger" + data.toJson() + MiniJSON.jsonEncode(data));
        _opendid = (string)data["openid"];
        _token = (string)data["unionid"];
        print("ranger" + data["nickname"] + "Token" + _token + "Openid" + _opendid);
    }

    public string Username() 
    {
        return _opendid;
    }
    public string Password()
    {
        return _token;
    }
}
