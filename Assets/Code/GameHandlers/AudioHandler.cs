using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioHandler : MonoBehaviour
{
    static AudioSource _audioSourceSounds, _audioSourceMusic; 

    [Serializable] 
    public struct Audio
    {
        public string name;
        public AudioClip clip;
    }
    public Audio[] sounds, musicTracks;
    static Dictionary<string,AudioClip> _sounds, _musicTracks;

    void Awake() 
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();   
        _audioSourceSounds = audioSources[0];
        _audioSourceMusic = audioSources[1];

        _sounds = new Dictionary<string, AudioClip>();
        foreach (Audio sound in sounds)
        {
            _sounds.Add(sound.name, sound.clip);
        } 

        _musicTracks = new Dictionary<string, AudioClip>();
        foreach (Audio track in musicTracks)
        {
            _musicTracks.Add(track.name, track.clip);
        } 
    }

    void Start() 
    {
        PlayMusic("MainTrack");    
    }

    public static void PlaySound(string clipName) 
    {
        _audioSourceSounds.PlayOneShot(_sounds[clipName]);
    }

    public static void PlayMusic(string clipName) 
    {
        _audioSourceMusic.clip = _musicTracks[clipName];
        _audioSourceMusic.Play();
    }

    public static void StopMusic() 
    {
        _audioSourceMusic.Stop();
    }
}
