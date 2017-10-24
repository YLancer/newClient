using UnityEngine;
using System.Collections;

public class PrefabUtils : MonoBehaviour {
	public static string EffectPath = "Prefabs/Effects/";
	public static string CardPath = "Prefabs/Cards/";
	public static string mjPath = "Prefabs/MJ/";

    /** wrapper for activating/de-activating a game object */
    static public void SetActive(GameObject gameObj, bool on)
    {
        gameObj.SetActive(on);
    }

    /** wrapper for testing if a game object is active */
    static public bool GetActive(GameObject go)
    {
        return go && go.activeInHierarchy;
    }

    static public GameObject AddChild(GameObject parent) { return AddChild(parent, true); }

    /// <summary>
    /// Add a new child game object.
    /// </summary>

    static public GameObject AddChild(GameObject parent, bool undo)
    {
        GameObject go = new GameObject();
#if UNITY_EDITOR
		if (undo) UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    /// <summary>
    /// Instantiate an object and add it to the specified parent.
    /// </summary>
    static public GameObject AddChild(MonoBehaviour parent, MonoBehaviour prefab)
    {
        return AddChild(parent.gameObject, prefab.gameObject);
    }
    static public GameObject AddChild(MonoBehaviour parent, GameObject prefab)
	{
		return AddChild(parent.gameObject, prefab);
	}
    static public GameObject AddChild(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
		UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent.transform);
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
            go.SetActive(true);
        }
        return go;
    }

    public static GameObject AddChild(GameObject parent, string Path)
    {
        GameObject go = GameObject.Instantiate(Resources.Load(Path)) as GameObject;
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent.transform);
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    public static void ClearChild(MonoBehaviour parent)
    {
        ClearChild(parent.transform);
    }

    public static void ClearChild(GameObject parent)
    {
        ClearChild(parent.transform);
    }

    public static void ClearChild(Transform parent)
    {
        while (parent.childCount > 0)
        {
            DestroyImmediate(parent.GetChild(0).gameObject);
        }
    }

    public static string GetPrefabPath(MyPrefabType prefabType)
	{
		switch (prefabType) {
		case MyPrefabType.Effect:
			return EffectPath;
		case MyPrefabType.Card:
			return CardPath;
		case MyPrefabType.MJ:
			return mjPath;

		}
		return "";
	}

    public static GameObject GetPrefab(string name,MyPrefabType prefabType)
    {
        string path = GetPrefabPath(prefabType);
        GameObject go = Resources.Load(path + name) as GameObject;
        return go;
    }
}
