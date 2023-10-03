using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeContent { None, Grass, Water, Path, Stronghold, Decoration}
public class Node 
{
    public Vector2Int coordinates;
    public Vector3 position;
    public NodeContent content;

    public bool isFree = true;
    public bool isExplored = false;
    public bool isPath = false;
    public Node connectedTo;

    public Node(Vector2Int coordinates)
    {
        this.coordinates = coordinates;        
        position = MapGenerator.CoordinatesToPosition(coordinates);

    }
}
