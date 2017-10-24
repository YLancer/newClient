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

public class ExcelTool : Editor
{
	private const string createConfigPath = "/IDS/Scripts/Configs/";
	private const string prefix = "Config";
	private const string templet_android = "#if UNITY_ANDROID\nusing  UnityEngine;\nusing System.Collections;\nusing System.Collections.Generic;\npublic partial class TempletName : IBaseDataObject{\nTempletContent\n}\n#endif"; 
	private const string templet_ios = "#if UNITY_IOS||UNITY_IPHONE\nusing  UnityEngine;\nusing System.Collections;\nusing System.Collections.Generic;\npublic partial class TempletName : IBaseDataObject{\nTempletContent\n}\n#endif";
	private const string templet = "using  UnityEngine;\nusing System.Collections;\nusing System.Collections.Generic;\npublic class TempletName {\nTempletContent\n}";
	private const string configPath = "Assets/IDS/Configs/";

    //[MenuItem("IDS/解析Excel(CS)")]
	static public void ParseExcel ()
	{
		Object[] objs = Selection.objects;
		if (null == objs || objs.Length == 0) {
			if (EditorUtility.DisplayDialog ("提示！", "请选择一个或多个你要解析的Excel文件。\n点击“是”将解析Assets\\IDS\\Configs目录所有Excel", "是", "否")) {
                objs = LoadAllAtPath(configPath);
			} else {
				return;
			}
		}

		foreach (Object obj in objs) {
			string path = AssetDatabase.GetAssetPath (obj);
			if (path.EndsWith ("csv")) {
				CSV (obj);
			} else {
				XLSX (obj);
			}
		}

//		         static UnityEngine.Object[] GetObjArr(string path,bool isMan) 
//		{}
//		SaveScriptContent (true);
//		SaveScriptContent (false);
	}

	static Object[] LoadAllAtPath (string AssetPath)
	{
		string[] paths = Directory.GetFiles (AssetPath, "*.*", SearchOption.AllDirectories);
		Object[] objs = new Object[paths.Length];
		for (int i = 0; i < paths.Length; ++i) {
			Object obj = AssetDatabase.LoadAssetAtPath (paths [i], typeof(Object));
			objs [i] = obj;
		}
		return objs;
	}

//	static void SaveScriptContent (bool isCSV)
//	{
//		string[] paths = Directory.GetFiles (configPath, isCSV ? "*.csv" : "*.xlsx", SearchOption.AllDirectories);
//		//Object[] objs = new Object[paths.Length];
//		//for (int i = 0; i < paths.Length; ++i) {
//		//        Object obj = AssetDatabase.LoadAssetAtPath (paths [i],typeof(Object));
//		//        objs [i] = obj;
//		//}
//		if (isCSV)
//		{
//			CSV (paths);
//		}
//		else
//		{
//			XLSX (paths);
//		}
//	}

//		static string GetTemplet ()
//		{
//				TextAsset txt = Resources.LoadAssetAtPath<TextAsset> (createConfigPath + "/ConfigTemplet.cs");
//				Debug.Log (txt.text);
//				return txt.text;
//		}

	static void SaveOneXLSX (DataTable tableData , bool bAndroidOnly = false , bool bIOSOnly = false)
	{
		DataTable table = tableData;

		Regex r = new Regex (@"[^a-zA-Z]");
		string scriptName = prefix + r.Replace (table.TableName, "");

		string curTemplet = bAndroidOnly ? templet_android : bIOSOnly ? templet_ios : templet;
		string configScript = curTemplet.Replace ("TempletName", scriptName);

		string keyName;
		string keyType;
		string dataDefine = GetDataDefine (table, out keyName, out keyType);
        string scriptContent = dataDefine + "\n\n\tpublic static List<" + scriptName + "> datas = new List<" + scriptName + ">(" + (table.Rows.Count - 3) + ");\n"
			+ "\tpublic static Dictionary<" + keyType + "," + scriptName + "> map = new Dictionary<" + keyType + ", " + scriptName + "> ();\n\n"
			+ "\tpublic static " + scriptName + " GetByKey (" + keyType + " key){\n\t\t" + scriptName + " data;\n\t\tif (map.TryGetValue (key, out data)) {\n\t\t\treturn data;\n\t\t} else {\n\t\t\treturn null;\n\t\t}\n\t}\n";

		scriptContent = scriptContent + GetScriptContent (scriptName, table, keyName);

		configScript = configScript.Replace ("TempletContent", scriptContent);

		Save (scriptName, configScript , bAndroidOnly , bIOSOnly);
	}

	static void CSV (Object obj)
	{
		string path = AssetDatabase.GetAssetPath (obj);
		Debug.Log ("开始解析：" + path);
		SaveOneXLSX (CSVControl.ReadData (path, true));
	}
	
	static void XLSX (Object obj)
	{
		string path = AssetDatabase.GetAssetPath (obj);
		if (path.EndsWith ("xlsx") == false && path.EndsWith ("xls") == false)
			return;

		Debug.Log ("开始解析：" + path);

		FileStream stream = File.Open (path, FileMode.Open, FileAccess.Read);
		IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader (stream);

		DataSet result = excelReader.AsDataSet ();
		DataTableCollection tables = result.Tables;

		bool bAndroidOnly = path.Contains ("_Android");
		bool bIOSOnly = path.Contains ("_IOS");

		for (int j = 0; j < tables.Count; j++) {

			SaveOneXLSX (tables [j] , bAndroidOnly , bIOSOnly);
		}
	}

