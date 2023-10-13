using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public enum PathUbication { None,UpperLeftCorner, LowerLeftCorner,UpperRightCorner,LowerRightCorner}
    public PathUbication ubication;
    public Vector2Int startCoordinates;   
    public Vector2Int destinationCoordinates;    
    public List<Node> nodes;
    public List<Vector2Int> middleCoordinates;

    public Path()
    {
        ubication = PathUbication.None;
        startCoordinates = Vector2Int.zero;
        destinationCoordinates = Vector2Int.zero;
        nodes = new List<Node>();
        middleCoordinates = new List<Vector2Int>();
    }
}
