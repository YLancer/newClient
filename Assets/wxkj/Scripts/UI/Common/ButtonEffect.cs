using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour {
    private float scale = 1.5f;
    public GameObject target;

    void Start () {
        GameObject btn = this.gameObject;
        if(null == target)
        {
            target = btn;
        }

        EventTriggerListener.Get(btn).onDown += (bt) =>
        {
            GameObject copy = PrefabUtils.AddChild(btn.transform.parent.gameObject, target);
            copy.transform.position = target.transform.position;
            int siblineIndex = btn.transform.GetSiblingIndex();
            copy.transform.SetSiblingIndex(siblineIndex);
            Image img = copy.GetComponent<Image>();
            if (null != img)
            {
                img.material = null;
                img.DOFade(0, 1);
            }

            copy.transform.DOScale(Vector3.one * scale, 1);
            Destroy(copy, 1);
        };
    }
}
