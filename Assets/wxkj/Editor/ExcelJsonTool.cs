using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Excel;
using System.Data;
using ImangiUtilities;

public class ExcelJsonTool : Editor
{
    public const string templatePath = "Assets/wxkj/Editor/Templete/ExcelJsonTemplate.txt";
    private const string ConfigSavePath = "/wxkj/Scripts/Configs/";
    private const string JsonSavePath = "/wxkj/Resources/Configs/{0}.txt";
    private const string configPath = "Assets/wxkj/Configs/";
    private const string prefix = "Config";

    [MenuItem("Tools/解析Excel(Json+CS)",false,0)]
    static public void ParseExcel()
    {
        Object[] objs = Selection.objects;
        if (null == objs || objs.Length == 0)
        {
            if (EditorUtility.DisplayDialog("提示！", "请选择一个或多个你要解析的Excel文件。\n点击“是”将解析Assets\\wxkj\\Configs目录所有Excel", "是", "否"))
            {
                objs = LoadAllAtPath(configPath);
            }
            else
            {
                return;
            }
        }

        foreach (Object obj in objs)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (path.EndsWith("csv"))
            {
                CSV(obj);
            }
            else
            {
                XLSX(obj);
            }
        }

        AssetDatabase.Refresh();
    }

    static Object[] LoadAllAtPath(string AssetPath)
    {
        string[] paths = Directory.GetFiles(AssetPath, "*.*", SearchOption.AllDirectories);
        Object[] objs = new Object[paths.Length];
        for (int i = 0; i < paths.Length; ++i)
        {
            Object obj = AssetDatabase.LoadAssetAtPath(paths[i], typeof(Object));
            objs[i] = obj;
        }
        return objs;
    }

    private static string GetTemplete(string templatePath)
    {
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(templatePath);
        string temp = textAsset.text;
        return temp;
    }

    static void SaveOneXLSX(DataTable tableData, bool bAndroidOnly = false, bool bIOSOnly = false)
    {
        DataTable table = tableData;

        Regex r = new Regex(@"[^a-zA-Z]");
        string scriptName = prefix + r.Replace(table.TableName, "");

        string configScript = GetTemplete(templatePath);
        configScript = configScript.Replace("ScriptName", scriptName);

        string keyName;
        string keyType;
        string dataDefine = GetDataDefine(table, out keyName, out keyType);
        configScript = configScript.Replace("AttrDefine", dataDefine);
        configScript = configScript.Replace("KeyName", keyName);
        configScript = configScript.Replace("KeyType", keyType);

        string scriptContent = GetScriptContent(table);
        configScript = configScript.Replace("TempletContent", scriptContent);

        string outPutFile = Application.dataPath + ConfigSavePath + scriptName + ".cs";
        Save(outPutFile, configScript);

        string outPutJsonFile = Application.dataPath + string.Format(JsonSavePath, scriptName);

        string jsonContent = GetJsonContent(table);
        jsonContent = GetMD5Hash(jsonContent + GlobalConfig.SECRET) + ":::" + jsonContent;

        Save(outPutJsonFile, jsonContent);
    }

    static void CSV(Object obj)
    {
        string path = AssetDatabase.GetAssetPath(obj);
        Debug.Log("开始解析：" + path);
        SaveOneXLSX(CSVControl.ReadData(path, true));
        Debug.Log("完成解析：" + path);
    }

    static void XLSX(Object obj)
    {
        string path = AssetDatabase.GetAssetPath(obj);
        if (path.EndsWith("xlsx") == false && path.EndsWith("xls") == false)
            return;

        Debug.Log("开始解析：" + path);

        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        DataTableCollection tables = result.Tables;

        for (int j = 0; j < tables.Count; j++)
        {

            SaveOneXLSX(tables[j]);
        }
        Debug.Log("完成解析：" + path);
    }

    static void Save(string outPutFile, string configScript)
    {
        StreamWriter sw = new StreamWriter(outPutFile, false, Encoding.UTF8);
        sw.Write(configScript);
        sw.Close();
    }

    static string GetDataDefine(DataTable table, out string keyName, out string keyType)
    {
        int columns = table.Columns.Count;
        if (0 >= columns)
        {
            Debug.LogError("表头不符合规范!");
            keyName = "";
            keyType = "";
            return "";
        }
        string dataDefine = "";

        keyName = table.Rows[1][0].ToString();
        keyType = table.Rows[3][0].ToString();

        for (int j = 0; j < columns; j++)
        {
            string name = table.Rows[1][j].ToString();
            if (string.IsNullOrEmpty(name)) continue;

            string type = table.Rows[3][j].ToString();

            if (type.EndsWith("[]"))
            {
                type = "List<"+type.Replace("[]","")+">";
            }

            dataDefine += "\tpublic " + type + " " + name + "{get;set;}\n";
        }

        return dataDefine;
    }

    static string GetScriptContent(DataTable table)
    {
        string content = "";
        string name = "";
        string cType = "";

        int columns = table.Columns.Count;
        for (int j = 0; j < columns; j++)
        {
            string n = table.Rows[1][j].ToString();
            string t = table.Rows[3][j].ToString();
            if (!string.IsNullOrEmpty(n)) name = n;
            if (!string.IsNullOrEmpty(t)) cType = t;

            string key = name;
            string platformCompile = table.Rows[2][j].ToString();
            if (!string.IsNullOrEmpty(platformCompile))
            {
                platformCompile = platformCompile.Replace(" ", "");
                key += platformCompile;
            }
            
            string cont = "";
            string type = cType;
            if (type.StartsWith("string"))
            {
                type = type.Replace("[]", "List");
                type = type.Replace("string", "Str");

                cont = string.Format("\t\tif (inDict.ContainsKey(\"{3}\"))  data.{0} = inDict[\"{1}\"].GetJsonConverter().to{2}();\n", name, key, type, name);
            }
            else if (type.StartsWith("float") || type.StartsWith("int") || type.StartsWith("long") || type.StartsWith("bool"))
            {
                type = type.Replace("[]", "List");
                type = type[0].ToString().ToUpper() + type.Substring(1, type.Length - 1);

                cont = string.Format("\t\tif (inDict.ContainsKey(\"{3}\"))  data.{0} = inDict[\"{1}\"].GetJsonConverter().to{2}();\n", name, key, type, name);
            }
            else
            {
                string temp = "\t\tif (inDict.ContainsKey(\"{3}\"))  data.{0} = inDict[\"{1}\"].GetJsonConverter().toEnum<{2}>());\n";
                if (type.EndsWith("[]"))
                {
                    temp = "\t\tif (inDict.ContainsKey(\"{3}\"))  data.{0} = ({2})inDict[\"{1}\"].GetJsonConverter().toIntList();\n";
                    Debug.LogError("枚举数组暂时不支持");
                    type = type.Replace("[]", "");
                }

                cont = string.Format(temp, name, key, type, name);
            }

            if (!string.IsNullOrEmpty(platformCompile))
            {
                content += string.Format("#if {0}\n{1}\n#endif\n", platformCompile,cont);
            }
            else
            {
                content += cont;
            }
        }
        return content;
    }

    static string GetJsonContent(DataTable table)
    {
        int columns = table.Columns.Count;
        int rows = table.Rows.Count;

        List<object> list = new List<object>();
        for (int i = 4; i < rows; i++)
        {
            string name = "";
            string type = "";
            Dictionary<string, object> data = new Dictionary<string, object>();
            for (int j = 0; j < columns; j++)
            {
                string platformCompile = table.Rows[2][j].ToString();
                string nvalue = table.Rows[i][j].ToString();
                if(string.IsNullOrEmpty(nvalue))continue;

                string n = table.Rows[1][j].ToString();
                string t = table.Rows[3][j].ToString();
                if (!string.IsNullOrEmpty(n)) name = n;
                if (!string.IsNullOrEmpty(t)) type = t;

                if (!string.IsNullOrEmpty(platformCompile))
                {
                    platformCompile = platformCompile.Replace(" ", "");
                    name += platformCompile;
                }

                if (type.EndsWith("[]"))
                {
                    string[] values = nvalue.Split(';');
                    
                    if (type.StartsWith("string"))
                    {
                        data.Add(name, values);
                    }
                    else if (type.StartsWith("float"))
                    {
                        List<float> fList = new List<float>();
                        foreach (string str in values)
                        {
                            string val = GetStrValue(type, str);
                            fList.Add(float.Parse(val));
                        }
                        data.Add(name, fList);
                    }
                    else if (type.StartsWith("int"))
                    {
                        List<int> iList = new List<int>();
                        foreach (string str in values)
                        {
                            string val = GetStrValue(type, str);
                            iList.Add(int.Parse(val));
                        }
                        data.Add(name, iList);
                    }
                    else if (type.StartsWith("long"))
                    {
                        List<long> lList = new List<long>();
                        foreach (string str in values)
                        {
                            string val = GetStrValue(type, str);
                            lList.Add(long.Parse(val));
                        }
                        data.Add(name, lList);
                    }
                    else if (type.StartsWith("bool"))
                    {
                        List<bool> bList = new List<bool>();
                        foreach (string str in values)
                        {
                            string val = GetStrValue(type, str);
                            bList.Add(bool.Parse(val));
                        }
                        data.Add(name, bList);
                    }
                    else
                    {
                        data.Add(name, values);
                    }
                }
                else
                {
                    string val = GetStrValue(type, nvalue);
                    if (type.StartsWith("string"))
                    {
                        data.Add(name, val);
                    }
                    else if (type.StartsWith("float"))
                    {
                        float v = 0;
                        if (string.IsNullOrEmpty(val) == false)
                        {
                            v = float.Parse(val);
                        }
                        data.Add(name, v);
                    }
                    else if (type.StartsWith("int"))
                    {
                        int v = 0;
                        if (string.IsNullOrEmpty(val) == false)
                        {
                            v = int.Parse(val);
                        }
                        data.Add(name, v);
                    }
                    else if (type.StartsWith("long"))
                    {
                        long v = 0;
                        if (string.IsNullOrEmpty(val) == false)
                        {
                            v = long.Parse(val);
                        }
                        data.Add(name, v);
                    }
                    else if (type.StartsWith("bool"))
                    {
                        bool v = false;
                        if (string.IsNullOrEmpty(val) == false)
                        {
                            v = bool.Parse(val);
                        }
                        data.Add(name, v);
                    }
                    else
                    {
                        data.Add(name, val);
                    }
                }
            }
            list.Add(data);
        }

        return list.toJson();
    }

    private static string GetMD5Hash(string unhashed)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] us = System.Text.Encoding.UTF8.GetBytes(unhashed);// Was System.Text.Encoding.Default
        byte[] hash = md5.ComputeHash(us);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }
        return sb.ToString();
    }

    static string GetStrValue(string type, string value)
    {
        if (value == "")
        {
            if (type.StartsWith("float") || type.StartsWith("int") || type.StartsWith("long"))
            {
                return "0";
            }
            else if (type.StartsWith("bool"))
            {
                return "false";
            }
        }

        return value;
    }
}
