using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletModel;
    float bulletSpeed = 20f;

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
