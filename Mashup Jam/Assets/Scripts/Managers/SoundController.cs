using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public enum Sound
    {
        PlayerMove,
        Death,
        Win,
        PortalPlace,
        Teleport,
        Spike,
        Hit,
        Key,
        Door,
    }

    private  Dictionary<Sound, float> soundTimerDictionary;

    private GameObject audioObject;
    private AudioSource audioSource;
    public void Start()
    {
        audioObject = GameObject.FindGameObjectWithTag("Audio");
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }
    public  void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if(audioObject == null) {
                audioObject = new GameObject("Sound");
                audioObject.transform.parent = gameObject.transform;
                audioSource = audioObject.AddComponent<AudioSource>();
            }
            audioSource.PlayOneShot(GetAudioClip(sound));
        }

    }

    private  bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.3f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                // break;
        }
    }

    private  AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                Debug.Log(soundAudioClip.audioClip);
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found");
        return null;
    }
}
