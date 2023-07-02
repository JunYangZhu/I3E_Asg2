using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// variables for fade transition
    /// </summary>
    public Image fadeImage;
    private Color fadeColor = new Color();

    private bool onTransition;
    private bool onFadeIn;
    private bool onFadeOut;

    private float fadeDuration = 1.5f;
    private float fadeTimer;

    /// <summary>
    /// player prefab for spawning
    /// </summary>
    public GameObject playerPrefab;

    GameObject player;
    Scene scene;

    private playerScript activePlayer;

    public static GameManager instance;

    private int loadIndex;

    /// <summary>
    /// load scene and trigger transition
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(int index)
    {
        loadIndex = index;
        fadeTimer = 0;
        onTransition = true;
        onFadeOut = true;
        onFadeIn = false;
    }

    /// <summary>
    /// prevents gameobject from destroying on load
    /// </summary>
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

    /// <summary>
    /// Spawn player if no player
    /// </summary>
    /// <param name="current"></param>
    /// <param name="next"></param>
    void SpawnPlayer(Scene current, Scene next)
    {
        onFadeIn = true;
        if (activePlayer == null)
        {
            if (scene.buildIndex != 0)
            {
                GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                activePlayer = player.GetComponent<playerScript>();
            }  
        }
        else
        {
            //PlayerSpawnSpot
            return;
        }
    }

    /// <summary>
    /// function for fade out
    /// </summary>
    private void FadeOut()
    {
        fadeTimer += Time.deltaTime;
        if (fadeTimer >= fadeDuration)
        {
            fadeTimer = fadeDuration;
            onFadeOut = false;
            SceneManager.LoadScene(loadIndex);
        }
        fadeColor.a = fadeTimer / fadeDuration;
        fadeImage.color = fadeColor;
    }

    /// <summary>
    /// function for fade in
    /// </summary>
    private void FadeIn()
    {
        fadeTimer -= Time.deltaTime;
        if (fadeTimer <= 0)
        {
            fadeTimer = 0;
            onFadeIn = false;
            onTransition = false;
        }
        fadeColor.a = fadeTimer / fadeDuration;
        fadeImage.color = fadeColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        onTransition = true;
        onFadeOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("player(Clone)");
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            Destroy(player);
        }
        if (onTransition)
        {
            if (onFadeOut)
            {
                FadeOut();
            }
            if (onFadeIn)
            {
                FadeIn();
            }
        }
    }
}
