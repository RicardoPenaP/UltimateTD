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
        foreach (WaveData wave in waves)
        {
            foreach (EnemyToSpawn enemy in wave.EnemiesToSpawn)
            {
                Dictionary<EnemyData, int> enemiesToInstantiate = new Dictionary<EnemyData, int>();

                if (!enemiesPools.ContainsKey(enemy.EnemyDataReference))
                {
                    EnemiesPool pool = Instantiate(enemyPoolPrefabReference,transform.position,Quaternion.identity,transform);
                    pool.SetEnemyToSpawn(enemy);
                    onResetPools += pool.ResetPool;
                    enemiesPools.Add(enemy.EnemyDataReference, pool);
                }
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
                enemiesPools[enemyInWave.EnemyDataReference].SetAmountOfEnemiesInWave(enemyInWave.AmountToSpawn);
                enemiesPools[enemyInWave.EnemyDataReference].CanSpawn = true;
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
