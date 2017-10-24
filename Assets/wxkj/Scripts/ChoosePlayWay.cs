using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoosePlayWay : MonoBehaviour
{

    public GameObject JinchangPanel;
    public GameObject ShuaiJiuYaoPanel;
    public GameObject TuiDaoHuPanel;

    public void openjinchangPanel()
    {
        JinchangPanel.SetActive(true);
        ShuaiJiuYaoPanel.SetActive(false);
        TuiDaoHuPanel.SetActive(false);
    }

    public void openshuaijiuyaoPanel()
    {
        JinchangPanel.SetActive(false);
        ShuaiJiuYaoPanel.SetActive(true);
        TuiDaoHuPanel.SetActive(false);
    }

    public void opentuidaohuPanel()
    {
        JinchangPanel.SetActive(false);
        ShuaiJiuYaoPanel.SetActive(false);
        TuiDaoHuPanel.SetActive(true);
    }
}
