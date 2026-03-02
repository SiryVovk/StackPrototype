using UnityEngine;

public class SoundBuilder
{
    private readonly SFXPool sfxPool;
    private SoundData soundData;
    private Vector3 position = Vector3.zero;
    private bool randomizePitch = false;

    public SoundBuilder(SFXPool sfxPool)
    {
        this.sfxPool = sfxPool;
    }
    
    public SoundBuilder WithSoundData(SoundData soundData)
    {
        this.soundData = soundData;
        return this;
    }

    public SoundBuilder AtPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public SoundBuilder WithRandomizedPitch()
    {
        this.randomizePitch = true;
        return this;
    }

    public SoundEmmiter Play(Transform parent = null)
    {
        SoundEmmiter soundEmmiter = sfxPool.Get();
        soundEmmiter.Initilize(soundData);
        soundEmmiter.transform.position = position;

        soundEmmiter.transform.parent = SFXPool.Instance.transform;

        if (randomizePitch)
        {
            soundEmmiter.WithEandomPitch();
        }

        soundEmmiter.PlaySound();

        return soundEmmiter;
    }
}
