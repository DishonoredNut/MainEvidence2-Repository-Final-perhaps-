using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Enemy
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed = 10f;
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target != null && Vector3.Distance(transform.position, target.position) <= EnemyAttackRange)
        {
            FireProjectile();
        }
    }

    public override void FireProjectile()
{
    if (target != null)
    {
        // Calculate the direction to the target
        Vector3 fireDirection = (target.position - firePoint.position).normalized;
        
        // Instantiate the projectile at the firepoint position
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        // Get the Rigidbody of the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        
        // Set the velocity based on the direction and speed
        rb.velocity = fireDirection * projectileSpeed;
         Debug.Log("Firing Projectile");
    }
}

}
