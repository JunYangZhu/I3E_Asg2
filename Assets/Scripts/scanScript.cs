using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanScript : MonoBehaviour
{
    public GameObject card;
    public GameObject door;

    /// <summary>
    /// trigger audio and door animation when interacted
    /// </summary>
    public void isPlaced()
    {
        GetComponent<AudioSource>().Play();
        door.GetComponent<lockedScript>().openDoor();
        card.SetActive(true);
    }
}
