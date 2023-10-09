using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("Obstacle Generator")]
    [SerializeField] private GameObject[] obtaclePrefabs;
    [SerializeField,Range(0f,1f)] private float instantiationProbability = 0.25f;

    private Tile myTile;    

    private void Awake()
    {
        myTile = GetComponentInParent<Tile>();
    }

    private void Start()
    {
        if (myTile.hasObstacle)
        {           
            InstantiateObstacles();
        }
    }

    private void InstantiateObstacles()
    {
        foreach (Transform decorationPoint in transform)
        {
            if (Random.value > instantiationProbability)
            {
                int itemToInstantiate = Random.Range(0, obtaclePrefabs.Length);
                Instantiate(obtaclePrefabs[itemToInstantiate], decorationPoint.transform.position, Quaternion.Euler(0, Random.Range(0f, 360f), 0), decorationPoint);
            }    
        }
    }

}
