using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(UISubBase), true)]
public class UISubBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UISubBase ui = (UISubBase)target;
        if (GUILayout.Button("设置引用对象"))
        {
            ui.SetAllMemberValue();
        }
        base.OnInspectorGUI();
    }
}