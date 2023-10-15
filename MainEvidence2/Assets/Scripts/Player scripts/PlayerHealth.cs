using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // Initial player health
    private int currentHealth; // Current player health

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
            // Player is dead, you can add game over logic here
            Debug.Log("Player is dead!");
        }
    }

    // Function to decrease player's health
    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            currentHealth -= damage;
            Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);
        }
    }

    // Detect and handle collisions with objects tagged as "EnemyAttack"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            // You can adjust the damage amount as needed
            int damageAmount = 10;
            TakeDamage(damageAmount);
        }
    }
}
