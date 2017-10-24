using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FlyTipsMgr : MonoBehaviour {
    public GameObject root;
    public GameObject tipsPrefab;

    private List<GameObject> caches = new List<GameObject>();

    public void Show(string tipsInfo)
    {
        GameObject tips = FindDisactive();
        if (null == tips)
        {
            tips = UIUtils.AddChild(root, tipsPrefab);
            caches.Add(tips);
        }

        if (null != tips)
        {
            UIUtils.SetActive(tips, true);
            StartCoroutine(delayHide(tips));
            RectTransform rect = tips.GetComponent<RectTransform>();
            rect.SetAsLastSibling();
            Text txt = tips.GetComponentInChildren<Text>();
            txt.text = tipsInfo;
        }
    }

    IEnumerator delayHide(GameObject go)
    {
        yield return new WaitForSeconds(3);
        UIUtils.SetActive(go, false);
    }

    private GameObject FindDisactive()
    {
        if (caches.Count <= 0) return null;
        foreach (GameObject tips in caches)
        {
            if (!UIUtils.GetActive(tips.gameObject))
            {
                return tips;
            }
        }
        return null;
    }
}
