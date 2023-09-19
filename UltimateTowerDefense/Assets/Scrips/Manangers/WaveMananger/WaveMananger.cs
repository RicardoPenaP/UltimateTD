using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMananger : MonoBehaviour
{
    [Header("Wave Mananger")]
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private EnemiesPool enemyPoolPrefabReference;
    [SerializeField] private WaveData[] waves;

    private WaveData currentWave;
    private List<EnemiesPool> currentEnemiesPool= new List<EnemiesPool>();


    private int waveIndex = 0;

    private void Awake()
    {
        InitWave();
    }

    private void Start()
    {
        StartWaves();
    }

    private void Update()
    {
        UpdateWave();
    }

    private void InitWave()
    {
        if (waves.Length < 1)
        {
            return;
        }
        currentWave = waves[0];
       
    }

    private void StartWaves()
    {
        SetEnemiesPool(currentWave.EnemiesToSpawn.Length);
    }

    private void SetEnemiesPool(int amountOfPools)
    {
        SetAmountOfEnemiesPool(amountOfPools);
        SetWaveDataToEnemiesPool();
    }

    private void SetAmountOfEnemiesPool(int amountOfPools)
    {     
        if (currentEnemiesPool.Count < amountOfPools)
        {
            int diference = amountOfPools - currentEnemiesPool.Count;

            for (int i = 0; i < diference; i++)
            {
                EnemiesPool newPool = Instantiate(enemyPoolPrefabReference, transform.position, Quaternion.identity, transform);
                currentEnemiesPool.Add(newPool);
            }
        }
    }

    private void SetWaveDataToEnemiesPool()
    {
        for (int i = 0; i < currentWave.EnemiesToSpawn.Length; i++)
        {            
            currentEnemiesPool[i].SetEnemyToSpawn(currentWave.EnemiesToSpawn[i]);
            currentEnemiesPool[i].gameObject.SetActive(true);
        }
    }

    private void UpdateWave()
    {
        if (!WaveFinished())
        {
            return;
        }
        waveIndex++;
        if (waveIndex < waves.Length)
        {
            currentWave = waves[waveIndex];
            SetEnemiesPool(currentWave.EnemiesToSpawn.Length);
        }
        else
        {
            Debug.Log("Level Completed");
        }
    }

    private bool WaveFinished()
    {
        foreach (EnemiesPool pool in currentEnemiesPool)
        {
            if (!pool.AllEnemiesKilled)
            {
                return false;
            }
        }
        return true;
    }
}
