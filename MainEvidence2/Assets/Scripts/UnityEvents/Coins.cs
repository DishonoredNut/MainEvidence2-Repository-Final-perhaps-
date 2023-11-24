using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coins : MonoBehaviour
{
    public UnityEvent collectCoin;
    public AudioSource collectSource; // Reference to the AudioSource
    public AudioClip coinCollectClip; // The audio clip to play when the coin is collected

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

            // Play the specified audio clip from the AudioSource
            if (collectSource != null && coinCollectClip != null)
            {
                collectSource.PlayOneShot(coinCollectClip);
            }

            Destroy(gameObject);
        }
    }
}
