using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManangerPortalVFX : MonoBehaviour
{
    [Header("Wave Mananger Portal VFX")]
    [SerializeField] private ParticleSystem[] portalActiveVFX;

    private WaveMananger myWaveMananger;

    private void Awake()
    {
        myWaveMananger = GetComponentInParent<WaveMananger>();
        if (myWaveMananger)
        {
            myWaveMananger.OnStartSpanwning += ActivatePortalVFX;
            myWaveMananger.OnStopSpawning += DesactivatePortalVFX;
        }
    }

    private void OnDestroy()
    {
        if (myWaveMananger)
        {
            myWaveMananger.OnStartSpanwning -= ActivatePortalVFX;
            myWaveMananger.OnStopSpawning -= DesactivatePortalVFX;
        }
    }

    private void ActivatePortalVFX()
    {
        foreach (ParticleSystem VFX in portalActiveVFX)
        {
            if (!VFX.isPlaying)
            {
                VFX?.Play();
            }            
        }
    }

    private void DesactivatePortalVFX()
    {
        foreach (ParticleSystem VFX in portalActiveVFX)
        {
            if (VFX.isPlaying)
            {
                VFX?.Stop();
            }           
        }
    }
}
