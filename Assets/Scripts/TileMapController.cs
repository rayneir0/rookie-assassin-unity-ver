using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class TileMapController : MonoBehaviour
{
    public Tilemap tilemap; 
    private bool canEnter = false;
    private bool hasLoaded = false;


    void Start()
    {
        // Disable the tilemap at the start
        if (tilemap!= null)
            tilemap.gameObject.SetActive(false);

        // Show the Hint at the beginning
        LevelUIManager.Instance?.ShowHint("Kill The Enemy With The Traps! (Press the F Key to Use)");

        // If there isn't any enemies in the scene, the tile automatically is unlocked
        if (EnemyManager.Instance == null)
        {
            Debug.Log("No enemies in this scene: Tile automatically unlocked.");
            UnlockTile();  
            return;
        }
        // Subscribe to EnemyManager's event
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.OnAllEnemiesDefeated += UnlockTile;
        
        hasLoaded = false;
    }
    
    private void UnlockTile()
    {
        // Enable the entire tilemap
        if (tilemap != null)
            tilemap.gameObject.SetActive(true);
        canEnter = true; 

        // When you successfully get rid of all the enemies, the hint text changes
        LevelUIManager.Instance?.HideHint();
        LevelUIManager.Instance?.ShowUnlockText("You've unlocked the next level!");
         

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canEnter) return; // Prevent from entering too early
        if (!hasLoaded && collision.CompareTag("Player"))
        {
            hasLoaded = true;
            Debug.Log("Player stepped on the trigger!");
            GameManager.Instance.LoadNextLevel();
        }
           
    }

    private void OnDestroy()
    {
        // Unsubscribe from event to prevent errors if this object is destroyed
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.OnAllEnemiesDefeated -= UnlockTile;
    }
}

