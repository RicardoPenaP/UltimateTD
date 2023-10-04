using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    private struct PerlinNodeContent
    {
        public NodeContent content;
        public float level;
    }

    [Header("Map Generator")]

    [Header("Perling Noise Settings")]
    [SerializeField] private bool randomSeed = false;
    [SerializeField] private float seed = 241197;
    [SerializeField] private Vector2Int offset = Vector2Int.zero;
    [SerializeField] private float scale = 1f;

    [Header("Size Settings")]
    [SerializeField] public static readonly float gridSize = 5;
    [SerializeField] private Vector2Int gridDimension;
    
    [Header("Manangers")]
    [SerializeField] private GridMananger gridManangerPrefab;

    [Header("Stronghold")]
    [SerializeField] private Stronghold strongholdPrefab;
    [Tooltip("The minimum amount of tiles from the borders that the stronghold possible can be positioned")]
    [SerializeField,Min(0)] private int tilesFromBorder;    

    [Header("Path Settings")]
    [SerializeField, Range(1, 4)] private int amountOfPaths = 1;

    [Header("Tiles Settings")]
    [SerializeField] private PerlinNoiseTileRange[] tilesRanges;
    [Header("Tiles Reference")]
    [SerializeField] private Tile pathTile;
    [SerializeField] private Tile lightGrassTile;
    [SerializeField] private Tile darkGrassTile;
    [SerializeField] private Tile snowTile;
    [SerializeField] private Tile mudTile;
    [SerializeField] private Tile sandTile;
    [SerializeField] private Tile waterTile;

    private Dictionary<Vector2Int, Node> myNodeGrid = new Dictionary<Vector2Int, Node>();

    private Node myStrongholdNode;

    private Path[] paths;

    private void Awake()
    {        
        InitNodesGrid();
    }

    private void InitNodesGrid()
    {
        if (randomSeed)
        {
            seed = Random.Range(0.000f, 999999f);
        }

        myNodeGrid = PerlingNoiseGenerator.GenerateRandomNodesGrid(gridDimension,scale,seed,offset,tilesRanges);
       
        InitStrongholdNode();        
        InitPaths();
        InitTiles();
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
            myStrongholdNode.Content = NodeContent.Stronghold;
            myNodeGrid.TryAdd(strongholdCoordinates, myStrongholdNode);
        }
        else
        {
            myStrongholdNode = myNodeGrid[strongholdCoordinates];
            myNodeGrid[strongholdCoordinates].isFree = false;
            myNodeGrid[strongholdCoordinates].Content = NodeContent.Stronghold;
        }
      
    }

    private void InitPaths()
    {
        paths = new Path[amountOfPaths];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = new Path();
        }
        SetPathsStartCoordinates(GeneratePathRandomUbication(amountOfPaths));
        SetPathDestinationCoordinates();
        SetPathNodes();
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

    private void SetPathsStartCoordinates(Path.PathUbication[] randomUbication)
    {
        for (int i = 0; i < paths.Length; i++)
        {            
            Vector2Int randomStarCoordinates = new Vector2Int();
            paths[i].ubication = randomUbication[i];
            switch (randomUbication[i])
            {               
                case Path.PathUbication.North:
                    randomStarCoordinates.x = Random.Range(0 + tilesFromBorder, gridDimension.x - tilesFromBorder);
                    randomStarCoordinates.y = gridDimension.y - 1;
                    break;
                case Path.PathUbication.South:
                    randomStarCoordinates.x = Random.Range(0 + tilesFromBorder, gridDimension.x - tilesFromBorder);
                    randomStarCoordinates.y = 0;
                    break;
                case Path.PathUbication.East:
                    randomStarCoordinates.x = gridDimension.x - 1;
                    randomStarCoordinates.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder); 
                    break;
                case Path.PathUbication.West:
                    randomStarCoordinates.x = 0;
                    randomStarCoordinates.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder);
                    break;
                default:
                    break;
            }

            paths[i].startCoordinates = randomStarCoordinates;            
        }
    }

    private void SetPathDestinationCoordinates()
    {
        foreach (Path path in paths)
        {
            switch (path.ubication)
            {               
                case Path.PathUbication.North:
                    path.destinationCoordinates=myStrongholdNode.Coordinates + Vector2Int.up;                    
                    break;
                case Path.PathUbication.South:
                    path.destinationCoordinates = myStrongholdNode.Coordinates + Vector2Int.down;
                    break;
                case Path.PathUbication.East:
                    path.destinationCoordinates = myStrongholdNode.Coordinates + Vector2Int.right;
                    break;
                case Path.PathUbication.West:
                    path.destinationCoordinates = myStrongholdNode.Coordinates + Vector2Int.left;
                    break;
                default:
                    break;
            }            
        }
    }

    private void SetPathNodes()
    {
        PathGeneratorData myPathData = new PathGeneratorData();

        myPathData.contentOfPathNodes = NodeContent.Path;
        foreach (Path path in paths)
        {
            myPathData.startCoordinates = path.startCoordinates;
            myPathData.destinationCoordinates = path.destinationCoordinates;
            myPathData.nodesGrid = myNodeGrid;
            path.nodes = PathGenerator.GetNewPath(myPathData);
            if (path.nodes != null)
            {
                foreach (Node node in path.nodes)
                {
                    myNodeGrid[node.Coordinates].Content = node.Content;
                    myNodeGrid[node.Coordinates].isFree = false;
                    myNodeGrid[node.Coordinates].isPath = true;
                }
            }
        }

    }

    private void InitTiles()
    {
        GridMananger gridMananger = Instantiate(gridManangerPrefab, transform.position, Quaternion.identity);
        foreach (KeyValuePair<Vector2Int,Node> node in myNodeGrid)
        {
            switch (node.Value.Content)
            {               
                case NodeContent.Path:
                    Instantiate(pathTile,node.Value.Position,Quaternion.identity, gridMananger.transform.GetChild(0));
                    break;
                case NodeContent.Stronghold:
                    Instantiate(lightGrassTile, node.Value.Position, Quaternion.identity, gridMananger.transform.GetChild(1));
                    Instantiate(strongholdPrefab, node.Value.Position, Quaternion.identity);
                    break;                
                default:
                    switch (node.Value.TileType)
                    {
                        case NodeTileType.LightGrass:
                            Instantiate(lightGrassTile, node.Value.Position, Quaternion.identity, gridMananger.transform.GetChild(1));
                            break;
                        case NodeTileType.DarkGrass:
                            Instantiate(darkGrassTile, node.Value.Position, Quaternion.identity, gridMananger.transform.GetChild(1));
                            break;
                        case NodeTileType.Snow:
                            Instantiate(snowTile, node.Value.Position, Quaternion.identity, gridMananger.transform.GetChild(1));
                            break;
                        case NodeTileType.Mud:
                            Instantiate(mudTile, node.Value.Position, Quaternion.identity, gridMananger.transform.GetChild(1));
                            break;
                        case NodeTileType.Sand:
                            Instantiate(sandTile, node.Value.Position, Quaternion.identity, gridMananger.transform.GetChild(1));
                            break;
                        case NodeTileType.Water:
                            Instantiate(waterTile, node.Value.Position, Quaternion.identity, gridMananger.transform.GetChild(1));
                            break;
                        default:
                            break;
                    }
                    break;
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
