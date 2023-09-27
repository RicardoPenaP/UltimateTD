using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;
using AnimatorHandler;

public class SiegeEngineAI : MonoBehaviour,IEnemy
{
    [Header("Siege Engine AI")]
    [SerializeField] private SiegeEngineAmmo ammoPrefab;
    [SerializeField] private Transform shootPosition;

    private EnemyAnimatorHandler myAnimatorHandler;
    private EnemyAnimatorHelper myAnimatorHelper;
    private EnemyMovementHandler myMovement;
    private Animator myAnimator;

    private EnemyState myState;

    private bool canAttack = true;
    private float attackRange;
    private int damageToStronghold;

    private void Awake()
    {
        myAnimatorHandler = GetComponent<EnemyAnimatorHandler>();
        myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        myMovement = GetComponent<EnemyMovementHandler>();
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
        myAnimatorHelper.OnAttackAnimationPerformed += Shoot;
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
            case EnemyState.Attacking:
                Attacking();
                break;
            default:
                break;
        }
    }

    private void Walking()
    {
        if (Vector3.Distance(transform.position, HealthMananger.Instance.GetStrongholdPos()) <= attackRange)
        {
            PathEnded();
        }
    }

    private void Attacking()
    {
        myAnimator.transform.LookAt(HealthMananger.Instance.GetStrongholdPos());
        if (!canAttack)
        {
            return;
        }
        Attack();
    }

    private void Attack()
    {
        myAnimatorHandler.PlayATriggerAnimation(TrigerAnimationsToPlay.Attack);
    }

    private void PathEnded()
    {
        myMovement.SetCanMove(false);
        myState = EnemyState.Attacking;
    }

    private void Shoot()
    {
        SiegeEngineAmmo currentAmmo = Instantiate(ammoPrefab, shootPosition.position, myAnimator.transform.rotation, transform);
        currentAmmo.SetRange(attackRange);
        currentAmmo.OnInpact += () => { HealthMananger.Instance.TakeDamage(damageToStronghold); };
    }

    public void InitializeEnemy(float attackRange, int damageToStronghold)
    {
        this.attackRange = attackRange;
        this.damageToStronghold = damageToStronghold;
    }

}
