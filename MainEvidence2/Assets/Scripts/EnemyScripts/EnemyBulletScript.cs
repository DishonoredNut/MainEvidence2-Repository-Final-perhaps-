using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public int damageAmount = 10; // The damage amount that this bullet inflicts

    // OnTriggerEnter is called when the bullet collides with another object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the collider is tagged as "Player"
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Inflict damage to the player's health
                playerHealth.TakeDamage(damageAmount);
            }

            // Destroy the bullet when it hits the player
            Destroy(gameObject);
        }
    }
}
