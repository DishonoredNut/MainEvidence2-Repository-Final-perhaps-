using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 10.0f; // Movespeed
    public float JumpHeight = 10.0f;
    public float grav; // Sets up a gravity function so I don't have to use a rigidbody
    private PlayerNewController controls;
    private Vector3 velocity;
    private UnityEngine.Vector2 move;
    private CharacterController controller;
    public Transform ground;
    public float distanceFromGround = 0.4f; // Elevates above ground by this
    public LayerMask Ground;
    private bool Grounded;
    public float teleportHeightThreshold = -10.0f; // Threshold height for teleportation
    public Transform teleportLocation; // Location to teleport the player
    public GameObject objectToTeleport;

    public GameObject projectilePrefab;
    public Transform firepoint;
    public float fireRate = 0.5f;
    private float lastFireTime;
    public AudioSource fireSound;
    public float bulletSpeed = 10f;

    public VisualEffect bulletVFX;
    public Transform vfxSpawnPoint;

    void Awake()
    {
        controls = new PlayerNewController();
        controller = GetComponent<CharacterController>();
        lastFireTime = 0f;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        ProcessInput();
        Gravity();
        PlayerMovement();
        Jump();
        CheckTeleport();
        FireDaBullet();
    }

    private void ProcessInput()
    {
        move = controls.GamePadMovement.Movement.ReadValue<UnityEngine.Vector2>();
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
        Vector3 movement = (move.y * transform.forward.normalized) + (move.x * transform.right.normalized);
        controller.Move(movement * MoveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceFromGround + 0.1f, Ground);

        if (isGrounded && controls.GamePadMovement.Jump.triggered)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * grav);
        }
    }

    private void FireDaBullet()
    {
        if (controls.GamePadMovement.Fire.triggered && projectilePrefab != null && firepoint != null)
        {
            if (Time.time - lastFireTime >= 1.0f / fireRate)
            {
                GameObject bullet = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = firepoint.forward.normalized * bulletSpeed;
                }

                lastFireTime = Time.time + 1.0f / fireRate;

                if (fireSound != null)
                {
                    fireSound.Play();
                }

                InstantiateBulletVFX(vfxSpawnPoint.position);
            }
        }
    }

    private void InstantiateBulletVFX(Vector3 position)
    {
        if (bulletVFX != null)
        {
            VisualEffect vfx = Instantiate(bulletVFX, position, Quaternion.identity);
            vfx.Play();
        }
    }

    private void CheckTeleport()
    {
        if (transform.position.y <= teleportHeightThreshold)
        {
            objectToTeleport.transform.position = teleportLocation.position;

            controller.enabled = false;
            transform.position = teleportLocation.position;
            velocity = Vector3.zero;

            foreach (Transform child in objectToTeleport.transform)
            {
                child.position = teleportLocation.position;
            }

            controller.enabled = true;
        }
    }
}
