using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class canvasScript : MonoBehaviour
{
    public int sceneIndex;

    public void playGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
