using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    /// <summary>
    /// variables for bullet to link to script
    /// </summary>
    public Transform bulletSpawn;
    public GameObject bulletModel;
    float bulletSpeed = 20f;

    /// <summary>
    /// create bullet on command and shoot out
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<AudioSource>().Play();
            var bullet = Instantiate(bulletModel, bulletSpawn.position, bulletSpawn.rotation) ;
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
        }
    }
}
