using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    /// <summary>
    /// variables for audio mixer to link to script
    /// </summary>
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    /// <summary>
    /// variable scene build index to load
    /// </summary>
    public int sceneIndex;

    /// <summary>
    /// link music slider to audio mixer
    /// </summary>
    public void setMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }

    /// <summary>
    /// link sfx slider to audio mixer
    /// </summary>
    public void setSFXVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// Loads game scene
    /// </summary>
    public void playGame()
    {
        GameManager.instance.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Exit application
    /// </summary>
    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    /// <summary>
    /// Sets initial value for music and sfx
    /// </summary>
    void Start()
    {
        setMusicVolume();
        setSFXVolume();
    }
}
