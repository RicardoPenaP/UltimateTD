using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesWaves;

public class GameMananger : Singleton<GameMananger>
{  
    [Header("Game Mananger")]
    [SerializeField] private GameData myGameData;
    [SerializeField] private GameManangerNextWavePanel nextWavePanel;
    [SerializeField] private float timeToStart;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private WaveMananger waveManangerPrefab;
        
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
        nextWavePanel?.SubscribeToStarNowButtonOnClickEvent(StarNextWave);
    }

    private void Start()
    {
        AmbientalMusicPlayer.Instance?.ChangeMusicWithTransition(AmbientalMusicToPlay.StanByGameSceneMusic);
        InitWaveManangers();
        currentWave = 1;
        enemiesKilled = 0;
        wavesCleared = 0;
        gameCompleted = false;
        StartCoroutine(PlayTimeCounterRoutine());
        StartCoroutine(WaitBetweenWavesRoutine(timeToStart));
        switch (GameMode.GameModeOption)
        {
            case GameModeOptions.SingleRoad:
                timeBetweenWaves = 20;
                break;
            case GameModeOptions.DoubleRoad:
                timeBetweenWaves = 25;
                break;
            case GameModeOptions.TripleRoad:
                timeBetweenWaves = 30;
                break;
            case GameModeOptions.QuadRoad:
                timeBetweenWaves = 40;
                break;
            default:
                break;
        }
        SceneTranstitionFade.Instance.FadeOut();
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
        waveManangers = new WaveMananger[enemiesPath.Length];         

        for (int i = 0; i < enemiesPath.Length; i++)
        {           
            waveManangers[i] = Instantiate(waveManangerPrefab, enemiesPath[i].nodes[0].Position, Quaternion.identity, transform);
            waveManangers[i].SetEnemiesPath(enemiesPath[i]);
            waveManangers[i].SetWaveData(myGameData.GetRandomWaveData(GameMode.GameModeOption));
        }              
    }

    private void GameLoop()
    {
        if (gameCompleted)
        {
            return;
        }        
        CheckForWaveCompleted();
        UpdateWaveInformationPanel();
        AllWavesCleared();
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

       

    }

    private void AllWavesCleared()
    {
        if (gameCompleted)
        {
            return;
        }
        foreach (WaveMananger waveMananger in waveManangers)
        {
            if (!waveMananger.GetAllWavesCleared())
            {
                return;
            }
        }
        gameCompleted = true;
        GameOverMenu.Instance.OpenGameOverMenu(GameOverMenu.GameOverMenuToOpen.GameCompleted);        
        //Win behaviour
    }

    private void StartWaves()
    {
        GlobalSFXPlayer.Instance?.PlayGlobalSFX(GlobalSFXToPlay.WaveStarting);
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

    private void UpdateWaveInformationPanel()
    {
        if (!WaveInformationPanel.Instance)
        {
            return;
        }
        int enemiesLeft = 0;
        foreach (WaveMananger waveMananger in waveManangers)
        {
            enemiesLeft += waveMananger.GetEnemiesLeftForSpawn();
        }
        WaveInformationPanel.Instance.UpdateWaveInformationPanel(currentWave, enemiesLeft);
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
