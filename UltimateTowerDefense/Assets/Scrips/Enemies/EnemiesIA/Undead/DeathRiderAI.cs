using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class DeathRiderAI : MonoBehaviour,IEnemy
{
    [Header("Death Knight AI")]
    [SerializeField, Min(0)] private float skillDurationTime = 1;
    [SerializeField, Min(0)] private int speedPercentageAugment = 100;
    [SerializeField, Min(0)] private float skillCooldownTime = 3;

    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private EnemyController myController;
    private EnemyAnimatorHelper myAnimatorHelper;
    private EnemyMovementHandler myMovement;
    private Animator myAnimator;

    private EnemyState myState;

    private bool canAttack = true;

    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        myMovement = GetComponent<EnemyMovementHandler>();
        myAnimator = GetComponentInChildren<Animator>();
        InitEventsSubscriptions();
    }

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        UpdateState();
    }

    private void ResetEnemy()
    {
        myState = EnemyState.Walking;
        canAttack = true;

        StartCoroutine(SkillCooldownRoutine());
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

    private void CastSkill()
    {
        myMovement.SetMovementSpeedMultiplier(1f + (speedPercentageAugment / 100));
        StartCoroutine(SkillDurationRoutine());
    }

    private void Walking()
    {
        if (Vector3.Distance(transform.position, HealthMananger.Instance.GetStrongholdPos()) <= myController.AttackRange)
        {
            PathEnded();
        }
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
        myMovement.SetCanMove(false);
        myState = EnemyState.Attacking;
    }

    private IEnumerator SkillCooldownRoutine()
    {
        yield return new WaitForSeconds(skillCooldownTime);
        CastSkill();
    }

    private IEnumerator SkillDurationRoutine()
    {
        yield return new WaitForSeconds(skillDurationTime);
        myMovement.SetMovementSpeedMultiplier(1f);
        StartCoroutine(SkillCooldownRoutine());
    }

    public void InitializeEnemy(float attackRange, int damageToStronghold)
    {

    }
}
