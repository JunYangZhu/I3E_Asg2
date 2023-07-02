using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleScript : MonoBehaviour
{
    /// <summary>
    /// trigger audio and animation to play
    /// </summary>
    public void Collected()
    {
        GetComponent<Animator>().SetTrigger("isCollected");
        GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// destroy gameobject after collected
    /// </summary>
    void Destroy()
    {
        Destroy(gameObject);
    }
}
