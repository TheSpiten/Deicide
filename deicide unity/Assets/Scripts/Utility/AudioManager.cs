using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioSource musicSource;

    public AudioClip soundMachineGun1;
    public AudioClip soundMachineGun2;
    public AudioClip soundMachineGun3;
    public AudioClip soundCannon;
    public AudioClip soundJavelinJab;
    public AudioClip soundJavelinHit;
    public AudioClip soundStormOut;
    public AudioClip soundStormIn;
    public AudioClip soundPickup;
    public AudioClip soundBossHit1;
    public AudioClip soundBossHit2;
    public AudioClip soundBossHit3;
    private List<int> bossHitSounds;

    public AudioClip soundDash;
    public AudioClip soundElectricShield;
    public AudioClip soundDynamite;
    public AudioClip soundRepair;
    public AudioClip soundPlayerHit;
    public AudioClip soundMenuSelect;
    public AudioClip soundMenuSwitch;

    public AudioClip musicBossIntro;
    public AudioClip musicBossMain;
    public AudioClip musicBoss1;
    public AudioClip musicBoss2;
    public AudioClip musicBoss3;
    public AudioClip musicBoss4;


    private enum Music { intro, main, one, two, three, four }
    private bool isMusicPlaying;
    private Music currentMusicPlaying;
    private float musicTimer;
    private float musicLength;
    private float musicVolume;
    private float musicVolumeMain;

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
        bossHitSounds = new List<int>();
        SetBossHitSounds();

        // Music is by default not playing
        isMusicPlaying = false;
        currentMusicPlaying = 0;
        musicTimer = 0;
        musicLength = 0;
        musicVolume = 0.15f;
        musicVolumeMain = 0.15f;
    }

    private void Start()
    {
        PlayMusic(0);
    }

    private void FixedUpdate()
    {
        MusicUpdate();
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
                if (bossHitSounds.Count < 1)
                {
                    SetBossHitSounds();
                }
                int randomGunSounds = bossHitSounds[Mathf.FloorToInt(Random.Range(0, bossHitSounds.Count - 0.0001f))];
                
                switch (randomGunSounds)
                {
                    case 1:
                        soundClip = soundBossHit1;
                        break;

                    case 2:
                        soundClip = soundBossHit2;
                        break;

                    case 3:
                        soundClip = soundBossHit3;
                        break;

                    default:
                        soundClip = soundBossHit1;
                        break;
                }
                bossHitSounds.Remove(randomGunSounds);
                if (bossHitSounds.Count < 1)
                {
                    SetBossHitSounds();
                    bossHitSounds.Remove(randomGunSounds);
                }
                soundVolume = 0.25f;
                break;

            // Cannon shot
            case 1:
                soundClip = soundCannon;
                soundVolume = 0.25f;
                break;

            // Javelin jab
            case 2:
                soundClip = soundJavelinJab;
                soundVolume = 1f;
                break;

            // Javelin hit
            case 3:
                //soundClip = soundJavelinHit;
                //soundVolume = 1f;
                break;

            // Pickup sound
            case 4:
                soundClip = soundPickup;
                soundVolume = 1f;
                break;

            // Storm out sound
            case 5:
                soundClip = soundStormOut;
                soundVolume = 1f;
                break;

            // Storm in sound
            case 6:
                soundClip = soundStormIn;
                soundVolume = 1f;
                break;

            // Shield hit sound
            case 7:
                soundClip = soundMachineGun1;
                soundVolume = 0.15f;
                break;

            case 9:
                soundClip = soundDash;
                soundVolume = 1f;
                break;

            case 10:
                soundClip = soundElectricShield;
                soundVolume = 1f;
                break;

            case 11:
                soundClip = soundDynamite;
                soundVolume = 1f;
                break;

            case 12:
                soundClip = soundRepair;
                soundVolume = 1f;
                break;

            case 13:
                soundClip = soundPlayerHit;
                soundVolume = 1f;
                break;

            case 14:
                soundClip = soundMenuSelect;
                soundVolume = 1f;
                break;

            case 15:
                soundClip = soundMenuSwitch;
                soundVolume = 1f;
                break;

        }

        soundSource.PlayOneShot(soundClip, soundVolume);
    }

    public void PlayMusic(int musicNumber)
    {
        // Plays music depending on the integer
        switch (musicNumber)
        {
            // Length of sound -0.155f
            case 0:
                currentMusicPlaying = Music.intro;
                musicLength = 13.534f;
                musicVolumeMain = 0.15f;
                break;

            case 1:
                currentMusicPlaying = Music.one;
                musicLength = 25.488f;
                musicVolume = 0.15f;
                musicVolumeMain = 0.15f;
                break;

            case 2:
                currentMusicPlaying = Music.two;
                musicLength = 25.488f;
                musicVolume = 0.2f;
                musicVolumeMain = 0.1f;
                break;

            case 3:
                currentMusicPlaying = Music.three;
                musicLength = 25.488f;
                musicVolume = 0.2f;
                musicVolumeMain = 0.15f;
                break;

            case 4:
                currentMusicPlaying = Music.four;
                musicLength = 25.488f;
                musicVolume = 0.2f;
                musicVolumeMain = 0.15f;
                break;
        }
    }

    private void MusicUpdate()
    {
        if (musicTimer > 0)
        {
            musicTimer -= Time.fixedDeltaTime;
        }
        else
        {
            musicTimer = musicLength;
            if (currentMusicPlaying == Music.intro)
            {
                musicSource.PlayOneShot(musicBossIntro, 0.15f);
                PlayMusic(1);
            }
            else
            {
                AudioClip playThisMusic = CurrentMusic(currentMusicPlaying);
                musicSource.PlayOneShot(playThisMusic, musicVolume);
                musicSource.PlayOneShot(musicBossMain, 0.15f);
            }
        }
    }

    private AudioClip CurrentMusic(Music music)
    {
        switch (music)
        {
            case Music.intro:
                return musicBossIntro;

            case Music.one:
                return musicBoss1;

            case Music.two:
                return musicBoss2;

            case Music.three:
                return musicBoss3;

            case Music.four:
                return musicBoss4;
        }

        return musicBossMain;
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

    private void SetBossHitSounds()
    {
        // Sets machineGunSounds list
        bossHitSounds.Clear();
        bossHitSounds.Add(1);
        bossHitSounds.Add(2);
        bossHitSounds.Add(3);
    }
}
