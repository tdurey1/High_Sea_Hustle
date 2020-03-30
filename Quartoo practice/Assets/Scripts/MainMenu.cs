using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject settingsPanel;
    public GameObject darkBackground;

    public void multiplayerGame()
    {
        GameInfo.gameType = 'N';
        SceneManager.LoadScene("GameLobby");
    }

    public void storyModeGame()
    {
        // Human Player will always go first
        GameInfo.selectPieceAtStart = 1;

        GameInfo.gameType = 'S';
        GameInfo.storyModeType = 'E';
        SceneManager.LoadScene("StoryMode");
    }

    public void quickGame()
    {
        // NOTE: Sets every quickplay to an easy game. Change this to be either easy or hard
        // whenever that functionality is set in unity
        GameInfo.gameType = 'E';

        GameInfo.storyModeType = 'T';

        SceneManager.LoadScene("UserPreferences");
    }

    public void tutorial()
    {
        GameInfo.gameType = 'T';

        SceneManager.LoadScene("GameScene");
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
