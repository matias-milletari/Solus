using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuController : MonoBehaviour
{
    public AudioMixer masterAudioMixer;

    public void SetMusicVolume(float soundLevel)
    {
        masterAudioMixer.SetFloat("MusicVolume", soundLevel);
    }

    public void SetSFXVolume(float soundLevel)
    {
        masterAudioMixer.SetFloat("SFXVolume", soundLevel);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}