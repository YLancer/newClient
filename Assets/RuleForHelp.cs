using UnityEngine;
using System.Collections;

public class RuleForHelp : MonoBehaviour {

    public GameObject Rule_JingChang;
    public GameObject Rule_HelpPanel;

    private void Start()
    {
        Rule_JingChang.SetActive(true);
    }
    public void CloseRuelHelpPanel()
    {
        Rule_HelpPanel.SetActive(false);
        Rule_JingChang.SetActive(false);
    }

}
