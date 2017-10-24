using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;
using System.Security.Cryptography;

public static class SaveLoad {
	
	//-- The names of these methods are INTENTIONALLY named incorrect for the prying eyes of users who care to 
	//-- look at the DLLs at runtime.
	
	private static string saveClass = "BonusItemProtoData";
	
	//-- Md5 the data and check against the read in hash.
	public static bool Load(Dictionary<string, object> data) {
		bool success = false;
		if(data == null)
		{
			Debug.Log("SaveLoad.Load(data)  data is null");
			return success;
		}
		
		string hashFromFile = null;
		if(data.ContainsKey("hash")) {
			hashFromFile = (string)data["hash"];
		}
		
		if(data.ContainsKey("data") && hashFromFile != null)
		{
			//-- Security check
			object secureData = data["data"] as object;
            string hash = SaveLoad.checkState(MiniJson.Json.Serialize(secureData) + saveClass);
			//Debug.LogError("HashFromFile:" + hashFromFile);
			//Debug.LogError("hash:" + hash);
			if(hash.CompareTo(hashFromFile) == 0) { 
				success = true;
			}
			else {
				Debug.Log("hashed are diff. {0} != {1}");
			}
		}
		else {
			if(hashFromFile == null) {
				Debug.Log("hashFromFile needs to be not null");
			}
			if(data.ContainsKey("data") == false) {
				Debug.Log("no data node found.");
			}
			
		}
		return success;
	}
	
	//-- MD5 the data and return a new dictionary with that hash.
	public static Dictionary<string, object> Save(object data) {
        string hash = SaveLoad.checkState(MiniJson.Json.Serialize(data) + saveClass);
		Dictionary<string, object> secureData = new Dictionary<string, object>();
		secureData.Add ("hash", hash);
		//TR.LOG ("Hash = {0}", hash);
		secureData.Add ("data", data);
		return secureData;
	}
	
	//-- Md5 the string and return a hash.
	private static string checkState(string strToEncrypt)
	{
		UTF8Encoding encoding = new System.Text.UTF8Encoding();
		byte[] bytes = encoding.GetBytes(strToEncrypt);
		
		// encrypt bytes
		MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (long i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, "0"[0]);
		}
		
		return hashString.PadLeft(32, "0"[0]);
	}
}