using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // Initial player health
    public int currentHealth; // Current player health

    private void Start()
    {
        // Initialize the player's health to the starting value
        currentHealth = startingHealth;
    }

    private void Update()
    {
        // Check if the player's health reaches zero or below
        if (currentHealth <= 0)
        {
            // destroy the player object
            Debug.Log("Player is dead! Deleting the player object.");
            Destroy(gameObject); // Destroy the player object
        }
    }

    // decreases player health
    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            currentHealth -= damage;
            Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);
        }
    }
}
