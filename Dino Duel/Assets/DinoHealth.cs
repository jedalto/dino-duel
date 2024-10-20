using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoHealth : MonoBehaviour
{
    public int maxHealth = 100;       // Max health of the dino
    public int currentHealth;        // Dino's current health
    public float sliderSpeed = .1f;

    public Slider healthSlider;       // UI element to display health

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;      // Set starting health to max
        UpdateHealthUI();               // Initialize health display
        SetSliderMaxValue();            // Initialize health bar value
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;      // Reduce health by damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Prevent health from going below 0

        UpdateHealthUI();             // Update health bar

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
            healthSlider.value = Mathf.MoveTowards(healthSlider.value, currentHealth, sliderSpeed);
        }
        else
        {
            Debug.LogWarning("Health slider not assigned!");
        }
    }

    // Handle the dino's death
    void DinoDeath()
    {
        // Logic for dino death, like playing an animation or respawning
        Debug.Log("Dino is dead!");
    }

    // Set maximum value of health bar to max dino health
    private void SetSliderMaxValue()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;  // Set the slider's max value
            healthSlider.value = currentHealth;  // Update the slider's value
        }
    }

    // will be able to change maximum health of dinos in future (if we implement powerups, etc)
    public void SetMaxHealth(int newMaxHealth)
    {
        int oldHealth = currentHealth;
        maxHealth = newMaxHealth;       // Update max health
        SetSliderMaxValue();             // Update the slider's max value
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure current health is within new limits
        UpdateHealthUI();                // Update health display
    }
}
