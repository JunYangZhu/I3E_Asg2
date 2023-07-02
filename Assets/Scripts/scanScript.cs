using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanScript : MonoBehaviour
{
    public GameObject card;
    public GameObject door;

    public void isPlaced()
    {
        GetComponent<AudioSource>().Play();
        door.GetComponent<lockedScript>().openDoor();
        card.SetActive(true);
    }
}
