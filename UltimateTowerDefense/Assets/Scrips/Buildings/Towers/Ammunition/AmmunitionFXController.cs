using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionFXController : MonoBehaviour
{
    [Header("Ammunition FX Controller")]
    [Header("SFX Audio Clips")]
    [SerializeField] private AudioClip onHitAudioClip;
    [Header("VFX reference")]
    [SerializeField] private ParticleSystem onHitVFX;

    private AudioSource myAudioSource;
    private IAmmunition myAmmunition;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAmmunition = GetComponentInParent<IAmmunition>();

        SubscribeToOnHit();
        StaticVolumeValues.OnSetSFXVolume += SetAudioSourceVolume;
    }

    private void OnDestroy()
    {
        UnsubcribeToOnHit();
        StaticVolumeValues.OnSetSFXVolume -= SetAudioSourceVolume;        
    }

    private void SetAudioSourceVolume(float volume)
    {
        myAudioSource.volume = volume;
    }

    private void SubscribeToOnHit()
    {
        if (myAmmunition == null)
        {
            return;
        }
        if (onHitAudioClip)
        {
            myAmmunition.OnHit += () => myAudioSource.PlayOneShot(onHitAudioClip);
        }

        if (onHitVFX)
        {
            myAmmunition.OnHit += () => onHitVFX.Play();
        }
    }

    private void UnsubcribeToOnHit()
    {
        if (myAmmunition == null)
        {
            return;
        }
        if (onHitAudioClip)
        {
            myAmmunition.OnHit -= () => myAudioSource.PlayOneShot(onHitAudioClip);
        }

        if (onHitVFX)
        {
            myAmmunition.OnHit -= () => onHitVFX.Play();
        }
    }
}
