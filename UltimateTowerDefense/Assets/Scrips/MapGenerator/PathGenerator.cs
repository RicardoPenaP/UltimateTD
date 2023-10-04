using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathGeneratorData
{
    public Dictionary<Vector2Int, Node> nodesGrid;
    public Vector2Int startCoordinates;
    public Vector2Int destinationCoordinates;
    public NodeContent contentOfPathNodes;
}
public class PathGenerator
{
    private static Vector2Int startCoordinates;
    private static Vector2Int destinationCoordinates;
    private static Queue<Node> frontier = new Queue<Node>();
    private static Dictionary<Vector2Int, Node> nodesGrid;
    private static Dictionary<Vector2Int, Node> nodesReached = new Dictionary<Vector2Int, Node>();
    private static Node currenSearchNode;
    private static Vector2Int[] exploreDirections;
    private static bool successfulSearch = false;

    public static List<Node> GetNewPath(PathGeneratorData pathData)
    {
        frontier.Clear();
        nodesReached.Clear();      

        startCoordinates = pathData.startCoordinates;
        destinationCoordinates = pathData.destinationCoordinates;
        nodesGrid = pathData.nodesGrid;        
        BreadthFirstSearch();

        if (successfulSearch)
        {
            return BuildPath(pathData.contentOfPathNodes);
        }
        else
        {
            Debug.Log("PathNotFounded");
            return null;
        }
        
    }

    private static void BreadthFirstSearch()
    { 
        bool isRunning = true;

        frontier.Enqueue(nodesGrid[startCoordinates]);
        nodesReached.Add(startCoordinates, new Node(startCoordinates));

        while (frontier.Count > 0 && isRunning)
        {
            currenSearchNode = frontier.Dequeue();
            currenSearchNode.isExplored = true;            
            ExploreNeighbors();
            if (currenSearchNode.Coordinates == destinationCoordinates)
            {
                isRunning = false;
                successfulSearch = true;
                return;
            }
        }

        successfulSearch = false;
    }

    private static void ExploreNeighbors()
    {
        if (currenSearchNode == null)
        {
            return;
        }
        exploreDirections = GetRandomExploreDirections();

        List<Node> neighbors = new List<Node>();

        for (int i = 0; i < exploreDirections.Length; i++)
        {
            Vector2Int neighborCoordinates = currenSearchNode.Coordinates + exploreDirections[i];

            if (nodesGrid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(nodesGrid[neighborCoordinates]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!nodesReached.ContainsKey(neighbor.Coordinates) && neighbor.isFree)
            {
                neighbor.connectedTo = currenSearchNode;
                nodesReached.Add(neighbor.Coordinates, nodesGrid[neighbor.Coordinates]);
                frontier.Enqueue(nodesGrid[neighbor.Coordinates]);
            }
        }
    }

    private static List<Node> BuildPath(NodeContent nodesContent)
    {
        List<Node> path = new List<Node>();
        Node currentNode = nodesReached[destinationCoordinates];

        currentNode.isPath = true;
        path.Add(currentNode);
        

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            currentNode.isPath = true;
            path.Add(currentNode);
        }

        path.Reverse();

        foreach (Node node in path)
        {
            node.Content = nodesContent;
        }

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
