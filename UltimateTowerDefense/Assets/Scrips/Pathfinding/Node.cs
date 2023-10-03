using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeContent { Grass, Water, Path, Stronghold,Decoration}
public class Node 
{
    public Vector2Int coordinates;
    public Vector3 position;
    public NodeContent content;

    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates, Node connectedTo)
    {
        this.coordinates = coordinates;
        this.connectedTo = connectedTo;
        position = MapGenerator.CoordinatesToPosition(coordinates);

    }
}
