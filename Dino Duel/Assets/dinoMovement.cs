using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoMovement : MonoBehaviour
{
    public Rigidbody2D rb;              // ref to RigidBody2D component
    public float moveSpeed = 5f;        // Speed at which the dino moves
    public float jumpForce = 10f;       // Force applied for jumping
    private bool isGrounded;            // To check if the dino is on the ground
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handle movement with LeftArrow and RightArrow keys
        if (Input.GetKey(KeyCode.LeftArrow) && canMoveLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);  // Move left
        }
        else if (Input.GetKey(KeyCode.RightArrow) && canMoveRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);  // Move right
        }
        else
        {
            // If no key is pressed, stop horizontal movement
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Jump when the Up Arrow is pressed and dino is grounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Apply jump force
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
        }
    }

    // Check if the dino is no longer touching the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
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
}
