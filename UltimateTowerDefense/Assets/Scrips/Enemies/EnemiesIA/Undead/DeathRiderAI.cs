using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;
using AnimatorHandler;
using System;

public class DeathRiderAI : MonoBehaviour,IEnemy
{
    [Header("Death Knight AI")]
    [SerializeField, Min(0)] private float skillDurationTime = 1;
    [SerializeField, Min(0)] private int speedPercentageAugment = 100;
    [SerializeField, Min(0)] private float skillCooldownTime = 3;

    private Action<float, float> OnUpdateSkillCooldownUI;

    private EnemyAnimatorHandler myAnimatorHandler;
    private EnemyAnimatorHelper myAnimatorHelper;
    private EnemyMovementHandler myMovement;

    private EnemyState myState;

    private bool canAttack = true;
    private float attackRange;
    private int damageToStronghold;
    private void Awake()
    {
        myAnimatorHandler = GetComponent<EnemyAnimatorHandler>();
        myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        myMovement = GetComponent<EnemyMovementHandler>();
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
        myAnimatorHelper.OnAttackAnimationPerformed += () => { HealthMananger.Instance.TakeDamage(damageToStronghold); };
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
            case EnemyState.Victory:
                myMovement.SetCanMove(false);
                myAnimatorHandler.PlayABoolAnimation(BoolAnimationsToPlay.Victory, true);
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
        if (Vector3.Distance(transform.position, HealthMananger.Instance.GetStrongholdPos()) <= attackRange)
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
        canAttack = false;
        myAnimatorHandler.PlayATriggerAnimation(TrigerAnimationsToPlay.Attack);
    }

    private void PathEnded()
    {
        myMovement.SetCanMove(false);
        myState = EnemyState.Attacking;
    }

    private IEnumerator SkillCooldownRoutine()
    {
        float elapsedTime = 0;
        OnUpdateSkillCooldownUI?.Invoke(elapsedTime, skillCooldownTime);
        while (elapsedTime < skillCooldownTime)
        {
            elapsedTime += Time.deltaTime;
            OnUpdateSkillCooldownUI?.Invoke(elapsedTime, skillCooldownTime);
            yield return null;
        }
        elapsedTime = skillCooldownTime;
        OnUpdateSkillCooldownUI?.Invoke(elapsedTime, skillCooldownTime);
        CastSkill();
    }

    private IEnumerator SkillDurationRoutine()
    {
        float remainingTime = skillDurationTime;
        OnUpdateSkillCooldownUI?.Invoke(remainingTime, skillDurationTime);
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            OnUpdateSkillCooldownUI?.Invoke(remainingTime, skillDurationTime);
            yield return null;
        }
        remainingTime = 0;
        OnUpdateSkillCooldownUI?.Invoke(remainingTime, skillDurationTime);        
        myMovement.SetMovementSpeedMultiplier(1f);
        StartCoroutine(SkillCooldownRoutine());
    }

    public void InitializeEnemy(float attackRange, int damageToStronghold)
    {
        this.attackRange = attackRange;
        this.damageToStronghold = damageToStronghold;
    }

    public void SubscribeToUpdateSkillCooldownUI(Action<float, float> OnUpdateSkillCooldownUIAction)
    {
        OnUpdateSkillCooldownUI += OnUpdateSkillCooldownUIAction;
    }
}
