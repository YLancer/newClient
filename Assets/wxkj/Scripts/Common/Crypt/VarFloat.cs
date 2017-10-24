using System;
using UnityEngine;
using System.Text;

//鍙互鐩戝惉鍙樺寲锛屾湁鍐呭瓨鍔犲瘑
public class VarFloat
{
	private byte _checksum;
	protected float _value;
	protected long _tick;
	
	protected float Get ()
	{
		return Var_Get (_value);
	}

	protected void Set (float t)
	{
		_value = Var_Set (t);
	}
	
	protected void Set (float t, long code)
	{
		_tick ^= code;
		_value = Var_Set (t);
	}
	
	//=======================涓嬮潰鏂板
	public delegate void OnChangeDelegate (float value);
	
	public OnChangeDelegate OnChange;
	
	public void FireOnChange ()
	{
		NotifyOnChange ();
	}
	
	private void NotifyOnChange ()
	{
		if (OnChange != null)
		{
			OnChange (Get ());
		}	
	}
	
	public float Value
	{
		get
		{
			return Get ();
		}
		
		set
		{
			bool changed = value.CompareTo (Get ()) != 0;	
			Set (value);
			if (changed)
			{
				NotifyOnChange ();
			}
		}
	}

	byte[] bytes;
	byte[] bytes2;

	public VarFloat ()
	{
		Set (0.0f);
	}

	public VarFloat (float Value)
	{
		Set (Value);
	}

	StringBuilder strResult;
	StringBuilder endPart;

	protected float Var_Get (float Value)
	{
		return getDecryptFloat (Value);

//		bytes= BitConverter.GetBytes(Value);
//        bytes2 = BitConverter.GetBytes(_tick);
//        for (int i = 0; i < bytes.Length; i++)
//        {
//            bytes[i] = (byte) ((bytes[i] ^ 0x12) ^ bytes2[i]);
//        }
//        return BitConverter.ToSingle(bytes, 0);
	}

	protected float Var_Set (float Value)
	{
		return getEncryptFloat (Value);

//        bytes = BitConverter.GetBytes(Value);
//        bytes2 = BitConverter.GetBytes(_tick);
//        for (int i = 0; i < bytes.Length; i++)
//        {
//            bytes[i] = (byte) ((bytes[i] ^ 0x12) ^ bytes2[i]);
//        }
//        return (long) BitConverter.ToInt32(bytes, 0);
	}

	int[] arrEncryptMap = {5,7,8,9,4,2,3,6,1};
	private float getEncryptFloat (float Value)
	{
		return DataLock.getMyFloat (Value , arrEncryptMap);
	}
	int[] arrDecryptMap = {9,6,7,5,1,8,2,3,4};
	private float getDecryptFloat (float Value)
	{
		return DataLock.getMyFloat (Value , arrDecryptMap);
	}

	/*private float getEncryptFloat(float Value)
	{
		int iIntPart = Mathf.FloorToInt (Value);
		string strIntPart = iIntPart.ToString ();
		if (strResult == null)
		{
			strResult = new StringBuilder ();
		}
		else
		{
			strResult.Remove(0,strResult.Length);
		}
		
		if (endPart == null)
		{
			endPart = new StringBuilder ();
		}
		else
		{
			endPart.Remove(0,endPart.Length);
		}
		bool StopAddEndPart = false;
		int index = 0;
		for (int i = strIntPart.Length - 1; i >= 0; i --)
		{
			if(strIntPart[i] != '0')
			{
				StopAddEndPart = true;
			}
			if(!StopAddEndPart)
			{
				endPart.Append("0");
			}
			else if(strIntPart[i] != '-')
			{
				strResult.Append(strIntPart[i]);
			}
		}
		
		strResult.Append (endPart);
		int iNewResult = int.Parse (strResult.ToString());
		iNewResult *= iIntPart > 0 ? 1 : -1;
		return iNewResult + Value - iIntPart;
	}*/
}

