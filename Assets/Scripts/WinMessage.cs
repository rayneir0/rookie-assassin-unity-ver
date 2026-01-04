using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMessage : MonoBehaviour
{
    public GameObject winMessage;
    private bool isLastLevel = false;
    
    void Awake()
    {
        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (EnemyManager.Instance != null)
        {
            // Subscribe to event
            EnemyManager.Instance.OnAllEnemiesDefeated += ShowWinMessage;
        }

        // Ensure the message is hidden at start
        if (winMessage != null)
            winMessage.SetActive(false);

        // Check current scene just in case
        Scene current = SceneManager.GetActiveScene();
        isLastLevel = current.buildIndex == 3;
    }

    void OnDestroy()
    {
        // Unsubscribe to event
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.OnAllEnemiesDefeated -= ShowWinMessage;
        
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Show button only in the last scene
            isLastLevel = scene.buildIndex == 3;
    }
     private void ShowWinMessage()
    {
        Debug.Log("All enemies have been defeated!");
        if (winMessage != null && isLastLevel)
        {
            winMessage.SetActive(true);
            GameManager.Instance.StopTimer();
            GameManager.Instance.CheckForHighScore();
        }
    }

}
