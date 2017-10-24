using System;
using UnityEngine;
using UnityEditor;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Playflock.Log
{
    [System.Serializable]
    public class HUDData : UnityEngine.Component
    {
        public string widgetName = "";
        public string currentPath = "";
        public bool isCreating = false;
    }

    public enum NamespaceType
    {
        System = 0,
        UnityEngine = 1,
        UnityEngineUI = 2
    }

    public enum UeTypeCode
    {
        Color = 0
    }

    public enum UeUiTypeCode
    {
        Button = 0,
        Image = 1,
        InputField = 2,
        Text = 3,
        Toggle = 4
    }


    public class Member
    {
        public bool IsSerialized;
        public MemberAttributes Attribute;
        public string Name;
        public NamespaceType NamespaceType;
        public System.TypeCode SystemTypeCode;
        public UeTypeCode UeTypeCode;
        public UeUiTypeCode UeUiTypeCode;
    }

    public class HUDDebugFactory : EditorWindow
    {

        #region PRIVATE_VARIABLES

        private const string createMenuTitle = "Assets/Playflock/Create/HUDDebug";
        private const string _nodeFolder = "/Nodes";
        private const string _widgetFolder = "/Widgets";
        private static EditorWindow window;

        /// <summary>
        /// Editor compiling status
        /// </summary>
        private bool isEditorBusy = false;

        private static string _rootPath;
        private string _nodeName;
        private Playflock.Log.NodeType _nodeType;
        private string _widgetName;

        private static Dictionary<Enum, string> _namespaceKV = new Dictionary<Enum, string>()
        {
            {NamespaceType.System, "System"},
            {NamespaceType.UnityEngine, "UnityEngine"},
            {NamespaceType.UnityEngineUI, "UnityEngine.UI"}

        };

        private static List<Member> _widgetMembers = new List<Member>();
        private Vector2 _membersScrollViewPos;

        private GameObject _templateWidget;

        #endregion

        public static void ShowCreateWindow()
        {
            window = (HUDDebugFactory) EditorWindow.GetWindow(typeof (HUDDebugFactory));
            window.title = "Create HUD";
        }

        private void InitHUDData()
        {
            EditorPrefs.SetString("nodeName", "");
            EditorPrefs.SetString("widgetName", "");
            EditorPrefs.SetBool("isCreatingNode", false);
            EditorPrefs.SetBool("isCreatingWidget", false);
            _rootPath = EditorPrefs.GetString("rootPath");
        }

        #region UNITY_EVENTS

        void Awake()
        {
            InitHUDData();
        }

        void Update()
        {
            if (EditorPrefs.GetBool("isCreatingNode"))
            {
                if (!EditorApplication.isCompiling)
                {
                    isEditorBusy = false;
                    AssetDatabase.Refresh();
                    EditorPrefs.SetBool("isCreatingNode", false);
                    var go = new GameObject();
                    EditorPrefs.SetBool("isCreatingNode", false);
                    go.AddComponent<RectTransform>();
#if UNITY_5
                    UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(go, "Assets/PlayFlock_Utils/HUDDebug/Editor/HUDDebugFactory.cs (126,21)", EditorPrefs.GetString("nodeName") + "Node");
#else
                    string componentName = EditorPrefs.GetString( "nodeName" ) + "Node";
                    go.AddComponent( componentName );
#endif
                    var path = EditorPrefs.GetString("rootPath") + _nodeFolder + "/" +
                               EditorPrefs.GetString("nodeName") + "Node.prefab";
                    var relativePath = path.Substring(path.IndexOf("Assets/", StringComparison.Ordinal));
                    Debug.Log("Node prefab path : " + relativePath);
                    PrefabUtility.CreatePrefab(relativePath, go);
                    DestroyImmediate(go, false);
                    AssetDatabase.Refresh();
                    Debug.Log("Create HUD node completed");
                }
                else
                    isEditorBusy = true;
            }

            if (EditorPrefs.GetBool("isCreatingWidget"))
            {
                if (!EditorApplication.isCompiling)
                {
                    isEditorBusy = false;
                    AssetDatabase.Refresh();
                    EditorPrefs.SetBool("isCreatingWidget", false);
                    GameObject go = null;
                    EditorPrefs.SetBool("isCreatingWidget", false);
                    if (_templateWidget)
                        go = (GameObject) Instantiate(_templateWidget);
                    else
                    {
                        go = new GameObject();
                        go.AddComponent<RectTransform>();
                    }
#if UNITY_5
                    UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(go, "Assets/PlayFlock_Utils/HUDDebug/Editor/HUDDebugFactory.cs (161,17)", EditorPrefs.GetString("widgetName") + "Widget");
#else
                    string componentName = EditorPrefs.GetString("widgetName") + "Widget";
                    go.AddComponent(componentName);
#endif
                    var path = EditorPrefs.GetString("rootPath") + _widgetFolder + "/" +
                               EditorPrefs.GetString("widgetName") +
                               "Widget.prefab";
                    var relativePath = path.Substring(path.IndexOf("Assets/", StringComparison.Ordinal));
                    Debug.Log("Widget prefab path : " + relativePath);
                    PrefabUtility.CreatePrefab(relativePath, go);
                    DestroyImmediate(go, false);
                    AssetDatabase.Refresh();
                    Debug.Log("Create HUD widget completed");
                }
                else
                    isEditorBusy = true;
            }

        }

        void OnGUI()
        {
            if (!isEditorBusy)
            {
                var isRootPathEmpty = String.IsNullOrEmpty( _rootPath );
                if (isRootPathEmpty)
                {
                    _rootPath = EditorPrefs.GetString("rootPath");
                    EditorGUILayout.HelpBox("Please select root directory of HUDDebug", MessageType.Error);
                }
                GUILayout.BeginHorizontal();
                GUILayout.Label("Root path : " + _rootPath);
                if (GUILayout.Button("Select folder"))
                {
                    var path = EditorUtility.OpenFolderPanel("Select HUDDebug folder", "Assets", "");
                    EditorPrefs.SetString("rootPath", path);
                    _rootPath = path;
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(50f);
                GUILayout.Label("Node name:", GUILayout.MaxWidth(100f));
                _nodeName = EditorPrefs.GetString("nodeName");
                _nodeName = GUILayout.TextField(_nodeName, GUILayout.MaxWidth(400f));
                EditorPrefs.SetString("nodeName", _nodeName);
                GUILayout.Space(10f);
                GUILayout.Label("Implementation type:", GUILayout.MaxWidth(150f));
                _nodeType =
                    (Playflock.Log.NodeType) EditorGUILayout.EnumPopup("", _nodeType, GUILayout.MaxWidth(200f));
                GUILayout.Space(10f);
                GUILayout.Label("Widget name:", GUILayout.MaxWidth(100f));
                _widgetName = EditorPrefs.GetString("widgetName");
                _widgetName = GUILayout.TextField(_widgetName, GUILayout.MaxWidth(400f));
                EditorPrefs.SetString("widgetName", _widgetName);
                _templateWidget =
                    (GameObject)
                        EditorGUILayout.ObjectField("From template:", _templateWidget, typeof (GameObject), true);
                GUILayout.Space(10f);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Members:", GUILayout.MaxWidth(100f));
                if (GUILayout.Button("+", GUILayout.MaxWidth(30f)))
                {
                    //add members
                    Member member = new Member();
                    member.IsSerialized = false;
                    member.Attribute = MemberAttributes.Public;
                    member.NamespaceType = NamespaceType.System;
                    member.Name = "";
                    member.SystemTypeCode = TypeCode.Boolean;
                    member.UeTypeCode = UeTypeCode.Color;
                    member.UeUiTypeCode = UeUiTypeCode.Text;
                    _widgetMembers.Add(member);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(10f);
                _membersScrollViewPos = GUILayout.BeginScrollView(_membersScrollViewPos);
                for (int i = 0; i < _widgetMembers.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    _widgetMembers[i].Attribute =
                        (MemberAttributes)
                            EditorGUILayout.EnumPopup("", _widgetMembers[i].Attribute, GUILayout.MaxWidth(200f));
                    _widgetMembers[i].NamespaceType =
                        (NamespaceType)
                            EditorGUILayout.EnumPopup("", _widgetMembers[i].NamespaceType, GUILayout.MaxWidth(200f));
                    switch (_widgetMembers[i].NamespaceType)
                    {
                        case NamespaceType.System:
                            _widgetMembers[i].SystemTypeCode =
                                (System.TypeCode)
                                    EditorGUILayout.EnumPopup("", _widgetMembers[i].SystemTypeCode,
                                        GUILayout.MaxWidth(200f));
                            break;
                        case NamespaceType.UnityEngine:
                            _widgetMembers[i].UeTypeCode =
                                (UeTypeCode)
                                    EditorGUILayout.EnumPopup("", _widgetMembers[i].UeTypeCode, GUILayout.MaxWidth(200f));
                            break;
                        case NamespaceType.UnityEngineUI:
                            _widgetMembers[i].UeUiTypeCode =
                                (UeUiTypeCode)
                                    EditorGUILayout.EnumPopup("", _widgetMembers[i].UeUiTypeCode,
                                        GUILayout.MaxWidth(200f));
                            break;
                    }
                    GUILayout.Label("Name : ", GUILayout.MaxWidth(40f));
                    _widgetMembers[i].Name = GUILayout.TextArea(_widgetMembers[i].Name);
                    _widgetMembers[i].IsSerialized = EditorGUILayout.Toggle(_widgetMembers[i].IsSerialized,
                        GUILayout.MaxWidth(10f));
                    GUILayout.Label("IsSerialized", GUILayout.MaxWidth(80f));
                    if (GUILayout.Button("-", GUILayout.MaxWidth(30f)))
                    {
                        _widgetMembers.RemoveAt(i);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10f);
                }
                GUILayout.EndScrollView();
            }
            if (!isEditorBusy && IsCreateAvailable())
            {
                if (GUILayout.Button("Create HUD"))
                {
                    if (File.Exists(_rootPath + _nodeFolder + "/" + EditorPrefs.GetString("nodeName") + "Node.cs"))
                    {
                        if (
                            !EditorUtility.DisplayDialog("HUD node",
                                "File with same name already exists, replace it?", "Replace",
                                "Cancel"))
                            return;
                    }

                    if (File.Exists(_rootPath + _widgetFolder + "/" + EditorPrefs.GetString("widgetName") + "Widget.cs"))
                    {
                        if (
                            !EditorUtility.DisplayDialog("HUD widget",
                                "File with same name already exists, replace it?", "Replace",
                                "Cancel"))
                            return;
                    }

                    Debug.Log("Creating HUD...");

                    if (!string.IsNullOrEmpty(EditorPrefs.GetString("nodeName")))
                    {
                        CreateNodeScript(EditorPrefs.GetString("nodeName"), _nodeType);
                        var nodeScriptPath = _rootPath + _nodeFolder + "/" + EditorPrefs.GetString("nodeName") +
                                             "Node.cs";
                        Debug.Log(nodeScriptPath);
                        AssetDatabase.Refresh();
                        EditorPrefs.SetBool("isCreatingNode", true);
                    }

                    if (!string.IsNullOrEmpty(EditorPrefs.GetString("widgetName")))
                    {
                        CreateWidgetScript(EditorPrefs.GetString("widgetName"));
                        var widgetScriptPath = _rootPath + _widgetFolder + "/" + EditorPrefs.GetString("widgetName") +
                                               "Widget.cs";
                        Debug.Log(widgetScriptPath);
                        AssetDatabase.Refresh();
                        EditorPrefs.SetBool("isCreatingWidget", true);
                    }
                }
            }
            else if (!IsCreateAvailable())
            {
                EditorGUILayout.HelpBox("Please fill at least one of the fields (Node or Widget)", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox("Please wait...", MessageType.Info);
            }
            Repaint();
        }

        void OnDestroy()
        {
            _widgetMembers.Clear();
        }

        #endregion

        private bool IsCreateAvailable()
        {
            return !String.IsNullOrEmpty(_nodeName) || !String.IsNullOrEmpty(_widgetName);
        }

        /// <summary>
        /// Create widget prefab and c# script with same name
        /// </summary>
        [MenuItem(createMenuTitle, false, 0)]
        static void Create()
        {
            ShowCreateWindow();
        }

        #region CODE_GENERATOR

        private enum accessModifier
        {
            Public,
            Private
        }

        private static void CreateNodeScript(string name, Playflock.Log.NodeType nodeType)
        {
            var unit = new CodeCompileUnit();
            //add namespace
            var importsNamespace = new CodeNamespace
            {
                Imports =
                {
                    new CodeNamespaceImport("UnityEngine"),
                    new CodeNamespaceImport("UnityEngine.UI"),
                    new CodeNamespaceImport("System.Collections")
                }
            };
            var playflockNamespace = new CodeNamespace("Playflock.Log.Node");
            unit.Namespaces.Add(importsNamespace);
            unit.Namespaces.Add(playflockNamespace);
            // add class 
            var declarationClass = new CodeTypeDeclaration(name + "Node");
            declarationClass.IsClass = true;
            //inherits from
            declarationClass.BaseTypes.Add("HUDNode");
            //Methods
            var baseMethod = new CodeMemberMethod();
            switch (nodeType)
            {
                case Playflock.Log.NodeType.Toggle:
                    baseMethod.Name = "OnToggle";
                    baseMethod.Parameters.Add(new CodeParameterDeclarationExpression("System.Boolean", "isOn"));
                    baseMethod.Attributes = MemberAttributes.Override | MemberAttributes.Public;
                    baseMethod.Statements.Add(new CodeSnippetStatement("\t\t\tbase.OnToggle( isOn );"));
                    break;
                case Playflock.Log.NodeType.Button:
                    baseMethod.Name = "OnClick";
                    baseMethod.Attributes = MemberAttributes.Override | MemberAttributes.Public;
                    baseMethod.Statements.Add(new CodeSnippetStatement("\t\t\tbase.OnClick();"));
                    break;
            }
            //Methods
            declarationClass.Members.Add(baseMethod);
            playflockNamespace.Types.Add(declarationClass);

            var provider = (CSharpCodeProvider) CodeDomProvider.CreateProvider("csharp");
            var scriptPath = _rootPath + _nodeFolder + "/" + name + "Node.cs";
            using (var sw = new StreamWriter(scriptPath, false))
            {
                var tw = new IndentedTextWriter(sw, "    ");
                //generate source code 
                var options = new CodeGeneratorOptions();
                options.BracingStyle = "C";
                options.BlankLinesBetweenMembers = true;
                provider.GenerateCodeFromCompileUnit(unit, tw, options);
                tw.Close();
                sw.Close();
            }

            //delete autogenerated summary (optional)
            using (var Reader = new StreamReader(scriptPath))
            {
                var fileContent = Reader.ReadToEnd();

                var sb = new StringBuilder(fileContent);
                sb.Remove(0, 420);
                Reader.Close();
                using (var streamWriter = new StreamWriter(scriptPath))
                {
                    streamWriter.Write(sb);
                    streamWriter.Close();
                }
            }
            //Debug.Log("HUD node script generation completed");
        }

        private static void CreateWidgetScript(string name)
        {
            var unit = new CodeCompileUnit();
            //add namespace
            var importsNamespace = new CodeNamespace
            {
                Imports =
                {
                    new CodeNamespaceImport("UnityEngine"),
                    new CodeNamespaceImport("UnityEngine.UI"),
                    new CodeNamespaceImport("System.Collections")
                }
            };
            var playflockNamespace = new CodeNamespace("Playflock.Log.Widget");
            unit.Namespaces.Add(importsNamespace);
            unit.Namespaces.Add(playflockNamespace);
            // add class 
            var declarationClass = new CodeTypeDeclaration(name + "Widget");
            declarationClass.IsClass = true;
            //inherits from
            declarationClass.BaseTypes.Add("HUDWidget");

            //add member (ex: private bool = false)
            //UnityEngine.UI
            List<CodeMemberField> memberFields = new List<CodeMemberField>();
            foreach (var wm in _widgetMembers)
            {
                CodeMemberField activeField = null;
                switch (wm.NamespaceType)
                {
                    case NamespaceType.System:
                        activeField = new CodeMemberField(_namespaceKV[wm.NamespaceType] + "." + wm.SystemTypeCode,
                            wm.Name);
                        break;
                    case NamespaceType.UnityEngine:
                        activeField = new CodeMemberField(_namespaceKV[wm.NamespaceType] + "." + wm.UeTypeCode, wm.Name);
                        break;
                    case NamespaceType.UnityEngineUI:
                        activeField = new CodeMemberField(_namespaceKV[wm.NamespaceType] + "." + wm.UeUiTypeCode,
                            wm.Name);
                        break;
                }
                activeField.Attributes = wm.Attribute;
                if (wm.IsSerialized)
                {
                    var customAttribute =
                        new CodeAttributeDeclaration(new CodeTypeReference(typeof (SerializeField)));
                    CodeAttributeDeclarationCollection declarationsCollection = new CodeAttributeDeclarationCollection();
                    declarationsCollection.Add(customAttribute);
                    activeField.CustomAttributes = new CodeAttributeDeclarationCollection(declarationsCollection);
                }
                memberFields.Add(activeField);
            }

            //Methods
            var initializeMethod = new CodeMemberMethod();
            initializeMethod.Name = "Initialize";
            initializeMethod.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            var drawMethod = new CodeMemberMethod();
            drawMethod.Name = "Draw";
            drawMethod.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            foreach (var mf in memberFields)
                declarationClass.Members.Add(mf);
            declarationClass.Members.Add(initializeMethod);
            declarationClass.Members.Add(drawMethod);

            //var attribute = new CodeAttributeDeclaration(
            //new CodeTypeReference(typeof(SerializableAttribute)));
            //unit.AssemblyCustomAttributes.Add(attribute);

            playflockNamespace.Types.Add(declarationClass);

            var provider = (CSharpCodeProvider) CodeDomProvider.CreateProvider("csharp");
            var scriptPath = _rootPath + _widgetFolder + "/" + name + "Widget.cs";
            using (var sw = new StreamWriter(scriptPath, false))
            {
                var tw = new IndentedTextWriter(sw, "    ");
                //generate source code 
                var options = new CodeGeneratorOptions();
                options.BracingStyle = "C";
                options.BlankLinesBetweenMembers = true;
                provider.GenerateCodeFromCompileUnit(unit, tw, options);
                tw.Close();
                sw.Close();
            }
            //delete autogenerated summary (optional)
            using (var Reader = new StreamReader(scriptPath))
            {
                var fileContent = Reader.ReadToEnd();

                var sb = new StringBuilder(fileContent);
                sb.Remove(0, 420);
                Reader.Close();
                using (var streamWriter = new StreamWriter(scriptPath))
                {
                    streamWriter.Write(sb);
                    streamWriter.Close();
                }
            }
            //Debug.Log("HUD widget script generation completed");
        }

        #endregion
    }
}
