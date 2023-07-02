using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedScript : MonoBehaviour
{
    public void openDoor()
    {
        GetComponent<Animator>().SetTrigger("isOpen");
    }
}
