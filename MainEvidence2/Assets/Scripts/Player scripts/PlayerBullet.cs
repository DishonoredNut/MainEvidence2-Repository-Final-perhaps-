using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 50; // Set the damage value in the Inspector
    public float despawnTime = 2.0f; // Set the despawn time in the Inspector

    void Start()
    {
        // Start a countdown to despawn the bullet
        StartCoroutine(DespawnAfterDelay());
    }

    void Update()
    {
        // This script could also handle bullet movement, if needed.
        // You can add code here to move the bullet.
    }

    // OnTriggerEnter is called when the bullet collides with another object
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Check if the colliding object is tagged as "Enemy"
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Deal damage to the enemy
                enemy.TakeDamage(damage);
            }

            // Destroy the bullet after damaging an enemy
            Destroy(gameObject);
        }
    }

    // Coroutine to despawn the bullet after a specified time
    private IEnumerator DespawnAfterDelay()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
