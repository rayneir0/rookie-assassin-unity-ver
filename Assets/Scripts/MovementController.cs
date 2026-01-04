using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movementInput;
    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        // Get Left and Right Movement as well as Up and Down Movement
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        
        // Flip Sprite in correct direction
        if(movementInput.x > 0)
            spriteRenderer.flipX = false;
        else if (movementInput.x < 0) 
            spriteRenderer.flipX = true;
            
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.velocity = movementInput * moveSpeed;
    }
}
