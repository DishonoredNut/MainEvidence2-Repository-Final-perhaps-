using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coins : MonoBehaviour
{
    public UnityEvent collectCoin;
    public AudioSource collectSound; // Reference to the AudioSource

    private void Start()
    {
        Controller controller = GameObject.FindGameObjectWithTag("Controller")?.GetComponent<Controller>();
        if (controller != null)
        {
            collectCoin.AddListener(controller.IncrementScore);
        }

        UI ui = GameObject.FindGameObjectWithTag("UIControl")?.GetComponent<UI>();
        if (ui != null)
        {
            collectCoin.AddListener(ui.UpdateScore);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectCoin.Invoke();

            // Play the collect sound if AudioSource is assigned
            if (collectSound != null)
            {
                collectSound.Play();
            }

            Destroy(gameObject);
        }
    }
}
