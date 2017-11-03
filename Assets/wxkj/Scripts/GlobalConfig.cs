using UnityEngine;
using System.Collections;

public class GlobalConfig : MonoBehaviour 
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
	//public static string address = "www.xskdqmj3d.cn:5000";//121.40.177.10
    public static string address = "103.26.79.31:5000";//121.40.177.10   103.47.82.46:5000
#else
    public static string address = "www.xskdqmj3d.cn:5000";
#endif

#if UNITY_IOS || UNITY_IPHONE
	public const string URL_ROOT = "https://103.26.79.31";
	//public const string URL_ROOT = "https://103.26.79.31";
#else
    public const string URL_ROOT = "https://103.26.79.31";
	#endif

	public const string SECRET = "fbfea1f1bc0342a22a28f0c93bfd94c0e9870a0f";

    //版本号
    public static string GetVersion
	{
        get
        {
            return "1.0.0";
        }
	}
	
	//appkey
	public static string GetAppKey
	{
		get { return "3a0d05645c96a5eea7e6"; }
	}

    //平台ID//设备号  1:ios 2:android 3:winphon 4:other
    public static int GetPlatformId
	{
		get
		{
			#if    UNITY_ANDROID
			return 2;
			#elif  UNITY_IOS || UNITY_IPHONE
			return 1;
			#endif
			return 4;
		}
	}

    public static string DeviceId
    {
        get
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }

	public static float HeartBeatTime = 30;
}
