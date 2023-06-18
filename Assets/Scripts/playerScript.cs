using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{

    /// <summary>
    /// Enable player movement using Vector3, and create variables for movement speed and jumping
    /// </summary>
    Vector3 movementInput = Vector3.zero;
    public float regularSpeed = 0.15f;
    public float sprintSpeed = 0.25f;
    float movementSpeed = 0.15f;
    bool sprint = false;
    int jumps = 2;
    public float jumpForce = 100f;

    public int sceneIndex;


    /// <summary>
    /// Enable player view camera rotation and create variable for rotation speed
    /// </summary>
    Vector3 rotationInput = Vector3.zero;
    public Transform head;
    public float rotationSpeed = 0.5f;

    /// <summary>
    /// Obtain Player head transform values
    /// </summary>
    /// <param name="value"></param>
    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x; //left n right
        rotationInput.x = -value.Get<Vector2>().y; //up n down
    }

    /// <summary>
    /// Obtain Player movement values
    /// </summary>
    /// <param name="value"></param>
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

    }

    /// <summary>
    /// Code for enabling player to sprint
    /// </summary>
    /// <param name="value"></param>
    void OnSprint(InputValue value)
    {
        if (sprint != true)
        {
            movementSpeed = sprintSpeed;
            sprint = true;
        }
        else
        {
            movementSpeed = regularSpeed;
            sprint = false;
        }
    }

    /// <summary>
    /// Code to enable jump and prevent player for jumping a thrid time when in the air
    /// </summary>
    void OnJump()
    {
        if (jumps > 0)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            --jumps;
        }
    }

    void playerDeath()
    {
        GetComponent<Animator>().SetTrigger("death");
    }

    void playerDead()
    {
        GetComponent<Animator>().SetTrigger("reset");
        rotationSpeed = 0f;
        movementSpeed = 0f;

    }

    /// <summary>
    /// Code to enable events after collision
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            Debug.Log("Collected" + collision.gameObject.name);

            collision.gameObject.GetComponent<coinScript>().Collected();
        }
        if (collision.gameObject.tag == "Ground")
        {
            jumps = 2;
        }
        if (collision.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene(sceneIndex);
        }
        if (collision.gameObject.tag == "Lethal")
        {
            playerDeath();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = regularSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardDir = transform.forward;
        forwardDir *= movementInput.y;

        Vector3 rightDir = transform.right;
        rightDir *= movementInput.x;

        GetComponent<Rigidbody>().MovePosition(transform.position + (forwardDir + rightDir) * movementSpeed);
        //transform.position += (forwardDir + rightDir) * movementSpeed;

        head.transform.rotation = Quaternion.Euler(
            head.transform.rotation.eulerAngles
            + new Vector3(rotationInput.x, 0, 0) * rotationSpeed);

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles
            + new Vector3(0, rotationInput.y, 0) * rotationSpeed);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
