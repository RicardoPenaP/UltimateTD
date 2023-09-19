using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private EnemyToSpawn enemyToSpawn;

    private List<EnemyController> enemiesPooled = new List<EnemyController>();
    private Vector2Int startCoordinates;
    private List<Tile> pooledEnemiesPath;

    private bool allEnemiesSpawned = false;
    private bool allEnemiesKilled = false;
    private bool canActivate = true;
    private int enemiesSpawned = 0;
    [SerializeField]private int enemiesKilled = 0;

    public bool AllEnemiesKilled { get { return allEnemiesKilled; } }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        startCoordinates = GridMananger.Instance.GetCoordinatesFromPosition(transform.position);
        pooledEnemiesPath = Pathfinder.Instance.GetNewPath(startCoordinates);
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

        foreach (EnemyController enemy in enemiesPooled)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemiesSpawned++;
                StartCoroutine(CanActivateRespawnRoutine());
                enemy.SetEnemyPath(pooledEnemiesPath);
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
        ResetPool();
        PopulatePool();
        StartCoroutine(CanActivateRespawnRoutine());
    }

    private void ResetPool()
    {
        allEnemiesKilled = false;
        allEnemiesSpawned = false;
        enemiesKilled = 0;
        enemiesSpawned = 0;
        foreach (EnemyController enemy in enemiesPooled)
        {
            Destroy(enemy.gameObject);
        }
        enemiesPooled.Clear();
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
            //newEnemy.SetEnemyPath(pooledEnemiesPath);
            newEnemy.onEnemyDie += OnEnemyDie;
            newEnemy.gameObject.SetActive(false);
            enemiesPooled.Add(newEnemy);
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
