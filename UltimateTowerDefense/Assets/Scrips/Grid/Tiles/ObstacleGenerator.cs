using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("Obstacle Generator")]
    [SerializeField] private GameObject[] obtaclePrefabs;
    [SerializeField,Range(0f,1f)] private float instantiationProbability = 0.25f;

    private TileManangement myTileManangement;
    private Tile myTile;

    private void Awake()
    {
        myTileManangement = GetComponentInParent<TileManangement>();
        myTile = GetComponent<Tile>();
        myTileManangement.OnCleanObstacles += CleanObstacles;
    }

    private void Start()
    {
        if (myTile.TileStatus == TileStatusID.HasObstacle)
        {           
            InstantiateObstacles();
        }
    }

    private void OnDestroy()
    {
        myTileManangement.OnCleanObstacles -= CleanObstacles;
    }

    private void InstantiateObstacles()
    {
        foreach (Transform decorationPoint in transform)
        {
            if (Random.value > instantiationProbability)
            {
                myTileManangement.SetAmounOfObstacles(myTileManangement.AmountOfObstacles + 1);
                int itemToInstantiate = Random.Range(0, obtaclePrefabs.Length);
                Instantiate(obtaclePrefabs[itemToInstantiate], decorationPoint.transform.position, Quaternion.Euler(0, Random.Range(0f, 360f), 0), decorationPoint);
            }    
        }
    }

    private void CleanObstacles()
    {
        foreach (Transform sons in transform)
        {
            sons.gameObject.SetActive(false);
        }        
    }

}
