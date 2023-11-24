using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDeleter : MonoBehaviour
{
    public float deleteDelay = 10.0f; // Time in seconds before the text is deleted
    private float deleteTimer = 0.0f;

    void Start()
    {
        // Start the timer
        deleteTimer = 0.0f;
    }

    void Update()
    {
        // Update the timer
        deleteTimer += Time.deltaTime;

        // Check if it's time to delete the text
        if (deleteTimer >= deleteDelay)
        {
            // Destroy the TextMesh Pro object
            Destroy(gameObject);

            
        }
    }
}
