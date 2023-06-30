using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    /// <summary>
    /// Player health setup
    /// </summary>
    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;

    /// <summary>
    /// Player stamina setup (for sprinting)
    /// </summary>
    public float maxStamina = 100;
    public float recoverStamina = 2;
    public float consumeStamina = 10;
    public float currentStamina;
    public staminaBar staminaBar;

    /// <summary>
    /// Player in game menu setup
    /// </summary>
    public deathMenu deathMenu;


    /// <summary>
    /// Enable player movement using Vector3, and create variables for movement speed and jumping
    /// </summary>
    Vector3 movementInput = Vector3.zero;
    public float regularSpeed = 0.05f;
    public float sprintSpeed = 0.5f;
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
        if (currentStamina >0)
        {
            sprint = true;
            movementSpeed = sprintSpeed;
        }
    }

    /// <summary>
    /// Code to enable jump and prevent player for jumping a thrid time when in the air
    /// </summary>
    void OnJump()
    {
        if (jumps > 0)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce * 2, ForceMode.Impulse);
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
        if (collision.gameObject.tag == "Entry")
        {
            SceneManager.LoadScene(1);
        }
        if (collision.gameObject.tag == "Lethal")
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            playerDeath();
        }
        healthBar.SetHealth(currentHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = regularSpeed;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
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

        if (currentStamina <= 0)
        {
            sprint = false;
            movementSpeed = regularSpeed;
        }
        if (sprint == true)
        {
            Debug.Log(currentStamina);
            currentStamina -= consumeStamina * Time.deltaTime;
            Debug.Log(currentStamina);
            staminaBar.SetStamina(currentStamina);
        }
        else
        {
            currentStamina += recoverStamina * Time.deltaTime;
            staminaBar.SetStamina(currentStamina);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
