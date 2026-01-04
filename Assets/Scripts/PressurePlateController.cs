using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public Animator animator;
    public TrapController[] trapControllers;
    public GameObject[] trapObjects;
    private bool isActive = false;
    
    private void ActivateMainTrap()
    {
        if (isActive) return; // prevent double activation
            isActive = true;
        
        animator.SetTrigger("Activate");
        // List of trap controllers to hold trap objects
        trapControllers[0].ActivateTrap(trapObjects[0]);
        isActive = true;

        // Activate in sequence
        for (int i = 0; i < trapControllers.Length; i++)
        {
            if (trapControllers[i] != null && trapObjects[i] != null)
                trapControllers[i].ActivateTrap(trapObjects[i]);
        }
        
    }
    private void DeactivateMainTrap()
    {
        if (!isActive) return; // prevent double deactivation
            isActive = false;

        animator.SetTrigger("Deactivate");
        // List of trap controllers to hold trap objects
        trapControllers[0].DeactivateTrap(trapObjects[0]);
        isActive = false;

        // Deactivate in sequence
          for (int i = 0; i < trapControllers.Length; i++)
        {
            if (trapControllers[i] != null && trapObjects[i] != null)
                trapControllers[i].DeactivateTrap(trapObjects[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            ActivateMainTrap();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            DeactivateMainTrap();
    }
}
