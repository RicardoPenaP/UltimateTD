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
    private Path myPath;
   
    private int pathIndex = 0;
    private Vector3 movementDirection;

    private float currentMovementSpeed;
    [SerializeField] private float movementSpeedMultiplier;

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
        if (!canMove || !myHealthHandler.IsAlive)
        {            
            return;
        }

        if (myPath == null)
        {
            return;
        }

        if (Vector3.Distance(myPath.nodes[pathIndex].Position, transform.position) <= distanceFromNextTileOffset)
        {
            transform.position = myPath.nodes[pathIndex].Position;

            if (pathIndex < myPath.nodes.Count - 1)
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
       
        transform.position += movementDirection * currentMovementSpeed * movementSpeedMultiplier * Time.fixedDeltaTime;
        myAnimatorHandler.PlayABoolAnimation(BoolAnimationsToPlay.Walk, true);
        myAnimatorHandler.ChangeAnimationSpeed(AnimationWithSpeedModifiers.Walk, movementSpeedMultiplier);
    }

    private void SetMovementDirection()
    {
        if (myPath == null)
        {
            return;
        }
        movementDirection = (myPath.nodes[pathIndex].Position - transform.position).normalized;
        meshTransform.LookAt(myPath.nodes[pathIndex].Position);
    }

    public void SetPath(Path path)
    {
        pathIndex = 0;
        myPath = path;
    }

    public Path GetRemainingPath()
    {
        Path remainingPath = new Path();
        for (int i = pathIndex; i < myPath.nodes.Count; i++)
        {
            remainingPath.nodes.Add(myPath.nodes[i]);
        }
        remainingPath.nodes.RemoveAt(0);
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
