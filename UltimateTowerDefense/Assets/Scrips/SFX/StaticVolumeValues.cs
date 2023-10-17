using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticVolumeValues
{
    public static readonly float DEFAULT_MUSIC_VOLUME = 0.5f;
    public static readonly float DEFAULT_SFX_VOLUME = 0.5f;

    private static float musicVolume;
    private static float sfxVolume;

    public static event Action<float> OnSetMusicVolume;
    public static event Action<float> OnSetSFXVolume;
    public static float MusicVolume { get { return musicVolume; } }
    public static float SFXVolume { get { return sfxVolume; } }

    public static void SetMusicVolume(float value)
    {
        if (value < 0 )
        {
            musicVolume = 0;
        }
        else
        {
            if (value > 1)
            {
                musicVolume = 1;
            }
            else
            {
                musicVolume = value;
            }
        }

        OnSetMusicVolume?.Invoke(musicVolume);
    }

    public static void SetSFXVolume(float value)
    {
        if (value < 0)
        {
            sfxVolume = 0;
        }
        else
        {
            if (value > 1)
            {
                sfxVolume = 1;
            }
            else
            {
                sfxVolume = value;
            }
        }

        OnSetSFXVolume?.Invoke(sfxVolume);
    }
}
