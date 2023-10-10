using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathGenerator 
{
    public static List<Node> FindPath(PathGeneratorData pathData)
    {
        Dictionary<Vector2Int, Node> grid = pathData.nodesGrid;
        Vector2Int startCoordinates = pathData.startCoordinates;
        Vector2Int destinationCoordinates = pathData.destinationCoordinates;
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        
        openSet.Add(grid[startCoordinates]);

        while (openSet.Count > 0 )
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].F < currentNode.F || (openSet[i].F == currentNode.F && openSet[i].H < currentNode.H))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == grid[destinationCoordinates])
            {
                return RetracePath(grid[startCoordinates], grid[destinationCoordinates]);
            }

            foreach (Node neighbor in GetNeighbors(grid, currentNode))
            {
                if (!neighbor.isWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newGCost = currentNode.G + GetDistance(currentNode, neighbor);

                if (newGCost < neighbor.G || !openSet.Contains(neighbor))
                {
                    neighbor.G = newGCost;
                    neighbor.H = GetDistance(neighbor, grid[destinationCoordinates]);
                    neighbor.connectedTo = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        // No se encontró un camino
        return null;
    }

    private static List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            currentNode.Content = NodeContent.Path;
            path.Add(currentNode);
            currentNode = currentNode.connectedTo;
        }
        currentNode.Content = NodeContent.Path;
        path.Add(currentNode);
        path.Reverse();
        return path;
    }

    private static List<Node> GetNeighbors(Dictionary<Vector2Int,Node> grid, Node node)
    {
        List<Node> neighbors = new List<Node>();
        Vector2Int[] neighborsDirections = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };       

        for (int i = 0; i < neighborsDirections.Length; i++)
        {
            Vector2Int neighborCoordinates = node.Coordinates + neighborsDirections[i];

            if (grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(grid[neighborCoordinates]);
            }
        }

        return neighbors;
    }

    private static int GetDistance(Node nodeA, Node nodeB)
    {
        //int distanceX = Mathf.Abs(nodeA.X - nodeB.X);
        //int distanceY = Mathf.Abs(nodeA.Y - nodeB.Y);

        // Usando la distancia de Manhattan (puede ser reemplazada por la distancia Euclidiana si es necesario)
        return Mathf.RoundToInt( Vector2Int.Distance(nodeA.Coordinates,nodeB.Coordinates));
    }

}
