using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeContent { None, Path, Stronghold, Decoration}
public enum NodeTileType { LightGrass, DarkGrass, Snow, Mud, Sand, Water}
public class Node 
{
    public Vector2Int Coordinates { get; set; }   
    public Vector3 Position { get; set; }
    public NodeContent Content { get; set; }
    public NodeTileType TileType { get; set; }

    public bool isFree = true;
    public bool isExplored = false;
    public bool isPath = false;
    public bool isWalkable = true;
    public Node connectedTo;

    public Node(Vector2Int coordinates)
    {
        Coordinates = coordinates;        
        Position = MapGenerator.CoordinatesToPosition(coordinates);       
        connectedTo = null;        
    }

}


