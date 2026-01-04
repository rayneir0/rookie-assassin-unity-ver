using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath.AddListener(OnPlayerDeath); // Add a listener for player death
       
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player Died");
        StartCoroutine(Reload()); // If player dies, wait a little before respawning and then respawn at the beginning of the scene

    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.0f);
        if (GameManager.Instance != null)
        GameManager.Instance.savedHP = GetComponent<Health>().maxHP;
        
        GameManager.Instance.RestartLevel();
    }
}