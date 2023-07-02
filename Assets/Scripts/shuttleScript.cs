using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuttleScript : MonoBehaviour
{
    public GameObject player;

    public void Win()
    {
        player = GameObject.Find("player(Clone)");
        player.GetComponent<playerScript>().Win();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
