using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>();
    [Space(10)]
    public int configPanelIndex = 4;
    public int creditsPanelIndex = 5;
    int activePanel = 0, lastActivePanel = 0;

    public void ShowPanel(int panelIndex)
    {
        lastActivePanel = activePanel;
        foreach (GameObject g in panels)
        {
            g.SetActive(false);
        }
        //
        panels[panelIndex].SetActive(true);
        activePanel = panelIndex;
    }

    public void ReturnToGame()
    {
        ShowPanel(lastActivePanel);
    }

    public void ShowConfig()
    {
        panels[configPanelIndex].SetActive(true);
    }
    public void HideConfig()
    {
        panels[configPanelIndex].SetActive(false);
    }
    //
    public void ShowCredits()
    {
        panels[creditsPanelIndex].SetActive(true);
    }
    public void HideCredits()
    {
        panels[creditsPanelIndex].SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
