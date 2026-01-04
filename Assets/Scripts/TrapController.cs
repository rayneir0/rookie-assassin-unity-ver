using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public Animator animator;
    private bool playerInRange = false;
    private bool enemyInRange = false;
    private bool isActive = false;
    public int damagePerSecond = 1;

    // List of All Health components currently touching the trap
    private List<Health> targetsInside = new List<Health>();
    // Handles continuous damage
    private Coroutine damageRoutine;

    
    public void ActivateTrap(GameObject trap)
    {
        isActive = true;
        animator.SetTrigger("Activate");
        Debug.Log("Trap Activated: " + trap.name);
        
        if (damageRoutine == null)
            damageRoutine = StartCoroutine(DamageLoop());
    }
    public void DeactivateTrap(GameObject trap)
    {
        isActive = false;
        animator.SetTrigger("Deactivate");
        Debug.Log("Trap Activated: " + trap.name);

        // Stop damage coroutine when trap is off
        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the player or enemy is on the trap (Trap does damage to both)
        if (collision.CompareTag("Player"))
            playerInRange = true;
        if (collision.CompareTag("Enemy"))
            enemyInRange = true;

        Health hp = collision.GetComponent<Health>();
        if (hp != null && !targetsInside.Contains(hp))
            targetsInside.Add(hp);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {   
        // Checks if the player or enemy is on the trap (Trap does damage to both)
        if (collision.CompareTag("Player"))
            playerInRange = false;
        if (collision.CompareTag("Enemy"))
            enemyInRange = false;

        Health hp = collision.GetComponent<Health>();
        if (hp != null)
            targetsInside.Remove(hp);
    }

    private IEnumerator DamageLoop()
    {
        while (true)
        {
            if (isActive)
                {    
                    for (int i = targetsInside.Count - 1; i >= 0; i--)
            {
                    Health target = targetsInside[i];

                    // Remove missing or destroyed objects
                    if (target == null || target.gameObject == null)
                    {
                        targetsInside.RemoveAt(i);
                        continue;
                    }

                    target.TakeDamage(damagePerSecond);
            }
                }
            yield return new WaitForSeconds(1f); // damage every second
            
        }
    }

}
