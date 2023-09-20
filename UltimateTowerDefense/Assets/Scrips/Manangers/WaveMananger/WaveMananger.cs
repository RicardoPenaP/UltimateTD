using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMananger : MonoBehaviour
{
    private delegate void OnResetPoolsDelegate();

    [Header("Wave Mananger")]
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private EnemiesPool enemyPoolPrefabReference;
    [SerializeField] private WaveData[] waves;

    private Dictionary<EnemyData, EnemiesPool> enemiesPools = new Dictionary<EnemyData, EnemiesPool>();
    private WaveData currentWave;
    private List<EnemiesPool> currentEnemiesPool= new List<EnemiesPool>();

    private OnResetPoolsDelegate onResetPools;

    private int waveIndex = 0;

    private void Awake()
    {
        InitEnemiesPool();
    }

    private void Start()
    {
        StartCoroutine(WaitBetweenWavesRoutine());
    }

    private void Update()
    {
        UpdateWave();
    }

    private void InitEnemiesPool()
    {
        //First, set all the enemies and the maximum amount of units to instantiate in a dictionary
        Dictionary<EnemyData, int> enemiesToInstantiate = new Dictionary<EnemyData, int>();

        foreach (WaveData wave in waves)
        {
            foreach (EnemyToSpawn enemy in wave.EnemiesToSpawn)
            {
                //Checks if the enemy exist on the dictionary; if not, it is added. If exist, check if
                //the current amount is lower than the new one to change it for the bigger
                if (!enemiesToInstantiate.ContainsKey(enemy.EnemyDataReference))
                {
                    enemiesToInstantiate.Add(enemy.EnemyDataReference, enemy.AmountToSpawn);
                }
                else
                {
                    if (enemiesToInstantiate[enemy.EnemyDataReference] < enemy.AmountToSpawn)
                    {
                        enemiesToInstantiate[enemy.EnemyDataReference] = enemy.AmountToSpawn;
                    }                    
                }
            }
        }

        //Second, instantiate a enemies pool for each unit of enemy in the previus diccionary,
        //each pool is seted before both , enemy and enemies pool, are added to the enemies pools diccionary 
        foreach (KeyValuePair<EnemyData, int> key in enemiesToInstantiate)
        {
            if (!enemiesPools.ContainsKey(key.Key))
            {
                EnemiesPool pool = Instantiate(enemyPoolPrefabReference, transform.position, Quaternion.identity, transform);
                pool.PopulatePool(key.Key,key.Value);
                onResetPools += pool.ResetPool;
                enemiesPools.Add(key.Key, pool);
            }
        }        
    }

    private void StartNewWave()
    {
        if (waveIndex >= waves.Length)
        {
            return;
        }
        currentWave = waves[waveIndex];
        foreach (EnemyToSpawn enemyInWave in currentWave.EnemiesToSpawn)
        {
            if (enemiesPools.ContainsKey(enemyInWave.EnemyDataReference))
            {
                enemiesPools[enemyInWave.EnemyDataReference].SetAmountOfEnemiesToSpawn(enemyInWave.AmountToSpawn);
                enemiesPools[enemyInWave.EnemyDataReference].SetTimeBetweenSpawn(enemyInWave.TimeBetweenSpawn);
                enemiesPools[enemyInWave.EnemyDataReference].EnemiesCanSpawn();
            }
        }
    }

    private void UpdateWave()
    {
        if (WaveCompleted())
        {
            waveIndex++;
            if (AllWavesCompleted())
            {
                Debug.Log("All Waves Completed");
            }
            else
            {
                ResetPools();
            }
        }
    }
    

    private bool WaveCompleted()
    {
        if (!currentWave)
        {
            return false;
        }

        foreach (EnemyToSpawn enemyInWave in currentWave.EnemiesToSpawn)
        {
            if (enemiesPools.ContainsKey(enemyInWave.EnemyDataReference))
            {                
                if (!enemiesPools[enemyInWave.EnemyDataReference].AllEnemiesKilled)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool AllWavesCompleted()
    {
        return waveIndex >= waves.Length;
    }

    private void ResetPools()
    {
        onResetPools.Invoke();
        StartCoroutine(WaitBetweenWavesRoutine());
    }

    private IEnumerator WaitBetweenWavesRoutine()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartNewWave();
    }
}
