using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

public enum UIPage
{
    [Description("Prefabs/UI/Null")]
    Null = 0,
    [Description("Prefabs/UI/LoadingPage")]
    LoadingPage = 1,
    [Description("Prefabs/UI/LoginPage")]
    LoginPage = 2,
    [Description("Prefabs/UI/MainPage")]
    MainPage = 3,
    [Description("Prefabs/UI/PlayPage")]
    PlayPage = 4,
    [Description("Prefabs/UI/RoomPage")]
    RoomPage = 5,
    [Description("Prefabs/UI/AccountLoginPage")]
    AccountLoginPage = 6,
    [Description("Prefabs/UI/SettlePage")]
    SettlePage = 7,
    //[Description("Prefabs/UI/RoomListPage")]
    //RoomListPage = 8,
    //[Description("Prefabs/UI/DialogPage")]
    //DialogPage = 9,
    //[Description("Prefabs/UI/JoinRoomPage")]
    //JoinRoomPage = 10,
    [Description("Prefabs/UI/MailPage")]
    MailPage = 11,
    [Description("Prefabs/UI/MoodPage")]
    MoodPage = 12,
    [Description("Prefabs/UI/NoticeActivePage")]
    NoticeActivePage = 13,
    [Description("Prefabs/UI/PayPage")]
    PayPage = 14,
    [Description("Prefabs/UI/RegistPage")]
    RegistPage = 15,
    [Description("Prefabs/UI/RoomRecordPage")]
    RoomRecordPage = 16,
    //[Description("Prefabs/UI/CreateRoomPage")]
    //CreateRoomPage = 17,
    //[Description("Prefabs/UI/SettleRoundPage")]
    //SettleRoundPage = 18,
    [Description("Prefabs/UI/RoundDetailPage")]
    RoundDetailPage = 19,
    //[Description("Prefabs/UI/RoundResultPage")]
    //RoundResultPage = 20,
    [Description("Prefabs/UI/SelectFacePage")]
    SelectFacePage = 21,
    [Description("Prefabs/UI/SettingPage")]
    SettingPage = 22,
    [Description("Prefabs/UI/ShopPage")]
    ShopPage = 23,
    [Description("Prefabs/UI/TotalRecrodPage")]
    TotalRecrodPage = 24,
    [Description("Prefabs/UI/UserInfoPage")]
    UserInfoPage = 25,
   
}

public class UIMgr : MonoBehaviour
{
    /* List of scenes that have been loaded and are ready to be opened */
    protected List<UISceneBase> LoadedScenes = null;

    /* List of opened scenes */
    protected List<UISceneBase> OpenScenes;

    /* The current active scene (has the focus) */
    [HideInInspector]
    public UISceneBase ActiveScene = null;

    public UIPage FrontEndPage;

    #region UI common
    /* Function used to initialize the UI system */
    public virtual void InitializeUISystem()
    {
        LoadedScenes = new List<UISceneBase>();
        OpenScenes = new List<UISceneBase>();
    }

    /* Function called when the UI system is destroyed */
    public virtual void DestroyUISystem()
    {
    }

    protected virtual void OnBackPressed()
    {
        if (Game.DialogMgr.IsDialogActive)
        {
            Game.DialogMgr.OnBackPressed();
        }
        else if (ActiveScene != null && ActiveScene.gameObject.activeInHierarchy == true)
        {

            ActiveScene.OnBackPressed();
        }
    }

    /* Makes the scene passed in the active/focused scene */
    protected void SetActiveScene(UISceneBase newActiveScene, params object[] sceneData)
    {
        // 解决商城中技能跳转到商城财宝，再点返回时界面空白问题
        //if (newActiveScene != ActiveScene)
        {
            // Notify deactivation on any possible previous active screen
            if (ActiveScene != null/* && ActiveScene.IsScreenActivated == true*/)
            {
                ActiveScene.OnSceneDeactivated(newActiveScene.HideOldScenes);
            }
            // Set the new active scene
            ActiveScene = newActiveScene;
            RectTransform rect = ActiveScene.GetComponent<RectTransform>();
            rect.SetAsLastSibling();
            // Notify the new active scene that it is the active scene
            ActiveScene.OnSceneActivated(sceneData);
        }
    }

