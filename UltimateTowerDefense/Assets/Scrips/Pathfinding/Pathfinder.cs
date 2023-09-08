using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : Singleton<Pathfinder>
{
    [Header("Pathfinder")]
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;
    [SerializeField] private int tileSize = 5;

    private Tile startTile;
    private Tile destinationTile;
    private Tile currentSearchTile;

    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    private GridMananger gridMananger;

    private Dictionary<Vector2Int, Tile> mapGrid = new Dictionary<Vector2Int, Tile>();
    private Dictionary<Vector2Int, Tile> tilesReached = new Dictionary<Vector2Int, Tile>();
    private Queue<Tile> frontier = new Queue<Tile>();

    private void Awake()
    {
        gridMananger = GetComponent<GridMananger>();

        if (gridMananger)
        {
            mapGrid = gridMananger.MapGrid;
            startTile = mapGrid[startCoordinates];
            destinationTile = mapGrid[destinationCoordinates];
        }

    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Tile> GetNewPath()
    {
        gridMananger.UpdateTiles();
        BreadthFirstSearch(startCoordinates);
        return BuildPath();
    }

    public List<Tile> GetNewPath(Vector2Int coordinates)
    {
        gridMananger.UpdateTiles();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }


    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        startTile.isWalkable = true;
        destinationTile.isWalkable = true;

        frontier.Clear();
        tilesReached.Clear();

        bool isRunning = true;

        frontier.Enqueue(mapGrid[coordinates]);
        tilesReached.Add(coordinates, mapGrid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchTile = frontier.Dequeue();
            currentSearchTile.isExplored = true;
            ExploreNeighbors();
            if (currentSearchTile.Coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private void ExploreNeighbors()
    {
        if (currentSearchTile == null)
        {
            return;
        }

        List<Tile> neighbors = new List<Tile>();

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int neighborCoordinates = currentSearchTile.Coordinates + directions[i];

            if (mapGrid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(mapGrid[neighborCoordinates]);
            }
        }

        foreach (Tile neightbor in neighbors)
        {
            if (!tilesReached.ContainsKey(neightbor.Coordinates) && neightbor.isWalkable)
            {
                neightbor.connectedTo = currentSearchTile;
                tilesReached.Add(neightbor.Coordinates, mapGrid[neightbor.Coordinates]);
                frontier.Enqueue(mapGrid[neightbor.Coordinates]);
            }
        }
    }
    private List<Tile> BuildPath()
    {
        List<Tile> path = new List<Tile>();
        Tile currentTile = destinationTile;

        path.Add(currentTile);
        currentTile.isPath = true;

        while (currentTile.connectedTo != null)
        {
            currentTile = currentTile.connectedTo;
            currentTile.isPath = true;
            path.Add(currentTile);
        }

        path.Reverse();

        return path;

    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (mapGrid.ContainsKey(coordinates))
        {
            bool previusState = mapGrid[coordinates].isWalkable;

            mapGrid[coordinates].isWalkable = false;
            List<Tile> newPath = GetNewPath();
            mapGrid[coordinates].isWalkable = previusState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        return new Vector3(coordinates.x * tileSize, 0, coordinates.y * tileSize);
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x / tileSize), Mathf.RoundToInt(position.z / tileSize));
    }
}
