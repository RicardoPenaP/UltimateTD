using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AnimatorHandler;

[RequireComponent(typeof(EnemyController))]
public class EnemyMovementHandler : MonoBehaviour
{
    [Header("Enemy Movement Handler")]
    [Tooltip("The offset distance that the enemy can consider that is on the next tile," +
    " higher values allows higher movement speed but can cause tearing on the movement")]
    [SerializeField] private float distanceFromNextTileOffset = 0.08f;

    public event Action OnPathEnded;
    private EnemyHealthHandler myHealthHandler;
    private EnemyAnimatorHandler myAnimatorHandler;
    private Transform meshTransform;
    private List<Tile> path;    
   
    private int pathIndex = 0;
    private Vector3 movementDirection;

    private float currentMovementSpeed;
    private float movementSpeedMultiplier;

    private bool canMove = true;
    private void Awake()
    {
        myHealthHandler = GetComponent<EnemyHealthHandler>();
        myAnimatorHandler = GetComponent<EnemyAnimatorHandler>();
        meshTransform = GetComponentInChildren<Animator>().transform;
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
        if (canMove || !myHealthHandler.IsAlive)
        {            
            return;
        }

        if (path == null)
        {
            return;
        }

        if (Vector3.Distance(path[pathIndex].GetPosition(), transform.position) <= distanceFromNextTileOffset)
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
       
        transform.position += movementDirection * currentMovementSpeed * Time.fixedDeltaTime;
        myAnimatorHandler.PlayABoolAnimation(BoolAnimationsToPlay.Walk, true);
        myAnimatorHandler.ChangeAnimationSpeed(AnimationWithSpeedModifiers.Walk, movementSpeedMultiplier);
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

    public void SetPath(List<Tile> newPath)
    {
        pathIndex = 0;
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

    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    public void InitializeMovementHandler(float baseMovementSpeed)
    {
        currentMovementSpeed = baseMovementSpeed;
        movementSpeedMultiplier = 1;
    }

    public void SetMovementSpeedMultiplier(float movementSpeedMultiplier)
    {
        this.movementSpeedMultiplier = movementSpeedMultiplier;
    }
}
