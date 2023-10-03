using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public enum PathUbication { None,North, South,East,West}
    public PathUbication ubication = PathUbication.None;
    public Vector2Int startCoordinates;
    public Vector3 startPosition;
    public Vector2Int destinationCoordinates;
    public List<Tile> nodes;

}
