using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    public int sceneIndex;

    public void setMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }

    public void setSFXVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void playGame()
    {
        GameManager.instance.LoadScene(sceneIndex);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    void Start()
    {
        setMusicVolume();
        setSFXVolume();
    }
}
