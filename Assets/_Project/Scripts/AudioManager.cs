using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Lazy Singleton
    private static AudioManager _instance = null;

    // Lazy singleton
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<AudioManager>();
                singletonObject.name = typeof(AudioManager).ToString() + " (Singleton)";
            }

            return _instance;
        }
    }
    #endregion
    [Header("Sound Effects")]
    [Range(0, 1)]
    [Tooltip("the sound fx volume")]
    public float SfxVolume = 0.3f;

    [Range(0, 1)]
    [Tooltip("the sound fx volume")]
    public float musicVolume = 0.1f;


    [SerializeField] private Transform _audioSpawn;
    
    private List<AudioSource> _loopedSounds = new List<AudioSource>();
    private void Start()
    {

    }

    public void PlaySound(AudioClip sfx, Vector3 location, bool loop = false)
    {
        if(!sfx) { return; }

        GameObject temporaryAudioHost = new GameObject("TempAudio");
        if (_audioSpawn) { temporaryAudioHost.transform.parent = _audioSpawn; }
        temporaryAudioHost.transform.position = location;
        AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource;
        audioSource.clip = sfx;
        audioSource.volume = SfxVolume;
        
        audioSource.loop = loop;
        audioSource.Play();
        if (loop) 
        { 
            _loopedSounds.Add(audioSource);
        }
        else
        {
            Destroy(temporaryAudioHost, sfx.length + .5f);
        }
    }



    public void PlayBackgroundMusic(AudioClip sfx, Vector3 location, bool loop = false)
    {
        if (!sfx) { return; }

        GameObject temporaryAudioHost = new GameObject("TempAudio");
        if (_audioSpawn) { temporaryAudioHost.transform.parent = _audioSpawn; }
        temporaryAudioHost.transform.position = location;
        AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource;
        audioSource.clip = sfx;
        audioSource.volume = musicVolume;

        audioSource.loop = loop;
        audioSource.Play();
        if (loop)
        {
            _loopedSounds.Add(audioSource);
        }
        else
        {
            Destroy(temporaryAudioHost, sfx.length + .5f);
        }
    }



    public void SetVolume(float volume)
    {
        foreach (var sound in _loopedSounds)
        {
            sound.volume = volume;
        }
    }

    public void StopAllSounds()
    {
        foreach (var sound in _loopedSounds)
        {
            sound.Stop();
        }
    }
}
