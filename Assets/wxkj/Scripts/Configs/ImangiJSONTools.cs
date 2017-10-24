using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImangiUtilities
{
	public partial class JSONTools
	{
		public static bool ReadBool(object inObj, bool inDefaultValue = false)
		{
			if (inObj != null)
			{
				if (inObj.GetType() == typeof(bool))
				{
					return (bool)inObj;
				}
			}
			return inDefaultValue;
		}

		public static int ReadInt(object inObj)
		{
			if (inObj != null)
			{
				if (inObj.GetType() == typeof(Int64))
				{
					return (int)((long) inObj);
				}
				else if (inObj.GetType() == typeof(int))
				{
					return (int)(inObj);
				}
			}
			return 0;
		}
		
		public static Int64 ReadInt64(object inObj)
		{
			if (inObj != null)
			{
				if (inObj.GetType() == typeof(Int64))
				{
					return (Int64)(inObj);
				}
				else if (inObj.GetType() == typeof(int))
				{
					return (Int64)((int)(inObj));
				}
			}
			return 0;
		}
		
		public static uint ReadUInt(object inObj)
		{
			if (inObj != null)
			{
				if (inObj.GetType() == typeof(Int64))
				{
					return (uint)((long) inObj);
				}
				else if(inObj.GetType() == typeof(uint))
				{
					return (uint)(inObj);
				}
			}
			return 0;
		}
		
		public static UInt64 ReadUInt64(object inObj)
		{
			if (inObj != null)
			{
				if (inObj.GetType() == typeof(UInt64))
				{
					return (UInt64)(inObj);
				}
				else if (inObj.GetType() == typeof(uint))
				{
					return (UInt64)((uint)(inObj));
				}
				else if (inObj.GetType() == typeof(Int64))
				{
					return (UInt64)((Int64)(inObj));
				}
				else if (inObj.GetType() == typeof(int))
				{
					return (UInt64)((uint)(inObj));
				}
			}
			return 0;
		}

		public static double ReadDouble(object inObj)
		{
			if (inObj != null)
			{
				if (inObj.GetType() == typeof(Int64))
				{
					return (double)((long) inObj);
				}
				else if (inObj.GetType() == typeof(Double))
				{
					return (double)(inObj);
				}
			}
			return 0.0;
		}

		public static Vector3 ReadVector3(object inObj)
		{
			if (inObj != null)
			{
				if (inObj.GetType() == typeof(Vector3))
				{
					return (Vector3)inObj;
				}
				else if (inObj.GetType() == typeof(IList))
				{
					Vector3 returnValue = Vector3.zero;

					List<float> floatList = ReadFloatList(inObj);
					if (floatList != null)
					{
						if (floatList.Count == 3)
						{
							returnValue.x = floatList[0];
							returnValue.y = floatList[1];
							returnValue.z = floatList[2];
						}
					}

					return returnValue;
				}
				else if (inObj.GetType() == typeof(string))
				{
					Vector3 returnValue = Vector3.zero;

					// Argh - parse it
					string tempString = inObj as String;
					if (tempString != null)
					{
						tempString = tempString.Replace("(", "");
						tempString = tempString.Replace(")", "");
						string[] splitStrings = tempString.Split(new char[] { ',' }, StringSplitOptions.None);
						if ((splitStrings.Length >= 1) && (splitStrings.Length <= 3))
						{
							for (int parseIdx = 0; parseIdx < splitStrings.Length; parseIdx++)
							{
								string number = splitStrings[parseIdx];
								float value = 0.0f;

								if (float.TryParse(number, out value) == true)
								{
									switch (parseIdx)
									{
									case 0:
										returnValue.x = value;
										break;
									case 1:
										returnValue.y = value;
										break;
									case 2:
										returnValue.z = value;
										break;
									}
								}
							}

							return returnValue;
						}
					}
				}
			}

			return Vector3.zero;
		}
		
		public static string ReadString(object inObj, string inDefaultValue = null)
		{
			string retValue = inDefaultValue;
			if ((inObj != null) && (inObj.GetType() == typeof(string)))
			{
				retValue = inObj as string;
			}
			
			return retValue;
		}

		public static List<object> ReadJSONList( IDictionary inBaseDictionary, string inKey )
		{
			object data;
			if ( inBaseDictionary.Contains( inKey ) )
			{
				data = inBaseDictionary[inKey];
				return data as List<object>;
			}
			
			return null;
		}

		public static List<int> ReadIntList(object inObj)
		{
			IList objList = inObj as IList;
			
			List<int> intList = null;
			
			if (objList != null)
			{
				intList = new List<int>(objList.Count);
				
				int tempInt;
				for (int listIdx = 0; listIdx < objList.Count; ++listIdx)
				{
					tempInt = ReadInt(objList[listIdx]);
					intList.Add(tempInt);
				}
			}
			
			return intList;
		}

		public static List<float> ReadFloatList(object inObj)
		{
			IList objList = inObj as IList;
			
			List<float> floatList = null;
			
			if (objList != null)
			{
				floatList = new List<float>(objList.Count);
				
				float tempFloat;
				for (int listIdx = 0; listIdx < objList.Count; ++listIdx)
				{
					tempFloat = (float)ReadDouble(objList[listIdx]);
					floatList.Add(tempFloat);
				}
			}
			
			return floatList;
		}
		
		public static List<string> ReadStringList(object inObj)
		{
			IList objList = inObj as IList;
			
			List<string> stringList = null;
			
			if (objList != null)
			{
				stringList = new List<string>(objList.Count);
				
				string tempString;
				for (int listIdx = 0; listIdx < objList.Count; ++listIdx)
				{
					tempString = objList[listIdx].ToString();
					stringList.Add(tempString);
				}
			}
			
			return stringList;
		}

		public static List<List<string>> ReadStringListList(object inObj)
		{
			IList objList = inObj as IList;

			List<List<string>> stringListList = null;

			if (objList != null)
			{
				stringListList = new List<List<string>>(objList.Count);
				string tempString;

				List<object> tempList;
				for (int listIdx = 0; listIdx < objList.Count; listIdx++)
				{
					tempList = objList[listIdx] as List<object>;
					if (tempList != null)
					{
						List<string> tempStringList = new List<string>();
						for (int tempIdx = 0; tempIdx < tempList.Count; tempIdx++)
						{
							tempString = tempList[tempIdx] as string;
							tempStringList.Add(tempString);
						}

						stringListList.Add(tempStringList);
					}
				}
			}

			return stringListList;
		}

		public static TEnum ReadEnum<TEnum>( object inObj, TEnum inDefaultValue ) where TEnum : struct, System.IComparable, System.IFormattable, System.IConvertible
		{
			TEnum retValue = inDefaultValue;
			
			if ( System.Enum.IsDefined( typeof( TEnum ), inObj ) )
			{
				retValue = (TEnum)System.Enum.Parse( typeof( TEnum ), inObj.ToString() );
			}
			
			return retValue;
		}

        #region IDS
        public static TEnum ReadEnumUnsafe<TEnum>(object inObj) where TEnum : struct, System.IComparable, System.IFormattable, System.IConvertible
        {
            return (TEnum)System.Enum.Parse(typeof(TEnum), inObj.ToString());
        }
        #endregion

        public static List<TEnum> ReadEnumList<TEnum>( object inObj ) where TEnum : struct, System.IComparable, System.IFormattable, System.IConvertible
		{
			IList objList = inObj as IList;
			
			List<TEnum> enumList = null;
			
			if ( objList != null )
			{
				enumList = new List<TEnum>( objList.Count );
				
				for ( int listIdx = 0; listIdx < objList.Count; ++listIdx )
				{
					if ( System.Enum.IsDefined( typeof( TEnum ), objList[listIdx] ) )
					{
						enumList.Add( (TEnum)System.Enum.Parse( typeof( TEnum ), (string)objList[listIdx] ) );
					}
				}
			}
			
			return enumList;
		}
		
		public static Dictionary<string, object> ReadDictionary( IDictionary inBaseDictionary, string inKey )
		{
			if ( inBaseDictionary != null && inBaseDictionary.Contains( inKey ) )
			{
				object data;
				data = inBaseDictionary[inKey];
				return data as Dictionary<string, object>;
			}

			return null;
		}

		public static string WriteDictionaryIntInt(Dictionary<int,int> inDictionary)
		{
			if (inDictionary != null)
			{
				Dictionary<string,object> tempDict = new Dictionary<string,object>();
				foreach (KeyValuePair<int,int> kvp in inDictionary)
				{
					string keyName = kvp.Key.ToString();
					int value = kvp.Value;
					tempDict.Add(keyName, value);
				}

				return tempDict.toJson();
			}

			return "";
		}

		public static Dictionary<int,int> ReadDictionaryIntInt(IDictionary inBaseDictionary, string inKey)
		{
			Dictionary<string,object> tempDict = ReadDictionary(inBaseDictionary, inKey);
			if (tempDict != null)
			{
				Dictionary<int,int> returnDict = new Dictionary<int,int>();

				foreach (KeyValuePair<string,object> kvp in tempDict)
				{
					int newKey = int.Parse(kvp.Key);
					int newValue = Convert.ToInt32(kvp.Value);

					returnDict.Add(newKey, newValue);
				}
				return returnDict;
			}
			return null;
		}

		/** Used to parse dictionaries from a JSON list */
		public static bool ObjectListToFromDictList<T>(List<object> inJSONList, ref List<T> data) where T : IParsableJSONListElement, new()
		{
			if (inJSONList != null)
			{
				int count = inJSONList.Count;
				for (int idx = 0; idx < count; ++idx)
				{
					Dictionary<string,object> dict = inJSONList[idx] as Dictionary<string,object>;
					if (dict != null)
					{
						T element = new T();
						element.FromDict(dict);
						data.Add(element);
					}
				}
				
				return true;
			}
			
			return false;
		}
		
		/** Used for saving a list of JSON elements back to */
		public static List<object> ToDictListToObjectList<T>(List<T> inList) where T : IParsableJSONListElement
		{
			List<object> objectList = new List<object>();
			
			if(inList != null)
			{
				for (int entryIdx = 0; entryIdx < inList.Count; entryIdx++)
				{
					objectList.Add(inList[entryIdx].ToDict());
				}
			}
			
			return objectList;
		}
	}

	/** Interface used to assist generalizing parsing lists from JSON files */
	public interface IParsableJSONListElement
	{
		/** Read data from dictionary; return true if successful, false if not */
		bool FromDict(Dictionary<string, object> inDict);

		/** Write data to dictionary */
		Dictionary<string, object> ToDict();
	}

	/** 
	 * Static methods for IParsableJSONListElement instances
	 * 
	 * Usage:
	 * 	IParsableJSONListElement element;
	 * 
	 * 	string jsonString = element.ToJSONString();
	 * 	bool succcess = element.FromJSONString(jsonString);
	 * 
	 * In a class function that implements the IParsableJSONListElement, you can do the following:
	 * 	this.ToJSONString();
	 * 	this.FromJSONString(jsonString);
	 * 
	 */
	public static class IParsableJSONListElementMethods
	{
		public static string ToJSONString(this IParsableJSONListElement inElement)
		{
			Dictionary<string,object> data = inElement.ToDict();
			if (data != null)
			{
				return MiniJsonTRExtensions.toJson(data);
			}
			
			return "";
		}
		
		public static bool FromJSONString(this IParsableJSONListElement inElement, string inJSONString)
		{
			if (string.IsNullOrEmpty(inJSONString) == false)
			{
				Dictionary<string,object> JSONDict = MiniJsonTRExtensions.dictionaryFromJson(inJSONString);
				if (JSONDict != null)			
				{
					return inElement.FromDict(JSONDict);
				}
			}

			return false;
		}
	}
}	//namespace ImangiUtilities
