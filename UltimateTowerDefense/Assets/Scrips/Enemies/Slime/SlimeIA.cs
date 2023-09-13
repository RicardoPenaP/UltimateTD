using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEnenmy;

public class SlimeIA : MonoBehaviour,IEnemy
{
    private EnemyController myController;
    private SlimeMovement myMovement;
    private Animator myAnimator;

    private List<Tile> path;

    private EnemyState myState;

    private bool canWalk = true;

    private int pathIndex = 0;

    public bool CanWalk { get { return canWalk; } }

    private void OnEnable()
    {
        myState = EnemyState.Walking;
    }

    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myMovement = GetComponentInChildren<SlimeMovement>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (myState)
        {            
            case EnemyState.Walking:
                UpdateWalking();
                break;            
        }
    }

    private void UpdateWalking()
    {
        if (myMovement.PositionReached)
        {
            if (pathIndex < path.Count-1)
            {
                pathIndex++;
                myMovement.SetPositionToMove(path[pathIndex].transform.position);
            }
            else
            {
                PathCompleted();
            }
        }
    }

    private void PathCompleted()
    {
        //Pending: implement attack animator behaviour;
        gameObject.SetActive(false);
        HealthMananger.Instance.TakeDamage(myController.DamageToStronghold);
        ResetWalkthroughPath();
    }
    

    private void ResetWalkthroughPath()
    {
        pathIndex = 0;
        myMovement.transform.position = path[pathIndex].transform.position;
    }

    public void SetPath(List<Tile> path)
    {
        this.path = path;
        ResetWalkthroughPath();
    }
}
