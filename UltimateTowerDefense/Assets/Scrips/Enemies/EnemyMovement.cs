using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Pathfinder pathfinder;
    private EnemyController myController;
    private Animator myAnimator;
    private List<Tile> path;    
   
    private int pathIndex = 0;
    private Vector3 movementDirection;

    private readonly int RUN_FOWARD_ANIMATOR_HASH = Animator.StringToHash("Run Forward");

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        myController = GetComponent<EnemyController>();
        myAnimator = GetComponent<Animator>();
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
        myAnimator.SetBool(RUN_FOWARD_ANIMATOR_HASH, false);
        if (!myController.CanMove)
        {
            //myAnimator.SetBool(RUN_FOWARD_ANIMATOR_HASH, false);
            return;
        }

        if (path.Count < 1)
        {
            //myAnimator.SetBool(RUN_FOWARD_ANIMATOR_HASH, false);
            return;
        }  

        if (Vector3.Distance(path[pathIndex].GetPosition(),transform.position) <= myController.DistanceFromNextTileOffset)
        {
            transform.position = path[pathIndex].GetPosition();
            if (pathIndex < path.Count - 1)
            {
                pathIndex++;
                SetMovementDirection();
            }
            else
            {
                
                return;
            }           
        }

        transform.position += movementDirection * myController.MovementSpeed * Time.deltaTime;
        myAnimator.SetBool(RUN_FOWARD_ANIMATOR_HASH, true);
    }

    private void SetMovementDirection()
    {
        movementDirection = (path[pathIndex].GetPosition() - transform.position).normalized;
        transform.LookAt(path[pathIndex].GetPosition());
    }

    public void SetPath(List<Tile> path)
    {
        this.path = path;
    }


}
