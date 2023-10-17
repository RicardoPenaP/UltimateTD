using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalSFXToPlay { WaveStarting}
public class GlobalSFXPlayer : Singleton<GlobalSFXPlayer>
{
    [Header("Global SFX Player")]
    [SerializeField] private AudioClip[] SFXClips;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        if (GlobalSFXPlayer.Instance == this)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            return;
        }
        audioSource = GetComponent<AudioSource>();
        StaticVolumeValues.OnSetSFXVolume += SetAudioSourceVolume;
    }

    private void OnDestroy()
    {
        StaticVolumeValues.OnSetSFXVolume -= SetAudioSourceVolume;
    }

    private void SetAudioSourceVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PlayGlobalSFX(GlobalSFXToPlay SFXToPlay)
    {
        audioSource.PlayOneShot(SFXClips[(int)SFXToPlay]);
    }
}
