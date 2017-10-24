using System.Collections.Generic;

public static class JsonExtensions {
    public static JsonConverter GetJsonConverter(this object obj)
    {
        JsonConverter.Instance.obj = obj;
        return JsonConverter.Instance;
    }
}

public class JsonConverter
{
    private static JsonConverter instance;
    public static JsonConverter Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new JsonConverter();
            }
            return instance;
        }
    }

    public object obj;

    public string toStr()
    {
        return JSONTools.ReadString(obj);
    }

    public List<string> toStrList()
    {
        return JSONTools.ReadStringList(obj);
    }

    public int toInt()
    {
        if (null != obj && obj.GetType() == typeof(string))
        {
            return int.Parse(obj.ToString());
        }

        return JSONTools.ReadInt(obj);
    }

    public List<int> toIntList()
    {
        return JSONTools.ReadIntList(obj);
    }

    public float toFloat()
    {
        if (null != obj && obj.GetType() == typeof(string))
        {
            return float.Parse(obj.ToString());
        }
        return (float)JSONTools.ReadDouble(obj);
    }

    public List<float> toFloatList()
    {
        return JSONTools.ReadFloatList(obj);
    }

    public bool toBool()
    {
        if (null != obj && obj.GetType() == typeof(string))
        {
            return bool.Parse(obj.ToString());
        }
        return JSONTools.ReadBool(obj);
    }

    public TEnum toEnum<TEnum>() where TEnum : struct, System.IComparable, System.IFormattable, System.IConvertible
    {
        return JSONTools.ReadEnumUnsafe<TEnum>(obj);
    }

    public List<TEnum> toEnumList<TEnum>() where TEnum : struct, System.IComparable, System.IFormattable, System.IConvertible
    {
        return JSONTools.ReadEnumList<TEnum>(obj);
    }
}
