using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class LeverController : MonoBehaviour
{
    public Animator animator;
    public TrapController[] trapControllers;
    public GameObject[] trapObjects;
    public TextMeshProUGUI cooldownText;

    public float activeTime = 3f; // How long trap stays active
    public float cooldownTime = 2f; // How long before the lever can be used again
    public float trapInterval = 0.5f; // A delay in the traps
    private bool playerInRange = false;
    private bool isActive = false;
    private bool isOnCooldown = false;


    void Start()
    {
        if(cooldownText != null)
            // Don't show the cooldown text before the lever has been pressed
            cooldownText.gameObject.SetActive(false); 
    }
    void Update()
    {
        if (playerInRange && !isOnCooldown && Input.GetKeyDown(KeyCode.F))
        {
            // When the F Key has been pressed
            StartCoroutine(ActivateSequence());
        }
    }

    private IEnumerator ActivateSequence()
    {
        isOnCooldown = true;   // disable lever

        // Activate traps
        ActivateMainTrap();
        Debug.Log("Trap Activated");

        // Activate traps one after the other at a certain trap interval 
        for (int i = 0; i < trapControllers.Length; i++)
        {
            trapControllers[i].ActivateTrap(trapObjects[i]);
            yield return new WaitForSeconds(trapInterval);
        }

        Debug.Log("All traps activated");

        // Wait for active duration
        yield return new WaitForSeconds(activeTime);

        // Deactivate all traps at a certain trap interval
        DeactivateMainTrap();
        for (int i = 0; i < trapControllers.Length; i++)
        {
            trapControllers[i].DeactivateTrap(trapObjects[i]);
            yield return new WaitForSeconds(trapInterval);  
        }

        Debug.Log("Trap Deactivated"); 

        // Wait before lever can be used again 
        // Show the cooldown now
        if(cooldownText != null)
            cooldownText.gameObject.SetActive(true);
       
        float timer = cooldownTime;

        // Counts down
        while (timer > 0)
        {
            cooldownText.text = Mathf.Ceil(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }

        // Hide cooldown when cooldown is over
        if(cooldownText != null)
            cooldownText.gameObject.SetActive(false);
       
        isOnCooldown = false;
        Debug.Log("Lever Ready Again");
    }
    
    // Main Trap refers to first trap that activates in a sequence
    private void ActivateMainTrap()
    {
        animator.SetTrigger("Activate");
        trapControllers[0].ActivateTrap(trapObjects[0]);
        isActive = true;
       
        
    }
    private void DeactivateMainTrap()
    {
        animator.SetTrigger("Deactivate");
        trapControllers[0].DeactivateTrap(trapObjects[0]);
        isActive = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}
