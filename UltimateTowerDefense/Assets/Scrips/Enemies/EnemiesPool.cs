using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [Header("Enemies Pool")]
    [SerializeField] private EnemyData enemyReference;
    [SerializeField,Min(0)] private int amountOfEnemiesToPool = 1;
    [SerializeField,Min(0f)] private float spawnTime = 1f;
    [SerializeField] private bool canSpawn;

    private List<EnemyController> enemies = new List<EnemyController>();
    private Vector2Int startCoordinates;
    private List<Tile> pooledEnemiesPath;

    private bool canActivate = true;


    private void Start()
    {
        startCoordinates = GridMananger.Instance.GetCoordinatesFromPosition(transform.position);
        pooledEnemiesPath = Pathfinder.Instance.GetNewPath(startCoordinates);
        PopulatePool();
    }

    private void Update()
    {
        ActivateEnemy();
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

        foreach (EnemyController enemy in enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                canActivate = false;
                StartCoroutine(CanActivateRespawnRoutine());
                enemy.gameObject.SetActive(true);
                return;
            }
        }
    }

    private void PopulatePool()
    {
        for (int i = 0; i < amountOfEnemiesToPool; i++)
        {
            EnemyController newEnemy = Instantiate(enemyReference.EnemyPrefab, transform.position, Quaternion.identity, transform);
            newEnemy.SetEnemyPath(pooledEnemiesPath);
            newEnemy.gameObject.SetActive(false);
            enemies.Add(newEnemy);
        }
    }

    

    private IEnumerator CanActivateRespawnRoutine()
    {
        yield return new WaitForSeconds(spawnTime);
        canActivate = true;
    }
}
