using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SFXPool : MonoBehaviour
{
    public static SFXPool Instance => instance;

    [SerializeField] private SoundEmmiter soundEmmiterPrefab;

    [SerializeField] private int poolSize = 10;
    [SerializeField] private int maxPoolSize = 50;

    private static SFXPool instance;

    private IObjectPool<SoundEmmiter> sfxPool;
    private readonly List<SoundEmmiter> activeEmitters = new List<SoundEmmiter>();

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        sfxPool = new ObjectPool<SoundEmmiter>(
            CreateAudioSource,
            OnGetAudioSource,
            OnReleaseAudioSource,
            OnDestroyAudioSource,
            false,
            poolSize,
            maxPoolSize
        );
    }

    public SoundBuilder CreateSoundBuilder()
    {
        return new SoundBuilder(this);
    }

    private SoundEmmiter CreateAudioSource()
    {
        var audioSource = Instantiate(soundEmmiterPrefab);
        audioSource.gameObject.SetActive(false);
        return audioSource;
    }

    private void OnGetAudioSource(SoundEmmiter source)
    {
        source.gameObject.SetActive(true);
        activeEmitters.Add(source);
    }

    private void OnReleaseAudioSource(SoundEmmiter source)
    {
        source.gameObject.SetActive(false);
        activeEmitters.Remove(source);
    }

    private void OnDestroyAudioSource(SoundEmmiter source)
    {
        if(source == null)
        {
            return;
        }
        Destroy(source.gameObject);
    }

    public SoundEmmiter Get()
    {
        return sfxPool.Get();
    }

    public void Release(SoundEmmiter source)
    {
        sfxPool.Release(source);
    }
}
