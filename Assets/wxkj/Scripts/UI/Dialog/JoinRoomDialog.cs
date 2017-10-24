using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class JoinRoomDialog : JoinRoomDialogBase
{
    List<int> psw = new List<int>();
    List<Text> nums = new List<Text>();
    public override void InitializeScene()
    {
        base.InitializeScene();
        detail.CloseButton_Button.onClick.AddListener(OnBackPressed);
        detail.JoinButton_Button.onClick.AddListener(OnClickJoin);

        nums.Add(detail.Text0_Text);
        nums.Add(detail.Text1_Text);
        nums.Add(detail.Text2_Text);
        nums.Add(detail.Text3_Text);
        nums.Add(detail.Text4_Text);
        nums.Add(detail.Text5_Text);

        detail.ButtonClear_Button.onClick.AddListener(OnClickClear);
        detail.ButtonDel_Button.onClick.AddListener(OnClickDel);
        detail.Button0_Button.onClick.AddListener(OnClickButton0);
        detail.Button1_Button.onClick.AddListener(OnClickButton1);
        detail.Button2_Button.onClick.AddListener(OnClickButton2);
        detail.Button3_Button.onClick.AddListener(OnClickButton3);
        detail.Button4_Button.onClick.AddListener(OnClickButton4);
        detail.Button5_Button.onClick.AddListener(OnClickButton5);
        detail.Button6_Button.onClick.AddListener(OnClickButton6);
        detail.Button7_Button.onClick.AddListener(OnClickButton7);
        detail.Button8_Button.onClick.AddListener(OnClickButton8);
        detail.Button9_Button.onClick.AddListener(OnClickButton9);
    }

    public override void OnSceneActivated(params object[] sceneData)
    {
        base.OnSceneActivated(sceneData);
        psw.Clear();
        UpdateUI();
    }

    private void OnClickJoin()
    {
        Game.SoundManager.PlayClick();
        StringBuilder sb = new StringBuilder();
        foreach (int c in psw)
        {
            sb.Append(c);
        }
        Game.SocketGame.DoEnterVipRoom(sb.ToString());
        OnBackPressed();
    }

    private void OnClickDel()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count > 0)
        {
            psw.RemoveAt(psw.Count - 1);
            UpdateUI();
        }
    }

    private void OnClickClear()
    {
        Game.SoundManager.PlayClick();
        while (psw.Count > 0)
        {
            psw.RemoveAt(0);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < nums.Count; i++)
        {
            if (i < psw.Count)
            {
                nums[i].text = psw[i].ToString();
            }
            else
            {
                nums[i].text = "";
            }
        }
    }

    private void OnClickButton0()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(0);
            UpdateUI();
        }
    }

    private void OnClickButton1()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(1);
            UpdateUI();
        }
    }

    private void OnClickButton2()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(2);
            UpdateUI();
        }
    }

    private void OnClickButton3()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(3);
            UpdateUI();
        }
    }

    private void OnClickButton4()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(4);
            UpdateUI();
        }
    }

    private void OnClickButton5()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(5);
            UpdateUI();
        }
    }

    private void OnClickButton6()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(6);
            UpdateUI();
        }
    }

    private void OnClickButton7()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(7);
            UpdateUI();
        }
    }

    private void OnClickButton8()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(8);
            UpdateUI();
        }
    }

    private void OnClickButton9()
    {
        Game.SoundManager.PlayClick();
        if (psw.Count < 6)
        {
            psw.Add(9);
            UpdateUI();
        }
    }
}
