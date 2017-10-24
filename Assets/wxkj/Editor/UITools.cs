using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/// <summary>
/// 自动根据UI生成脚本
/// 1、名字以“!”开头的GameObject会被忽略
/// </summary>
public class UITools : Editor
{
    public const string prefabPath = "Assets/wxkj/Resources/Prefabs/UI/";

    public const string detailPath = "/wxkj/Scripts/UI/UIDetail/";
    public const string tempPath = "/wxkj/Editor/Templete/";
    public const string exName = "Detail";

    private static List<string> attrList = new List<string>();
    private static List<string> methodList = new List<string>();
    private static HashSet<string> uniName = new HashSet<string>();

    private static string[] ignoreList = new string[] { "Outline" };

    static bool IsIgnore(string name)
    {
        foreach (string s in ignoreList)
        {
            if (s == name)
            {
                return true;
            }
        }
        return false;
    }

    [MenuItem("Tools/生成UI #&C", false, 1)]
    static public void CreateUIPage()
    {
        GameObject active = UnityEditor.Selection.activeGameObject;

        if (null == active || (!active.name.EndsWith("Page") && !active.name.EndsWith("Dialog") && !active.name.EndsWith("Sub")))
        {
            EditorUtility.DisplayDialog("提示！", "请在Hierarchy面板选择一个UI文件(Page或者Sub)。", "是");
            return;
        }

        if (active.name.EndsWith("Page"))
        {
            CreateUI("UIPageTemplet");
        }
        else if (active.name.EndsWith("Dialog"))
        {
            CreateUI("UIDialogTemplet");
        }
        else
        {
            CreateUI("UISubTemplet");
        }
    }

    static void CreateUI(string templetName)
    {
        attrList.Clear();
        methodList.Clear();
        uniName.Clear();

        GameObject active = UnityEditor.Selection.activeGameObject;

        string scriptName = active.name + exName;

        Transform root = active.transform;

        string notUniName = GetNotUniName(root);
        if (null != notUniName)
        {
            Debug.LogWarning("有重复GameObject名字。" + notUniName);
        }
        //==============================

        uniName.Clear();
        //GetComponents(root, "");
        foreach (Transform child in root)
        {
            GetComponents(child, "");
        }
        CreateDetail(scriptName);

        //GetMethod(root, root.name);

        string csFile = CreateCS(active.name, templetName);

        Debug.Log("创建成功 : " + csFile);

        UnityEditor.Selection.activeGameObject = active;

        //System.Threading.Thread.Sleep(8000);

        //UIToolTemp.SetAllMemberValue(active);

        //GameObject prefab = PrefabUtility.CreatePrefab(UIPath + active.name + ".prefab", active);
        //保存
        //AssetDatabase.SaveAssets();
        //AssetDatabase.ImportAsset(csFile, ImportAssetOptions.ForceSynchronousImport);
        AssetDatabase.Refresh();
        //AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

        //UnityEditor.Selection.activeGameObject = prefab;
        //Debug.Log("创建预设成功 : " + AssetDatabase.GetAssetPath(prefab));
        //Object cs = AssetDatabase.LoadAssetAtPath<Object>("Assets" + csPath + active.name + ".cs");
        //MonoBehaviour mono = (MonoBehaviour)cs;
        //active.AddComponent(cs.GetType());
    }

    private static string CreateCS(string scriptName, string templeteName)
    {
        string tempMethod = "";
        foreach (string method in methodList)
        {
            tempMethod += method;
        }

        string temp = GetTemplete(templeteName);
        temp = temp.Replace("ScriptName", scriptName);
        temp = temp.Replace("TempletMethod", tempMethod);
        string outPutFile = Application.dataPath + detailPath + scriptName + "Base.cs";
        Save(outPutFile, temp);
        return outPutFile;
    }

    private static void CreateDetail(string scriptName)
    {
        string tempContent = "";
        foreach (string attr in attrList)
        {
            tempContent += attr;
        }

        string detailTemplet = GetTemplete("UIDetailTemplet");
        detailTemplet = detailTemplet.Replace("TempletName", scriptName);
        detailTemplet = detailTemplet.Replace("TempletContent", tempContent);

        string outPutFile = Application.dataPath + detailPath + scriptName + ".cs";
        Save(outPutFile, detailTemplet);

        Debug.Log("创建脚本成功 " + scriptName);
    }

