using UnityEngine;
using System.Collections;

public class JsonMessageBase
{
    public string msg;
    public Hashtable data;
    public string server_time;
    public string ret;
    public string error_code;
    public JsonMessageBase(Hashtable ht)
    {
        if (ht.ContainsKey("msg")) msg = ht["msg"].ToString();
        if (ht.ContainsKey("data")) data = (Hashtable)ht["data"];
        if (ht.ContainsKey("server_time")) server_time = ht["server_time"].ToString();
        if (ht.ContainsKey("ret")) ret = ht["ret"].ToString();
        if (ht.ContainsKey("error_code")) error_code = ht["error_code"].ToString();
    }
}
public class JsonNetMessageHelper {
    public static void PrintCsCode(string name, Hashtable ht)
    {
        if (null == ht)
        {
            return;
        }
        
        try
        {
            Debug.Log(GetCsCode(name, ht));
        }
        catch (System.Exception)
        {

        }
    }

    private static string GetCsCode(string name, Hashtable ht)
    {
#if !UNITY_EDITOR
        return "";
#endif

        string cs = "public class " + GetFirstUpperStr(name) + "{\n";
        foreach (string key in ht.Keys)
        {
            object val = ht[key];
            System.Type tp = val.GetType();
            if (tp == typeof(string) || tp == typeof(double))
            {
                cs += "public string " + key + ";\n";
            }
            else if (tp == typeof(ArrayList))
            {
                ArrayList list = (ArrayList)val;
                System.Type stp = list[0].GetType();
                if (stp == typeof(string))
                {
                    cs += "public string[] " + key + ";\n";
                }
                else if (stp == typeof(Hashtable))
                {
                    cs += "public " + GetFirstUpperStr(key) + "[] " + key + ";\n";
                    cs += "\n" + GetCsCode(key, (Hashtable)list[0]);
                }
            }
            else if (tp == typeof(Hashtable))
            {
                cs += "public " + GetFirstUpperStr(key) + " " + key + ";\n";
                cs += "\n" + GetCsCode(key, (Hashtable)val);
            }
        }

        cs += GetConstructor(name,ht);
        cs += "}\n";
        return cs;
    }

    private static string GetConstructor(string name, Hashtable ht)
    {
        string cs = "public " + GetFirstUpperStr(name) + "(Hashtable ht){\n";
        foreach (string key in ht.Keys)
        {
            object val = ht[key];
            System.Type tp = val.GetType();
            if (tp == typeof(string) || tp == typeof(double))
            {
                cs += "if (ht.ContainsKey(\"" + key + "\"))" + key + " = ht[\"" + key + "\"].ToString();\n";
            }
            else if (tp == typeof(ArrayList))
            {
                ArrayList list = (ArrayList)val;
                System.Type stp = list[0].GetType();
                if (stp == typeof(string))
                {
                    cs += "if (ht.ContainsKey(\"" + key + "\")){";
                    cs += "ArrayList " + key + "s = (ArrayList)ht[\"" + key + "\"];\n";
                    cs += key + " = new string[" + key + "s.Count];\n";
                    cs += "for (int i = 0; i < " + key + "s.Count; i++){\n";
                    cs += "\t" + key + "[i] = " + key + "s[i].ToString();\n";
                    cs += "}}\n";
                }
                else if (stp == typeof(Hashtable))
                {
                    cs += "if (ht.ContainsKey(\"" + key + "\")){";
                    cs += "ArrayList " + key + "s = (ArrayList)ht[\"" + key + "\"];\n";
                    cs += key + " = new " + GetFirstUpperStr(key) + "[" + key + "s.Count];\n";
                    cs += "for (int i = 0; i < " + key + "s.Count; i++){\n";
                    cs += key + "[i] = new " + GetFirstUpperStr(key) + "((Hashtable)" + key + "s[i]);\n";
                    cs += "}}\n";
                }
            }
            else if (tp == typeof(Hashtable))
            {
                cs += "if (ht.ContainsKey(\"" + key + "\"))" + key + " = new " + GetFirstUpperStr(key) + "((Hashtable)ht[\"" + key + "\"]);\n";
            }
        }
        cs += "}\n";
        return cs;
    }

    private static string GetFirstUpperStr(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
            return char.ToUpper(str[0]).ToString();
        }
        return null;
    }
}
