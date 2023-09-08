using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [Header("Enemies Pool")]
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField,Min(0)] private int amountOfEnemiesToPool = 1;
    [SerializeField,Min(0f)] private float spawnTime = 1f;
    [SerializeField] private bool canSpawn;

    private List<EnemyController> enemies = new List<EnemyController>();

    private void Awake()
    {
        EnemiesPooling();
    }

    private void Start()
    {
        StartCoroutine(EnemiesSpawnRoutine());
    }

    private void EnemiesPooling()
    {
        for (int i = 0; i < amountOfEnemiesToPool; i++)
        {
            EnemyController newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
            newEnemy.gameObject.SetActive(false);
            enemies.Add(newEnemy);
        }
    }

    private IEnumerator EnemiesSpawnRoutine()
    {
        while (true)
        {
            while (canSpawn)
            {                
                foreach (EnemyController enemy in enemies)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        enemy.gameObject.SetActive(true);
                        yield return new WaitForSeconds(spawnTime);
                    }
                }               
            }
            yield return null;
        }
    }
}