    private static string GetTemplete(string tempName)
    {
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets" + tempPath + tempName + ".txt");
        string temp = textAsset.text;
        return temp;
    }

    private static void GetComponents(Transform root, string parentPath)
    {
        if (root.name.StartsWith("!"))
        {
            return;
        }

        string transName = GetRightName(root.name);
        if (!uniName.Contains(transName))
        {
            uniName.Add(transName);
            //attrList.Add("    public GameObject " + transName + ";\n");
            MonoBehaviour[] monos = root.GetComponents<MonoBehaviour>();
            Dictionary<string, int> map = GetMonoBehavNum(monos);

            foreach (string tpe in map.Keys)
            {
                string name = GetRightName(tpe);
                if (IsIgnore(name))
                {
                    continue;
                }

                int count = map[tpe];
                if (count > 1)
                {
                    attrList.Add("    public " + tpe + "[] " + transName + "_" + name + "s;\n");
                }
                else
                {
                    attrList.Add("    public " + tpe + " " + transName + "_" + name + ";\n");
                }
            }
        }
        
        string thisPath = root.name;
        if (string.IsNullOrEmpty(parentPath) == false)
        {
            thisPath = parentPath + "/" + root.name;
        }

        UISubBase subUI = root.GetComponent<UISubBase>();
        if (null == subUI)
        {
            foreach (Transform child in root)
            {
                GetComponents(child, thisPath);
            }
        }

        GetMethod(root, thisPath);
    }

    private static Dictionary<string, int> GetMonoBehavNum(MonoBehaviour[] monos)
    {
        Dictionary<string, int> map = new Dictionary<string, int>();
        foreach (MonoBehaviour com in monos)
        {
            string tpe = com.GetType().ToString();
            if (tpe.Contains("."))
            {
                tpe = tpe.Substring(tpe.LastIndexOf('.') + 1);
            }
            
            if (map.ContainsKey(tpe))
            {
                int count = map[tpe];
                map[tpe] = count + 1;
            }
            else
            {
                map.Add(tpe, 1);
            }
        }

        return map;
    }

    private static void GetMethod(Transform root, string thisPath)
    {
        if (root.name.StartsWith("!"))
        {
            return;
        }

        string transName = GetRightName(root.name);

        //methodList.Add("        detail." + transName + " = transform.Find(\"" + thisPath + "\").gameObject;\n");

        MonoBehaviour[] monos = root.GetComponents<MonoBehaviour>();
        Dictionary<string, int> map = GetMonoBehavNum(monos);

        foreach (string tpe in map.Keys)
        {
            string name = GetRightName(tpe);
            if (IsIgnore(name))
            {
                continue;
            }
            int count = map[tpe];
            if (count > 1)
            {
                //methodList.Add("        detail." + transName + "_" + name + "s = detail." + transName + ".GetComponents<" + tpe + ">();\n");
                methodList.Add("        detail." + transName + "_" + name + "s = transform.Find(\"" + thisPath + "\").gameObject.GetComponents<" + tpe + ">();\n");
            }
            else
            {
                //methodList.Add("        detail." + transName + "_" + name + " = detail." + transName + ".GetComponent<" + tpe + ">();\n");
                methodList.Add("        detail." + transName + "_" + name + " = transform.Find(\"" + thisPath + "\").gameObject.GetComponent<" + tpe + ">();\n");
            }
        }
    }

    private static string GetNotUniName(Transform root)
    {
        if (root.name.StartsWith("!"))
        {
            return null;
        }

        string transName = GetRightName(root.name);
        if (uniName.Contains(transName))
        {
            return transName;
        }

        uniName.Add(transName);
        foreach (Transform child in root)
        {
            if (child.name.StartsWith("!!")) continue;

            string notUniName = GetNotUniName(child);
            if (notUniName != null)
            {
                return notUniName;
            }
        }
        return null;
    }
    private static string GetRightName(string oldName)
    {
        string name = Regex.Replace(oldName, "[^0-9A-Za-z_@]", "");
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("GameObject 名字不合法！");
            return "";
        }

        // 名字开头不能是特殊字符
        char firstChar = name[0];
        if (Regex.IsMatch(firstChar.ToString(), "[0-9@]"))
        {
            name += "_";
        }

        return name;
    }

    static void Save(string outPutFile, string uiScript)
    {
        StreamWriter sw = new StreamWriter(outPutFile, false);
        sw.Write(uiScript);
        sw.Close();
        AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
    }
}
