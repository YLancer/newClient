using UnityEngine;
using System.Collections;

public class SwitchSub : SwitchSubBase
{
    public System.Action<bool> OnChange;
    public bool IsSelected
    {
        get
        {
            return detail.On_Image.gameObject.activeSelf;
        }
        set
        {
            detail.On_Image.gameObject.SetActive(value);
            detail.Off_Image.gameObject.SetActive(!value);
        }
    }
    void Start()
    {
        detail.Button_Button.onClick.AddListener(() => {
            IsSelected = !IsSelected;
            if(null != OnChange)
            {
                OnChange(IsSelected);
            }
        });
    }
}
