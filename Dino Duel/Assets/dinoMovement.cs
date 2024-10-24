using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms.Impl;

public class dinoMovement : MonoBehaviour
{
    public Rigidbody2D rb;              // ref to RigidBody2D component
    public float moveSpeed = 5f;        // Speed at which the dino moves
    public float jumpForce = 10f;       // Force applied for jumping
    public bool isGrounded = false;            // To check if the dino is on the ground

    // move keys for each dino
    public KeyCode moveLeftKey;     // key to move left
    public KeyCode moveRightKey;    // key to move right
    public KeyCode jumpKey;         // key to jump
    public KeyCode attackKey;       // key to shoot

    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private bool movingUp = false;
    public bool facingLeft = true;

    public GameObject laserBulletPrefab;   // Reference to the bullet prefab
    public Transform gunPoint;             // Where the bullet should be spawned
    public float bulletSpeed = 10f;        // Speed of the bullet

    Animator animator;
    bool isRunning = false;
    bool isJumping = false;

    // variables used for raycast --> makes isGrounded more accurate than just using collisions
    public LayerMask groundLayer;           // specifies ground layers
    public Transform groundCheck;           // groundcheck point
    public float groundCheckRadius = 0.1f;  // radius for groundchecking with raycast

    // pause variables
    public bool isPaused = false;
    Text pause;
    Text quit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        pause = GameObject.Find("Pause").GetComponent<Text>();
        quit = GameObject.Find("Quit").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveDino();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // animation conditions:
        animator.SetBool("IsRunning", isRunning);
        
        animator.SetBool("Jump", (!isGrounded));
    }

    void MoveDino()
    {
        float moveDirection = 0f;

        if (Input.GetKey(moveLeftKey) && canMoveLeft && !isPaused)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);  // Move left
            moveDirection = -1f;
            isRunning = true;
        }
        else if (Input.GetKey(moveRightKey) && canMoveRight && !isPaused)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);  // Move right
            moveDirection = 1f;
            isRunning = true;
        }
        else
        {
            // If no key is pressed, stop horizontal movement
            rb.velocity = new Vector2(0, rb.velocity.y);
            isRunning = false;
        }

        // flip sprite based on movement direction
        if (moveDirection > 0 && facingLeft && !isPaused)
        {
            FlipSprite();
        }
        else if (moveDirection < 0 && !facingLeft && !isPaused)
        {
            FlipSprite();
        }

        // Jump when the jump key is pressed and dino is grounded
        if (Input.GetKeyDown(jumpKey) && isGrounded && !isPaused)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Apply jump force
            isGrounded = false;
        }

        // Handle attack input
        if (Input.GetKeyDown(attackKey) && !isPaused)
        {
            Attack();
        }

        // Pause Game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused)
            {
                isPaused = false;
                pause.text = "";
                quit.text = "";
                // Debug.Log(isPaused);
            }
            else
            {
                isPaused = true;
                pause.text = "Paused";
                quit.text = "Esc to Quit";
                // Debug.Log(isPaused);
            }
        }

        // Quit Game
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            SceneManager.LoadScene("MenuScene");
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

        // Allow dinos to jump on each other's heads
        if (collision.gameObject.CompareTag("Dino"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                isGrounded = true;  // Treat the other dino as ground
            }
        } 

        // Check if dino is standing on a platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;  // reset grounded status so player can jump off platform
        }

        //Check if dino is standing on a rock
        if (collision.gameObject.CompareTag("Rock"))
        {
            isGrounded = true;  // reset grounded status so player can jump off platform
        }
    }

    // Check if the dino is no longer touching the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag ("Rock"))
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

    // attack method (just laser gun attack for now)
    void Attack()
    {
        // Instantiate bullet at gun's position and direction
        GameObject bullet = Instantiate(laserBulletPrefab, gunPoint.position, gunPoint.rotation);

        // Get Rigidbody2D component from the bullet and set its velocity
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Check if dino is facing left or right and apply velocity accordingly
        if (facingLeft)
        {
            bulletRb.velocity = -transform.right * bulletSpeed;  // Move left
        }
        else
        {
            bulletRb.velocity = transform.right * bulletSpeed;  // Move right
        }

        // Destroy bullet after 3 seconds to clean up
        Destroy(bullet, 2f);
    }
}
