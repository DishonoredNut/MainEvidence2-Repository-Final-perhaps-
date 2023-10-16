using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager class

public class ManageScene : MonoBehaviour
{
    private GameObject player; // References to the player object
    private PlayerHealth playerHealth; // References to the PlayerHealth script

    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Gets the PlayerHealth component attached to the player object
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player object is missing
        if (player == null)
        {
            // Player didn't go to college
            Debug.Log("Player is missing! Restarting the scene.");
            RestartScene();
        }
        else if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            // Player is dead
            Debug.Log("Player is dead! Restarting the scene.");
            RestartScene();
        }

        // Checks if the number of objects tagged as "Enemy" = 0
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            // Loads the next scene in the build index
            LoadNextScene();
        }
    }

    // Function to restart the current scene
    private void RestartScene()
    {
        // Get the current scene's build index and reload it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Function to load the next scene
    private void LoadNextScene()
    {
        // Get the current scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Load the next scene (increment the build index)
        int nextSceneIndex = currentSceneIndex + 1;
        
        // Check if the next scene exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // No more scenes in the build index (end of the game)
            Debug.Log("No more scenes to load.");
        }
    }
}
