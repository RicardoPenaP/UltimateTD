using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMovement : MonoBehaviour
{
    private EnemyController myController;

    private Vector3 positionToMove;

    private bool positionReached = true;
    public bool PositionReached { get { return positionReached; } }

    private void Awake()
    {
        myController = GetComponentInParent<EnemyController>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!myController.IsAlive)
        {
            return;
        }

        if (!myController.CanWalk)
        {
            return;
        }

        if (positionReached)
        {
            return;
        }

        transform.position += transform.forward * myController.MovementSpeed * Time.deltaTime;

        if (Vector3.Distance(positionToMove, transform.position) <= myController.DistanceFromNextTileOffset)
        {
            transform.position = positionToMove;
            positionReached = true;
        }
    }

    private void SetMovementDirection()
    {
        transform.forward = (positionToMove - transform.position).normalized;
    }

    public void SetPositionToMove(Vector3 positionToMove)
    {
        this.positionToMove = positionToMove;
        positionReached = false;
        SetMovementDirection();
    }

}
