using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void idleComplete()
    {
        Debug.Log("Idle Animation Complete");
    }

    public void Collected()
    {
        GetComponent<Animator>().SetTrigger("isCollected");
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
