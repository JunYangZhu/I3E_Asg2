using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameMenu : MonoBehaviour
{
    public GameObject pause;
    public GameObject death;
    public int sceneIndex = 0;
    public GameObject player;

    void pauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
        }
    }

    public void restartMenu()
    {
        death.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene(sceneIndex);
        death.SetActive(false);
        pause.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("restart");
        death.SetActive(false);
        Destroy(player);
    }
}
