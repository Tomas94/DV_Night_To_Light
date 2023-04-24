using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Musica");
        PlayMusic("Luciernagas");
        PlayMusic("Viento_lluvia");
        PlayMusic("Lluvia_fuerte");
        PlayMusic("Lluvia_media");
        PlayMusic("Lluvia_suave");
        
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s != null)
        {
            Debug.Log("Sound Not Found");
        }
        
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }

    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s != null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.clip = s.clip;
        }
    }
}
