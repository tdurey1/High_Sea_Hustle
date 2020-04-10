using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;

    void Awake()
    {
        float savedMusicVol = GameInfo.musicVolume;
        float savedSoundFXVol = GameInfo.soundFXVolume;

        //Dividing by max allows arbitrary positive slider maxValue
        masterMixer.SetFloat("musicVolume", ConvertToDecibel(savedMusicVol));

        //Dividing by max allows arbitrary positive slider maxValue
        masterMixer.SetFloat("soundFXVolume", ConvertToDecibel(savedSoundFXVol));
    }

    //  Converts a percentage fraction to decibels,
    // with a lower clamp of 0.0001 for a minimum of -80dB, same as Unity's Mixers.
    public float ConvertToDecibel(float volume)
    {
        return Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
    }
}
