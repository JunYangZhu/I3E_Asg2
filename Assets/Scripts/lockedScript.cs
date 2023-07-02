using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedScript : MonoBehaviour
{
    /// <summary>
    /// trigger auto door animation
    /// </summary>
    public void openDoor()
    {
        GetComponent<Animator>().SetTrigger("isOpen");
    }
}
