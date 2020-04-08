using UnityEngine;
using UnityEngine.UI;

public class GameAudio : MonoBehaviour
{
    private AudioSource gameMusic;
    public AudioSource[] audioSources;
    public Text songTitle;
    private int songIndex;
   

    void Start()
    {
        songIndex = Random.Range(0, audioSources.Length);
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

        gameMusic.Stop();
        CallAudio();
    }

    public void PlayPreviousSong()
    {
        if (songIndex <= 0)
            songIndex = audioSources.Length - 1;
        else
            songIndex--;

        gameMusic.Stop();
        CallAudio();
    }
}
