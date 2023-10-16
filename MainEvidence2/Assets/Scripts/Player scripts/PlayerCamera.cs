using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerCamera : MonoBehaviour
{
    private PlayerNewController controls; 
    private float MouseSense = 100f; // Mouse sensitivity
    private Vector2 mouseLook; 
    private float xRotation = 0f; 
    public Transform player;
    
    void Awake()
{
    player = transform.parent; 
    controls = new PlayerNewController(); 
    Cursor.lockState = CursorLockMode.Locked;

    // Rotate the camera by 180 degrees to face the correct direction cuase weird Sheise
    transform.Rotate(Vector3.up, 180);
}

    void Update() //calls look function
    {
        Look(); 
    }

    private void Look()
    {
        mouseLook = controls.GamePadMovement.Look.ReadValue<Vector2>(); 
        float mouseX = mouseLook.x * MouseSense * Time.deltaTime; 
        float mouseY = mouseLook.y * MouseSense * Time.deltaTime; 

        // Clamps
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90); 

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); 
        player.Rotate(Vector3.up * mouseX); 
    }

    private void OnEnable()
    {
        controls.Enable(); 
    }

    private void OnDisable()
    {
        controls.Disable(); 
    }
}
