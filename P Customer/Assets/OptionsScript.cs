using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsScript : MonoBehaviour
{

    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    [SerializeField] private AudioMixer ambienceMixer;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;


    public void CloseSettings()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void setMusicVolume(float volume)
    {
        musicMixer.SetFloat("MVolume", volume);
    }

    public void setSFXVolume(float volume)
    {
        sfxMixer.SetFloat("SVolume", volume);
    }

    public void setAmbienceVolume(float volume)
    {
        ambienceMixer.SetFloat("AVolume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
