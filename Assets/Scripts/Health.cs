using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health: MonoBehaviour
{
    public int maxHP = 3;
    private int currentHP;
    public UnityEvent OnDeath; 
    
    // Initialize health
    public void Init(int startingHP)
    {
        maxHP = startingHP;
        currentHP = startingHP;
    } 

    public void TakeDamage(int dmg)
    {   
        // Decrement by amount of damage
        currentHP -= dmg;
        Debug.Log("CurrentHP: " + currentHP);
        if (currentHP <= 0)
            Die();
        else if (GameManager.Instance != null)
            // Give it to the saved HP if the player took damage
            GameManager.Instance.savedHP = currentHP;
    }

    private void Die()
    {
        // When health = 0
        OnDeath.Invoke();
    }


    public void SetCurrentHP(int value)
    {
        currentHP = value;
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
