using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Generator")]

    [Header("Size Settings")]
    [SerializeField] float gridSize;
    [SerializeField] Vector2Int gridDimension;

    [Header("Manangers")]
    [SerializeField] GridMananger gridManangerPrefab;

    [Header("Stronghold")]
    [SerializeField] Stronghold strongholdReference;
    [Tooltip("The minimum amount of tiles from the borders that the stronghold possible can be positioned")]
    [SerializeField,Min(0)] int tilesFromBorder;

    [Header("Tiles Reference")] 
    [SerializeField] Tile defaultTilePrefab;
    [SerializeField] Tile pathTile;

    [Header("Path Settings")]
    [SerializeField, Range(1, 4)] private int amountOfPaths = 1;

    private GridMananger myGridMananger;
    private Dictionary<Vector2Int, Tile> myGrid = new Dictionary<Vector2Int, Tile>();

    private Stronghold myStronghold;

    private void Awake()
    {
        InitGrid();
        InitStronghold();
        InitPaths();
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

    public void InitStronghold()
    {
        Vector2Int gridPosition = new Vector2Int();
        gridPosition.x = Random.Range(0 + tilesFromBorder, gridDimension.x - tilesFromBorder);
        gridPosition.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder);


        Vector3 strongholdPos = myGrid[gridPosition].GetPosition();

        myStronghold = Instantiate(strongholdReference,strongholdPos,Quaternion.identity);
        myGrid[gridPosition].TileStatus = TileStatusID.Occuped;
    }

    public void InitPaths()
    {
       
    }

}
