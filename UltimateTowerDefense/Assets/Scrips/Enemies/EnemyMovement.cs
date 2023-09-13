using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyController myController;
    private Animator myAnimator;
    private List<Tile> path;    
   
    private int pathIndex = 0;
    private Vector3 movementDirection;

    private readonly int RUN_FOWARD_ANIMATOR_HASH = Animator.StringToHash("Run Forward");

    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetMovementDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        myAnimator.SetBool(RUN_FOWARD_ANIMATOR_HASH, false);
        if (!myController.CanWalk)
        {            
            return;
        }

        if (path == null)
        {
            return;
        }

        if (path.Count < 1)
        {            
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
                //Reach the end of the path and do damage to the base life
                gameObject.SetActive(false);
                //Only for testing, change for a variable and use it in enemy controller
                HealthMananger.Instance.TakeDamage(1);
                return;
            }           
        }

        transform.position += movementDirection * myController.MovementSpeed * Time.deltaTime;
        myAnimator.SetBool(RUN_FOWARD_ANIMATOR_HASH, true);
    }

    private void SetMovementDirection()
    {
        if (path == null)
        {
            return;
        }
        movementDirection = (path[pathIndex].GetPosition() - transform.position).normalized;
        transform.LookAt(path[pathIndex].GetPosition());
    }

    public void ResetWalkthroughPath()
    {
        pathIndex = 0;        
    }

    public void SetPath(List<Tile> newPath)
    {
        path = newPath;
    }
}
