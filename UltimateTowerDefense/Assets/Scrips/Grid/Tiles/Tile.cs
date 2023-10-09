using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileStatusID { Free, Occuped, HasObstacle,IsPath}

public class Tile : MonoBehaviour
{
    [Header("Tile")]    
    [SerializeField] private TileStatusID tileStatus = TileStatusID.Free; 
    
    private Vector2Int coordinates = new Vector2Int();

    //public bool hasObstacle; 
    

    public TileStatusID TileStatus { get { return tileStatus; } set { tileStatus = value; } }    
    public Vector2Int Coordinates { get { return coordinates; } }

    public void SetTileCoordinates(Vector2Int coordinates)
    {
        this.coordinates = coordinates;
                  
    }

}
