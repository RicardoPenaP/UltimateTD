using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesWaves;

public class WaveMananger : MonoBehaviour
{  
    [Header("Wave Mananger")]    
    [SerializeField] private EnemiesPool enemyPoolPrefabReference;

    public Action OnStartSpanwning;
    public Action OnStopSpawning;
    private event Action OnResetPools;

    private WaveData[] waveData;    

    private Dictionary<EnemyType, EnemyController> myEnemies = new Dictionary<EnemyType, EnemyController>();
    private Dictionary<EnemyType, EnemiesPool> enemiesPools = new Dictionary<EnemyType, EnemiesPool>();   
    private Path enemiesPath;
    private WaveData currentWave;

    private int waveIndex = 0;
    
    public bool WaveCompleted { get { return CheckWaveCompleted(); } }
    public bool HavePendingWaves { get { return waveIndex < waveData.Length-1; } }

    private void Start()
    {
        InitEnemiesPool();
    }

    private void Update()
    {
        CheckForEnemiesPendingToSpawn();
    }

    private void InitEnemiesPool()
    {
        //First, set all the enemies and the maximum amount of units to instantiate in a dictionary
        Dictionary<EnemyType, int> enemiesToInstantiate = new Dictionary<EnemyType, int>();

        foreach (WaveData wave in waveData)
        {
            foreach (EnemyToSpawn enemy in wave.EnemiesToSpawn)
            {
                //Checks if the enemy exist on the dictionary; if not, it is added. If exist, check if
                //the current amount is lower than the new one to change it for the bigger
                if (!enemiesToInstantiate.ContainsKey(enemy.EnemyType))
                {
                    enemiesToInstantiate.Add(enemy.EnemyType, enemy.AmountToSpawn);
                }
                else
                {
                    if (enemiesToInstantiate[enemy.EnemyType] < enemy.AmountToSpawn)
                    {
                        enemiesToInstantiate[enemy.EnemyType] = enemy.AmountToSpawn;
                    }                    
                }
            }
        }

        //Second, instantiate a enemies pool for each unit of enemy in the previus diccionary,
        //each pool is seted before both , enemy and enemies pool, are added to the enemies pools diccionary 
        foreach (KeyValuePair<EnemyType, int> keyValuePar in enemiesToInstantiate)
        {
            if (!enemiesPools.ContainsKey(keyValuePar.Key))
            {
                EnemiesPool pool = Instantiate(enemyPoolPrefabReference, transform.position, Quaternion.identity, transform);
                pool.SetEnemiesPath(enemiesPath);
                pool.PopulatePool(GameMananger.Instance.GetEnemyPrefab(keyValuePar.Key),keyValuePar.Value);
                OnResetPools += pool.ResetPool;
                enemiesPools.Add(keyValuePar.Key, pool);
            }
        }        
    }

    public void StartNewWave(int waveIndex)
    {
        if (waveIndex >= waveData.Length || waveIndex < 0)
        {
            return;
        }
        ResetPools();
        OnStartSpanwning?.Invoke();
        this.waveIndex = waveIndex;
        currentWave = waveData[waveIndex];
        foreach (EnemyToSpawn enemyInWave in currentWave.EnemiesToSpawn)
        {
            if (enemiesPools.ContainsKey(enemyInWave.EnemyType))
            {
                enemiesPools[enemyInWave.EnemyType].SetAmountOfEnemiesToSpawn(enemyInWave.AmountToSpawn);
                enemiesPools[enemyInWave.EnemyType].SetTimeBetweenSpawn(enemyInWave.TimeBetweenSpawn);
                enemiesPools[enemyInWave.EnemyType].SetEnemiesLevel(enemyInWave.EnemyLevel);
                enemiesPools[enemyInWave.EnemyType].EnemiesCanSpawn();                
            }
        }
    }

    private bool CheckWaveCompleted()
    {
        if (currentWave == null)
        {
            return false;
        }

        foreach (EnemyToSpawn enemyInWave in currentWave.EnemiesToSpawn)
        {
            if (enemiesPools.ContainsKey(enemyInWave.EnemyType))
            {                
                if (!enemiesPools[enemyInWave.EnemyType].AllEnemiesKilled)
                {
                    return false;
                }
            }
        }        
        return true;
    }

    private void ResetPools()
    {
        OnResetPools.Invoke();        
    }    

    private void CheckForEnemiesPendingToSpawn()
    {
        if (currentWave == null)
        {
            return;
        }

        foreach (EnemyToSpawn enemyInWave in currentWave.EnemiesToSpawn)
        {
            if (enemiesPools.ContainsKey(enemyInWave.EnemyType))
            {
                if (!enemiesPools[enemyInWave.EnemyType].AllEnemiesSpawned)
                {
                    return;
                }
            }
        }

        OnStopSpawning?.Invoke();
    }

    public void SetEnemiesPath(Path enemiesPath)
    {
        this.enemiesPath = enemiesPath;
    }

    public void SetEnemies(EnemiesReference[] enemies)
    {
        myEnemies.Clear();
        EnemiesReference[] newEnemies = enemies;

        foreach (EnemiesReference enemy in newEnemies)
        {
            myEnemies.TryAdd(enemy.EnemyType,enemy.EnemyPrefab);
        }
    }

    public void SetWaveData(WaveData[] waveData)
    {
        this.waveData = waveData;
    }
}
