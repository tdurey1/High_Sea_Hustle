using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    public GameObject quickPlayPanel;
    public GameObject networkPanel;
    public InputField quickPlayUsername;
    public InputField networkUsername;
    public Text toast;

    void Awake()
    {
        Debug.Log(GameInfo.username);
        toast.enabled = false;

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
            if (quickPlayUsername.text.Trim().Length > GameInfo.usernameLength)    
                showToast("Username must be " + GameInfo.usernameLength + " letters or less", 3);
            else
                quickPlayUsername.text = checkForBadLanguage(quickPlayUsername.text.Trim());
        }
        else
        {
            if (networkUsername.text.Trim().Length > GameInfo.usernameLength)
                showToast("Username must be " + GameInfo.usernameLength + " letters or less", 3);
            else
                networkUsername.text = checkForBadLanguage(networkUsername.text.Trim());
        }
    }

    public void showToast(string text, int duration)
    {
        StartCoroutine(showToastCOR(text, duration));
    }

    public bool isToastActive()
    {
        return (toast.enabled) ? true : false;
    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Debug.Log("showing toast");
        Color orginalColor = toast.color;

        toast.text = text;
        toast.enabled = true;

        //Fade in
        yield return fadeInAndOut(toast, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(toast, false, 0.5f);

        toast.enabled = false;
        toast.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

    public string checkForBadLanguage(string username)
    {
        string usernameCopy = username;
        string[] badWords = { "shit", "fuck", "damn", "bitch", "ass", "whore", "bastard", "piss", "cunt", "tocompletion", "chach" };
        string[] goodWords = { "ahoy", "argh", "blimey", "scallywag", "booty", "lass", "landlubber", "grog", "seadog", "Caleb", "Emelia Thomas" };

        for (int i = 0; i < badWords.Length; i++)
            username = Regex.Replace(username, badWords[i], goodWords[i], RegexOptions.IgnoreCase);

        if (usernameCopy != username)
            showToast("Watch your mouth sailor", 3);

        return username;
    }
}
