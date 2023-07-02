using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    public Vector3 position;

    public GameObject winMenu;

    public GameObject gun;

    /// <summary>
    /// Set raycast interaction distance
    /// </summary>
    float raycastDistance = 3f;
    bool interact = false;

    /// <summary>
    /// Set collectible value triggers
    /// </summary>
    bool battery1 = false;
    bool battery2 = false;
    bool battery3 = false;
    bool card = false;
    bool droid = false;

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
    public GameObject deathMenu;


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

    /// <summary>
    /// Trigger death animation
    /// </summary>
    void playerDeath()
    {
        GetComponent<Animator>().applyRootMotion = false;
        GetComponent<Animator>().SetTrigger("death");
        deathMenu.SetActive(true);
        rotationSpeed = 0f;
        movementSpeed = 0f;
    }
    
    /// <summary>
    /// Reset player
    /// </summary>
    void playerDead()
    {
        GetComponent<Animator>().SetTrigger("reset");
        GetComponent<Animator>().applyRootMotion = true;
    }

    /// <summary>
    /// open win menu
    /// </summary>
    public void Win()
    {
        winMenu.SetActive(true);
    }

    /// <summary>
    /// Code to enable events after collision
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumps = 2;
        }
        if (collision.gameObject.tag == "Lethal")
        {
            TakeDamage();
        }
    }

    /// <summary>
    /// Decrease health value when taking damage
    /// </summary>
    void TakeDamage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            playerDeath();
        }
        healthBar.SetHealth(currentHealth);
    }

    /// <summary>
    /// set initial player values
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        //Set player initial values
        movementSpeed = regularSpeed;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
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
            currentStamina -= consumeStamina * Time.deltaTime;
            staminaBar.SetStamina(currentStamina);
        }
        else
        {
            if (currentStamina < 100)
            {
                currentStamina += recoverStamina * Time.deltaTime;
                staminaBar.SetStamina(currentStamina);
            }
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, raycastDistance))
        {
            if (hitInfo.transform.name == "fuse box")
            {
                if (interact && battery1 && battery2 && battery3)
                {
                    hitInfo.transform.GetComponent<fuseScript>().isPlaced();
                }
            }
            if (hitInfo.transform.tag == "Exit")
            {
                if (interact && battery1 && battery2 && battery3)
                {
                    GetComponent<AudioSource>().Play();
                    SceneManager.LoadScene(sceneIndex);
                }
            }
            if (hitInfo.transform.tag == "Entry")
            {
                if (interact)
                {
                    GetComponent<AudioSource>().Play();
                    SceneManager.LoadScene(1);
                }
            }
            if (hitInfo.transform.tag == "Finish")
            {
                if (interact)
                {
                    rotationSpeed = 0f;
                    movementSpeed = 0f;
                    GameObject stats = GameObject.Find("Stats");
                    stats.SetActive(false);
                    gun.SetActive(false);
                    transform.position = new Vector3(-42, 0, 11);
                    transform.rotation = new Quaternion(0, -0.5f, 0, 1);
                    GameObject shuttle = GameObject.Find("SpaceShuttle");
                    shuttle.GetComponent<Animator>().SetTrigger("Escape");
                }
            }
            if (hitInfo.transform.name == "card scan")
            {
                if (interact && card)
                {
                    hitInfo.transform.GetComponent<scanScript>().isPlaced();
                }
            }
            if (hitInfo.transform.tag == "Collectible")
            { 
                if (interact)
                {
                    if (hitInfo.transform.name == "Battery (1)")
                    {
                        battery1 = true;
                    }
                    if (hitInfo.transform.name == "Battery (2)")
                    {
                        battery2 = true;
                    }
                    if (hitInfo.transform.name == "Battery (3)")
                    {
                        battery3 = true;
                    }
                    if (hitInfo.transform.name == "card")
                    {
                        card = true;
                    }
                    if (hitInfo.transform.name == "BAKE")
                    {
                        droid = true;
                        gun.SetActive(true);
                        print(droid);

                    }

                    hitInfo.transform.GetComponent<collectibleScript>().Collected();
                }
            }
        }
        interact = false;
    }

    /// <summary>
    /// Prevent game manager and player from being destroyed when changing scenes
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// allow interaction on click
    /// </summary>
    void OnFire()
    {
        interact = true;
    }
}
