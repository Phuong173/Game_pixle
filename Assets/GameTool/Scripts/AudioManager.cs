using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameTool;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : SingletonMonoBehavior<AudioManager>
{
    [SerializeField] private AudioSource musicSpeaker;
    [SerializeField] private AudioSource soundSpeaker;
    [SerializeField] private List<TrackAudio> MusicTracks = new List<TrackAudio>();
    [SerializeField] private List<TrackAudio> SoundTracks = new List<TrackAudio>();

    protected override void Awake()
    {
        base.Awake();
        if (GameData.I.MusicFX)
        {
            TurnOnMusic();
        }
        else
        {
            TurnOffMusic();
        }
        
        if (GameData.I.SoundFX)
        {
            TurnOnSound();
        }
        else
        {
            TurnOffSound();
        }
        PlayMusic("BGM");
    }

    public void PlayMusic(string filename)
    {
        TrackAudio track = MusicTracks.Find(trackAudio => trackAudio.name == filename);
        musicSpeaker.clip = (track.listAudio[Random.Range(0,track.listAudio.Count)]);
        musicSpeaker.Play();
    }

    public void Shot(string filename, float volume = 1)
    {
        TrackAudio track = SoundTracks.Find(trackAudio => trackAudio.name == filename);
        soundSpeaker.PlayOneShot(track.listAudio[Random.Range(0,track.listAudio.Count)], volume);
    }

    public void Fade(float volume, float duration)
    {
        musicSpeaker.DOFade(volume, duration);
    }

    public void PauseMusic()
    {
        musicSpeaker.Pause();
    }

    public void ResumeMusic()
    {
        musicSpeaker.UnPause();
    }

    public void TurnOffMusic()
    {
        musicSpeaker.volume = 0;
    }

    public void TurnOnMusic()
    {
        musicSpeaker.volume = 1;
    }

    public void TurnOffSound()
    {
        soundSpeaker.volume = 0;
    }

    public void TurnOnSound()
    {
        soundSpeaker.volume = 1;
    }
}

[Serializable]
public class TrackAudio
{
    [SerializeField] public string name;
    [SerializeField] public List<AudioClip> listAudio;
}