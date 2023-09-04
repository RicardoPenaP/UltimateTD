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


    private Vector2Int tileCoordinates = new Vector2Int();

    public TileStatusID TileStatus { get { return tileStatus; } }
    public Vector2Int TileCoordinates { get { return tileCoordinates; } }

    private void Awake()
    {
        SetTileCoordinates();
    }    

    private void Update()
    {
        
    }

    private void SetTileCoordinates()
    {
        tileCoordinates.x = Mathf.RoundToInt(transform.position.x / tileSize);
        tileCoordinates.y = Mathf.RoundToInt(transform.position.z / tileSize);
    }
}
