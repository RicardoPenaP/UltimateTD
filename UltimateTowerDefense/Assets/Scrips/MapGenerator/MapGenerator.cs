using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{   
    private enum NeighborLocation { None,Up,Down,Left,Right}
    [Header("Map Generator")]

    [Header("Perling Noise Settings")]
    [SerializeField] private bool randomSeed = false;
    [SerializeField] private float seed = 241197;
    [SerializeField] private Vector2Int offset = Vector2Int.zero;
    [SerializeField] private float scale = 1f;

    [Header("Size Settings")]
    [SerializeField] public static readonly float gridSize = 5;
    [SerializeField] private Vector2Int gridDimension;
    
    [Header("Stronghold Settings")]
    [SerializeField] private Stronghold strongholdPrefab;
    [Tooltip("The minimum amount of tiles from the borders that the stronghold possible can be positioned")]
    [SerializeField, Min(0)] private int tilesFromBorder;
    [SerializeField, Min(0)] private int pathEndTilesDistanceFromCenter = 1;
    [SerializeField, Min(0)] private int wallsDistanceFromCenter = 1;

    [Header("Enemies Path Settings")]
    [SerializeField] private GameObject pathPrefab;
    [Header("Localization Settings")]    
    [Tooltip("If the value is 0 the start point of each Enemy Path will be in the border")]
    [SerializeField, Min(0)] private int startPointMinDistanceFromBorder = 0;
    [Tooltip("If the value is 0 the start point of each Enemy Path will be in the middle betwen the border corners")]
    [SerializeField, Min(0)] private int startPointMinDistanceFromCorners = 0;   

    [Header("Path Rules Tiles")]
    [Header("Star and Fish")]
    [SerializeField] private GameObject startFinishUp;
    [SerializeField] private GameObject startFinishDown;
    [SerializeField] private GameObject startFinishRight;
    [SerializeField] private GameObject startFinishLeft;
    [Header("Straigh")]
    [SerializeField] private GameObject straightVertical;
    [SerializeField] private GameObject straightHorizontal;
    [Header("Corners")]
    [SerializeField] private GameObject upperLeftCorner;
    [SerializeField] private GameObject upperRightCorner;
    [SerializeField] private GameObject lowerLeftCorner;
    [SerializeField] private GameObject lowerRightCorner;

    [Header("Tiles Settings")]
    [SerializeField] private PerlinNoiseTileRange[] tilesRanges;
    [Header("Tiles Reference")]    
    [SerializeField] private Tile lightGrassTile;
    [SerializeField] private Tile darkGrassTile;    
    [SerializeField] private Tile mudTile;
    [SerializeField] private Tile sandTile;
    [SerializeField] private Tile waterTile;

    private Dictionary<Vector2Int, Node> myNodeGrid = new Dictionary<Vector2Int, Node>();

    private Node myStrongholdNode;    

    private Path[] enemiesPaths;

    private bool tilesCanBeInstatiated = false;
    private int amountOfPaths = 1;

    private void Awake()
    {        
        InitNodesGrid();
    }

    private void InitNodesGrid()
    {
        amountOfPaths = (int)GameMode.GameModeOption;        
        if (randomSeed)
        {
            seed = Random.Range(0.000f, 999999f);
        }

        myNodeGrid = PerlingNoiseGenerator.GenerateRandomNodesGrid(gridDimension,scale,seed,offset,tilesRanges);
       
        InitStrongholdNode();        
        InitEnemiesPaths();
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
            myNodeGrid.TryAdd(strongholdCoordinates, myStrongholdNode);
        }
        else
        {
            myStrongholdNode = myNodeGrid[strongholdCoordinates];           
        }
        myNodeGrid[strongholdCoordinates].isFree = false;
        myNodeGrid[strongholdCoordinates].Content = NodeContent.Stronghold;
        myNodeGrid[strongholdCoordinates].isWalkable = false;

        for (int x = myStrongholdNode.Coordinates.x - wallsDistanceFromCenter; x < myStrongholdNode.Coordinates.x + wallsDistanceFromCenter; x++)
        {
            for (int y = myStrongholdNode.Coordinates.y - wallsDistanceFromCenter; y < myStrongholdNode.Coordinates.y + wallsDistanceFromCenter; y++)
            {
                myNodeGrid[new Vector2Int(x, y)].isFree = false;
                myNodeGrid[strongholdCoordinates].isWalkable = false;
                myNodeGrid[new Vector2Int(x, y)].Content = NodeContent.Stronghold;
            }
        }
    }

    private void InitEnemiesPaths()
    {        
        enemiesPaths = new Path[amountOfPaths];
        for (int i = 0; i < enemiesPaths.Length; i++)
        {
            enemiesPaths[i] = new Path();
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
        for (int i = 0; i < enemiesPaths.Length; i++)
        { 
            enemiesPaths[i].ubication = randomUbication[i];           

            enemiesPaths[i].startCoordinates = GetStartRandomCoordinates(randomUbication[i]);           
        }        
    }

    private Vector2Int GetStartRandomCoordinates(Path.PathUbication randomUbication)
    {
        Vector2Int randomCoordinates = new Vector2Int();
        int minX, maxX, minY, maxY;
        minX = 0;
        maxX = 0;
        minY = 0;
        maxY = 0;
        switch (randomUbication)
        {
            case Path.PathUbication.UpperLeftCorner:
                if (startPointMinDistanceFromCorners == 0)
                {
                    minX = 0;
                    maxX = minX + 1;
                }
                else
                {
                    minX = 0 + startPointMinDistanceFromCorners;
                    maxX = Mathf.RoundToInt( gridDimension.x/2 )- startPointMinDistanceFromCorners;
                }


                if (startPointMinDistanceFromBorder == 0)
                {
                    minY = gridDimension.y - 1;
                    maxY = minY + 1;
                }
                else
                {
                    minY = Mathf.RoundToInt(gridDimension.y / 2) + startPointMinDistanceFromBorder;
                    maxY = gridDimension.y - startPointMinDistanceFromBorder;
                }


                break;

            case Path.PathUbication.LowerLeftCorner:
                if (startPointMinDistanceFromCorners == 0)
                {
                    minX = 0;
                    maxX = minX + 1;
                }
                else
                {
                    minX = 0 + startPointMinDistanceFromCorners;
                    maxX = Mathf.RoundToInt(gridDimension.x / 2) - startPointMinDistanceFromCorners;
                }


                if (startPointMinDistanceFromBorder == 0)
                {
                    minY = 0;
                    maxY = minY + 1;
                }
                else
                {
                    minY = 0 + startPointMinDistanceFromBorder;
                    maxY = Mathf.RoundToInt(gridDimension.y / 2) - startPointMinDistanceFromBorder;
                }

                break;

            case Path.PathUbication.UpperRightCorner:
                if (startPointMinDistanceFromCorners == 0)
                {
                    minX = gridDimension.x - 1;
                    maxX = minX + 1;
                }
                else
                {
                    minX = Mathf.RoundToInt(gridDimension.x / 2) + startPointMinDistanceFromCorners;
                    maxX = gridDimension.x - startPointMinDistanceFromCorners;
                }


                if (startPointMinDistanceFromBorder == 0)
                {
                    minY = gridDimension.y - 1;
                    maxY = minY + 1;
                }
                else
                {
                    minY = Mathf.RoundToInt(gridDimension.y / 2) + startPointMinDistanceFromBorder;
                    maxY = gridDimension.y - startPointMinDistanceFromBorder;
                }

                //if (startPointMinDistanceFromCorners == 0)
                //{
                //    minY = Mathf.RoundToInt(gridDimension.y / 2) - 1;
                //    maxY = minY + 1;
                //}
                //else
                //{
                //    minY = 0 + startPointMinDistanceFromCorners;
                //    maxY = gridDimension.y - startPointMinDistanceFromCorners;
                //}

                break;

            case Path.PathUbication.LowerRightCorner:
                if (startPointMinDistanceFromCorners == 0)
                {
                    minX = gridDimension.x - 1;
                    maxX = minX + 1;
                }
                else
                {
                    minX = Mathf.RoundToInt(gridDimension.x / 2) + startPointMinDistanceFromCorners;
                    maxX = gridDimension.x - startPointMinDistanceFromCorners;
                }

                if (startPointMinDistanceFromBorder == 0)
                {
                    minY = 0;
                    maxY = minY + 1;
                }
                else
                {
                    minY = 0 + startPointMinDistanceFromBorder;
                    maxY = Mathf.RoundToInt(gridDimension.y / 2) - startPointMinDistanceFromBorder;
                }
                //if (startPointMinDistanceFromCorners == 0)
                //{
                //    minY = Mathf.RoundToInt(gridDimension.y / 2) - 1;
                //    maxY = minY + 1;
                //}
                //else
                //{
                //    minY = 0 + startPointMinDistanceFromCorners;
                //    maxY = gridDimension.y - startPointMinDistanceFromCorners;
                //}

                break;
        }

        randomCoordinates.x = Random.Range(minX, maxX);
        randomCoordinates.y = Random.Range(minY, maxY);

        return randomCoordinates;
    }

    private void SetPathDestinationCoordinates()
    {
        foreach (Path path in enemiesPaths)
        {
            switch (path.ubication)
            {               
                case Path.PathUbication.UpperLeftCorner:
                    path.destinationCoordinates=myStrongholdNode.Coordinates + (Vector2Int.up * pathEndTilesDistanceFromCenter);                    
                    break;
                case Path.PathUbication.LowerLeftCorner:
                    path.destinationCoordinates = myStrongholdNode.Coordinates + (Vector2Int.down * pathEndTilesDistanceFromCenter);
                    break;
                case Path.PathUbication.UpperRightCorner:
                    path.destinationCoordinates = myStrongholdNode.Coordinates + (Vector2Int.right * pathEndTilesDistanceFromCenter);
                    break;
                case Path.PathUbication.LowerRightCorner:
                    path.destinationCoordinates = myStrongholdNode.Coordinates + (Vector2Int.left * pathEndTilesDistanceFromCenter);
                    break;
                default:
                    break;
            }            
        }
    }

    private void SetPathNodes()
    {       
        foreach (Path path in enemiesPaths)
        {
            path.nodes = GetNewPath(path);
                        
            tilesCanBeInstatiated = true;

            if (path.nodes != null)
            {
                foreach (Node node in path.nodes)
                {
                    myNodeGrid[node.Coordinates].Content = node.Content;
                    myNodeGrid[node.Coordinates].isFree = false;
                    myNodeGrid[node.Coordinates].isPath = true;
                }
            }
            else
            {
                Debug.Log($"Path not found, seed: {seed}");
                tilesCanBeInstatiated = false;
            }
        }
    }

    private List<Node> GetNewPath(Path path)
    {
        PathGeneratorData myPathData = new PathGeneratorData();
        myPathData.contentOfPathNodes = NodeContent.Path;
        myPathData.nodesGrid = myNodeGrid;
        myPathData.startCoordinates = path.startCoordinates;
        myPathData.destinationCoordinates = path.destinationCoordinates;

        return AStarPathGenerator.FindPath(myPathData);
    }

    private void InitTiles()
    {
        if (!tilesCanBeInstatiated)
        {
            randomSeed = true;
            InitNodesGrid();
            return;
        }

        Tile instantiatedTile = null;
        foreach (KeyValuePair<Vector2Int,Node> node in myNodeGrid)
        {
            switch (node.Value.TileType)
            {
                case NodeTileType.LightGrass:
                    instantiatedTile = Instantiate(lightGrassTile, node.Value.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(1));
                    break;
                case NodeTileType.DarkGrass:
                    instantiatedTile = Instantiate(darkGrassTile, node.Value.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(1));
                    break;               
                case NodeTileType.Mud:
                    instantiatedTile = Instantiate(mudTile, node.Value.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(1));
                    break;
                case NodeTileType.Sand:
                    instantiatedTile = Instantiate(sandTile, node.Value.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(1));
                    instantiatedTile.TileStatus = TileStatusID.Occuped;
                    break;
                case NodeTileType.Water:
                    instantiatedTile = Instantiate(waterTile, node.Value.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(1));
                    instantiatedTile.TileStatus = TileStatusID.Occuped;
                    break;
                default:
                    break;
            }

            switch (node.Value.Content)
            {
                case NodeContent.Path:
                    instantiatedTile.TileStatus = TileStatusID.IsPath;
                    break;
                case NodeContent.Obstacle:
                    instantiatedTile.TileStatus = TileStatusID.HasObstacle;
                    break;
                case NodeContent.Stronghold:
                    if (node.Key == myStrongholdNode.Coordinates)
                    {
                        //Instantiate(lightGrassTile, node.Value.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(1));
                        Instantiate(strongholdPrefab, node.Value.Position, Quaternion.identity);
                    }
                    instantiatedTile.TileStatus = TileStatusID.Occuped;
                    break;                
                default:
                   
                    break;
            }

            instantiatedTile.SetTileCoordinates(node.Value.Coordinates);
        }

        InitPathTiles();
    }

    private void InitPathTiles()
    {
        foreach (Path path in enemiesPaths)
        {
            for (int i = 0; i < path.nodes.Count; i++)
            {
                if (i == 0)
                {
                    InstanciatePathTile(path.nodes[i], path.nodes[i + 1]);                    
                }
                else
                {
                    if (i == path.nodes.Count - 1)
                    {
                        InstanciatePathTile(path.nodes[i], null, path.nodes[i - 1]);
                    }
                    else
                    {
                        InstanciatePathTile(path.nodes[i], path.nodes[i + 1], path.nodes[i - 1]);
                    }
                }
            }
        }
    }

    private void InstanciatePathTile(Node currentNode, Node nextNode = null, Node previousNode = null)
    {        
        NeighborLocation previusNeighbor = previousNode != null ? GetNeighborLocation(currentNode.Coordinates, previousNode.Coordinates):NeighborLocation.None;
        NeighborLocation nextNeighbor = nextNode != null ? GetNeighborLocation(currentNode.Coordinates, nextNode.Coordinates): NeighborLocation.None;
        if (nextNode != null && previousNode != null)
        {
            if ((nextNeighbor== NeighborLocation.Right && previusNeighbor == NeighborLocation.Left) || 
                (nextNeighbor == NeighborLocation.Left && previusNeighbor == NeighborLocation.Right))
            {
                Instantiate(straightHorizontal, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
            }

            if ((nextNeighbor == NeighborLocation.Up && previusNeighbor == NeighborLocation.Down) ||
                (nextNeighbor == NeighborLocation.Down && previusNeighbor == NeighborLocation.Up))
            {
                Instantiate(straightVertical, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
            }

            if (previusNeighbor == NeighborLocation.Up)
            {
                if (nextNeighbor == NeighborLocation.Right)
                {
                    Instantiate(lowerLeftCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }

                if (nextNeighbor == NeighborLocation.Left)
                {
                    Instantiate(lowerRightCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }
            }

            if (previusNeighbor == NeighborLocation.Down)
            {
                if (nextNeighbor == NeighborLocation.Right)
                {
                    Instantiate(upperLeftCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }

                if (nextNeighbor == NeighborLocation.Left)
                {
                    Instantiate(upperRightCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }
            }

            if (previusNeighbor == NeighborLocation.Right)
            {
                if (nextNeighbor == NeighborLocation.Down)
                {
                    Instantiate(upperLeftCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }

                if (nextNeighbor == NeighborLocation.Up)
                {
                    Instantiate(lowerLeftCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }
            }

            if (previusNeighbor == NeighborLocation.Left)
            {
                if (nextNeighbor == NeighborLocation.Down)
                {
                    Instantiate(upperRightCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }

                if (nextNeighbor == NeighborLocation.Up)
                {
                    Instantiate(lowerRightCorner, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                }
            }


            return;
        }

        if (nextNode != null)
        {
            switch (nextNeighbor)
            {
                case NeighborLocation.None:
                    break;
                case NeighborLocation.Up:
                    Instantiate(startFinishDown, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                case NeighborLocation.Down:
                    Instantiate(startFinishUp, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                case NeighborLocation.Left:
                    Instantiate(startFinishRight, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                case NeighborLocation.Right:
                    Instantiate(startFinishLeft, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                default:
                    break;
            }
            return;
        }

        if (previousNode != null)
        {
            switch (previusNeighbor)
            {
                case NeighborLocation.None:
                    break;
                case NeighborLocation.Up:
                    Instantiate(startFinishDown, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                case NeighborLocation.Down:
                    Instantiate(startFinishUp, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                case NeighborLocation.Left:
                    Instantiate(startFinishRight, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                case NeighborLocation.Right:
                    Instantiate(startFinishLeft, currentNode.Position, Quaternion.identity, GridMananger.Instance.transform.GetChild(0));
                    break;
                default:
                    break;
            }
            return;
        }
    }

    private NeighborLocation GetNeighborLocation(Vector2Int currentCoordinates, Vector2Int neighborCoordinates)
    {
        if (neighborCoordinates == currentCoordinates + Vector2Int.up)
        {
            return NeighborLocation.Up;
        }

        if (neighborCoordinates == currentCoordinates + Vector2Int.down)
        {
            return NeighborLocation.Down;
        }

        if (neighborCoordinates == currentCoordinates + Vector2Int.left)
        {
            return NeighborLocation.Left;
        }

        if (neighborCoordinates == currentCoordinates + Vector2Int.right)
        {
            return NeighborLocation.Right;
        }
        return NeighborLocation.None;
    }

    public Path[] GetEnemiesPaths()
    {
        return enemiesPaths;
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
