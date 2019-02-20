using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip soundMachineGun1;
    public AudioClip soundMachineGun2;
    public AudioClip soundMachineGun3;
    public AudioClip soundCannon;
    public AudioClip soundJavelinJab;
    public AudioClip soundJavelinHit;

    private AudioSource soundSource;

    private bool isMusicPlaying;
    private int currentMusicPlaying;
    private List<int> machineGunSounds;

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

        // Sets list for machine gun sounds
        machineGunSounds = new List<int>();
        SetMachineGunSounds();

        // Music is by default not playing
        isMusicPlaying = false;
        currentMusicPlaying = 0;
    }

    private void Start()
    {
        // Gets attached audio source component
        soundSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundNumber)
    {
        AudioClip soundClip = soundMachineGun1;

        float soundVolume = 1f;

        // Plays a sound depending on the integer
        switch (soundNumber)
        {
            // Machine gun shot
            // Plays a random sound from a list of 3, never repeating the same sound in a row
            case 0:
                if (machineGunSounds.Count < 1)
                {
                    SetMachineGunSounds();
                }
                int randomGunSounds = machineGunSounds[Mathf.FloorToInt(Random.Range(0, machineGunSounds.Count - 0.0001f))];
                
                switch (randomGunSounds)
                {
                    case 1:
                        soundClip = soundMachineGun1;
                        break;

                    case 2:
                        soundClip = soundMachineGun2;
                        break;

                    case 3:
                        soundClip = soundMachineGun3;
                        break;

                    default:
                        soundClip = soundMachineGun1;
                        break;
                }
                machineGunSounds.Remove(randomGunSounds);
                if (machineGunSounds.Count < 1)
                {
                    SetMachineGunSounds();
                    machineGunSounds.Remove(randomGunSounds);
                }
                soundVolume = 0.1f;
                break;

            // Cannon shot
            case 1:
                soundClip = soundCannon;
                soundVolume = 1f;
                break;

            // Javelin jab
            case 2:

                break;

            // Javelin hit
            case 3:
                soundClip = soundJavelinHit;
                break;
        }

        soundSource.PlayOneShot(soundClip, soundVolume);
    }

    public void PlayMusic(int musicNumber)
    {
        // Plays music depending on the integer
        switch (musicNumber)
        {
            // Place music here plz
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

    private void SetMachineGunSounds()
    {
        // Sets machineGunSounds list
        machineGunSounds.Clear();
        machineGunSounds.Add(1);
        machineGunSounds.Add(2);
        machineGunSounds.Add(3);
    }
}
