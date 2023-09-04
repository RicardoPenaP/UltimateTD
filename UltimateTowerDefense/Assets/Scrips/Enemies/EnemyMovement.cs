using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Pathfinder pathfinder;
    private EnemyController myController;
    private List<Tile> path;    
   
    private int pathIndex = 0;
    private Vector3 movementDirection;
    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        myController = GetComponent<EnemyController>();
    }

    private void Start()
    {
        path = pathfinder.GetNewPath();
        SetMovementDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!myController.CanMove)
        {
            return;
        }

        if (path.Count < 1)
        {
            return;
        }      

        transform.position += movementDirection * myController.MovementSpeed * Time.deltaTime;

        if (Vector3.Distance(path[pathIndex].GetPosition(),transform.position) <= myController.DistanceFromNextTileOffset)
        {
            pathIndex++;
            SetMovementDirection();
        }
    }

    private void SetMovementDirection()
    {
        Vector3 movementDirection = (path[pathIndex].GetPosition() - transform.position).normalized;
    }

    public void SetPath(List<Tile> path)
    {
        this.path = path;
    }


}
