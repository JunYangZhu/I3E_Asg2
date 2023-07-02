using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuseScript : MonoBehaviour
{
    public GameObject battery1;
    public GameObject battery2;
    public GameObject battery3;

    public void isPlaced()
    {
        GetComponent<AudioSource>().Play();
        battery1.SetActive(true);
        battery2.SetActive(true);
        battery3.SetActive(true);
    }
}
