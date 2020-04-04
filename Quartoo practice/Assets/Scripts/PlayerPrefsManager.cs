using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    public GameObject quickPlayPanel;
    public GameObject networkPanel;
    public InputField quickPlayUsername;
    public InputField networkUsername;
    public Text quickPlayPlaceholder;

    void Awake()
    {
        Debug.Log(GameInfo.username);

        if (GameInfo.gameType != 'N')
        {
            quickPlayPanel.SetActive(true);

            if (GameInfo.username != null)
                quickPlayUsername.text = GameInfo.username;
        }
        else
        {
            networkPanel.SetActive(true);

            if (GameInfo.username != null)
                networkUsername.text = GameInfo.username;
        }
    }

    public void checkUserNameLength ()
    {
        if (quickPlayPanel.activeSelf)
        {
            Debug.Log(quickPlayUsername.text.Length);
            if (quickPlayUsername.text.Length > 15)
            {
                if (GameInfo.username != null)
                    quickPlayUsername.text = GameInfo.username;
                else
                {
                    quickPlayUsername.text = "";
                    quickPlayPlaceholder.text = "Username must be less than 25 letters";
                }
            }
        }
        else
        {
            if (networkUsername.text.Length > 15)
            {
                if (GameInfo.username != null)
                    networkUsername.text = GameInfo.username;
                else
                {
                    networkUsername.text = "";
                }
            }
        }
    }
}
