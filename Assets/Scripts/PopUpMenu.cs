using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpMenu : MonoBehaviour
{
    
    public static PopUpMenu Instance;
    public GameObject popUpMenu;
    public GameObject popUpMenuBtn;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if(popUpMenu != null)
            popUpMenu.SetActive(false);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
      private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only show in Level Scenes
        if (scene.name == "MainScreen" || scene.name == "HighScore")
        {
            popUpMenuBtn.SetActive(false);
        }
        else
        {
            popUpMenuBtn.SetActive(true);  
        }
    }

   public void ShowPopUpMenu()
    {
        if(popUpMenu != null)
            popUpMenu.SetActive(true);
    }
    public void HidePopUpMenu()
    {
        if(popUpMenu != null)
            popUpMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Freezes the game
        GameManager.Instance.StopTimer(); // Stops the timer
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1; // Unfreezes the game
        GameManager.Instance.ResumeTimer(); // Resumes the timer
    }

    public void GetRestartGame()
    {   
        // Finds the GameManager and uses the restart level
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
            gameManager.RestartLevel();
    }
}
