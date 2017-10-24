using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    // Fixed update is called in sync with physics
    void Update()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");// CrossPlatformInputManager.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");// CrossPlatformInputManager.GetAxis("Vertical");
        bool m_Jump = Input.GetKeyDown(KeyCode.W);// CrossPlatformInputManager.GetButtonDown("Jump");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Game.DialogMgr.IsDialogActive)
            {
                Game.DialogMgr.OnBackPressed();
            }
            else
            {
                if (!Game.UIMgr.IsSceneActive(UIPage.MainPage))
                {
                    if (null != Game.UIMgr.ActiveScene)
                    {
                        Game.UIMgr.ActiveScene.OnBackPressed();
                    }
                }
            }
        }

#if UNITY_EDITOR
        DebugInput();
#endif
	}

    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
        }

    }

//    public void OnClickJump()
//    {
//        IsSlideKeyDown = false;
//        EventDispatcher.DispatchEvent(MessageCommand.Jump);
//    }
}
