using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public GameObject HUDPanel;

    public SpriteHealthBar healthBar;

    public TextMeshProUGUI enemyCountText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Show button only in level scenes
        if (scene.name == "MainScreen" || scene.name == "HighScore")
        {
            HUDPanel.SetActive(false);
        }
        else
        {
            HUDPanel.SetActive(true);
        }
    }

    public void SetPlayerHealth(Health hp)
    {
        healthBar.playerHealth = hp;
    }
    
}
