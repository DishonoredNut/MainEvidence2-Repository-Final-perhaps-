using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyHealth = 50;
    public int Damage = 50;
    public float EnemyAttackRange = 30f;

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
            // Enemy is defeated;
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        FireProjectile();
    }
}
