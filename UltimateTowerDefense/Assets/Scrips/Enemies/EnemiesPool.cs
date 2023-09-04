using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [Header("Enemies Pool")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amountOfEnemies;
    [SerializeField] private float spawnTime;
    [SerializeField] private bool canSpawn;

    private void Awake()
    {
        EnemiesPooling();
    }

    private void EnemiesPooling()
    {
        
    }

    private IEnumerator EnemiesSpawnRoutine()
    {
        while (true)
        {
            while (canSpawn)
            {
                yield return new WaitForSeconds(spawnTime);

            }

        }
    }
}
