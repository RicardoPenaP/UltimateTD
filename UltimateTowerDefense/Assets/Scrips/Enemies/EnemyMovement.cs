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

    private readonly int WALK_ANIMATOR_HASH = Animator.StringToHash("Walk");

    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //For test only
        path = Pathfinder.Instance.GetNewPath();
        SetMovementDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        myAnimator.SetBool(WALK_ANIMATOR_HASH, false);
        if (!myController.CanMove)
        {            
            return;
        }

        if (path == null)
        {
            return;
        }

        if (Vector3.Distance(path[pathIndex].GetPosition(), transform.position) <= myController.DistanceFromNextTileOffset)
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
                //HealthMananger.Instance.TakeDamage(1);
                return;
            }
        }

        transform.position += movementDirection * myController.CurrentMovementSpeed * Time.deltaTime;
        myAnimator.SetBool(WALK_ANIMATOR_HASH, true);
    }

    private void SetMovementDirection()
    {
        if (path == null)
        {
            return;
        }
        movementDirection = (path[pathIndex].GetPosition() - transform.position).normalized;
        myAnimator.transform.LookAt(path[pathIndex].GetPosition());
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
