using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip soundCannon;

    private AudioSource soundSource;

    private int soundIteration;
    private List<float> soundTimers;
    private List<AudioSource> soundSources;
    private List<Component> soundComponents;

    private bool isMusicPlaying;
    private int currentMusicPlaying;

    private void Awake()
    {
        // Checks if another AudioManager already exists when it is created, and if there is one this destroys itself
        GameObject[] gameObjectList = GameObject.FindGameObjectsWithTag("AudioManager");

        for (int i = 0; i < gameObjectList.Length; i++)
        {
            if (gameObjectList[i] != gameObject)
            {
                Destroy(gameObject);
            }
        }

        // Does not destroy itself when a scene is loaded
        DontDestroyOnLoad(gameObject);

        soundIteration = 0;
        soundTimers = new List<float>();
        soundSources = new List<AudioSource>();
        soundComponents = new List<Component>();
        soundSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Music is by default not playing
        isMusicPlaying = false;
        currentMusicPlaying = 0;
    }

    private void Update()
    {/*
        // Updates soundTimers
        for (int i = 0; i < soundTimers.Count; i++)
        {
            soundTimers[i] -= Time.deltaTime;

            // If a soundTimer is up it removes the source and the elements from the lists
            if (soundTimers[i] <= 0)
            {
                Destroy(soundComponents[i]);
                soundComponents.Remove(soundComponents[i]);
                soundSources.Remove(soundSources[i]);
                soundTimers[i] = 0;
                soundTimers.Remove(0);
                soundIteration--;
            }
        }*/
    }

    public void PlaySound(int soundNumber)
    {
        AudioClip soundClip = soundCannon;
        //int soundLength = 0;

        // Plays a sound depending on the integer
        switch (soundNumber)
        {
            // Normal shot
            case 0:

                break;

            // Cannon shot
            case 1:
                soundClip = soundCannon;
                //soundLength = 4;
                break;
        }

        soundSource.PlayOneShot(soundClip, 1f);
        /*
        // Even I don't know what's going on here /Daniel
        soundComponents.Add(gameObject.AddComponent<AudioSource>());
        soundSources.Add(soundComponents[soundIteration].GetComponent<AudioSource>());
        soundSources[soundIteration].clip = soundClip;
        soundSources[soundIteration].Play();
        soundTimers.Add(soundLength);
        soundIteration++;*/
    }

    public void PlayMusic(int musicNumber)
    {
        // Plays music depending on the integer
        switch (musicNumber)
        {
            case 0:

                break;

            case 1:

                break;
        }
    }

    public void ResumeMusic()
    {
        isMusicPlaying = true;
    }

    public void PauseMusic()
    {
        isMusicPlaying = false;
    }

    public bool GetIsMusicPlaying()
    {
        return isMusicPlaying;
    }
}
