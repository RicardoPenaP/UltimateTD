using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : Singleton<GameMananger>
{
    [Header("Game Mananger")]
    [SerializeField] private GameManangerNextWavePanel nextWavePanel;
    [SerializeField] private float timeToStart;
    [SerializeField] private float timeBetweenWaves;
        
    private WaveMananger[] waveManangers;
    
    private int currentWave = 1;
    private float timeLeft;

    private float timePlayed;
    private int enemiesKilled;
    private int wavesCleared;

    private bool canCheckForNextWave = true;
    private bool gameCompleted = false;


    public float TimePlayed { get { return timePlayed; } }
    public int EnemiesKilled { get { return enemiesKilled; } }
    public int WavesCleared { get { return wavesCleared; } }
    protected override void Awake()
    {
        base.Awake();
        InitWaveManangers();
    }

    private void Start()
    {
        currentWave = 1;
        enemiesKilled = 0;
        wavesCleared = 0;
        gameCompleted = false;
        StartCoroutine(PlayTimeCounterRoutine());
        StartCoroutine(WaitBetweenWavesRoutine(timeToStart));
    }

    private void Update()
    {
        GameLoop();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void InitWaveManangers()
    {
        waveManangers = GetComponentsInChildren<WaveMananger>();        
    }

    private void GameLoop()
    {
        if (gameCompleted)
        {
            return;
        }
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
        if (!canCheckForNextWave)
        {
            return;
        }
        wavesCleared++;
        foreach (WaveMananger waveMananger in waveManangers)
        {
            if (waveMananger.HavePendingWaves)
            {
                canCheckForNextWave = false;
                currentWave++;
                StartCoroutine(WaitBetweenWavesRoutine(timeBetweenWaves));
                return;
            }
        }

        AllWavesCleared();

    }

    private void AllWavesCleared()
    {
        gameCompleted = true;
        GameOverMenu.Instance.OpenGameOverMenu(GameOverMenu.GameOverMenuToOpen.GameCompleted);
        //Win behaviour
    }

    private void StartWaves()
    {
        foreach (WaveMananger waveMananger in waveManangers)
        {
            waveMananger.StartNewWave(currentWave-1);
        }
    }

    private IEnumerator WaitBetweenWavesRoutine(float timeToWait)
    {
        timeLeft = timeToWait;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        timeLeft = 0;
        canCheckForNextWave = true;
        StartWaves();
    }

    private IEnumerator PlayTimeCounterRoutine()
    {
        timePlayed = 0;
        while (true)
        {
            if (PauseMenu.Instance.IsPaused)
            {
                yield return null;
            }
            timePlayed += Time.deltaTime;
            yield return null;
        }
    }

    public void AddEnemyKilled()
    {
        enemiesKilled++;
    }

}