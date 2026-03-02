using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundData
{
    public AudioClip sfxClip;
    public AudioMixerGroup mixerGroup;
    public bool loop;
    public bool playOnAwake;
}
