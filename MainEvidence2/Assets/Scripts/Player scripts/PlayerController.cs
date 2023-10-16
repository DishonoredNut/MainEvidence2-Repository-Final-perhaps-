using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    public float JumpHeight = 10.0f;
    public float RotationSpeed = 5.5f;
    private PlayerNewController controls;
    private Vector3 velocity;
    public float grav;
    private UnityEngine.Vector2 move; // Specify UnityEngine namespace
    private CharacterController controller;
    public Transform ground;
    public float distanceFromGround = 0.4f;
    public LayerMask Ground;
    private bool Grounded;
    public float teleportHeightThreshold = -10.0f; // Threshold height for teleportation
    public Transform teleportLocation; // Location to teleport the player to
    public GameObject objectToTeleport; // Reference to the object to teleport

    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firepoint; // Reference to the firepoint transform
    public float fireRate = 0.5f; // Firing rate in bullets per second
    private float lastFireTime; // Time of the last bullet fired
    public AudioSource fireSound; // Reference to an AudioSource for the firing sound
    public float bulletSpeed = 10f; // Speed of the fired bullet

    void Awake()
    {
        controls = new PlayerNewController();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        Gravity();
        PlayerMovement();
        Jump();
        CheckTeleport();
        FireDaBullet(); // Call the fire bullet method
    }

    private void Gravity()
    {
        Grounded = Physics.CheckSphere(ground.position, distanceFromGround, Ground);
        if (Grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += grav * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        move = controls.GamePadMovement.Movement.ReadValue<UnityEngine.Vector2>(); // Specify UnityEngine namespace

        // Revert the forward and backward controls
        Vector3 movement = (move.y * transform.forward.normalized) + (move.x * transform.right.normalized);

        controller.Move(movement * MoveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (controls.GamePadMovement.Jump.triggered)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * grav);
        }
    }

    private void FireDaBullet()
    {
        if (controls.GamePadMovement.Fire.triggered && projectilePrefab != null && firepoint != null)
        {
            // Check if enough time has passed to fire another bullet
            if (Time.time - lastFireTime >= 1.0f / fireRate)
            {
                // Create the projectile at the firepoint's position and rotation
                GameObject bullet = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);

                // Access the bullet's Rigidbody component
                Rigidbody rb = bullet.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Propel the bullet in the direction the firepoint is facing
                    rb.velocity = firepoint.forward.normalized * bulletSpeed;
                }

                // Play the firing sound
                if (fireSound != null)
                {
                    fireSound.Play();
                }

                // Update the last fire time
                lastFireTime = Time.time;
            }
        }
    }

    private void CheckTeleport()
    {
        if (transform.position.y <= teleportHeightThreshold)
        {
            // Teleport the specified object to the teleport location
            objectToTeleport.transform.position = teleportLocation.position;

            // Optionally, reset the player's position as well.
            controller.enabled = false; // Disable the CharacterController temporarily
            transform.position = teleportLocation.position;
            velocity = Vector3.zero; // Reset the player's velocity

            // Teleport all child objects
            foreach (Transform child in objectToTeleport.transform)
            {
                child.position = teleportLocation.position;
            }

            controller.enabled = true; // Re-enable the CharacterController
        }
    }
}
