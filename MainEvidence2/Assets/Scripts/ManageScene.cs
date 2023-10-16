using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager class

public class ManageScene : MonoBehaviour
{
    private GameObject player; // Reference to the player object
    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object in the scene by tag (assuming it's tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Get the PlayerHealth component attached to the player object
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player object is missing
        if (player == null)
        {
            // Player is missing, restart the scene
            Debug.Log("Player is missing! Restarting the scene.");
            RestartScene();
        }
        else if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            // Player is dead, restart the scene
            Debug.Log("Player is dead! Restarting the scene.");
            RestartScene();
        }
    }

    // Function to restart the current scene
    private void RestartScene()
    {
        // Get the current scene's build index and reload it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
