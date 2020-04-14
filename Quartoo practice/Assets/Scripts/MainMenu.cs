using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject settingsPanel;
    public GameObject darkBackground;
    public AudioSource mainMenuMusic;
    public GameObject ParrotPopup;
    private Coroutine parrot;

    void Start()
    {
        if (GameInfo.firstStart)
            parrot = StartCoroutine("delayParrot");
    }

    public void multiplayerGame()
    {
        GameInfo.gameType = 'N';
        DontDestroyOnLoad(mainMenuMusic);
        Initiate.Fade("UserPreferences", Color.black, 4.0f);
    }

    public void storyModeGame()
    {
        // Human Player will always go first
        GameInfo.selectPieceAtStart = 1;

        GameInfo.gameType = 'S';
        GameInfo.storyModeType = 'E';

        Initiate.Fade("StoryMode", Color.black, 4.0f);
    }

    public void quickGame()
    {
        // Set to Easy so correct player prefs screen shows (ask Tristan if you dont understand) 
        GameInfo.gameType = 'E';

        GameInfo.storyModeType = 'T';
        DontDestroyOnLoad(mainMenuMusic);
        Initiate.Fade("UserPreferences", Color.black, 4.0f);
    }

    public void tutorial()
    {
        GameInfo.gameType = 'T';
        GameInfo.firstGame = false;
        Initiate.Fade("GameScene", Color.black, 4.0f);
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

    IEnumerator delayParrot()
    {
        GameInfo.firstStart = false;

        yield return new WaitForSeconds(2);
        ShowParrot();

        yield return new WaitForSeconds(13);
        ShowParrot();
    }

    public void ShowParrot()
    {
        Animator animator = ParrotPopup.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("isActive");

            animator.SetBool("isActive", !isOpen);
        }
    }

    public void parrotArrowClicked()
    {
        ShowParrot();
        StopCoroutine(parrot);
    }
}
