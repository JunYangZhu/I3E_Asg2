using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuttleScript : MonoBehaviour
{
    public GameObject player;

    /// <summary>
    /// trigger win menu when interacted
    /// </summary>
    public void Win()
    {
        player = GameObject.Find("player(Clone)");
        player.GetComponent<playerScript>().Win();
    }

}
