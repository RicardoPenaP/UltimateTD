using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMananger : MonoBehaviour
{
    [Header("Grid Mananger")]

    [SerializeField] private Dictionary<Vector2Int, Tile> mapGrid = new Dictionary<Vector2Int, Tile>();

    public Dictionary<Vector2Int, Tile> MapGrid { get { return mapGrid; } }

    private void Awake()
    {
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

    
}
