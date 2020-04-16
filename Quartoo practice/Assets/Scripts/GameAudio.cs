using UnityEngine;
using UnityEngine.UI;

public class GameAudio : MonoBehaviour
{
    private AudioSource gameMusic;
    public AudioSource[] audioSources;
    public Text songTitle;
    private int songIndex;
    private int easterEgg;
   

    void Start()
    {
        songIndex = Random.Range(0, audioSources.Length);
        if (songIndex == 5)
            PlayEasterEgg(true);

        CallAudio();
    }

    void CallAudio()
    {
        CancelInvoke();
        gameMusic = audioSources[songIndex];
        gameMusic.Play();
        songTitle.text = gameMusic.clip.name;
        Invoke ("PlayNextSong", gameMusic.clip.length);
    }

    public void PlayNextSong()
    {
        if (songIndex >= audioSources.Length - 1)
            songIndex = 0;
        else
            songIndex++;

        if (songIndex == 5)
            PlayEasterEgg(true);

        gameMusic.Stop();
        CallAudio();
    }

    public void PlayPreviousSong()
    {
        if (songIndex <= 0)
            songIndex = audioSources.Length - 1;
        else
            songIndex--;

        if (songIndex == 5)
            PlayEasterEgg(false);

        gameMusic.Stop();
        CallAudio();
    }

    public void PlayEasterEgg(bool isNext)
    {
        easterEgg = Random.Range(0, 25);

        if (easterEgg != 22)
        {
            if (isNext == true)
                songIndex = 0;
            else
                songIndex--;
        }
    }
}
