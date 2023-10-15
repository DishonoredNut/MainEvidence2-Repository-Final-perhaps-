using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int PlayerHealth = 100;
    public float MoveSpeed = 10.0f;
    public float JumpHeight = 10.0f;
    public float RotationSpeed = 5.5f;
    private PlayerNewController controls;
    private Vector3 velocity;
    public float grav;
    private UnityEngine.Vector2 move;
    private CharacterController controller;
    public Transform ground;
    public float distanceFromGround = 0.4f;
    public LayerMask Ground;
    private bool Grounded;

    void Awake()
    {
        controls = new PlayerNewController();
        controller = GetComponent<CharacterController();
    }

    void Update()
    {
        Gravity();
        PlayerMovement();
        Jump();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
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
        move = controls.GamePadMovement.Movement.ReadValue<UnityEngine.Vector2>();

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

    // Deduct health when hit by an object with the "EnemyBullet" tag
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            DeductHealth(20);
        }
    }

    // Deduct health when hit by a raycast called "EnemyMelee"
    public void DeductHealth(int damage)
    {
        PlayerHealth -= damage;
        if (PlayerHealth <= 0)
        {
            // Reload the scene or perform a game over action
            ReloadScene();
        }
    }

    // Reload the current scene
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
