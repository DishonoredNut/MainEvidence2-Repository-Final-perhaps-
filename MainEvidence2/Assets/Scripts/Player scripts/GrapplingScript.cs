using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingScript : MonoBehaviour
{
    [Header("Refs")]
    public Transform cam;
    public Transform grappleTip;
    public LayerMask whatToGrapple;
    public LineRenderer lr;
    public AudioSource grapplingSound; // Reference to the grappling sound

    [Header("Grappling")]
    public float maxGrapple;
    public float grappleTime; // Delay
    public float grappleSpeed = 10.0f; // Grapple speed
    private Vector3 pointToGrapple;

    [Header("Cooldown")]
    public float grapplingCooldown;
    private float grapplingTimer;
    private bool grappling;
    private bool isGrappling;

    private PlayerNewController controls; // Define the controls variable

    private void GrapplingBoi()
    {
        if (controls.GamePadMovement.Grappel.triggered) // Check input action for grapple
        {
            if (isGrappling)
            {
                StopGrappling();
            }
            else
            {
                StartGrappling();
                PlayGrapplingSound(); // Play the grappling sound
            }
        }
    }

    private void Awake()
    {
        controls = new PlayerNewController(); // Initialize the controls
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
        GrapplingBoi();

        if (grappleTime > 0)
            grapplingTimer -= Time.deltaTime;

        // Simulate player movement towards the grapple point
        if (grappling)
        {
            float step = grappleSpeed * Time.deltaTime; // Use the grappleSpeed
            transform.position = Vector3.MoveTowards(transform.position, pointToGrapple, step);
        }
    }

    private void LateUpdate()
    {
        if (grappling)
            lr.SetPosition(0, grappleTip.position);
    }

    private void StartGrappling()
    {
        if (grapplingTimer > 0) return;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrapple, whatToGrapple))
        {
            pointToGrapple = hit.point;
            Invoke(nameof(BeginGrappling), grappleTime);

            lr.enabled = true;
            lr.SetPosition(0, grappleTip.position);
            lr.SetPosition(1, pointToGrapple);
        }
        else
        {
            pointToGrapple = cam.position + cam.forward * maxGrapple;
            Invoke(nameof(StopGrappling), grappleTime);
        }

        grappling = true;
        isGrappling = true;
    }

    private void BeginGrappling()
    {
        // Add logic for what happens when the player starts grappling
    }

    private void StopGrappling()
    {
        grappling = false;
        grapplingTimer = grapplingCooldown;
        lr.enabled = false;
        isGrappling = false;
    }

    private void PlayGrapplingSound()
    {
        if (grapplingSound != null)
        {
            grapplingSound.Play();
        }
    }
}
