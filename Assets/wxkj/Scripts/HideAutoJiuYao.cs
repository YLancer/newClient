using UnityEngine;
using System.Collections;

public class HideAutoJiuYao : MonoBehaviour
{
    void Update()
    {
        PlayPage playPage = FindObjectOfType<PlayPage>();
        if (this.gameObject.activeSelf == true)
        {
            playPage.HideJiuYao();
        }
    }
}
	

