using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Define me music states
    public enum MusicState
    {
        Default,
        Explore,
        Battle,
        Victory,
    }

    public MusicState currentMusicState = MusicState.Default;

    public AudioSource defaultMusic;
    public AudioSource exploreMusic;
    public AudioSource battleMusic;
    public AudioSource victoryMusic;

    

    private void Start()
    {
        
        PlayMusic(currentMusicState);
    }

    public void ChangeMusic(MusicState newState)
    {
        if (newState != currentMusicState)
        {
            // Stops the current music
            StopMusic(currentMusicState);

            // Updates the current music state
            currentMusicState = newState;

            // Plays the new music
            PlayMusic(currentMusicState);
        }
    }

    private void PlayMusic(MusicState state)
    {
        switch (state)
        {
            case MusicState.Default:
                defaultMusic.Play();
                break;
            case MusicState.Explore:
                exploreMusic.Play();
                break;
            case MusicState.Battle:
                battleMusic.Play();
                break;
            case MusicState.Victory:
                victoryMusic.Play();
                break;
        }
    }

    private void StopMusic(MusicState state)
    {
        switch (state)
        {
            case MusicState.Default:
                defaultMusic.Stop();
                break;
            case MusicState.Explore:
                exploreMusic.Stop();
                break;
            case MusicState.Battle:
                battleMusic.Stop();
                break;
            case MusicState.Victory:
                victoryMusic.Stop();
                break;
        }
    }

    
}
