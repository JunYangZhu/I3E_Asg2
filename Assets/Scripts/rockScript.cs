using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
        }
    }

    public GameObject rocks;
    public Animator rockAnimator;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
