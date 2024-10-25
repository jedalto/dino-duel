using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoHealth : MonoBehaviour
{
    public int maxHealth = 100;         // Max health of the dino
    public int currentHealth;           // Dino's current health
    public int playerNumber;
    public float sliderSpeed = 50f;
    public Slider healthSlider;         //UI element to display dino health
    private float targetSliderValue;

    Animator animator;

    private bool isInvulnerable = false;
    private dinoMovement movementScript;

    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip death;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        targetSliderValue = maxHealth;  // Initialize target value first

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        animator = GetComponent<Animator>();
        movementScript = GetComponent<dinoMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        // check if invulnerable before taking damage
        if (isInvulnerable) return;

        currentHealth -= damage;      // Reduce health by damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Prevent health from going below 0
        targetSliderValue = currentHealth;  // set target value for slider

        AudioSource.PlayClipAtPoint(damageSound, transform.position, 1f);
        if (currentHealth <= 0)
        {
            DinoDeath();              // Handle dino death
        }
    }

    // Update the health UI slider
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            // smoothly move the slider toward the target value
            // hopefully this way is more consistent with deltaTime
            healthSlider.value = Mathf.MoveTowards(healthSlider.value, targetSliderValue, sliderSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Health slider not assigned!");
        }
    }

    // Handle the dino's death
    void DinoDeath()
    {
        // Logic for dino death, playing an animation
        animator.SetTrigger("Death");

        AudioSource.PlayClipAtPoint(death, transform.position, 1f);
        GameManager.Instance.PlayerDied(playerNumber); // Pass player number (1 or 2)
        Debug.Log("Dino is dead!");

        // Disable movement if dead
        if (movementScript != null)
        {
            // Set velocity to zero to stop any ongoing movement
            if (movementScript.rb != null)
            {
                movementScript.rb.velocity = Vector2.zero;
            }

            // Disable movement script
            movementScript.enabled = false;
        }
    }

    public void SetInvulnerable(bool invulnerable)
    {
        isInvulnerable = invulnerable;
    }

    // Set maximum value of health bar to max dino health
    private void SetSliderMaxValue()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;  // Set the slider's max value
            healthSlider.value = currentHealth;  // Update the slider's value
            targetSliderValue = currentHealth;  // Init target value
        }
    }

    // will be able to change maximum health of dinos in future (if we implement powerups, etc)
    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;       // Update max health
        SetSliderMaxValue();             // Update the slider's max value
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure current health is within new limits
        targetSliderValue = currentHealth;  // Update target value
    }
}
