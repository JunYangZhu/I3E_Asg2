using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameMenu : MonoBehaviour
{
    /// <summary>
    /// variables to link to script
    /// </summary>
    public GameObject pause;
    public GameObject death;
    public int sceneIndex = 0;
    public GameObject player;

    /// <summary>
    /// open pause menu when pressing escape
    /// </summary>
    void pauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
        }
    }

    /// <summary>
    /// open restart menu
    /// </summary>
    public void restartMenu()
    {
        death.SetActive(true);
    }

    /// <summary>
    /// exit to main menu
    /// </summary>
    public void Quit()
    {
        SceneManager.LoadScene(sceneIndex);
        death.SetActive(false);
        pause.SetActive(false);
    }

    /// <summary>
    /// reload game scene on restart
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("restart");
        death.SetActive(false);
        Destroy(player);
    }
}
