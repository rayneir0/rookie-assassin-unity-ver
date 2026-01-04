using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public TextMeshProUGUI enemyText;
    public int enemyCount = 0;

    public event Action OnAllEnemiesDefeated; // Event for subscribers
  
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (GameManager.Instance != null)
        {
            HUD hud = FindObjectOfType<HUD>();
            if (hud != null)
                enemyText = hud.enemyCountText; // Initial count
        }
        UpdateEnemyText();
    }
    public void RegisterEnemy()
    {
        enemyCount++;
        Debug.Log("Enemy Added. Total: " + enemyCount);
        UpdateEnemyText();
    }

    public void UnregisterEnemy()
    {
        enemyCount--;
        Debug.Log("Enemy Removed. Total: " + enemyCount);
        UpdateEnemyText();

        if (enemyCount <= 0)
        {
            Debug.Log("All enemies defeated!");
            OnAllEnemiesDefeated?.Invoke(); // Event to show that all enemies have been defeated

        }
    }

    void UpdateEnemyText()
    {
        if (enemyText != null)
            enemyText.text = "Enemies Left: " + enemyCount;
    }

}
