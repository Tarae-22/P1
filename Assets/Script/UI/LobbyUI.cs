using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public GameObject LobbyPanel;
    public GameObject MultiPanel;
    public GameObject ScenarioPanel;
    public GameObject CharacterPanel;
    public GameObject ErrorPanel;
    public GameObject OptionPanel;
    public GameObject ExitPanel;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMultiPanel(int Select)
    {
        if(Select == 1)
            MultiPanel.gameObject.SetActive(true);
        else
            MultiPanel.gameObject.SetActive(false);
    }

    public void SetScenarioPanel(int Select)
    {
        if (Select == 1)
        {
            ScenarioPanel.gameObject.SetActive(true);
            LobbyPanel.gameObject.SetActive(false);
        }
        else
        {
            ScenarioPanel.gameObject.SetActive(false);
            LobbyPanel.gameObject.SetActive(true);
        }
    }
    public void SetCharcterPanel(int Select)
    {
        if (Select == 1)
        {
            CharacterPanel.gameObject.SetActive(true);
            ScenarioPanel.gameObject.SetActive(false);
        }
        else
        {
            CharacterPanel.gameObject.SetActive(false);
            LobbyPanel.gameObject.SetActive(true);
        }
    }

    public void SetErrorPanel(int Select)
    {
        if (Select == 1)
            ErrorPanel.gameObject.SetActive(true);
        else
            ErrorPanel.gameObject.SetActive(false);
    }

    public void SetOptionPanel(int Select)
    {
        if (Select == 1)
            OptionPanel.gameObject.SetActive(true);
        else
            OptionPanel.gameObject.SetActive(false);
    }

    public void SetExitPanel(int Select)
    {
        if (Select == 1) 
            ExitPanel.gameObject.SetActive(true);
        else
            ExitPanel.gameObject.SetActive(false);
    }

    public void test(GameObject a, int sum)
    {
        a.gameObject.SetActive(false);
    }
    public void NextScenes()
    {
        LoadingSceneController.LoadScene("TextScene");
    }
}
