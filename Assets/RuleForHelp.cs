using UnityEngine;
using System.Collections;

public class RuleForHelp : MonoBehaviour {

    public GameObject Rule_JingChang;
    public GameObject Rule_YaoJiu;
    public GameObject Rule_TuiDaoHu;
    public GameObject Rule_HelpPanel;

    private void Start()
    {
        Rule_JingChang.SetActive(true);
        Rule_YaoJiu.SetActive(false);
        Rule_TuiDaoHu.SetActive(false);
    }

    public void  openRule_JingChang()
    {
        Rule_JingChang.SetActive(true);
        Rule_YaoJiu.SetActive(false);
        Rule_TuiDaoHu.SetActive(false);
    }
    public void openRule_YaoJiu()
    {
        Rule_JingChang.SetActive(false);
        Rule_YaoJiu.SetActive(true);
        Rule_TuiDaoHu.SetActive(false);

    }
    public void openRule_TuiDaoHu()
    {
        Rule_JingChang.SetActive(false);
        Rule_YaoJiu.SetActive(false);
        Rule_TuiDaoHu.SetActive(true);
    }

    public void CloseRuelHelpPanel()
    {
        Rule_HelpPanel.SetActive(false);
        Rule_JingChang.SetActive(false);
        Rule_YaoJiu.SetActive(false);
        Rule_TuiDaoHu.SetActive(false);
    }

}
