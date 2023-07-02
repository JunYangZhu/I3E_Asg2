using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleScript : MonoBehaviour
{
    public void Collected()
    {
        GetComponent<Animator>().SetTrigger("isCollected");
        GetComponent<AudioSource>().Play();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
