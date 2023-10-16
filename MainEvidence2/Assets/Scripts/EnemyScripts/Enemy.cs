using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX; // Import the Visual Effect namespace

public class Enemy : MonoBehaviour
{
    public int EnemyHealth = 50;
    public int Damage = 50;
    public float EnemyAttackRange = 30f;
    public VisualEffect explosionVFX; // Reference to the VFX Graph in the Inspector
    public GameObject explosionPoint; // Reference to the explosion point in the Inspector
    public float explosionDuration = 1.0f; // Duration of the explosion VFX

    public Transform target; // Reference to the target in the Inspector

    // Virtual method for firing the projectile
    public virtual void FireProjectile()
    {
        // Check if the target is within attack range
        if (target != null && Vector3.Distance(transform.position, target.position) <= EnemyAttackRange)
        {
            // Implement projectile firing logic here
            Debug.Log("Firing at the target!");
        }
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        EnemyHealth -= damage;

        if (EnemyHealth <= 0)
        {
            // Play the explosion VFX from the explosion point
            PlayExplosionEffect();

            // Delay for the duration of the explosion effect, then destroy the enemy
            StartCoroutine(DestroyAfterExplosion());

            // Optionally, disable any other components or scripts here if needed
        }
    }

    // Play the explosion VFX from the explosion point
    private void PlayExplosionEffect()
    {
        if (explosionVFX != null && explosionPoint != null)
        {
            // Set the position of the explosion point
            explosionVFX.transform.position = explosionPoint.transform.position;

            // Play the explosion effect
            explosionVFX.Play();
        }
    }

    // Destroy the enemy after the explosion effect duration
    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        FireProjectile();
    }
}
