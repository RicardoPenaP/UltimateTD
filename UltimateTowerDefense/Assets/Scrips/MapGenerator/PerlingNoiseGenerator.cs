using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlingNoiseGenerator
{
    public static Dictionary<Vector2Int,Node> GenerateRandomNodesGrid(Vector2Int size, float scale,float seed, Vector2 offset)
    {
        Dictionary<Vector2Int, Node> nodesGrid = new Dictionary<Vector2Int, Node>();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Node newNode = new Node(new Vector2Int(x, y));
                float perlingNoiseValue = Mathf.PerlinNoise(x * scale + seed + offset.x,y * scale + seed + offset.y);
                newNode.tileType = GetNodeTileType(perlingNoiseValue);
                nodesGrid.TryAdd(new Vector2Int(x, y), newNode);
            }
        }

        return nodesGrid;
    }

    private static NodeTileType GetNodeTileType(float perlingNoiseValue)
    {
        int tileIndex = Mathf.RoundToInt(perlingNoiseValue * 10);

        switch ((NodeTileType)tileIndex)
        {
            case NodeTileType.LightGrass:                
            case NodeTileType.DarkGrass:               
            case NodeTileType.Snow:                
            case NodeTileType.Mud:                
            case NodeTileType.Sand:                
            case NodeTileType.Water:
                return (NodeTileType)tileIndex;               
            default:
                return NodeTileType.LightGrass;                
        }

       
    }
}