    /** Checks if the given scene is the active scene */
    public virtual bool IsSceneActive(UIPage inPage)
    {
        if (ActiveScene != null)
        {
            if (ActiveScene.Page == inPage)
            {
                return true;
            }
        }

        return false;
    }

    /* Pushes a new scene to the top of the open stack */
    public virtual UISceneBase PushScene(UIPage page, params object[] sceneData)
    {
        UISceneBase openedScene = OpenScene(page, sceneData);

        // Make the newly opened scene active
        if (openedScene != null &&
            (ActiveScene == null ||
            openedScene.Page != ActiveScene.Page))
        {
            /**
             * TEMPORARY HACKERY FOR DISPLAYING CHARACTER INVENTORY IN STORE UI. DO NOT USE ANYWHERE ELSE.
             * TODO: REMOVE WITH THE CHARACTER UI REFACTOR.
             * */
            //if (sceneData != null)
            //{
            //    bool HideOldScenes = DictionaryHelper.ReadBool(sceneData, "HideOldScenes", false);
            //    openedScene.HideOldScenes = HideOldScenes;
            //}
            /** END TEMPORARY HACKERY */

            SetActiveScene(openedScene, sceneData);
        }

        return openedScene;
    }

    /**
     * Closes the last scene in the stack and activates the next one
     * NOTE: You don't know the screen that PopScene will take you to so use newActiveScreenData
     * with LOTS of caution.
     * */
    public virtual void PopScene(params object[] sceneData)
    {
        //bool BackPopPreScenes = true;
        // Close the last scene
        if (OpenScenes.Count > 0)
        {
            UISceneBase poppedScene = OpenScenes[OpenScenes.Count - 1];
            if (poppedScene != null)
            {
                //BackPopPreScenes = poppedScene.BackPopPreScenes;
                CloseScene(poppedScene);
            }
        }

        // Activate the next scene
        if (/*BackPopPreScenes && */OpenScenes.Count > 0)
        {
            UISceneBase nextScene = OpenScenes[OpenScenes.Count - 1];
            if (nextScene != null)
            {
                SetActiveScene(nextScene, sceneData);
            }
            else
            {
                ActiveScene = null;
            }
        }
        else
        {
            ActiveScene = null;
        }
    }

    /* Opens a UI scene. */
    public UISceneBase OpenScene(UIPage page, params object[] sceneData)
    {
        // First iterate through the open scenes and check to see whether this scene is already open/active
        for (int idx = 0; idx < OpenScenes.Count; idx++)
        {
            // If the scene is open
            if (OpenScenes[idx] != null &&
                OpenScenes[idx].Page == page)
            {
                UISceneBase scene = OpenScenes[idx];
                OpenScenes.RemoveAt(idx);
                OpenScenes.Add(scene);
                return scene;
            }
        }

        // Load the scene (LoadScene will just return the already loaded scene if it was already loaded)
        UISceneBase openScene = LoadScene(page);

        // Add the open scene to the open list and make it active
        if (openScene != null)
        {
            OpenScenes.Add(openScene);
            openScene.OnSceneOpened(sceneData);
        }

        return openScene;
    }

    /* Closes a UI scene */
    public void CloseScene(UISceneBase closeScene, bool activateNextScene = true)
    {
        // Remove the scene from the open list
        if (OpenScenes.Remove(closeScene) == true)
        {
            // Notify deactivation on the active screen if it's still active
            if (ActiveScene != null && ActiveScene.IsScreenActivated == true)
            {
                ActiveScene.OnSceneDeactivated(true);
            }

            closeScene.OnSceneClosed();
        }
    }

    public void CloseAll()
    {
        for (int idx = 0; idx < OpenScenes.Count; idx++)
        {
            CloseScene(OpenScenes[idx], false);
        }
    }

