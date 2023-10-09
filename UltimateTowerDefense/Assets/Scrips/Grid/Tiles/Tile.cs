using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileStatusID { Free, Occuped}

public class Tile : MonoBehaviour
{
    [Header("Tile")]    
    [SerializeField] private TileStatusID tileStatus = TileStatusID.Free; 
    
    private Vector2Int coordinates = new Vector2Int();

    public bool hasObstacle; 
    public bool isPath;
    

    public TileStatusID TileStatus { get { return tileStatus; } set { tileStatus = value; } }    
    public Vector2Int Coordinates { get { return coordinates; } }

    private void SetTile()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / MapGenerator.gridSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / MapGenerator.gridSize);       
    }

}
