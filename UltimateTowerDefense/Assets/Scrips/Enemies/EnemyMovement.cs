using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AnimatorHandler;

[RequireComponent(typeof(EnemyController))]
public class EnemyMovement : MonoBehaviour
{
    public event Action OnPathEnded;
    private EnemyController myController;
    private EnemyAnimatorHandler myAnimatorHandler;
    private Transform meshTransform;
    private List<Tile> path;    
   
    private int pathIndex = 0;
    private Vector3 movementDirection;


    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myAnimatorHandler = GetComponent<EnemyAnimatorHandler>();
        meshTransform = GetComponentInChildren<Animator>().transform;
    }
    private void OnEnable()
    {
        ResetWalkthroughPath();
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
        myAnimatorHandler.PlayABoolAnimation(BoolAnimationsToPlay.Walk, false);
        if (!myController.CanMove || !myController.IsAlive)
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
                OnPathEnded?.Invoke();               
                return;
            }
        }
       
        transform.position += movementDirection * myController.CurrentMovementSpeed * Time.fixedDeltaTime;
        myAnimatorHandler.PlayABoolAnimation(BoolAnimationsToPlay.Walk, true);
        myAnimatorHandler.ChangeAnimationSpeed(AnimationWithSpeedModifiers.Walk, myController.MovementSpeedMultiplier);
    }

    private void SetMovementDirection()
    {
        if (path == null)
        {
            return;
        }
        movementDirection = (path[pathIndex].GetPosition() - transform.position).normalized;
        meshTransform.LookAt(path[pathIndex].GetPosition());
    }

    public void ResetWalkthroughPath()
    {
        pathIndex = 0;        
    }

    public void SetPath(List<Tile> newPath)
    {
        path = newPath;
    }

    public List<Tile> GetRemainingPath()
    {
        List<Tile> remainingPath = new List<Tile>();
        for (int i = pathIndex; i < path.Count; i++)
        {
            remainingPath.Add(path[i]);
        }
        remainingPath.RemoveAt(0);
        return remainingPath;
    }
}
