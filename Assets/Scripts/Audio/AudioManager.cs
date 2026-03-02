using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    [Header("Music Clips")]
    [SerializeField] private AudioClip mainThemeMusic;
    [Header("Settings")]
    [SerializeField] private float volumeFadeTime = 1f;

    private readonly string[] VOLUME_PARAMS = { "MasterVolume", "MusicVolume", "SFXVolume" };
    
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadAllVolume();
    }

    private void LoadAllVolume()
    {
        foreach(var param in VOLUME_PARAMS)
        {
            float volume = PlayerPrefs.GetFloat(param,1f);
            SetVolume(param, volume);
        }   
    }

     public void SetVolume(string exposedParam, float value01)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value01, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(exposedParam, volume);
        PlayerPrefs.SetFloat(exposedParam, value01);
    }

    public float GetVolume(string exposedParam)
    {
        return PlayerPrefs.GetFloat(exposedParam, 1f);
    }

    public IEnumerator MusicTransitionIn()
    {
        yield return MusicFade(0f, VOLUME_PARAMS[1]);
    }

    public IEnumerator MusicTransitionOut()
    {
        yield return MusicFade(GetVolume(VOLUME_PARAMS[1]), VOLUME_PARAMS[1]);
    }

    private IEnumerator MusicFade(float targetVolume, string exposedParam)
    {
        audioMixer.GetFloat(exposedParam, out float startVolumeDb);

        float targetVolumeDb = Mathf.Log10(Mathf.Clamp(targetVolume, 0.0001f, 1f)) * 20;

        float elapsTime = 0f;

        while(elapsTime < volumeFadeTime)
        {
            elapsTime += Time.deltaTime;
            float t = elapsTime / volumeFadeTime;
            float currentDb = Mathf.Lerp(startVolumeDb, targetVolumeDb, t);
            audioMixer.SetFloat(exposedParam, currentDb);
            yield return null;
        }

        audioMixer.SetFloat(exposedParam, targetVolumeDb);
    }
}
