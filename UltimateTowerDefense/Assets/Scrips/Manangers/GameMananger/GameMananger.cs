using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesWaves;

public class GameMananger : Singleton<GameMananger>
{
    private enum GameModeOptions { SingleRoad, DoubleRoad, TripleRoad, QuadRoad}

    [Header("Game Mananger")]
    [SerializeField] private GameData myGameData;
    [SerializeField] private GameManangerNextWavePanel nextWavePanel;
    [SerializeField] private float timeToStart;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private WaveMananger waveManangerPrefab;
        
    private WaveMananger[] waveManangers;
    private GameModeOptions gameMode;
    
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
        nextWavePanel?.SubscribeToStarNowButtonOnClickEvent(StarNextWave);
    }

    private void Start()
    {
        InitWaveManangers();
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
        nextWavePanel?.UnsubscribeToStarNowButtonOnClickEvent(StarNextWave);

        StopAllCoroutines();
    }

    private void InitWaveManangers()
    {
        Path[] enemiesPath = FindObjectOfType<MapGenerator>().GetEnemiesPaths();
        gameMode = (GameModeOptions)enemiesPath.Length-1;
        waveManangers = new WaveMananger[enemiesPath.Length];
        WaveData[] waveDataToUse;
        switch (gameMode)
        {
            case GameModeOptions.SingleRoad:
                waveDataToUse = myGameData.SingleRoadWaves;
                break;
            case GameModeOptions.DoubleRoad:
                waveDataToUse = myGameData.DoubleRoadWaves;
                break;
            case GameModeOptions.TripleRoad:
                waveDataToUse = myGameData.TripleRoadWaves;
                break;
            case GameModeOptions.QuadRoad:
                waveDataToUse = myGameData.QuadRoadWaves;
                break;
            default:
                waveDataToUse = new WaveData[0];
                break;
        }


        for (int i = 0; i < enemiesPath.Length; i++)
        {
            waveManangers[i] = Instantiate(waveManangerPrefab, enemiesPath[i].nodes[0].Position, Quaternion.identity, transform);
            waveManangers[i].SetEnemiesPath(enemiesPath[i]);
            waveManangers[i].SetWaveData(waveDataToUse);

        }              
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

    private void StarNextWave()
    {
        if (PauseMenu.Instance.IsPaused)
        {
            return;
        }
        timeLeft = 0;
    }

    private IEnumerator WaitBetweenWavesRoutine(float timeToWait)
    {
        timeLeft = timeToWait;
        nextWavePanel.TogglePanel();
        while (timeLeft > 0)
        {            
            timeLeft -= Time.deltaTime;
            nextWavePanel.UpdateTimer(timeLeft);
            yield return null;
        }
        nextWavePanel.TogglePanel();
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

    public EnemyController GetEnemyPrefab(EnemyType enemyType)
    {        
        return myGameData.GetEnemyPrefab(enemyType);
    }
}
