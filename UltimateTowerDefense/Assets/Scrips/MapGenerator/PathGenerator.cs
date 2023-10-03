using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathGeneratorData
{
    public Dictionary<Vector2Int, Node> nodesGrid;
    public Node startNode;
    public Node destinationNode;
    public NodeContent contentOfPathNodes;
}



public class PathGenerator : MonoBehaviour
{
    private static Node startNode;
    private static Node destinationNode;
    private static Queue<Node> frontier = new Queue<Node>();
    private static Dictionary<Vector2Int, Node> nodesGrid;
    private static Dictionary<Vector2Int, Node> nodesReached = new Dictionary<Vector2Int, Node>();
    private static Node currenSearchNode;

    public static List<Node> GetNewPath(PathGeneratorData pathData)
    {
        frontier.Clear();
        nodesReached.Clear();

        startNode = pathData.startNode;
        destinationNode = pathData.destinationNode;
        nodesGrid = pathData.nodesGrid;
        currenSearchNode = new Node(startNode.coordinates);
        BreadthFirstSearch();
        return BuildPath();
    }

    private static void BreadthFirstSearch()
    { 
        bool isRuning = true;

        frontier.Enqueue(nodesGrid[startNode.coordinates]);
        nodesReached.Add(startNode.coordinates, startNode);

        while (frontier.Count > 0 && isRuning)
        {
            currenSearchNode = frontier.Dequeue();
            currenSearchNode.isExplored = true;            
            ExploreNeighbors();
            if (currenSearchNode.coordinates == destinationNode.coordinates)
            {
                isRuning = false;
            }
        }
    }

    private static void ExploreNeighbors()
    {
        if (currenSearchNode == null)
        {
            return;
        }

        Vector2Int[] exploreDirections = GetRandomExploreDirections();

        List<Node> neighbors = new List<Node>();

        for (int i = 0; i < exploreDirections.Length; i++)
        {
            Vector2Int neighborCoordinates = currenSearchNode.coordinates + exploreDirections[i];

            if (nodesGrid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(nodesGrid[neighborCoordinates]);
            }
        }

        foreach (Node neightbor in neighbors)
        {
            if (!nodesReached.ContainsKey(neightbor.coordinates) && neightbor.isFree)
            {
                neightbor.connectedTo = currenSearchNode;
                nodesReached.Add(neightbor.coordinates, nodesGrid[neightbor.coordinates]);
                frontier.Enqueue(nodesGrid[neightbor.coordinates]);
            }
        }
    }

    private static List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            currentNode.isPath = true;
            path.Add(currentNode);
        }

        path.Reverse();

        return path;
    }

    private static Vector2Int[] GetRandomExploreDirections()
    {
        Vector2Int[] randomExploreDirections = new Vector2Int[4];

        for (int i = 0; i < randomExploreDirections.Length; i++)
        {
            randomExploreDirections[i] = GetRandomDirectionVector();

            for (int j = 0; j < randomExploreDirections.Length; j++)
            {
                if (i!=j)
                {
                    if (randomExploreDirections[i] == randomExploreDirections[j])
                    {
                        randomExploreDirections[i] = GetRandomDirectionVector();
                        j = -1;
                    }
                }
            }
        }

        return randomExploreDirections;
    }

    private static Vector2Int GetRandomDirectionVector()
    {
        int randomNumber = Random.Range(0, 4);
        Vector2Int randomDirectionVector = Vector2Int.zero;
        switch (randomNumber)
        {
            case 0:
                randomDirectionVector = Vector2Int.up;
                break;
            case 1:
                randomDirectionVector = Vector2Int.down;
                break;
            case 2:
                randomDirectionVector = Vector2Int.right;
                break;
            case 3:
                randomDirectionVector = Vector2Int.left;
                break;
            default:
                break;
        }

        return randomDirectionVector;
    }
}
