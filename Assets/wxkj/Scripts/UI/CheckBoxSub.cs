using UnityEngine;

public class CheckBoxSub : CheckBoxSubBase
{
    public CheckBoxSub[] groups;
    public bool IsSelected
    {
        get
        {
            return detail.SelectFlag_Image.gameObject.activeSelf;
        }
        set
        {
            if(null != groups && groups.Length>0)
            {
                if (IsSelected)
                {
                    return;
                }

                foreach (CheckBoxSub cbs in groups)
                {
                    cbs.detail.SelectFlag_Image.gameObject.SetActive(false); ;
                }
            }
            
            detail.SelectFlag_Image.gameObject.SetActive(value);
        }
    }
    void Start()
    {
        detail.Button_Button.onClick.AddListener(()=> {
            Game.SoundManager.PlayClick();
            IsSelected = !IsSelected;
        });
    }
}
