using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawler : Enemy
{
    public float raycastRadius = 2f; // Radius of the raycast
    public int damageAmount = 10; // Damage amount to apply to the player
    public float damageCooldown = 2f; // Cooldown time between attacks
    private float lastDamageTime; // Time of the last damage dealt

    // Update is called once per frame
    void Update()
    {
        // Cast a raycast to detect the player within the specified radius
        RaycastHit hit;
        if (Time.time - lastDamageTime >= damageCooldown && Physics.SphereCast(transform.position, raycastRadius, transform.forward, out hit))
        {
            // Check if the raycast hits the player
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Use the TakeDamage method
                Debug.Log("Player hit by Brawler for " + damageAmount + " damage.");
                lastDamageTime = Time.time; // Update the last damage time
            }
        }
    }
}
