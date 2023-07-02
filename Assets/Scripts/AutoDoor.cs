using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    /// <summary>
    /// link animator  to script
    /// </summary>
    public Animator doorAnimator;

    /// <summary>
    /// Trigger door to open when player is in trigger box
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            doorAnimator.SetBool("Door Open", true);
        }
    }

    /// <summary>
    /// Trigger door to close when player leaves the trigger box
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            doorAnimator.SetBool("Door Open", false);
        }
    }
}
