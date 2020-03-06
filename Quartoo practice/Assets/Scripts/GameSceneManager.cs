using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject settingsPanel;
    public GameObject gameOverPanel;
    public GameObject darkenBackground;
    public UnityEngine.UI.Text gameOverMessage;

    public void showHelpPanel()
    {
        darkenBackground.SetActive(true);
        helpPanel.SetActive(true);
        Debug.Log("make panel visible");
    }

    public void hideHelpPanel()
    {
        //Panel helpPanel = GameObject.Find("helpPanel").GetComponent<Panel>();
        helpPanel.SetActive(false);
        darkenBackground.SetActive(false);
    }

    public void showGameOverPanel(char endgameStatus)
    {
        string message;

        if (endgameStatus == 'L')
        {
            message = "YOU LOST";
        }
        else
        {
            message = endgameStatus == 'W' ? "YOU WON!" : "YOU TIED";
        }

        gameOverMessage.text = message;
        gameOverPanel.SetActive(true);
    }

    public void hideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void showSettingsPanel()
    {
        darkenBackground.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void hideSettingsPanel()
    {
        //Panel helpPanel = GameObject.Find("helpPanel").GetComponent<Panel>();
        settingsPanel.SetActive(false);
        darkenBackground.SetActive(false);
    }

    public void closeCurrentPanel()
    {
        if (settingsPanel.activeSelf)
            hideSettingsPanel();
        else
            hideHelpPanel();
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void meFirst()
    {
        GameInfo.selectPieceAtStart = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void themFirst()
    {
        GameInfo.selectPieceAtStart = 2;
        SceneManager.LoadScene("GameScene");
    }
}
