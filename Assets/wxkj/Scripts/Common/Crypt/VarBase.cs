using System;
using UnityEngine;

//鍙互鐩戝惉鍙樺寲锛屾湁鍐呭瓨鍔犲瘑
public class VarBase< T > where T : IComparable
{
	private byte _checksum;
	protected long _value;
	protected long _tick;

	protected virtual T Var_Get (long n)
	{
		return default(T);
	}

	protected virtual long Var_Set (T t)
	{
		return 0;
	}

	protected T Get ()
	{
		if (GetChecksum (_value) != _checksum) {
			Set (default(T));
			throw new Exception ("Checksum error");
		}
		return Var_Get (_value);
	}

	private byte GetChecksum (long n)
	{
		byte num = 0;
		byte[] bytes = BitConverter.GetBytes (n);
		int index = 0;
		int length = bytes.Length;
		while (index < length) {
			num = (byte)((num + bytes [index]) & 0xff);
			index++;
		}
		return (byte)(((num ^ 0xff) + 1) & 0xff);
	}

	protected void Set (T t)
	{
		_tick ^= DateTime.Now.Ticks;
		_value = Var_Set (t);
		_checksum = GetChecksum (_value);
	}

	protected void Set (T t, long code)
	{
		_tick ^= code;
		_value = Var_Set (t);
		_checksum = GetChecksum (_value);
	}

	//=======================涓嬮潰鏂板
	public delegate void OnChangeDelegate (T value);

	public OnChangeDelegate OnChange;

	public void FireOnChange ()
	{
		NotifyOnChange ();
	}
	
	private void NotifyOnChange ()
	{
		if (OnChange != null) {
			OnChange (Get ());
		}	
	}
	
	public T Value {
		get {
			return Get ();
		}
		
		set {
			bool changed = value.CompareTo (Get ()) != 0;	
			Set (value);
			if (changed) {
				NotifyOnChange ();
			}
		}
	}
}

