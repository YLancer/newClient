using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

public class XmlSaver
{   
	//内容加密
	public string Encrypt (string toE)
	{
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes (GDM.SKEY);
		RijndaelManaged rDel = new RijndaelManaged ();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateEncryptor ();
		
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes (toE);
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		
		return Convert.ToBase64String (resultArray, 0, resultArray.Length);
	}
	
	//内容解密
	public string Decrypt (string toD)
	{
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes (GDM.SKEY);
		
		RijndaelManaged rDel = new RijndaelManaged ();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateDecryptor ();
		
		byte[] toEncryptArray = Convert.FromBase64String (toD);
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		
		return UTF8Encoding.UTF8.GetString (resultArray);
	}
	
	public string SerializeObject (object pObject, System.Type ty)
	{
		string XmlizedString = null;
		MemoryStream memoryStream = new MemoryStream ();
		XmlSerializer xs = new XmlSerializer (ty);
		XmlTextWriter xmlTextWriter = new XmlTextWriter (memoryStream, Encoding.UTF8);
		xs.Serialize (xmlTextWriter, pObject);
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
		XmlizedString = UTF8ByteArrayToString (memoryStream.ToArray ());
		return XmlizedString;
	}
	
	public object DeserializeObject (string pXmlizedString, System.Type ty)
	{
		XmlSerializer xs = new XmlSerializer (ty);
		MemoryStream memoryStream = new MemoryStream (StringToUTF8ByteArray (pXmlizedString));
		XmlTextWriter xmlTextWriter = new XmlTextWriter (memoryStream, Encoding.UTF8);
		return xs.Deserialize (memoryStream);
	}
	
	//创建XML文件
	public void CreateXML (string fileName, string thisData)
	{
		Debug.Log("save:" + fileName);
		StreamWriter writer;
		writer = System.IO.File.CreateText (fileName);

		if (GDM.encrypt) {
			string xxx = Encrypt (thisData);
			writer.Write (xxx);
		} else {
			writer.Write (thisData);
		}

		writer.Close ();
	}
	
	//读取XML文件
	public string LoadXML (string fileName)
	{
//		Debug.Log (fileName);
		StreamReader sReader = File.OpenText (fileName);
		string dataString = sReader.ReadToEnd ();
		sReader.Close ();

		if (GDM.encrypt) {
			try
			{
				string xxx = Decrypt (dataString);
				return xxx;
			}
			catch(Exception e)
			{
				//加密的解析不来
				return dataString;
			}
		} else {
			return dataString;
		}
	}
	
	//判断是否存在文件
	public bool hasFile (String fileName)
	{
		return File.Exists (fileName);
	}

	public string UTF8ByteArrayToString (byte[] characters)
	{    
		UTF8Encoding encoding = new UTF8Encoding ();
		string constructedString = encoding.GetString (characters);
		return (constructedString);
	}
	
	public byte[] StringToUTF8ByteArray (String pXmlString)
	{
		UTF8Encoding encoding = new UTF8Encoding ();
		byte[] byteArray = encoding.GetBytes (pXmlString);
		return byteArray;
	}
}