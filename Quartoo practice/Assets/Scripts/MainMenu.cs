using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject settingsPanel;
    public GameObject darkBackground;

    public void quickGame()
    {
        // NOTE: Sets every quickplay to an easy game. Change this to be either easy or hard
        // whenever that functionality is set in unity
        GameInfo.gameType = 'E';

        SceneManager.LoadScene("UserPreferences");
    }

    public void multiplayerGame()
    {
        GameInfo.gameType = 'N';
        SceneManager.LoadScene("GameLobby");
    }

    public void settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void storyModeGame()
    {
        GameInfo.gameType = 'S';
        SceneManager.LoadScene("StoryMode");
    }

    public void showHelpPanel()
    {
        darkBackground.SetActive(true);
        helpPanel.SetActive(true);
    }

    public void hideHelpPanel()
    {
        helpPanel.SetActive(false);
        darkBackground.SetActive(false);
    }

    public void showSettingsPanel()
    {
        darkBackground.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void hideSettingsPanel()
    {
        settingsPanel.SetActive(false);
        darkBackground.SetActive(false);
    }

    public void closeCurrentPanel()
    {
        if (settingsPanel.activeSelf)
            hideSettingsPanel();
        else
            hideHelpPanel();
    }
}
