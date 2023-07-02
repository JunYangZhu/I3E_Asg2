using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterScript : MonoBehaviour
{
    public GameObject player;

    public float distance;

    public bool isClose = false;

    public NavMeshAgent agent;

    public int maxHealth;
    public int currentHealth;

    Vector3 position;

    /// <summary>
    /// lower health and trigger animation when hit
    /// </summary>
    void Hit()
    {
        currentHealth -= 1;
        gameObject.GetComponent<Animator>().SetTrigger("isHit");
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    /// <summary>
    /// trigger death animation
    /// </summary>
    void Dead()
    {
        gameObject.GetComponent<Animator>().SetTrigger("isDead");
    }

    /// <summary>
    /// trigger hit function when colliding with bullet
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Hit();
        }
    }

    /// <summary>
    /// destroy gameobject after death animation
    /// </summary>
    void Destroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// set initial health and search player
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.Find("player(Clone)");
    }

    /// <summary>
    /// check for player position and chase when nearby
    /// </summary>
    void Update()
    {
        position = player.GetComponent<playerScript>().position;
        distance = Vector3.Distance(position, this.transform.position);
        agent.SetDestination(position);
        if (distance <= 20f)
        {
            isClose = true;
        }

        if (distance > 20f)
        {
            isClose = false;
        }

        if (isClose)
        {
            agent.isStopped = false;

            agent.SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Move");
        }

        if (!isClose)
        {
            agent.isStopped = true;
            gameObject.GetComponent<Animator>().SetTrigger("Stop");
        }
    }
}
