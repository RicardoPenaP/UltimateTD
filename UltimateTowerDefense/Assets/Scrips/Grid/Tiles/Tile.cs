using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileStatusID { Free, Walkable, Occuped}

public class Tile : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField] private int tileSize = 5;
    [SerializeField] private TileStatusID tileStatus = TileStatusID.Free;
    
    private Vector2Int coordinates = new Vector2Int();

    //Pathfinding variables

    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Tile connectedTo;

    public TileStatusID TileStatus { get { return tileStatus; } set { tileStatus = value; } }    
    public Vector2Int Coordinates { get { return coordinates; } }

    private void Awake()
    {
        SetTile();
    }    

    private void SetTile()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / tileSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / tileSize);
        isWalkable = tileStatus == TileStatusID.Walkable;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
