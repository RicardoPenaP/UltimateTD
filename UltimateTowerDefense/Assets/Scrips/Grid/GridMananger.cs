using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMananger : MonoBehaviour
{
    [Header("Grid Mananger")]

    [SerializeField] private Dictionary<Vector2Int, Tile> mapGrid = new Dictionary<Vector2Int, Tile>();

    private void Awake()
    {
        Tile[] tiles = transform.GetComponentsInChildren<Tile>(); 

        foreach (Tile tile in tiles)
        {
            if (!mapGrid.ContainsKey(tile.TileCoordinates))
            {
                mapGrid.Add(tile.TileCoordinates, tile);
                Debug.Log(tile.TileCoordinates);
            }
        }
    }
}
