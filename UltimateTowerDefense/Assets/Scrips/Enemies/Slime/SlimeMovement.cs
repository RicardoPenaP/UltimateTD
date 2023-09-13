using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    private readonly int RUN_FOWARD_ANIMATOR_HASH = Animator.StringToHash("Run Forward");

    private SlimeIA myIA;
    private EnemyController myController;
    private Animator myAnimator;

    private Vector3 positionToMove;

    private bool positionReached = false;
    public bool PositionReached { get { return positionReached; } }

    private void Awake()
    {
        myIA = GetComponent<SlimeIA>();
        myController = GetComponentInParent<EnemyController>();
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (positionReached)
        {
            return;
        }

        transform.position += transform.forward * myController.MovementSpeed * Time.deltaTime;

        if (Vector3.Distance(positionToMove,transform.position) <= myController.DistanceFromNextTileOffset)
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
