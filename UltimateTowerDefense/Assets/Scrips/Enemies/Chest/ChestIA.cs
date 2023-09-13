using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class ChestIA : MonoBehaviour,IEnemy
{
    private EnemyController myController;
    private ChestMovement myMovement;
    private Animator myAnimator;

    private List<Tile> path;

    private EnemyState myState;

    private bool pathCompleted = false;

    private int pathIndex = 0;

    //Animator hash codes
    private readonly int ANIMATOR_ATTACK_HASH = Animator.StringToHash("Attack");
    private readonly int ANIMATOR_WALK_HASH = Animator.StringToHash("Walk");
    private readonly int ANIMATOR_DIE_HASH = Animator.StringToHash("Die");

    private void OnEnable()
    {
        myState = EnemyState.Walking;
    }

    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myMovement = GetComponentInChildren<ChestMovement>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (!myController.IsAlive)
        {
            return;
        }

        switch (myState)
        {
            case EnemyState.Walking:
                UpdateWalking();
                break;
        }
    }

    private void UpdateWalking()
    {
        if (pathCompleted)
        {
            return;
        }
        myAnimator.SetBool(ANIMATOR_WALK_HASH, !myMovement.PositionReached);
        if (myMovement.PositionReached)
        {
            if (pathIndex < path.Count - 1)
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
        pathCompleted = true;
        myAnimator.SetTrigger(ANIMATOR_ATTACK_HASH);
    }

    public void AttackAnimationCompleted()
    {
        HealthMananger.Instance.TakeDamage(myController.DamageToStronghold);
        gameObject.SetActive(false);
        ResetWalkthroughPath();
    }

    private void ResetWalkthroughPath()
    {
        pathCompleted = false;
        pathIndex = 0;
        if (path == null)
        {
            return;
        }
        myMovement.transform.position = path[pathIndex].transform.position;
        myMovement.SetPositionToMove(path[pathIndex].transform.position);
    }

    public void DieAnimationCompleted()
    {
        BankMananger.Instance.AddGold(myController.GoldReward);
        gameObject.SetActive(false);
        ResetWalkthroughPath();
    }

    //Interface Implementation Methods
    public void SetPath(List<Tile> path)
    {
        this.path = path;
        ResetWalkthroughPath();
    }

    public void Die()
    {
        myAnimator.SetTrigger(ANIMATOR_DIE_HASH);
    }
}
