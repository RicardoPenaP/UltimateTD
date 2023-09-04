using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileStatus { Free, Walkable, Occuped}

public class Tile : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField] private int tileSize = 5;
    [SerializeField] private TileStatus tileStatus = TileStatus.Free;


    private Vector2Int tileCoordinates = new Vector2Int();


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
