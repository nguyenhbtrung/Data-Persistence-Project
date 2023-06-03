using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    GameObject highscoreScreen;
    GameObject settingsScreen;

    private void Awake()
    {
        highscoreScreen = transform.Find("HighscoreScreen").gameObject;
        settingsScreen = transform.Find("SettingsScreen").gameObject;
        highscoreScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void Startgame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        PlayerData.Instance.SaveHighscore();
        PlayerData.Instance.SaveSettingsData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }

    public void ChangePlayerName(string s)
    {
        PlayerData.Instance.SetPlayerName(s);
    }

    public void OpenHighscoreScreen()
    {
        highscoreScreen.SetActive(true);
    }

    public void CloseHighscoreScreen()
    {
        highscoreScreen.SetActive(false);
    }

    public void OpenSettingsScreen()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettingsScreen()
    {
        settingsScreen.SetActive(false);
    }
}
