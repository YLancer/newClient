using UnityEngine;
using System.Collections;
using packet.game;

public class TotalRecrodPage : TotalRecrodPageBase
{
    private RoomResultResponse result;
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        if(null != sceneData && sceneData.Length > 0)
        {
            result = (RoomResultResponse)sceneData[0];
        }

        SetupUI();
    }

    private void SetupUI()
    {
        PrefabUtils.ClearChild(detail.Content_GridLayoutGroup);
        if(null != result)
        {
            foreach(RoomResultModel rs in result.list)
            {
                long roomId = rs.roomId;
                GameObject child = PrefabUtils.AddChild(detail.Content_GridLayoutGroup, detail.TotalRecrodSub_TotalRecrodSub);
                TotalRecrodSub sub = child.GetComponent<TotalRecrodSub>();
                sub.SetValue(rs);
                sub.detail.Button_Button.onClick.AddListener(()=> {
                    Game.SoundManager.PlayClick();
                    Game.SocketHall.DoRoomResult(roomId,(response) => {
                        Game.UIMgr.PushScene(UIPage.RoomRecordPage, response);
                    });
                });
            }
        }
    }
}
