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
        
    }

    private void StartWaves()
    {
        while (waveIndex < waves.Length)
        {
            SetEnemiesPool(waves[waveIndex].EnemiesToSpawn.Length);
            waveIndex++;
        }
       
       
    }

    private void SetEnemiesPool(int amount)
    {
        foreach (EnemiesPool pool in currentEnemiesPool)
        {
            pool.gameObject.SetActive(true);
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
        else
        {
            if (currentEnemiesPool.Count > amount)
            {
                int diference = currentEnemiesPool.Count - amount;
                for (int i = 0; i < diference; i++)
                {                   
                    currentEnemiesPool[i].gameObject.SetActive(false);
                }
            }
        }
    }

}
