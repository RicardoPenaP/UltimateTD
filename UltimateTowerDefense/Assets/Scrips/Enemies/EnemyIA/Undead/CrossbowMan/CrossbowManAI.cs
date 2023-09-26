using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class CrossbowManAI : MonoBehaviour,IEnemy
{
    [Header("Crossbow Man AI")]
    [SerializeField] private CrossbowManAmmo ammoPrefab;
    [SerializeField] private Transform shootPosition;

    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private EnemyController myController;
    private EnemyAnimatorHelper myAnimatorHelper;    
    private EnemyMovement myMovement;
    private Animator myAnimator;

    private EnemyState myState;

    private bool canAttack = true;

    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        myMovement = GetComponent<EnemyMovement>();
        myAnimator = GetComponentInChildren<Animator>();
        InitEventsSubscriptions();
    }

    private void OnEnable()
    {
        myState = EnemyState.Walking;
        canAttack = true;
    }

    private void Update()
    {
        UpdateState();
    }

    private void InitEventsSubscriptions()
    {
        SetAttackAnimationsEvents();
        myMovement.OnPathEnded += PathEnded;
    }

    private void SetAttackAnimationsEvents()
    {
        myAnimatorHelper.OnAttackAnimationStarted += () => { canAttack = false; };
        myAnimatorHelper.OnAttackAnimationPerformed += () => { HealthMananger.Instance.TakeDamage(myController.DamageToStronghold); };
        myAnimatorHelper.OnAttackAnimationEnded += () => { canAttack = true; };
    }

    private void UpdateState()
    {
        switch (myState)
        {
            case EnemyState.None:
                break;
            case EnemyState.Walking:
                Walking();
                break;
            case EnemyState.CastingSkill:
                CastingSkill();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
            default:
                break;
        }
    }

    private void Walking()
    {        
        if (Vector3.Distance(transform.position,HealthMananger.Instance.GetStrongholdPos())<= myController.AttackRange)
        {
            PathEnded();            
        }
    }

    private void CastingSkill()
    {

    }

    private void Attacking()
    {
        if (!canAttack)
        {
            return;
        }
        Attack();
    }

    private void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    private void PathEnded()
    {
        myController.SetCanMove(false);
        myState = EnemyState.Attacking;
    }


}
