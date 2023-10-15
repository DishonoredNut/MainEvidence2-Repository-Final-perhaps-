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

    private void CheckTeleport()
    {
        if (transform.position.y <= teleportHeightThreshold)
        {
            controller.enabled = false; // Disable the CharacterController temporarily
            transform.position = teleportLocation.position;
            velocity = Vector3.zero; // Reset the player's velocity
            controller.enabled = true; // Re-enable the CharacterController
        }
    }
}
