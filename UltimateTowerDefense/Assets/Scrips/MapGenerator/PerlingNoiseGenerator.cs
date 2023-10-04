using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PerlinNoiseTileRange
{
    public NodeTileType tileType;
    public float value;
}

public static class PerlingNoiseGenerator
{
    private static PerlinNoiseTileRange[] localTileRanges;
    public static Dictionary<Vector2Int,Node> GenerateRandomNodesGrid(Vector2Int size, float scale,float seed, Vector2 offset, PerlinNoiseTileRange[] tileRanges)
    {
        localTileRanges = tileRanges;
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
        for (int i = 0; i < localTileRanges.Length; i++)
        {
            if (perlingNoiseValue < localTileRanges[i].value)
            {
                return localTileRanges[i].tileType;
            }
        }  

        return NodeTileType.LightGrass;
    }
}
