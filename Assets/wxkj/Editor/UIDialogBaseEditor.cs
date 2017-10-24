using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIDialogBase), true)]
public class UIDialogBaseEditor : Editor
{
    int itemMask;
    public override void OnInspectorGUI()
    {
        UIDialogBase ui = (UIDialogBase)target;
        if (GUILayout.Button("设置引用对象"))
        {
            ui.SetAllMemberValue();
        }

        EditorGUILayout.BeginHorizontal();
        ui.UseBoxCollider = GUILayout.Toggle(ui.UseBoxCollider, "阻挡", "button", GUILayout.Width(35));
        ui.UseBlackMask = GUILayout.Toggle(ui.UseBlackMask, "蒙版", "button", GUILayout.Width(35));
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }

    void ShowBtn(int nav, string name)
    {
        itemMask = (GUILayout.Toggle(Has(nav), name, "button", GUILayout.Width(35))) ? (itemMask | nav) : (itemMask & (~nav));
    }

    bool Has(int nav)
    {
        return ((itemMask & nav) > 0);
    }
}