using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeContent { Grass, Water, Path, Stronghold,Decoration}
public class Node 
{
    public Vector2Int coordinates;
    public Vector3 position;
    public NodeContent content;
   
}