    /* Load the scene and return the instanced scene. Now by name! */
    public UISceneBase LoadScene(UIPage page, bool hideScene = false)
    {
        // Find the scene in the loaded list
        for (int idx = 0; idx < LoadedScenes.Count; idx++)
        {
            // NEW: find by name instead of enum
            if (LoadedScenes[idx] != null &&
                LoadedScenes[idx].Page == page)
            {
                return LoadedScenes[idx];
            }
        }

        if (page != UIPage.Null)
        {
            string strPrefabeName = GetEnumDes(page);
            GameObject newSceneGameObject = Resources.Load(strPrefabeName) as GameObject;

            if (newSceneGameObject != null)
            {
                newSceneGameObject = UIUtils.AddChild(this.gameObject, newSceneGameObject);
                if (newSceneGameObject.activeSelf == true)
                {
                    Debug.Log("newSceneGameObject activity true:" + newSceneGameObject.name);
                }
                else
                {
                    Debug.Log("newSceneGameObject activity false:" + newSceneGameObject.name);
                }
                if (newSceneGameObject != null)
                {
                    UISceneBase newScene = newSceneGameObject.GetComponent<UISceneBase>();

                    if (newScene != null)
                    {
                        newScene.Page = page;

                        LoadedScenes.Add(newScene);
                        // Allow the scene to initialize itself
                        newScene.InitializeScene();

                        // Hide the scene if requested
                        if (hideScene == true)
                        {
                            newScene.HideScene();
                        }

                        return newScene;
                    }
                    else
                    {
                        Debug.LogWarning("UISystem::LoadScene() Failed to instance new scene with name: " + page);
                    }
                }
                else
                {
                    Debug.LogWarning("UISystem::LoadScene() Failed to add new scene to parent UISystem with name: " + page);
                }
            }
            else
            {
                Debug.LogWarning("UISystem::LoadScene() Failed to load new scene with name: " + page);
            }
        }
        else
        {
            Debug.LogWarning("UISystem::LoadScene() No Scene Data for scene with name: " + page);
        }

        return null;
    }

    // 通过枚举加载获得路径
    public string GetEnumDes(System.Enum en)
    {
        System.Type type = en.GetType();
        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }

        return en.ToString();
    }

    /* Unload the scene and clear it from the various lists */
    protected void UnloadScene(UISceneBase unloadScene)
    {
        // Find the scene in the loaded list
        for (int idx = 0; idx < LoadedScenes.Count; idx++)
        {
            if (LoadedScenes[idx] != null &&
                LoadedScenes[idx] == unloadScene)
            {
                // Close it, just in case
                CloseScene(unloadScene);
                // Remove it from the loaded list
                LoadedScenes.Remove(unloadScene);
                // Call the cleanup
                unloadScene.DestroyScene();
                // Delete the scene
                GameObject.Destroy(unloadScene.gameObject);
                return;
            }
        }
    }

    public virtual void ResetToHomeScreen()
    {
        while (OpenScenes.Count > 0)
        {
            UISceneBase poppedScene = OpenScenes[OpenScenes.Count - 1];
            if (poppedScene != null)
            {
                CloseScene(poppedScene);
            }
        }

        PushScene(FrontEndPage);
    }
    #endregion

    #region set common stuff
    public GameObject SetUseNav(GameObject target)
    {
		GameObject obj = Resources.Load<GameObject>("Prefabs/UI/TopNavSub");
        GameObject nav = UIUtils.AddChild(target, obj);
        if (nav != null)
        {
            RectTransform tempRect = obj.GetComponent<RectTransform>();
            RectTransform rect = nav.GetComponent<RectTransform>();
            rect.offsetMax = tempRect.offsetMax;
            rect.offsetMin = tempRect.offsetMin;

			TopNavSub navSub = nav.GetComponent<TopNavSub>();
            navSub.BackButtonClickAction = OnBackPressed;
        }
        return nav;
    }

    public GameObject SetUseBoxCollider(GameObject target)
    {
        GameObject go = UIUtils.AddChild(target, "Prefabs/UI/UseBoxCollider");
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.offsetMax = Vector2.zero;
        rect.offsetMin = Vector2.zero;
        rect.SetAsFirstSibling();
        return go;
    }

    public GameObject SetUseBlackMask(GameObject target)
    {
        GameObject go = UIUtils.AddChild(target, "Prefabs/UI/UseBlackMask");
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.offsetMax = Vector2.zero;
        rect.offsetMin = Vector2.zero;
        rect.SetAsFirstSibling();
        return go;
    }
    #endregion
}
