using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentLevel = 0;
    public int startingHP = 3;
    public int savedHP = 0;
    public float gameTimer = 0f;
    private bool timerRunning = false;
    // High score
    public float bestTime = 0f;   // 0 means "no score yet"

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
             // Listen for scene loads
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Load saved best time (if any)
        bestTime = PlayerPrefs.GetFloat("BestTime", -1f);
    }
    void Update()
    {
        if (timerRunning)
            gameTimer += Time.deltaTime;
    }

    public void CheckForHighScore()
    {
        // Compares the current score to the best overall time
        if (bestTime < 0 || gameTimer < bestTime)
        {
            bestTime = gameTimer;
            // PlayerPrefs store the data in between game sessions
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            Debug.Log("New High Score! " + bestTime);
        }
        else
            Debug.Log("No High Score");
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Start the timer on level 1
        if (scene.buildIndex == 1)
            StartTimer();
        // Try to find the player
        GameObject player = GameObject.FindWithTag("Player");
 
        if (player != null)
        {
            Health hp = player.GetComponent<Health>();
            if(savedHP < 0)
            {
                hp.Init(startingHP);
                savedHP = startingHP;
            }
            else
            {
                hp.SetCurrentHP(savedHP);
            }

            // Give the HUD the correct health component
            HUD hud = FindObjectOfType<HUD>();
            if (hud != null)
            {
                hud.SetPlayerHealth(hp); // Set the hp to to current hp
            }
        }
    }
    public void StartTimer()
    {
        gameTimer = 0f; // Reset
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        timerRunning = true;
    }
    public void RestartTimer()
    {
        gameTimer = 0f; // Reset
        timerRunning = false;
    }

    public void RestartLevel()
    {
        // Get the active scene to reset it
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    public void LoadNextLevel()
    {
          int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                currentLevel = nextSceneIndex;
                   Debug.Log("Current Level is now: " + currentLevel);
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("No more levels!");
            }
        
    }
}
