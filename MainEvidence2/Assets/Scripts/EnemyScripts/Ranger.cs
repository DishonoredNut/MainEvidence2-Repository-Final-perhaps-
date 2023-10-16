using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Enemy
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed = 10f;
    public Transform firePoint;
    public float fireRate = 1.0f; // Bullets per second

    private float fireCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 1.0f / fireRate; // Set the initial cooldown based on the fire rate
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= EnemyAttackRange)
        {
            // Check if the fire cooldown has called
            if (fireCooldown <= 0f)
            {
                FireProjectile();
                fireCooldown = 1.0f / fireRate; // Reset cooldwn
            }
            else
            {
                fireCooldown -= Time.deltaTime; // Reduce the cooldwn timer
            }
        }
    }

    public override void FireProjectile()
    {
        if (target != null)
        {
            // Calculates the direction to the target
            Vector3 fireDirection = (target.position - firePoint.position).normalized;
            
            // Make the projectile at the firepoint position
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            
            // Get the Rigidbody of the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            // Set the velocity based on the direction and speed
            rb.velocity = fireDirection * projectileSpeed;
            Debug.Log("Firing Projectile");
        }
    }
}
