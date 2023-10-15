using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Transform targetPosition; // Reference to the target position in the Inspector
    public GameObject objectToMove; // Reference to the object to be moved in the Inspector

    // This method is called when another object enters the collider of the checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (objectToMove != null)
        {
            // Move the specified object to the target position
            objectToMove.transform.position = targetPosition.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // You can put any initialization code here
    }

    // Update is called once per frame
    void Update()
    {
        // You can put any update code here
    }
}
