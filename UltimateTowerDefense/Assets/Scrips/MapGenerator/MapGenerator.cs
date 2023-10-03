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
    private Dictionary<Vector2Int, Node> myNodeGrid = new Dictionary<Vector2Int, Node>();


    private Stronghold myStronghold;
    private Node myStrongholdNode;

    private Path[] paths;

    private void Awake()
    {        
        InitNodesGrid();  
    }

    private void InitNodesGrid()
    {
        for (int i = 0; i < gridDimension.x; i++)
        {
            for (int j = 0; j < gridDimension.y; j++)
            {               
                Node newNode = new Node(new Vector2Int(i, j));
            }
        }
        InitStrongholdNode();        
        InitPaths();

       

    }

    private void InitStrongholdNode()
    {
        Vector2Int strongholdCoordinates = new Vector2Int();
        strongholdCoordinates.x = Random.Range(0 + tilesFromBorder, gridDimension.x - tilesFromBorder);
        strongholdCoordinates.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder);
        if (!myNodeGrid.ContainsKey(strongholdCoordinates))
        {
            myStrongholdNode = new Node(strongholdCoordinates);
            myStrongholdNode.isFree = false;
            myStrongholdNode.content = NodeContent.Stronghold;
            myNodeGrid.TryAdd(strongholdCoordinates, myStrongholdNode);
        }
        else
        {
            myStrongholdNode = myNodeGrid[strongholdCoordinates];
            myNodeGrid[strongholdCoordinates].isFree = false;
            myNodeGrid[strongholdCoordinates].content = NodeContent.Stronghold;
        }
      
    }

    private void InitPaths()
    {
        paths = new Path[amountOfPaths];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = new Path();
        }
        SetPathsCoordinates(GeneratePathRandomUbication(amountOfPaths));        

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

            paths[i].startNode = new Node(randomStarCoordinate);
            paths[i].startNode.content = NodeContent.Path;
            myNodeGrid.TryAdd(randomStarCoordinate, paths[i].startNode);
            paths[i].destinationNode = new Node(myStrongholdNode.coordinates);
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
