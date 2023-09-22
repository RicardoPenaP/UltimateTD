using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{   
    private List<EnemyController> enemiesPooled = new List<EnemyController>();
    private Vector2Int startCoordinates;
    private List<Tile> pooledEnemiesPath;

    private bool allEnemiesKilled = false;
    private bool canActivate = true;
    private float timeBetweenSpawn = 0f;
    private int enemiesSpawned = 0;
    private int enemiesToSpawn = 0;
    private int enemiesKilled = 0;

    private bool canSpawn = false;
    public bool AllEnemiesKilled { get { return allEnemiesKilled; } }


    private void Start()
    {
        startCoordinates = GridMananger.Instance.GetCoordinatesFromPosition(transform.position);
        pooledEnemiesPath = Pathfinder.Instance.GetNewPath(startCoordinates);
    }    

    private void Update()
    {       
        ActivateEnemy();
    }

    public void PopulatePool(EnemyController enemyToSpawn, int amountToInstantiate)
    {
        for (int i = 0; i < amountToInstantiate; i++)
        {
            EnemyController newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity, transform);
            newEnemy.OnDie += OnEnemyDie;
            newEnemy.gameObject.SetActive(false);
            enemiesPooled.Add(newEnemy);
        }
    }

    private void ActivateEnemy()
    {
        if (!canSpawn)
        {
            return;
        }

        if (!canActivate)
        {
            return;
        }

        foreach (EnemyController enemy in enemiesPooled)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemiesSpawned++;
                StartCoroutine(CanActivateRespawnRoutine());
                enemy.SetPath(pooledEnemiesPath);
                enemy.gameObject.SetActive(true);
                if (enemiesSpawned >= enemiesToSpawn)
                {                    
                    canSpawn = false;
                }
                return;
            }
        }
    }

    public void ResetPool()
    {
        allEnemiesKilled = false;        
        enemiesKilled = 0;
        enemiesSpawned = 0;        
    }

    public void SetAmountOfEnemiesToSpawn(int amount)
    {
        enemiesToSpawn = amount;
    }

    public void SetTimeBetweenSpawn(float timeBetweenSpawn)
    {
        this.timeBetweenSpawn = timeBetweenSpawn;
    }

    private void OnEnemyDie()
    {
        enemiesKilled++;
        if (enemiesKilled >= enemiesToSpawn)
        {
            allEnemiesKilled = true;
        }
    }

    public void EnemiesCanSpawn()
    {
        StartCoroutine(EnemiesCanSpawnRoutine());
    }

    private IEnumerator EnemiesCanSpawnRoutine()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        canSpawn = true;
    }

    private IEnumerator CanActivateRespawnRoutine()
    {
        canActivate = false;
        yield return new WaitForSeconds(timeBetweenSpawn);
        canActivate = true;
    }
}
