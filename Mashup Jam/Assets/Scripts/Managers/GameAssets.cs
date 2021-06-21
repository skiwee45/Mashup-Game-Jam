using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i ==null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    void Start() {
        SoundController.Initialize();
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundController.Sound sound;
        public AudioClip audioClip;
    }
}
