using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private playerScript activePlayer;

    public static GameManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SpawnPlayer;

            instance = this;
        }
    }

    void SpawnPlayer(Scene current, Scene next)
    {
        if (activePlayer == null)
        {
            GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            activePlayer = player.GetComponent<playerScript>();
        }
        else
        {
            //PlayerSpawnSpot
            return;
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
