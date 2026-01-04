using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent; // Gives each enemy an AI NavMeshAgent
    private SpriteRenderer spriteRenderer; // Renders the sprites
    private Health health;
    public Transform[] waypoints; // Waypoints for the enemies to know where to go
    public float speed = 2f;
    public int damageAmount = 1;
    private int currWaypoint = 0; // Keeping track of current way point
   

    void Awake()
    {
        health = GetComponent<Health>(); // Gives enemy health
        health.OnDeath.AddListener(OnEnemyDeath); // Adds a death listener
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // For stiffer movement
        agent.stoppingDistance = 0;
        agent.speed = speed;  // Keep at your desired constant speed
        // Set acceleration and angular speed to a really high value
        agent.acceleration = 9999f;      
        agent.angularSpeed = 9999f;      
        agent.updateRotation = false;
        agent.stoppingDistance = 0;

        // If there are waypoints, set the next destination to the first way point
        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[0].position);
        
        // If an enemy exists register it to the enemy manager
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.RegisterEnemy();
        else
            Debug.LogError("EnemyManager instance not found!");
    }
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            currWaypoint = (currWaypoint + 1) % waypoints.Length; // Loop through the waypoints 
            agent.SetDestination(waypoints[currWaypoint].position);
        }
        
        // Flip the sprite
        Vector3 velocity = agent.velocity;
        if (velocity.x > 0.01f)
            spriteRenderer.flipX = false; 
        else if (velocity.x < -0.01f)
            spriteRenderer.flipX = true; 
    }
    private void OnEnemyDeath()
    {
        Debug.Log("Enemy died: " + gameObject.name);
        EnemyManager.Instance.UnregisterEnemy(); // If died, unregister enemy
        gameObject.SetActive(false);
    }
    
     private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.CompareTag("Player"))
        {
            // Get the player's Health component
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Deal damage immediately
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Player hit by enemy! Damage: " + damageAmount);
            }
        }
    }
}
