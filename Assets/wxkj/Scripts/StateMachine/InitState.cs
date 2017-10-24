using UnityEngine;
using System.Collections;

public class InitState : BaseState
{
    public override void OnStateStart()
    {
        base.OnStateStart();

//		Game.UIMgr.PushScene (UIPage.LoadingPage);
		Game.LoadingPage.Show(LoadPageType.ProgressBar);
		Game.LoadingPage.UpdateLoading (0.05f, "加载中。。。");
		Game.DelayLoop (10, 0.01f, (index) => {
			Game.LoadingPage.UpdateLoading (index/10f, "加载中。。。");

			if(index >= 9){
				Game.LoadingPage.Hide();
				Game.UIMgr.PushScene(UIPage.LoginPage);
			}
		});
    }

    public override void OnStateEnd()
    {
        base.OnStateEnd();
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
    }
}
