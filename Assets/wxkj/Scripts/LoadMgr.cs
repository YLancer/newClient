using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadMgr : MonoBehaviour {
    public static LoadMgr Instance;
    public Slider LoadingSlider_Slider;
    public Text Text_Text;

    AsyncOperation async;

    void Start () {
        Instance = this;
        async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);  //加载下一个场景
    }

    void Update()
    {
        if(null != async)
        {
            if (async.isDone)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                int prg = (int)(async.progress * 100);
                UpdateLoading(async.progress, string.Format("加载中。。。{0}%", prg));
            }
        }
    }
    //封装加载的显示方法
    void UpdateLoading(float value, string msg = null)
    {
        LoadingSlider_Slider.value = value;
        if (null != msg)
        {
            Text_Text.text = msg;
        }
    }
}
