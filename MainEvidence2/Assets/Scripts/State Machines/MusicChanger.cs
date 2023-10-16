using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public MusicManager musicManager;
    public MusicManager.MusicState newMusicState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            musicManager.ChangeMusic(newMusicState);
        }
    }
}
