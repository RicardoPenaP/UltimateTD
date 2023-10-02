using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Generator")]

    [Header("Size Settings")]
    [SerializeField] float gridSize;
    [SerializeField] Vector2Int gridDimension;

    [Header("Manangers Reference")]
    [SerializeField] GridMananger gridManangerPrefab;

    [Header("Stronghold Reference")]
    [SerializeField] GameObject strongholdReference;

    [Header("Tiles Reference")] 
    [SerializeField] Tile defaultTilePrefab;

    private GridMananger myGridMananger;
    private Dictionary<Vector2Int, Tile> myGrid = new Dictionary<Vector2Int, Tile>();



    private void Awake()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        myGridMananger = Instantiate(gridManangerPrefab, transform.position, Quaternion.identity);        
        for (int i = 0; i < gridDimension.x; i++)
        {
            for (int j = 0; j < gridDimension.y; j++)
            {
                float x = i * gridSize;
                float z = j * gridSize;
                Tile newTile = Instantiate(defaultTilePrefab, new Vector3(x, 0,z), Quaternion.identity, myGridMananger.transform.GetChild(1));

                if (!myGrid.ContainsKey(newTile.Coordinates))
                {
                    myGrid.Add(newTile.Coordinates, newTile);
                }
            }
        }
    }

    private void InitStronghold()
    {

    }

}
