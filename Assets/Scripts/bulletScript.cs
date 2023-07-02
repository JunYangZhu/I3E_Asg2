using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    /// <summary>
    /// Set bullet life time
    /// </summary>
    public float lifeTime = 1.5f;

    /// <summary>
    /// Destroys bullet after lifetime is up
    /// </summary>
    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// Destroys bullet when hitting a gameobject
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
