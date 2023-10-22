using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BunnyHopController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;
    public float stepOffset = 0.3f;
    public float centerOfMassOffset = 0.9f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.stepOffset = stepOffset; // Adjust the step offset for better stability
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        // Apply gravity to the player
        if (isGrounded)
        {
            moveDirection.y = 0f;
        }
        moveDirection.y -= gravity * Time.deltaTime;

        // Get player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 move = (forward * verticalInput + right * horizontalInput).normalized;

        // Apply movement input
        moveDirection.x = move.x * moveSpeed;
        moveDirection.z = move.z * moveSpeed;

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        // Apply movement to the CharacterController
        controller.Move(moveDirection * Time.deltaTime);

        // Adjust the center of mass for better stability
        controller.center = new Vector3(0, -centerOfMassOffset, 0);
    }
}
