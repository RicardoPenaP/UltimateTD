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

    private int currentWave = 0;

    private float playTime;
    private int enemiesKilled;
    private int wavesCleared;

    private void Awake()
    {
        InitWaveManangers();
    }

    private void Start()
    {
        StartCoroutine(WaitBetweenWavesRoutine(timeToStart));
    }

    private void Update()
    {
        GameLoop();
    }

    private void InitWaveManangers()
    {
        waveManangers = GetComponentsInChildren<WaveMananger>();        
    }

    private void GameLoop()
    {
        CheckForWaveCompleted();
    }

    private void CheckForWaveCompleted()
    {
        foreach (WaveMananger waveMananger in waveManangers)
        {
            if (!waveMananger.WaveCompleted)
            {
                return;
            }
        }

        CheckForNextWave();
    }

    private void CheckForNextWave()
    {
        foreach (WaveMananger waveMananger in waveManangers)
        {
            if (waveMananger.HavePendingWaves)
            {
                currentWave++;
                StartCoroutine(WaitBetweenWavesRoutine(timeBetweenWaves));
                return;
            }
        }

        AllWavesCleared();

    }

    private void AllWavesCleared()
    {

    }

    private void StartWaves()
    {
        foreach (WaveMananger waveMananger in waveManangers)
        {
            waveMananger.StartNewWave(currentWave);
        }
    }

    private IEnumerator WaitBetweenWavesRoutine(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        StartWaves();
    }

}
