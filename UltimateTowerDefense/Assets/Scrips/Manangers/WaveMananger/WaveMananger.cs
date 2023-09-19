using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMananger : MonoBehaviour
{
    [Header("Wave Mananger")]
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

    private void InitWave()
    {
        if (waves.Length < 1)
        {
            return;
        }

        currentWave = waves[0];
        SetEnemiesPool(currentWave.EnemiesToSpawn.Length);
    }

    private void StartWaves()
    {  
       
    }

    private void SetEnemiesPool(int amount)
    {
        SetAmountOfEnemiesPool(amount);
        SetWaveDataToEnemiesPool();
    }

    private void SetAmountOfEnemiesPool(int amount)
    {
        foreach (EnemiesPool pool in currentEnemiesPool)
        {
            pool.gameObject.SetActive(false);
        }

        if (currentEnemiesPool.Count < amount)
        {
            int diference = amount - currentEnemiesPool.Count;

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
}
