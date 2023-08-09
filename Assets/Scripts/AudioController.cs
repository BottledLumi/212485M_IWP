using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    Dictionary<string, AudioSource> sounds = new();
    [SerializeField] AudioSource[] soundObjects;

    public Slider SoundSlider;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        for (int i = 0; i < soundObjects.Length; ++i)
        {
            sounds.Add(soundObjects[i].name, soundObjects[i]);
        }
    }

    public void PlaySound(string soundName)
    {
        sounds[soundName].PlayOneShotSoundManaged(sounds[soundName].clip);
    }

    public void LoopSound(string soundName)
    {
        sounds[soundName].PlayLoopingMusicManaged(sounds[soundName].volume, 0,false);
    }

    public void StopSound(string soundName)
    {
        if (sounds[soundName].isPlaying)
            sounds[soundName].Stop();
    }

    public void SoundVolumeChanged()
    {
        SoundManager.SoundVolume = SoundSlider.value;
    }
}
