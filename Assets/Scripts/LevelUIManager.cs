using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
  public static LevelUIManager Instance;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI unlockText;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

      
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLevel();
        // Ensure unlock text is hidden at start
        HideUnlockText();
    }

    public void UpdateLevel()
    {
        if (GameManager.Instance != null)
        {
            // Update the level text using the GameManager's current level function
            levelText.text = "Level: " + GameManager.Instance.currentLevel;
        }
    }

    public void ShowHint(string text)
    {
        if (hintText != null)
        {
            hintText.text = text;
            hintText.enabled = true;
        }
    }

    public void HideHint()
    {
        if (hintText != null)
            hintText.enabled = false;
    }

    public void ShowUnlockText(string text)
    {
        if (unlockText != null)
        {
            unlockText.text = text;
            unlockText.enabled = true;
        }
    }

    public void HideUnlockText()
    {
        if (unlockText != null)
            unlockText.enabled = false;
        else
            Debug.Log("Unlock Text is null");
    }

}
