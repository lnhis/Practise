using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class SoundSystem : MonoBehaviour
{
    public AudioClip[] soundDatas;
    public AudioClip[] musicDatas;
    public AudioClip[] ambientDatas;

    [Range(0.0f, 1.0f)]
    public float soundVolume = 1f;
    [Range(0.0f, 1.0f)]
    public float musicVolume = 1f;
    [Range(0.0f, 1.0f)]
    public float ambientVolume = 1f;

    private AudioSource Music { get; set; }
    private AudioSource Ambient { get; set; }

    private float lastSoundVolume = -1f;
    private float lastMusicVolume = -1f;
    private float lastAmbientVolume = -1f;

    public bool MuteMusic = false;
    public bool MuteAmbient = false;

    public bool MuteAllSound = false;
    private bool lastMuteAllSound = false;

    private static SoundSystem instance = null;
    public static SoundSystem Instance
    {
        get
        {
            if(instance == null)
            {
                SoundSystem ss = Resources.Load<SoundSystem>("SoundSystem");
                GameObject newInstance = GameObject.Instantiate(ss.gameObject);
                instance = newInstance.GetComponent<SoundSystem>();
            }
            return instance;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
    
    public void PlayAmbient(string n)
    {
        AudioClip clip = ambientDatas.FirstOrDefault(xs => xs.name == n);
        if (clip == null) return;
        
        StopAmbient();
        
        GameObject prefab = new GameObject();
        prefab.name = "Ambient-" + n;
        AudioSource ass = prefab.AddComponent<AudioSource>();
        
        ass.clip = clip;
        ass.loop = true;
        ass.volume = ambientVolume;
        ass.mute = MuteAllSound;
        prefab.transform.SetParent(this.transform);
        prefab.transform.position = Vector2.zero;

        Ambient = ass;
        ass.Play();
    }
    public void StopMusic()
    {
        if (Music != null)
        {
            Music.Stop();
            GameObject.Destroy(Music.gameObject);
            Music = null;
        }
    }
    public void StopAmbient()
    {
        if (Ambient != null)
        {
            Ambient.Stop();
            GameObject.Destroy(Ambient.gameObject);
            Ambient = null;
        }
    }
    public void PlayMusic(string n)
    {
        AudioClip clip = musicDatas.FirstOrDefault(xs => xs.name == n);
        if (clip == null) return;

        StopMusic();

        GameObject prefab = new GameObject();
        prefab.name = "Music-" + n;
        AudioSource ass = prefab.AddComponent<AudioSource>();

        ass.clip = clip;
        ass.loop = true;
        ass.volume = ambientVolume;
        ass.mute = MuteAllSound;
        prefab.transform.SetParent(this.transform);
        prefab.transform.position = Vector2.zero;

        Music = ass;
        ass.Play();
    }
    
    public void PlaySound(string n)
    {
        AudioClip clip = soundDatas.FirstOrDefault(xs => xs.name == n);
        if (clip == null) return;

        GameObject prefab = new GameObject();
        
        prefab.name = "Sound-" + n;
        AudioSource ass = prefab.AddComponent<AudioSource>();
        ass.loop = false;
        ass.volume = soundVolume;
        ass.clip = clip;
        ass.mute = MuteAllSound;
        prefab.transform.SetParent(this.transform);
        SoundStopper ss = prefab.AddComponent<SoundStopper>();
    }
    

    private void UpdateVolume()
    {
        if (Music != null)
        {
            Music.volume = musicVolume;
            Music.mute = MuteAllSound;
        }

        if(Ambient != null)
        {
            Ambient.volume = ambientVolume;
            Ambient.mute = MuteAllSound;
        }
    }

	void Update ()
    {
	    if(lastSoundVolume!=soundVolume || lastMusicVolume != musicVolume || lastAmbientVolume != ambientVolume || lastMuteAllSound != MuteAllSound)
        {
            lastSoundVolume = soundVolume;
            lastMusicVolume = musicVolume;
            lastAmbientVolume = ambientVolume;
            lastMuteAllSound = MuteAllSound;

            UpdateVolume();
        }
        
        if(!MuteAllSound && Music != null)
        {
            Music.mute = MuteMusic;
        }
        
	}
    

}
