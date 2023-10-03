using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public enum PathUbication { None,North, South,East,West}
    public PathUbication ubication = PathUbication.None;
    public Node startNode;   
    public Node destinationNode;
    public List<Node> nodes;

}
