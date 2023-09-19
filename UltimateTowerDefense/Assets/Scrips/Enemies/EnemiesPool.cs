using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private EnemyToSpawn enemyToSpawn;

    private List<EnemyController> enemies = new List<EnemyController>();
    private Vector2Int startCoordinates;
    private List<Tile> pooledEnemiesPath;

    private bool allEnemiesSpawned = false;
    private bool allEnemiesKilled = false;
    private bool canActivate = true;
    private int enemiesSpawned = 0;
    [SerializeField]private int enemiesKilled = 0;

    public bool AllEnemiesKilled { get { return allEnemiesKilled; } }

    private void Start()
    {
        startCoordinates = GridMananger.Instance.GetCoordinatesFromPosition(transform.position);
        pooledEnemiesPath = Pathfinder.Instance.GetNewPath(startCoordinates);
        PopulatePool();
        StartCoroutine(CanActivateRespawnRoutine());
    }

    private void Update()
    {
        ActivateEnemy();
    }

    private void ActivateEnemy()
    {
        if (allEnemiesSpawned)
        {
            return;
        }

        if (!canActivate)
        {
            return;
        }

        foreach (EnemyController enemy in enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemiesSpawned++;
                StartCoroutine(CanActivateRespawnRoutine());
                enemy.gameObject.SetActive(true);
                if (enemiesSpawned >= enemyToSpawn.AmountToSpawn)
                {
                    allEnemiesSpawned = true;
                }
                return;
            }
        }
    }

    public void SetEnemyToSpawn(EnemyToSpawn enemyToSpawn)
    {
        this.enemyToSpawn = enemyToSpawn;
       
    }

    private void ResetPool()
    {
        allEnemiesKilled = false;
        allEnemiesSpawned = false;
        enemiesKilled = 0;
        enemiesSpawned = 0;
    }

    private void PopulatePool()
    {
        if (!enemyToSpawn.EnemyDataReference)
        {
            return;
        }

        for (int i = 0; i < enemyToSpawn.AmountToSpawn; i++)
        {
            EnemyController newEnemy = Instantiate(enemyToSpawn.EnemyDataReference.EnemyPrefab, transform.position, Quaternion.identity, transform);
            newEnemy.SetEnemyPath(pooledEnemiesPath);
            newEnemy.onEnemyDie += OnEnemyDie;
            newEnemy.gameObject.SetActive(false);
            enemies.Add(newEnemy);
        }
    }

    private void OnEnemyDie()
    {
        enemiesKilled++;
        if (enemiesKilled >= enemyToSpawn.AmountToSpawn)
        {
            allEnemiesKilled = true;
        }
    }

    private IEnumerator CanActivateRespawnRoutine()
    {
        canActivate = false;
        yield return new WaitForSeconds(enemyToSpawn.TimeBetweenSpawn);
        canActivate = true;
    }
}
