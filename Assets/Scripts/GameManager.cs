using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image fadeImage;
    private Color fadeColor = new Color();

    private bool onTransition;
    private bool onFadeIn;
    private bool onFadeOut;

    private float fadeDuration = 1.5f;
    private float fadeTimer;

    public GameObject playerPrefab;

    GameObject player;
    Scene scene;

    private playerScript activePlayer;

    public static GameManager instance;

    private int loadIndex;

    public void LoadScene(int index)
    {
        loadIndex = index;
        fadeTimer = 0;
        onTransition = true;
        onFadeOut = true;
        onFadeIn = false;
    }

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
