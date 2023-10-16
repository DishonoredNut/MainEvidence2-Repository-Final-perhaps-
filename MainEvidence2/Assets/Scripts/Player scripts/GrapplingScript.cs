using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingScript : MonoBehaviour
{
    [Header("Refs")]
    private PlayerController pm;
    public Transform cam;
    public Transform grappleTip;
    public LayerMask whatToGrapple;
    public LineRenderer lr; 

    [Header("Grappling")]
    public float maxGrapple;
    public float grappleTime; // Delay
    private Vector3 pointToGrapple;

    [Header("Cooldown")]
    public float grapplingCooldown;
    private float grapplingTimer;

    private bool grappling;

    private PlayerNewController controls; // Define the controls variable

   private void GrapplingBoi()
{
    if (controls.GamePadMovement.Grappel.triggered) // Check  input action for grapple
    {
        Debug.Log("Grapple input triggered"); // Add a debug log for testing
        StartGrappling();
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

    private void Start()
    {
        pm = GetComponent<PlayerController>();
    }

   private void Update()
    {
        GrapplingBoi();

        if (grappleTime > 0)
        grapplingTimer -= Time.deltaTime;
    }
    private void LateUpdate()
    {
        if(grappling)
        lr.SetPosition(0,grappleTip.position); 
    }


    private void StartGrappling()
    {
        if (grapplingTimer > 0) return;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrapple, whatToGrapple))
        {
            pointToGrapple = hit.point;
            Invoke(nameof(BeginGrappling), grappleTime);
        }
        else
        {
            pointToGrapple = cam.position + cam.forward * maxGrapple;
            Invoke(nameof(StopGrappling), grappleTime);
        }
        lr.enabled = true; 
        lr.SetPosition(1, pointToGrapple); 
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
    }
}
