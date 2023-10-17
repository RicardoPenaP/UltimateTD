using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWeaponFXController : MonoBehaviour
{
    [Header("Tower Weapon FX Controller")]
    [Header("SFX Audio Clips")]
    [SerializeField] private AudioClip onShootAudioClip;
    [Header("VFX reference")]
    [SerializeField] private ParticleSystem onShootVFX;

    private AudioSource myAudioSource;
    private ITowerWeapon myTowerWeapon;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        myTowerWeapon = GetComponentInParent<ITowerWeapon>();

        SubscribeToOnAttack();
        StaticVolumeValues.OnSetSFXVolume += SetAudioSourceVolume;
    }

    private void OnDestroy()
    {
        StaticVolumeValues.OnSetSFXVolume -= SetAudioSourceVolume;
        UnsubcribeToOnAttack();
    }

    private void SetAudioSourceVolume(float volume)
    {
        myAudioSource.volume = volume;
    }

    private void SubscribeToOnAttack()
    {
        if (myTowerWeapon == null)
        {
            return;
        }
        if (onShootAudioClip)
        {
            myTowerWeapon.OnAttack += () => myAudioSource.PlayOneShot(onShootAudioClip);
        }

        if (onShootVFX)
        {
            myTowerWeapon.OnAttack += () => onShootVFX.Play();
        }
    }

    private void UnsubcribeToOnAttack()
    {
        if (myTowerWeapon == null)
        {
            return;
        }
        if (onShootAudioClip)
        {
            myTowerWeapon.OnAttack -= () => myAudioSource.PlayOneShot(onShootAudioClip);
        }

        if (onShootVFX)
        {
            myTowerWeapon.OnAttack -= () => onShootVFX.Play();
        }
    }
}
