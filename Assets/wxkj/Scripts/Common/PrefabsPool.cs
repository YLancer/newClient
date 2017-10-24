using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PrefabsPool : MonoBehaviour
{
    private Dictionary<string,Stack<GameObject>> m_objectStack;
    private Dictionary<GameObject, string> gameObjectToKey;
    [SerializeField]
    private MyPrefabType prefabType;
    [SerializeField]
    private int preloadAmount;
    private Action<GameObject> m_resetAction;
    private bool inited = false;
	public List<PrefabsPoolItem> list = new List<PrefabsPoolItem> ();

    public void InitPool(Action<GameObject> ResetAction = null)
    {
        if (inited) return;
        inited = true;

        this.m_objectStack = new Dictionary<string, Stack<GameObject>>();
        this.gameObjectToKey = new Dictionary<GameObject, string>();
        this.m_resetAction = ResetAction;
        string path = PrefabUtils.GetPrefabPath(prefabType);

        if (string.IsNullOrEmpty(path))
        {
            print("pool path is null. "+prefabType);
            return;
        }

        GameObject[] gos = Resources.LoadAll<GameObject>(path);
        foreach (GameObject go in gos)
        {
            Stack<GameObject> stack = new Stack<GameObject>();
			int count = GetPreloadAmount (go.name);
			for(int i=0;i<count;i++){
                GameObject child = PrefabUtils.AddChild(this.gameObject,go);
                child.SetActive(false);
				child.name = go.name;
                child.layer = go.layer;
                stack.Push(child);
                gameObjectToKey.Add(child,go.name);
            }
            m_objectStack.Add(go.name, stack);
        }
    }

    public GameObject Spawn(string name)
    {
        if (m_objectStack.ContainsKey(name))
        {
            Stack<GameObject> stack = m_objectStack[name];
            if (stack.Count > 0)
            {
                GameObject t = stack.Pop();

                if (m_resetAction != null)
                    m_resetAction(t);

                t.SetActive(true);
                return t;
            }
            else
            {
				print ("pool is not enough! " + this.name + " " + name);
                GameObject go = PrefabUtils.GetPrefab(name,prefabType);
                GameObject child = PrefabUtils.AddChild(this.gameObject,go);
                child.layer = go.layer;
				child.name = name;
                gameObjectToKey.Add(child, name);
                child.SetActive(true);
                return child;
            }
        }
        else
        {
            Debug.LogWarningFormat("pool no name:[{0}]", name);
            return null;
        }
    }

	public void Despawn(Transform obj)
	{
		Despawn (obj.gameObject);
	}

    public void Despawn(GameObject obj)
    {
        if (gameObjectToKey.ContainsKey(obj))
        {
            string name = gameObjectToKey[obj];
            obj.SetActive(false);
            if (m_objectStack.ContainsKey(name))
            {
                Stack<GameObject> stack = m_objectStack[name];
                obj.transform.SetParent(this.transform);
                stack.Push(obj);
            }
        }
    }

    public void Despawn(GameObject obj,float time)
    {
        StartCoroutine(delay(obj, time));
    }

    IEnumerator delay(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Despawn(obj);
    }

	private int GetPreloadAmount(string name){
		foreach (PrefabsPoolItem item in list) {
			if (item.name == name) {
				return item.preloadAmount;
			}
		}
		return preloadAmount;
	}
}

[System.Serializable]
public class PrefabsPoolItem{
	public string name;
	public int preloadAmount;
}