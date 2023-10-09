using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridMananger : Singleton<GridMananger>
{
    public struct MapTresholds
    {
        public float minX, maxX, minZ, maxZ;
    }

    private Dictionary<Vector2Int, Tile> mapGrid = new Dictionary<Vector2Int, Tile>();

    private MapTresholds myTresholds = new MapTresholds();   
    public MapTresholds MyThresholds { get { return myTresholds; } }


    private void Start()
    {
        StoreTiles();
        SetTresholds();
    }

    private void SetTresholds()
    {
        float minX=0, maxX=0, minZ=0, maxZ=0;
        foreach (KeyValuePair<Vector2Int,Tile> tile in mapGrid)
        {
            if (tile.Value.transform.position.x < minX)
            {
                minX = tile.Value.transform.position.x;
            }

            if (tile.Value.transform.position.x > maxX)
            {
                maxX = tile.Value.transform.position.x;
            }

            if (tile.Value.transform.position.z < minZ)
            {
                minZ = tile.Value.transform.position.z;
            }

            if (tile.Value.transform.position.z > maxZ)
            {
                maxZ = tile.Value.transform.position.z;
            }
        }

        myTresholds.minX = minX;
        myTresholds.maxX = maxX;
        myTresholds.minZ = minZ;
        myTresholds.maxZ = maxZ;
    }

    private void StoreTiles()
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