	static void Save (string scriptName, string configScript , bool bAndroidOnly = false , bool bIOSOnly = false)
	{
		if (bAndroidOnly)
		{
			scriptName += "_android";
		}
		else if (bIOSOnly)
		{
			scriptName += "_ios";
		}
		string outPutFile = Application.dataPath + createConfigPath + scriptName + ".cs";
		StreamWriter sw = new StreamWriter (outPutFile, false, Encoding.UTF8);
		//						StreamWriter SW = File.CreateText (outPutFile);
		sw.Write (configScript);
		sw.Close ();
		AssetDatabase.Refresh ();
	}

//	public class Data
//	{
//		public string name;
//	}
//	
//	public static Data[] datas = new Data[1];
//	
//	static ConfigTemplet1 ()
//	{
//		datas [0] = new Data ();
//		datas [0].name = "123";
//	}

	static string GetDataDefine (DataTable table, out string keyName, out string keyType)
	{
		int columns = table.Columns.Count;
		if (0 >= columns) {
			Debug.LogError ("表头不符合规范!");
			keyName = "";
			keyType = "";
			return "";
		}
		string dataDefine = "";

		keyName = table.Rows [1] [0].ToString ();
		keyType = table.Rows [3] [0].ToString ();

		for (int j =0; j < columns; j++) {
			string name = table.Rows [1] [j].ToString ();
            if (string.IsNullOrEmpty(name)) continue;

			string type = table.Rows [3] [j].ToString ();

            if (type.EndsWith("[]"))
            {
                type = "List<" + type.Replace("[]", "") + ">";
            }

            dataDefine += "\tpublic " + type + " " + name + "{get;set;}\n";
		}
		
		return dataDefine;
	}

	// TODO ID列

	static string GetScriptContent (string scriptName, DataTable table, string keyName)
	{
		int columns = table.Columns.Count;
		int rows = table.Rows.Count;

        string name = "";
        string cType = "";

		string content = "\tstatic " + scriptName + " (){\n";
		for (int i = 4; i< rows; i++) {
			int index = i - 4;
			content += "\n\t\tdatas [" + index + "] = new " + scriptName + " ();\n";
			for (int j =0; j < columns; j++) {
				string n = table.Rows [1] [j].ToString ();
				string t = table.Rows [3] [j].ToString ();
                if (!string.IsNullOrEmpty(n)) name = n;
                if (!string.IsNullOrEmpty(t)) cType = t;

				string nvalue = table.Rows [i] [j].ToString ();

                string platformCompile = table.Rows[2][j].ToString();
                if (!string.IsNullOrEmpty(platformCompile))
                {
                    platformCompile = platformCompile.Replace(" ", "");
                }

                string cont = "";
                string type = cType;
				if (type.StartsWith ("float") || type.StartsWith ("int") || type.StartsWith ("long") || type.StartsWith ("bool")) {
					if (type.EndsWith ("[]")) {
						string[] values = nvalue.Split (';');
                        type = type.Substring(0, type.Length - 2);

                        cont += "\t\tdatas [" + index + "]." + name + " = new List<" + type + ">(" + values.Length + ")" + ";\n";
						for (int x = 0; x<values.Length; x++) {
                            cont += "\t\tdatas [" + index + "]." + name + "[" + x + "] = " + GetStrValue(type, values[x]) + (type.StartsWith("float") ? "f" : "") + ";\n";
						}
					} else {
                        cont += "\t\tdatas [" + index + "]." + name + " = " + GetStrValue(type, nvalue) + (type.StartsWith("float") ? "f" : "") + ";\n";
					}
				} else if (type.StartsWith ("string")) {
					if (type.EndsWith ("[]")) {
						string[] values = nvalue.Split (';');
                        type = type.Substring(0, type.Length - 2);

                        cont += "\t\tdatas [" + index + "]." + name + " = new List<" + type + ">(" + values.Length + ")" + ";\n";
						for (int x = 0; x<values.Length; x++) {
                            cont += "\t\tdatas [" + index + "]." + name + "[" + x + "] = \"" + GetStrValue(type, values[x]) + "\";\n";
						}
					} else {
                        cont += "\t\tdatas [" + index + "]." + name + " = \"" + GetStrValue(type, nvalue) + "\";\n";
					}
				} else {
                    if (type.EndsWith("[]"))
                    {
                        string[] values = nvalue.Split(';');
                        type = type.Substring(0, type.Length - 2);

                        cont += "\t\tdatas [" + index + "]." + name + " = new List<" + type + ">(" + values.Length + ")" + ";\n";
                        for (int x = 0; x < values.Length; x++)
                        {
                            cont += "\t\tdatas [" + index + "]." + name + "[" + x + "] = " + type + "." + GetStrValue(type, values[x]) + ";\n";
                        }
                    }
                    else
                    {
                        cont += "\t\tdatas [" + index + "]." + name + " = " + type + "." + GetStrValue(type, nvalue) + ";\n";
                    }
				}

                if (!string.IsNullOrEmpty(platformCompile))
                {
                    content += string.Format("#if {0}\n{1}\n#endif\n", platformCompile, cont);
                }
                else
                {
                    content += cont;
                }
			}
			content += "\n\t\tmap.Add (datas [" + index + "]." + keyName + ",datas [" + index + "]);\n";
		}
		content += "\t}";
		return content;
	}

	static string GetStrValue (string type, string value)
	{
		if (value == "") {
			if (type.StartsWith ("float") || type.StartsWith ("int") || type.StartsWith ("long")) {
				return "0";
			} else if (type.StartsWith ("bool")) {
				return "false";
			}
		}

		return value;
	}
}
