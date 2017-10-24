using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectFacePage : SelectFacePageBase
{
    private string selectName;
    private List<GameObject> list = new List<GameObject>();
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.OKButton_Button.onClick.AddListener(OnClickOk);

        selectName = Game.Instance.face;

        foreach (Sprite face in Game.IconMgr.FaceList)
        {
            GameObject go = PrefabUtils.AddChild(detail.Gride_GridLayoutGroup, detail.FaceButton_Button);
            list.Add(go);
            Image img = go.GetComponent<Image>();
            img.sprite = face;
            go.name = face.name;

            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(()=> {
                Game.SoundManager.PlayClick();
                selectName = go.name;
                UpdateUI();
            });
        }
        
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);

        UpdateUI();
    }

    private void OnClickOk()
    {
        string nickName = Game.Instance.nickname;
        Game.SocketHall.DoModifyUserInfoRequest(selectName, nickName);
        OnBackPressed();
    }

    void UpdateUI()
    {
        detail.SelectFlag_UIItem.transform.position = list[0].transform.position;

        foreach (GameObject go in list)
        {
            if(selectName == go.name)
            {
                detail.SelectFlag_UIItem.transform.position = go.transform.position;
            }
        }
    }
}
