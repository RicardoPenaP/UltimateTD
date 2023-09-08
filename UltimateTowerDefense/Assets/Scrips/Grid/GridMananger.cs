using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMananger : Singleton<GridMananger>
{
    [Header("Grid Mananger")]
    [SerializeField] private int tileSize = 5;

    private Dictionary<Vector2Int, Tile> mapGrid = new Dictionary<Vector2Int, Tile>();

    public int TileSize { get { return tileSize; } }
    public Dictionary<Vector2Int, Tile> MapGrid { get { return mapGrid; } }

    protected override void Awake()
    {
        base.Awake();
        UpdateTiles();
    }

    public void UpdateTiles()
    {
        Tile[] tiles = transform.GetComponentsInChildren<Tile>();

        foreach (Tile tile in tiles)
        {
            if (!mapGrid.ContainsKey(tile.Coordinates))
            {
                mapGrid.Add(tile.Coordinates, tile);                
            }
        }
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
