using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dinoMovement : MonoBehaviour
{
    public Rigidbody2D rb;              // ref to RigidBody2D component
    public float moveSpeed = 5f;        // Speed at which the dino moves
    public float jumpForce = 10f;       // Force applied for jumping
    private bool isGrounded;            // To check if the dino is on the ground

    // move keys for each dino
    public KeyCode moveLeftKey;  // Default for player 2 (WAD keys)
    public KeyCode moveRightKey;
    public KeyCode jumpKey;

    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private bool movingUp = false;
    private bool facingLeft = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDino();
    }

    void MoveDino()
    {
        float moveDirection;
        if (facingLeft)
            moveDirection = -1f;
        else moveDirection = 0f;

        // Handle movement with LeftArrow and RightArrow keys
        if (Input.GetKey(moveLeftKey) && canMoveLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);  // Move left
            moveDirection = -1f;
        }
        else if (Input.GetKey(moveRightKey) && canMoveRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);  // Move right
            moveDirection = 1f;
        }
        else
        {
            // If no key is pressed, stop horizontal movement
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // flip sprite based on movement direction
        if (moveDirection > 0 && facingLeft)
        {
            FlipSprite();
        }
        else if (moveDirection < 0 && !facingLeft)
        {
            FlipSprite();
        }

        // Jumping
        if (isGrounded && Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Jump when the Up Arrow is pressed and dino is grounded
        if (Input.GetKeyDown(jumpKey) && isGrounded && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Apply jump force
        }

        if (rb.velocity.y > 0)
        {
            movingUp = true;
        }
        else
        {
            movingUp = false;
        }
    }

    // Check if the dino is touching the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object the dino collided with is tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // Dino is on the ground
        }

        // Check if dino collides with wall from left, then block left movement
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Check which side of the dino collided with wall
            if (collision.contacts[0].normal.x > 0)
            {
                // Hit from left side of dino, block left movement
                canMoveLeft = false;
            } else if (collision.contacts[0].normal.x < 0)
            {
                // Hit from right side of dino, block right movement
                canMoveRight = false;
            }

            if (collision.gameObject.CompareTag("Dino"))
            {
                if (transform.position.y > collision.transform.position.y)
                {
                    isGrounded = true;  // Treat the other dino as ground
                }
            }
        }

        // Check if dino is jumping up through platform collider or landing on top
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;  // reset grounded status so player can jump off platform
        }
    }

    // Check if the dino is no longer touching the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;  // Dino is not on the ground
        }

        // if dino stops colliding with wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            canMoveRight = true;
            canMoveLeft = true;
        }
    }

    // Flip sprite based on movement direction
    void FlipSprite()
    {
        // Flip the localScale.x value to mirror the sprite
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
