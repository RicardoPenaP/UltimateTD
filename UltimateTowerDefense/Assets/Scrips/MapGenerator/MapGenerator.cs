using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Generator")]

    [Header("Size Settings")]
    [SerializeField] public static readonly float gridSize = 5;
    [SerializeField] private Vector2Int gridDimension;

    [Header("Manangers")]
    [SerializeField] private GridMananger gridManangerPrefab;

    [Header("Stronghold")]
    [SerializeField] private Stronghold strongholdReference;
    [Tooltip("The minimum amount of tiles from the borders that the stronghold possible can be positioned")]
    [SerializeField,Min(0)] private int tilesFromBorder;

    [Header("Tiles Reference")] 
    [SerializeField] private Tile defaultTilePrefab;
    [SerializeField] private Tile pathTile;

    [Header("Path Settings")]
    [SerializeField, Range(1, 4)] private int amountOfPaths = 1;

    private GridMananger myGridMananger;
    private Dictionary<Vector2Int, Tile> myGrid = new Dictionary<Vector2Int, Tile>();

    private Stronghold myStronghold;

    private Path[] paths;

    private void Awake()
    {        
        InitGrid();  
    }

    private void InitGrid()
    {
        InitStronghold();
        myGridMananger = Instantiate(gridManangerPrefab, transform.position, Quaternion.identity);
        InitPaths();

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

        BlockStrongholdCoordinates();
    }

    private void InitStronghold()
    {
        Vector2Int strongholdCoordinates = new Vector2Int();
        strongholdCoordinates.x = Random.Range(0 + tilesFromBorder, gridDimension.x - tilesFromBorder);
        strongholdCoordinates.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder);


        Vector3 strongholdPos = CoordinatesToPosition(strongholdCoordinates);

        myStronghold = Instantiate(strongholdReference,strongholdPos,Quaternion.identity);
        //myStronghold.Coordinates=strongholdCoordinates;
    }

    private void BlockStrongholdCoordinates()
    {
        Vector2Int strongholdCoordinates = PositionToCoordinates(myStronghold.transform.position);        
        myGrid[strongholdCoordinates].TileStatus = TileStatusID.Occuped;
    }

    private void InitPaths()
    {
        paths = new Path[amountOfPaths];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = new Path();
        }
        SetPathsCoordinates(GeneratePathRandomUbication(amountOfPaths));
        SetPathsTiles();

    }

    private Path.PathUbication[] GeneratePathRandomUbication(int amount)
    {
        Path.PathUbication[] randomUbications = new Path.PathUbication[amount];

        for (int i = 0; i < randomUbications.Length; i++)
        {
            randomUbications[i] = (Path.PathUbication)Random.Range(1, 5);
            for (int j = 0; j < randomUbications.Length; j++)
            {
                if (j != i)
                {
                    if (randomUbications[i] == randomUbications[j])
                    {
                        randomUbications[i] = (Path.PathUbication)Random.Range(1, 5);
                        j = -1;
                    }
                }
            }
        }
        return randomUbications;
    }

    private void SetPathsCoordinates(Path.PathUbication[] randomUbication)
    {
        for (int i = 0; i < paths.Length; i++)
        {            
            Vector2Int randomStarCoordinate = new Vector2Int();
            paths[i].ubication = randomUbication[i];
            switch (randomUbication[i])
            {               
                case Path.PathUbication.North:
                    randomStarCoordinate.x = Random.Range(0 + tilesFromBorder, gridDimension.x - tilesFromBorder);
                    randomStarCoordinate.y = gridDimension.y - 1;
                    break;
                case Path.PathUbication.South:
                    randomStarCoordinate.x = Random.Range(0 + tilesFromBorder, gridDimension.x - tilesFromBorder);
                    randomStarCoordinate.y = 0;
                    break;
                case Path.PathUbication.East:
                    randomStarCoordinate.x = 0;
                    randomStarCoordinate.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder); 
                    break;
                case Path.PathUbication.West:
                    randomStarCoordinate.x = gridDimension.x - 1;
                    randomStarCoordinate.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder);
                    break;
                default:
                    break;
            }

            paths[i].startCoordinates = randomStarCoordinate;
            paths[i].startPosition = CoordinatesToPosition(randomStarCoordinate);
            paths[i].destinationCoordinates = myStronghold.Coordinates;
        }
    }

    private void SetPathsTiles()
    {
        foreach (Path path in paths)
        {
            Tile pathStarPoint = Instantiate(pathTile, path.startPosition, Quaternion.identity, myGridMananger.transform.GetChild(0));
            if (!myGrid.ContainsKey(path.startCoordinates))
            {
                myGrid.Add(path.startCoordinates, pathStarPoint);
            }
        }
    }

    public static Vector3 CoordinatesToPosition(Vector2Int coordinates)
    {
        return new Vector3(coordinates.x * gridSize,0, coordinates.y * gridSize);
    }

    public static Vector2Int PositionToCoordinates(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x/gridSize), Mathf.RoundToInt(position.z/ gridSize));
    }
}
