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

    [Header("Size Settings")]
    [SerializeField] public static readonly float gridSize = 5;
    [SerializeField] private Vector2Int gridDimension;

    [Header("Perlin Noise Settings")]
    [SerializeField] private PerlinNodeContent[] contentLevels;
    [SerializeField] private float seed;
    [SerializeField] private float scale;
    
    [Header("Manangers")]
    [SerializeField] private GridMananger gridManangerPrefab;

    [Header("Stronghold")]
    [SerializeField] private Stronghold strongholdReference;
    [Tooltip("The minimum amount of tiles from the borders that the stronghold possible can be positioned")]
    [SerializeField,Min(0)] private int tilesFromBorder;    

    [Header("Path Settings")]
    [SerializeField, Range(1, 4)] private int amountOfPaths = 1;

    [Header("Tiles Reference")]
    [SerializeField] private Tile grassDefaultTile;
    [SerializeField] private Tile pathDefaultTile;
    [SerializeField] private Tile iceDefaultTile;
    [SerializeField] private Tile waterDefaultTile;
    [SerializeField] private Tile treesDefaultTile;
    [SerializeField] private Tile sandDefaultTile;
    [SerializeField] private Tile rocksDefaultTile;

    private GridMananger myGridMananger;    
    private Dictionary<Vector2Int, Node> myNodeGrid = new Dictionary<Vector2Int, Node>();


    private Stronghold myStronghold;
    private Node myStrongholdNode;

    private Path[] paths;

    private void Awake()
    {        
        InitNodesGrid();        
        InitTilesGrid();
    }

    private void InitNodesGrid()
    {
        for (int i = 0; i < gridDimension.x; i++)
        {
            for (int j = 0; j < gridDimension.y; j++)
            {               
                Node newNode = new Node(new Vector2Int(i, j));
                newNode.content = NodeContent.Grass;
                myNodeGrid.Add(newNode.coordinates, newNode);
            }
        }
        InitStrongholdNode();        
        InitPaths();       

    }

    private NodeContent GetRandomContent(float value)
    {
        for (int i = 0; i < contentLevels.Length; i++)
        {
            if (value < contentLevels[i].level)
            {
                return contentLevels[i].content;
            }

        }
        return NodeContent.None;
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
                    randomStarCoordinate.x = gridDimension.x - 1;
                    randomStarCoordinate.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder); 
                    break;
                case Path.PathUbication.West:
                    randomStarCoordinate.x = 0;
                    randomStarCoordinate.y = Random.Range(0 + tilesFromBorder, gridDimension.y - tilesFromBorder);
                    break;
                default:
                    break;
            }

            paths[i].startCoordinates = randomStarCoordinate;            
        }
    }

    private void SetPathDestinationCoordinates()
    {
        foreach (Path path in paths)
        {
            switch (path.ubication)
            {               
                case Path.PathUbication.North:
                    path.destinationCoordinates=myStrongholdNode.coordinates + Vector2Int.up;
                    break;
                case Path.PathUbication.South:
                    path.destinationCoordinates = myStrongholdNode.coordinates + Vector2Int.down;
                    break;
                case Path.PathUbication.East:
                    path.destinationCoordinates = myStrongholdNode.coordinates + Vector2Int.right;
                    break;
                case Path.PathUbication.West:
                    path.destinationCoordinates = myStrongholdNode.coordinates + Vector2Int.left;
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
                    myNodeGrid[node.coordinates].content = node.content;
                    myNodeGrid[node.coordinates].isFree = false;
                    myNodeGrid[node.coordinates].isPath = true;
                }
            }           
        }

    }

    private void InitTilesGrid()
    {       
        myGridMananger = Instantiate(gridManangerPrefab, transform.position, Quaternion.identity);
        foreach (KeyValuePair<Vector2Int,Node> keyValue in myNodeGrid)
        {
            switch (keyValue.Value.content)
            {               
                case NodeContent.Grass:
                    Instantiate(grassDefaultTile, keyValue.Value.position, Quaternion.identity, myGridMananger.transform.GetChild(1));
                    break;
                case NodeContent.Ice:
                    Instantiate(iceDefaultTile, keyValue.Value.position, Quaternion.identity, myGridMananger.transform.GetChild(1));
                    break;
                case NodeContent.Water:
                    Instantiate(waterDefaultTile, keyValue.Value.position, Quaternion.identity, myGridMananger.transform.GetChild(1));
                    break;
                case NodeContent.Trees:
                    Instantiate(treesDefaultTile, keyValue.Value.position, Quaternion.identity, myGridMananger.transform.GetChild(1));
                    break;
                case NodeContent.Sand:
                    Instantiate(sandDefaultTile, keyValue.Value.position, Quaternion.identity, myGridMananger.transform.GetChild(1));
                    break;
                case NodeContent.Rock:
                    Instantiate(rocksDefaultTile, keyValue.Value.position, Quaternion.identity, myGridMananger.transform.GetChild(1));
                    break;
                case NodeContent.Path:
                    Instantiate(pathDefaultTile, keyValue.Value.position, Quaternion.identity, myGridMananger.transform.GetChild(0));
                    keyValue.Value.isFree = false;
                    keyValue.Value.isPath = true;
                    break;
                case NodeContent.Stronghold:
                    myStronghold = Instantiate(strongholdReference, keyValue.Value.position, Quaternion.identity);
                    keyValue.Value.isFree = false;
                    break;
                case NodeContent.Decoration:
                    break;
                default:
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
