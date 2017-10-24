using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DictionaryHelper {
    public const int DefaultInt = 0;
    public const float DefaultFloat = 0.0f;
    public const double DefaultDouble = 0.0;

    /** Helper functions for serializing from a dictionary save */

    public static bool ReadJSONList(Dictionary<string, object> inDict, string inStringName, ref List<object> refValue)
    {
        if (inDict.ContainsKey(inStringName) == true)
        {
            refValue = JSONTools.ReadJSONList(inDict, inStringName);

            return true;
        }

        return false;
    }

    public static bool ReadStringListList(Dictionary<string, object> inDict, string inListName, ref List<List<string>> refValue)
    {
        if (inDict.ContainsKey(inListName) == true)
        {
            refValue = JSONTools.ReadStringListList(inDict[inListName]);
            return true;
        }

        return false;
    }

    public static bool ReadStringList(Dictionary<string, object> inDict, string inStringName, ref List<string> refValue)
    {
        if (inDict.ContainsKey(inStringName) == true)
        {
            refValue = JSONTools.ReadStringList(inDict[inStringName]);
            return true;
        }

        return false;
    }

    public static string ReadString(Dictionary<string, object> inDict, string inStringName, string inDefaultValue = null)
    {
        string returnVal = inDefaultValue;

        if (inDict.ContainsKey(inStringName) == true)
        {
            returnVal = (string)JSONTools.ReadString(inDict[inStringName]);
        }

        return returnVal;
    }

    public static string ReadString(Dictionary<string, object> inDict, string inStringName, out bool outSuccess, string inDefaultValue = null)
    {
        string returnVal = inDefaultValue;

        if (inDict.ContainsKey(inStringName) == true)
        {
            returnVal = (string)JSONTools.ReadString(inDict[inStringName]);
            outSuccess = true;
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static bool ReadBool(Dictionary<string, object> inDict, string inBoolName, bool inDefaultValue = false)
    {
        bool returnVal = inDefaultValue;

        if (inDict.ContainsKey(inBoolName) == true)
        {
            returnVal = (bool)JSONTools.ReadBool(inDict[inBoolName]);
        }

        return returnVal;
    }

    public static bool ReadBool(Dictionary<string, object> inDict, string inBoolName, out bool outSuccess, bool inDefaultValue = false)
    {
        bool returnVal = inDefaultValue;

        if (inDict.ContainsKey(inBoolName) == true)
        {
            returnVal = (bool)JSONTools.ReadBool(inDict[inBoolName]);
            outSuccess = true;
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static int ReadInt(Dictionary<string, object> inDict, string inIntName, int inDefaultValue = DefaultInt)
    {
        int returnVal = inDefaultValue;

        if (inDict.ContainsKey(inIntName) == true)
        {
            returnVal = JSONTools.ReadInt(inDict[inIntName]);
        }

        return returnVal;
    }

    public static int ReadInt(Dictionary<string, object> inDict, string inIntName, out bool outSuccess, int inDefaultValue = DefaultInt)
    {
        int returnVal = inDefaultValue;

        if (inDict.ContainsKey(inIntName) == true)
        {
            returnVal = JSONTools.ReadInt(inDict[inIntName]);
            outSuccess = true;
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static uint ReadUInt(Dictionary<string, object> inDict, string inUIntName, uint inDefaultValue = DefaultInt)
    {
        uint returnVal = inDefaultValue;

        if (inDict.ContainsKey(inUIntName) == true)
        {
            returnVal = JSONTools.ReadUInt(inDict[inUIntName]);
        }

        return returnVal;
    }

    public static uint ReadUInt(Dictionary<string, object> inDict, string inUIntName, out bool outSuccess, uint inDefaultValue = DefaultInt)
    {
        uint returnVal = inDefaultValue;

        if (inDict.ContainsKey(inUIntName) == true)
        {
            returnVal = JSONTools.ReadUInt(inDict[inUIntName]);
            outSuccess = true;
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static double ReadDouble(Dictionary<string, object> inDict, string inDoubleName, double inDefaultValue = DefaultDouble)
    {
        double returnVal = inDefaultValue;

        if (inDict.ContainsKey(inDoubleName) == true)
        {
            returnVal = JSONTools.ReadDouble(inDict[inDoubleName]);
        }

        return returnVal;
    }

    public static double ReadDouble(Dictionary<string, object> inDict, string inDoubleName, out bool outSuccess, double inDefaultValue = DefaultDouble)
    {
        double returnVal = inDefaultValue;

        if (inDict.ContainsKey(inDoubleName) == true)
        {
            returnVal = JSONTools.ReadDouble(inDict[inDoubleName]);
            outSuccess = true;
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static float ReadFloat(Dictionary<string, object> inDict, string inFloatName, float inDefaultValue = DefaultFloat)
    {
        float returnVal = inDefaultValue;

        if (inDict.ContainsKey(inFloatName) == true)
        {
            returnVal = (float)JSONTools.ReadDouble(inDict[inFloatName]);
        }

        return returnVal;
    }

    public static float ReadFloat(Dictionary<string, object> inDict, string inFloatName, out bool outSuccess, float inDefaultValue = DefaultFloat)
    {
        float returnVal = inDefaultValue;

        if (inDict.ContainsKey(inFloatName) == true)
        {
            returnVal = (float)JSONTools.ReadDouble(inDict[inFloatName]);
            outSuccess = true;
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static void AddVector3(Dictionary<string, object> inDict, string inVector3Name, Vector3 inValue)
    {
        List<float> valueArray = new List<float>();
        valueArray.Add(inValue.x);
        valueArray.Add(inValue.y);
        valueArray.Add(inValue.z);

        if (inDict.ContainsKey(inVector3Name) == true)
        {
            inDict[inVector3Name] = valueArray;
        }
        else
        {
            inDict.Add(inVector3Name, valueArray);
        }
    }

    public static void AddQuaternion(Dictionary<string, object> inDict, string inQuatName, Quaternion inValue)
    {
        List<float> valueArray = new List<float>();
        valueArray.Add(inValue.x);
        valueArray.Add(inValue.y);
        valueArray.Add(inValue.z);
        valueArray.Add(inValue.w);

        if (inDict.ContainsKey(inQuatName) == true)
        {
            inDict[inQuatName] = valueArray;
        }
        else
        {
            inDict.Add(inQuatName, valueArray);
        }
    }

    public static Vector3 ReadVector3(Dictionary<string, object> inDict, string inVector3Name, Vector3 inDefaultValue = default(Vector3))
    {
        bool success;
        return ReadVector3(inDict, inVector3Name, out success, inDefaultValue);
    }

    public static Vector3 ReadVector3(Dictionary<string, object> inDict, string inVector3Name, out bool outSuccess, Vector3 inDefaultValue = default(Vector3))
    {
        Vector3 returnVal = inDefaultValue;

        if (inDict.ContainsKey(inVector3Name) == true)
        {
            outSuccess = false;
            List<float> floatList = JSONTools.ReadFloatList(inDict[inVector3Name]);
            if (floatList != null)
            {
                if (floatList.Count == 3)
                {
                    returnVal.x = floatList[0];
                    returnVal.y = floatList[1];
                    returnVal.z = floatList[2];
                    outSuccess = true;
                }
            }
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static Quaternion ReadQuaternion(Dictionary<string, object> inDict, string inQuatName, Quaternion inDefaultValue = default(Quaternion))
    {
        bool success;
        return ReadQuaternion(inDict, inQuatName, out success, inDefaultValue);
    }

    public static Quaternion ReadQuaternion(Dictionary<string, object> inDict, string inQuatName, out bool outSuccess, Quaternion inDefaultValue = default(Quaternion))
    {
        Quaternion returnVal = inDefaultValue;

        if (inDict.ContainsKey(inQuatName) == true)
        {
            outSuccess = false;
            List<float> floatList = JSONTools.ReadFloatList(inDict[inQuatName]);
            if (floatList != null)
            {
                if (floatList.Count == 4)
                {
                    returnVal.x = floatList[0];
                    returnVal.y = floatList[1];
                    returnVal.z = floatList[2];
                    returnVal.w = floatList[3];
                    outSuccess = true;
                }
            }
        }
        else
        {
            outSuccess = false;
        }

        return returnVal;
    }

    public static TEnum ReadEnum<TEnum>(Dictionary<string, object> inDict, string inVariableName, TEnum inDefaultValue)
        // With Unity 4.3.4 and VS Express 2012 WP8 having the contraint causes an exception 
#if !UNITY_WP8
        where TEnum : struct, System.IComparable, System.IFormattable, System.IConvertible
#endif
    {
        TEnum returnVal = inDefaultValue;

        if (typeof(TEnum).IsEnum == false)
        {
            throw new System.ArgumentException("TEnum must be an enumerated type");
        }

        if (inDict.ContainsKey(inVariableName) == true)
        {
            returnVal = JSONTools.ReadEnum(inDict[inVariableName], inDefaultValue);
        }

        return returnVal;
    }

    public static TEnum ReadEnum<TEnum>(Dictionary<string, object> inDict, string inVariableName, out bool inSuccess, TEnum inDefaultValue)
        // With Unity 4.3.4 and VS Express 2012 WP8 having the contraint causes an exception 
#if !UNITY_WP8
        where TEnum : struct, System.IComparable, System.IFormattable, System.IConvertible
#endif
    {
        TEnum returnVal = inDefaultValue;

        if (typeof(TEnum).IsEnum == false)
        {
            throw new System.ArgumentException("TEnum must be an enumerated type");
        }

        if (inDict.ContainsKey(inVariableName) == true)
        {
            returnVal = JSONTools.ReadEnum(inDict[inVariableName], inDefaultValue);
            inSuccess = true;
        }
        else
        {
            inSuccess = false;
        }

        return returnVal;
    }

    public static bool ReadIntList(Dictionary<string, object> inDict, string inVariableName, ref List<int> refValue)
    {
        if (inDict.ContainsKey(inVariableName) == true)
        {
            if (refValue == null)
            {
                refValue = new List<int>();
            }

            IList tempList = inDict[inVariableName] as IList;
            for (int itemIdx = 0; itemIdx < tempList.Count; itemIdx++)
            {
                int readValue = JSONTools.ReadInt(tempList[itemIdx]);
                refValue.Add(readValue);
            }
            return true;
        }

        return false;
    }

    public static bool ReadDictionaryIntInt(Dictionary<string, object> inDict, string inVariableName, ref Dictionary<int, int> refValue)
    {
        if (inDict.ContainsKey(inVariableName) == true)
        {
            if (refValue == null)
            {
                refValue = new Dictionary<int, int>();
            }

            refValue = JSONTools.ReadDictionaryIntInt(inDict, inVariableName);

            return true;
        }

        return false;
    }

    public static bool ReadDictionary(Dictionary<string, object> inDict, string inVariableName, ref Dictionary<string, object> refValue)
    {
        if (inDict.ContainsKey(inVariableName) == true)
        {
            if (refValue == null)
            {
                refValue = new Dictionary<string, object>();
            }

            refValue = JSONTools.ReadDictionary(inDict, inVariableName);

            return true;
        }

        return false;
    }

    public static System.UInt64 ReadUInt64(Dictionary<string, object> inDict, string inVariableName, System.UInt64 inDefaultValue = DefaultInt)
    {
        System.UInt64 returnVal = inDefaultValue;

        if (inDict.ContainsKey(inVariableName) == true)
        {
            returnVal = JSONTools.ReadUInt64(inDict[inVariableName]);
        }

        return returnVal;
    }

    public static System.Int64 ReadInt64(Dictionary<string, object> inDict, string inVariableName, System.Int64 inDefaultValue = DefaultInt)
    {
        System.Int64 returnVal = inDefaultValue;

        if (inDict.ContainsKey(inVariableName) == true)
        {
            returnVal = JSONTools.ReadInt64(inDict[inVariableName]);
        }

        return returnVal;
    }

    public static Color ReadColor(Dictionary<string, object> inDict, string inVariableName, out bool outSuccess)
    {
        outSuccess = false;

        Color returnVal = Color.black;
        if (inDict.ContainsKey(inVariableName) == true)
        {
            string colorString = inDict[inVariableName] as string;
            if (colorString != null)
            {
                int leftParen = colorString.IndexOf("(");
                int rightParen = colorString.IndexOf(")");

                if (leftParen < rightParen)
                {
                    string colorValuesString = colorString.Substring(leftParen + 1, rightParen - leftParen - 1);
                    if (string.IsNullOrEmpty(colorValuesString) == false)
                    {
                        string[] splitColorValues = colorValuesString.Split(new char[] { ',' }, System.StringSplitOptions.None);

                        if (splitColorValues.Length >= 3)
                        {
                            returnVal.r = float.Parse(splitColorValues[0]);
                            returnVal.g = float.Parse(splitColorValues[1]);
                            returnVal.b = float.Parse(splitColorValues[2]);
                            if (splitColorValues.Length >= 4)
                            {
                                returnVal.a = float.Parse(splitColorValues[3]);
                            }
                            outSuccess = true;
                        }
                    }
                }
            }
        }

        return returnVal;
    }

    /// <summary>
    /// Tries the get key value from the dictionary, otherwise returns defaultValue
    /// </summary>
    /// <returns>The get key value or default.</returns>
    /// <param name="dict">Dictionary to do the lookup in</param>
    /// <param name="keyName">Key name.</param>
    /// <param name="defaultValue">Default value to return if key not found</param>
    /// <typeparam name="valueT">The type of cast the value object to</typeparam>
    public static valueT TryGetKeyValueOrDefault<valueT>(Dictionary<string, object> inDict, string inKeyName, valueT inDefaultValue = default(valueT))
    {
        valueT returnVal = inDefaultValue;

        if (inDict != null)
        {
            object objval = null;

            if (inDict.TryGetValue(inKeyName, out objval) == true)
            {
                try
                {
                    if ((objval is valueT) == true)
                    {
                        returnVal = (valueT)objval;
                    }
                    else
                    {
                        returnVal = (valueT)System.Convert.ChangeType(objval, typeof(valueT));
                    }
                }
                catch (System.InvalidCastException e)
                {
                    Debug.LogError(e.ToString());
                    Debug.LogErrorFormat("Invalid Cast Exception: key:({0}) for object: {1} + expectedType: {2}", inKeyName, (objval != null ? objval.GetType().ToString() : "<null objval>"), typeof(valueT));

                    returnVal = inDefaultValue;
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.ToString());
                    Debug.LogErrorFormat("Invalid Cast Exception: key:({0}) for object: {1} + expectedType: {2}", inKeyName, (objval != null ? objval.GetType().ToString() : "<null objval>"), typeof(valueT));
                }
            }
        }

        return returnVal;
    }

    // System.DateTime.MinValue =  00:00:00.0000000 UTC, January 1, 0001, in the Gregorian calendar
    // We will use the default accepted Epoch time...
    public static readonly System.DateTime EpochDateTime = new System.DateTime(1970, 1, 1);

    public static double DateTimeToSecondsSinceEpoch(System.DateTime inDateTime)
    {
        // Taking universal time into account
        System.DateTime universalDate = inDateTime.ToUniversalTime();
        System.TimeSpan timeSpan = new System.TimeSpan(universalDate.Ticks - EpochDateTime.Ticks);

        return timeSpan.TotalSeconds;
    }

    public static double DateTimeToSecondsSinceEpochLocal(System.DateTime inDateTime)
    {
        System.TimeSpan timeSpan = new System.TimeSpan(inDateTime.Ticks - EpochDateTime.Ticks);

        return timeSpan.TotalSeconds;
    }

    /// <summary>
    /// Converts the Seconds since the epoch to date time Local.
    /// </summary>
    /// <returns>The since epoch to date time local.</returns>
    /// <param name="inSecondsSinceEpoch">In seconds since epoch.</param>
    public static System.DateTime SecondsSinceEpochToDateTimeLocal(double inSecondsSinceEpoch)
    {
        System.DateTime newDateTime = EpochDateTime.AddSeconds(inSecondsSinceEpoch);
        // Offset by the UTC difference... 
        return newDateTime.ToLocalTime();
    }

    /// <summary>
    /// Converts the Seconds since the epoch to date time.
    /// </summary>
    /// <returns>The seconds since epoch to date time.</returns>
    /// <param name="inSecondsSinceEpoch">In seconds since epoch.</param>
    public static System.DateTime SecondsSinceEpochToDateTime(double inSecondsSinceEpoch)
    {
        return EpochDateTime.AddSeconds(inSecondsSinceEpoch);
    }

    /// <summary>
    /// Converts Milliseconds since epoch to a DateTime UTC.
    /// </summary>
    /// <returns>A DateTime UTC representation of the Milliseconds since epoch</returns>
    /// <param name="inMillisecondsSinceEpoch">Milliseconds since epoch.</param>
    public static System.DateTime MillisecondsSinceEpochToDateTimeUtc(long inMillisecondsSinceEpoch)
    {
        System.DateTime newDateTime = EpochDateTime.AddMilliseconds(inMillisecondsSinceEpoch);

        return System.DateTime.SpecifyKind(newDateTime, System.DateTimeKind.Utc);	// Marks the returned DateTime as a Utc time
    }

    /// <summary>
    /// Converts Milliseconds since epoch to a DateTime Local.
    /// </summary>
    /// <returns>A DateTime Local representation of the Milliseconds since epoch</returns>
    /// <param name="inMillisecondsSinceEpoch">Milliseconds since epoch.</param>
    public static System.DateTime MillisecondsSinceEpochToDateTimeLocal(long inMillisecondsSinceEpoch)
    {
        System.DateTime newDateTime = EpochDateTime.AddMilliseconds(inMillisecondsSinceEpoch);
        // Offset by the UTC difference... 
        return newDateTime.ToLocalTime();
    }

    public static bool AddDateTime(string inVariableName, System.DateTime inDate, ref Dictionary<string, object> inJSONDict, bool inIsLocal = false)
    {
        double seconds;

        seconds = (inIsLocal) ? DateTimeToSecondsSinceEpochLocal(inDate) : DateTimeToSecondsSinceEpoch(inDate);

        inJSONDict.Add(inVariableName, seconds);

        return true;
    }

    public static bool ReadDateTime(Dictionary<string, object> inDict, string inVariableName, ref System.DateTime refValue, bool inIsLocal = false)
    {
        if (inDict.ContainsKey(inVariableName) == true)
        {
            double seconds = JSONTools.ReadDouble(inDict[inVariableName]);

            refValue = (inIsLocal) ? SecondsSinceEpochToDateTime(seconds) : SecondsSinceEpochToDateTimeLocal(seconds);

            return true;
        }

        return false;
    }

    public static bool ReadDateTimeBinary(Dictionary<string, object> inDict, string inVariableName, ref System.DateTime refValue)
    {
        if (inDict.ContainsKey(inVariableName) == true)
        {
            string dateTimeString = ReadString(inDict, inVariableName, "");
            long dateTime = System.Convert.ToInt64(dateTimeString);
            refValue = System.DateTime.FromBinary(dateTime);
            return true;
        }

        return false;
    }
}
