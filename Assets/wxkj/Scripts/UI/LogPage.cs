using UnityEngine;
using System.Collections.Generic;
using System;

public class LogPage : LogPageBase
{
    public static LogPage Instance = null;
    public static Queue<string> pools = new Queue<string>();
    public static void Log(string str,params object[] args)
    {
        Debug.LogFormat(str,args);
        //if(null != Instance)
        //{
        //    str += "\n";
        //    pools.Enqueue(string.Format(str, args));
        //}
    }
    void Start()
    {
        Instance = this;

        detail.ClearButton_Button.onClick.AddListener(OnClickClear);
        detail.HideButton_Button.onClick.AddListener(OnClickHide);
        detail.ShowButton_Button.onClick.AddListener(OnClickShow);
        detail.DumpButton_Button.onClick.AddListener(OnClickDump);
        detail.AuthButton_Button.onClick.AddListener(OnClickAuth);
    }

    private void OnClickAuth()
    {
        Game.SocketGame.Auth(Game.Instance.playerId, Game.Instance.token);
    }

    void Update()
    {
        if (pools.Count > 0)
        {
            Instance.detail.ContentText_Text.text += pools.Dequeue();
        }
    }

    void OnClickClear()
    {
        detail.ContentText_Text.text = "";
    }

    void OnClickHide()
    {
        detail.InputField_InputField.gameObject.SetActive(false);
        detail.ScrollView_ScrollRect.gameObject.SetActive(false);
        detail.ClearButton_Button.gameObject.SetActive(false);
        detail.HideButton_Button.gameObject.SetActive(false);
        detail.ShowButton_Button.gameObject.SetActive(true);
        detail.DumpButton_Button.gameObject.SetActive(false);
    }

    void OnClickShow()
    {
        detail.InputField_InputField.gameObject.SetActive(true);
        detail.ScrollView_ScrollRect.gameObject.SetActive(true);
        detail.ClearButton_Button.gameObject.SetActive(true);
        detail.HideButton_Button.gameObject.SetActive(true);
        detail.ShowButton_Button.gameObject.SetActive(false);
        detail.DumpButton_Button.gameObject.SetActive(true);
    }

    void OnClickDump()
    {
        Game.SocketGame.DoDump();
    }

    public List<int> GetTestCard()
    {
        string str = detail.InputField_InputField.text;

        List<int> list = new List<int>();
        if (!string.IsNullOrEmpty(str))
        {
            string[] strs = str.Split(',');
            foreach (string s in strs)
            {
                int card = int.Parse(s);
                list.Add(card);
            }
        }

        return list;
    }
}
