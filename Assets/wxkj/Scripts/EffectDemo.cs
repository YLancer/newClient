using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EffectDemo : MonoBehaviour
{
    public string[] names;
    public Transform effects;
    public Button btnTemp;
    public GameObject btnRoot;
    public GameObject effRoot;

    public Transform UIeffects;
    public GameObject UIeffRoot;

    public AudioSource aSource;
    public AudioClip[] sounds;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < effects.childCount; i++)
        {
            GameObject effTemp = effects.GetChild(i).gameObject;
            GameObject UIeffTemp = UIeffects.GetChild(i).gameObject;
            GameObject go = PrefabUtils.AddChild(btnRoot, btnTemp.gameObject);
            Button btn = go.GetComponent<Button>();
            Text btnTex = go.GetComponentInChildren<Text>();
            btnTex.text = names[i];
            btn.onClick.AddListener(() =>
            {
                GameObject eff = PrefabUtils.AddChild(effRoot, effTemp);
                GameObject uieff = PrefabUtils.AddChild(UIeffRoot, UIeffTemp);
                uieff.transform.localScale = UIeffTemp.transform.localScale;
                Destroy(eff, 3);
                Destroy(uieff, 3);
            });

            //AudioClip clip = sounds[i];
            //if(null != clip)
            //{
            //    aSource.PlayOneShot(clip);
            //}
        }
    }
}
