using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float switchBackDelay = 5f;

    private Camera mainCamera;
    private CinemachineBrain cinemachineBrain;

    private void Start()
    {
        mainCamera = Camera.main;
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchToVirtualCamera();
            StartCoroutine(SwitchBackAfterDelay());
        }
    }

    private void SwitchToVirtualCamera()
    {
        // Disables main camera
        mainCamera.enabled = false;

        // Enable the Cinemachine Brain to have virtual cam take over 
        cinemachineBrain.enabled = true;

        //priority
        virtualCamera.Priority = 10;
    }

    private IEnumerator SwitchBackAfterDelay()
    {
        // wait for this long before returing to the normal camera
        yield return new WaitForSeconds(switchBackDelay);

        // v]back to main cam
        SwitchToMainCamera();
    }

    private void SwitchToMainCamera()
    {
        // Enables m cam
        mainCamera.enabled = true;

        // Disables the Cinemachine Brain to return control
        cinemachineBrain.enabled = false;

        // Resets the priority 
        virtualCamera.Priority = 0;
    }
}
