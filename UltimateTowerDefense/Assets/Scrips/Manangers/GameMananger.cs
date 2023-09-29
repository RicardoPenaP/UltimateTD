using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : MonoBehaviour
{
    [Header("Game Mananger")]
    [SerializeField] private float timeToStart;
    [SerializeField] private float timeBetweenWaves;

    private WaveMananger[] waveManangers;

    private float playTime;
    private int enemiesKilled;
    private int wavesCleared;

    private void Awake()
    {
        InitWaveManangers();
    }

    private void InitWaveManangers()
    {
        waveManangers = GetComponentsInChildren<WaveMananger>();
        foreach (WaveMananger waveMananger in waveManangers)
        {
            waveMananger.SetTimeBetweenWaves(timeBetweenWaves);
        }
    }


}
