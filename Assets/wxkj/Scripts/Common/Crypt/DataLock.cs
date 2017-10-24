using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

public class DataRand
{
    public int rand1;
    public int rand2;
    public int rand3;
}

public class DataRand_F
{
	public int rand1;
	public int rand2;
	public float rand3;
}

public class DataLock
{
	private static Dictionary<string, DataRand_F> rDataF = new Dictionary<string, DataRand_F>();
    private static Dictionary<string, DataRand> rData = new Dictionary<string, DataRand>();

    public static DataLock Instance
    {
        get
        {
			if(_Instance == null)
			{
				_Instance = new DataLock();
			}

			return _Instance;
        }
    }
	private static DataLock _Instance;

    public int getintSimpleData(string key)
    {
        if (!rData.ContainsKey(key))
        {
            return 0;
        }
        DataRand rand = rData[key];
		return (rand.rand1 + rand.rand2 + rand.rand3);
    }

    public void setintSimpleData(string key, int value)
    {
		DataRand dr;
		
		if (rData.ContainsKey(key))
		{
			dr = rData[key];
		}
		else
		{
			dr = new DataRand();
			rData.Add(key, dr);
		}
		
		dr.rand1 = Random.Range(value - 10000, value + 10000);
		dr.rand2 = Random.Range(value - 10000, value + 10000);
		dr.rand3 = value - dr.rand1 - dr.rand2;
    }

    public float getfloatSimpleData(string key)
    {
		if (!rDataF.ContainsKey(key))
		{
			return 0;
		}

		DataRand_F rand = rDataF[key];
		return (rand.rand1 + rand.rand2 + rand.rand3);
    }

    public void setfloatSimpleData(string key, float fValue)
    {
        //int value = (int)(fValue * 100000);
        //setSimpleData(key, value);
		DataRand_F dr = null;
		
		if (rDataF.ContainsKey(key))
		{
			dr = rDataF[key];
		}
		else
		{
			dr = new DataRand_F();
			rDataF.Add(key, dr);
		}

		int value = (int)fValue;
		dr.rand1 = Random.Range(value - 10000, value + 10000);
		dr.rand2 = Random.Range(value - 10000, value + 10000);
		dr.rand3 = fValue - dr.rand1 - dr.rand2;
    }

    public bool getboolSimpleData(string key)
    {
        int bValue = getintSimpleData(key);

        return bValue == 1 ? true : false;
    }

    public void setboolSimpleData(string key, bool bValue)
    {
        int value = bValue ? 1 : 0;
        setintSimpleData(key, value);
    }

	public int getintLockedValue(int value)
	{
		return ~value;
	}

	public int getintUnLockedValue(int value)
	{
		return ~value;
	}

	int[] arrEncryptMap = {5,7,8,9,4,2,3,6,1};
	public float getfloatLockedValue(float value)
	{
		return getMyFloat (value , arrEncryptMap);
	}

	int[] arrDecryptMap = {9,6,7,5,1,8,2,3,4};
	public float getfloatUnLockedValue(float value)
	{
		return getMyFloat (value , arrDecryptMap);
	}

	public static float getMyFloat(float Value , int[] map)
	{
		int iIntPart = Value >= 0 ? Mathf.FloorToInt (Value) : Mathf.FloorToInt(Value) + 1;
		int index = 1;
		int iResult = 0;
		while (true)
		{
			int iValue = (Mathf.Abs(iIntPart) % Mathf.RoundToInt (Mathf.Pow (10, index))) / Mathf.RoundToInt (Mathf.Pow (10, index - 1));
			if (iValue != 0)
			{
				iResult += map [iValue - 1] * Mathf.RoundToInt (Mathf.Pow (10, index - 1));
			}
			index ++;
			if (Mathf.Abs(iIntPart) < Mathf.Pow (10, index - 1))
			{
				break;
			}
		}
		iResult *= iIntPart > 0 ? 1 : -1;
		float dResult = iResult + Value - iIntPart;
		//Debug.LogError ("Decrypt float : " + Value + " to " + dResult);
		return dResult;
	}
}
