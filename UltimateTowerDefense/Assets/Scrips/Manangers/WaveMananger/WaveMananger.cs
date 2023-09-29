using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesWaves;

public class WaveMananger : MonoBehaviour
{  
    [Header("Wave Mananger")]    
    [SerializeField] private EnemiesPool enemyPoolPrefabReference;
    [SerializeField] private WaveData[] waves; 

    private event Action OnResetPools;

    private Dictionary<EnemyController, EnemiesPool> enemiesPools = new Dictionary<EnemyController, EnemiesPool>();
    private WaveData currentWave;

    private int waveIndex = 0;
    
    public bool WaveCompleted { get { return CheckWaveCompleted(); } }
    public bool HavePendingWaves { get { return waveIndex < waves.Length; } }

    private void Awake()
    {
        InitEnemiesPool();
    }

    private void InitEnemiesPool()
    {
        //First, set all the enemies and the maximum amount of units to instantiate in a dictionary
        Dictionary<EnemyController, int> enemiesToInstantiate = new Dictionary<EnemyController, int>();

        foreach (WaveData wave in waves)
        {
            foreach (EnemyToSpawn enemy in wave.EnemiesToSpawn)
            {
                //Checks if the enemy exist on the dictionary; if not, it is added. If exist, check if
                //the current amount is lower than the new one to change it for the bigger
                if (!enemiesToInstantiate.ContainsKey(enemy.EnemyPrefabReference))
                {
                    enemiesToInstantiate.Add(enemy.EnemyPrefabReference, enemy.AmountToSpawn);
                }
                else
                {
                    if (enemiesToInstantiate[enemy.EnemyPrefabReference] < enemy.AmountToSpawn)
                    {
                        enemiesToInstantiate[enemy.EnemyPrefabReference] = enemy.AmountToSpawn;
                    }                    
                }
            }
        }

        //Second, instantiate a enemies pool for each unit of enemy in the previus diccionary,
        //each pool is seted before both , enemy and enemies pool, are added to the enemies pools diccionary 
        foreach (KeyValuePair<EnemyController, int> keyValuePar in enemiesToInstantiate)
        {
            if (!enemiesPools.ContainsKey(keyValuePar.Key))
            {
                EnemiesPool pool = Instantiate(enemyPoolPrefabReference, transform.position, Quaternion.identity, transform);
                pool.PopulatePool(keyValuePar.Key,keyValuePar.Value);
                OnResetPools += pool.ResetPool;
                enemiesPools.Add(keyValuePar.Key, pool);
            }
        }        
    }

    public void StartNewWave(int waveIndex)
    {
        if (waveIndex >= waves.Length || waveIndex < 0)
        {
            return;
        }
        ResetPools();
        this.waveIndex = waveIndex;
        currentWave = waves[waveIndex];
        foreach (EnemyToSpawn enemyInWave in currentWave.EnemiesToSpawn)
        {
            if (enemiesPools.ContainsKey(enemyInWave.EnemyPrefabReference))
            {
                enemiesPools[enemyInWave.EnemyPrefabReference].SetAmountOfEnemiesToSpawn(enemyInWave.AmountToSpawn);
                enemiesPools[enemyInWave.EnemyPrefabReference].SetTimeBetweenSpawn(enemyInWave.TimeBetweenSpawn);
                enemiesPools[enemyInWave.EnemyPrefabReference].SetEnemiesLevel(enemyInWave.EnemyLevel);
                enemiesPools[enemyInWave.EnemyPrefabReference].EnemiesCanSpawn();
                
            }
        }
    }

    private bool CheckWaveCompleted()
    {
        if (!currentWave)
        {
            return false;
        }

        foreach (EnemyToSpawn enemyInWave in currentWave.EnemiesToSpawn)
        {
            if (enemiesPools.ContainsKey(enemyInWave.EnemyPrefabReference))
            {                
                if (!enemiesPools[enemyInWave.EnemyPrefabReference].AllEnemiesKilled)
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
    

}
