using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(UISceneBase), true)]
public class UISceneBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UISceneBase ui = (UISceneBase)target;
        if (GUILayout.Button("设置引用对象"))
        {
            ui.SetAllMemberValue();
        }

        EditorGUILayout.BeginHorizontal();
        ui.ShowNav = GUILayout.Toggle(ui.ShowNav, "导航", "button", GUILayout.Width(35));
        ui.UseBoxCollider = GUILayout.Toggle(ui.UseBoxCollider, "阻挡", "button", GUILayout.Width(35));
        ui.UseBlackMask = GUILayout.Toggle(ui.UseBlackMask, "蒙版", "button", GUILayout.Width(35));
        ui.HideOldScenes = GUILayout.Toggle(ui.HideOldScenes, "关旧", "button", GUILayout.Width(35));
        ui.BackPopPreScenes = GUILayout.Toggle(ui.BackPopPreScenes, "弹旧", "button", GUILayout.Width(35));
        EditorGUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}