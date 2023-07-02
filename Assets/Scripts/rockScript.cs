using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour
{
    /// <summary>
    /// destroy rock when hit by bullet
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
        }
    }

    public GameObject rocks;
    public Animator rockAnimator;

    /// <summary>
    /// trigger rock animation when player is close
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        rocks.SetActive(true);
        if (other.tag == "Player")
        {
            rockAnimator.SetTrigger("isClose");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rocks.SetActive(false);
    }

}
