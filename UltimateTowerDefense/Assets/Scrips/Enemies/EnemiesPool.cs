using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private EnemyData enemyToSpawn;

    private List<EnemyController> enemiesPooled = new List<EnemyController>();
    private Vector2Int startCoordinates;
    private List<Tile> pooledEnemiesPath;

    private bool allEnemiesKilled = false;
    private bool canActivate = true;
    private float timeBetweenSpawn = 0f;
    private int enemiesSpawned = 0;
    private int enemiesToSpawn = 0;
    private int enemiesKilled = 0;

    public bool CanSpawn { get; set; }
    public bool AllEnemiesKilled { get { return allEnemiesKilled; } }

    private void Awake()
    {
        CanSpawn = false;
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
        if (!CanSpawn)
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
                if (enemiesSpawned >= enemiesToSpawn)
                {                    
                    CanSpawn = false;
                }
                return;
            }
        }
    }

    public void SetEnemyToSpawn(EnemyData enemyToSpawn)
    {
        this.enemyToSpawn = enemyToSpawn;
        gameObject.name = $"{enemyToSpawn.name} Pool";
        ResetPool();
        PopulatePool();        
    }

    public void ResetPool()
    {
        allEnemiesKilled = false;        
        enemiesKilled = 0;
        enemiesSpawned = 0;        
    }

    public void SetAmountOfEnemiesInWave(int amount)
    {
        enemiesToSpawn = amount;
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
        if (enemiesKilled >= enemiesToSpawn)
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
