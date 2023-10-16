using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour //This script didn't end up seing any use cause the player just kept going through it// DEFUNCT
{
    public Transform targetPosition; // Reference to the target position in the Inspector
    public GameObject objectToTarget; // Reference to the game object to target

    // This method is called when another object enters the collider of the checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move the specified game object to the target position
            objectToTarget.transform.position = targetPosition.position;
        }
    }

    
}
