using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;
using AnimatorHandler;
using System;

public class DeathKnightAI : MonoBehaviour,IEnemy
{
    [Header("Death Knight AI")]    
    [SerializeField, Min(0)] private int shieldPercentageRecover = 100;
    [SerializeField, Min(0)] private float skillCooldownTime = 3;

    private Action<float, float> OnUpdateSkillCooldownUI;

    private EnemyAnimatorHandler myAnimatorHandler;
    private EnemyHealthHandler myHealthHandler;
    private EnemyAnimatorHelper myAnimatorHelper;
    private EnemyMovementHandler myMovement;
    private Animator myAnimator;
    private ParticleSystem skillVFX;

    private EnemyState myState;

    private bool canAttack = true;
    private float attackRange;
    private int damageToStronghold;

    private void Awake()
    {
        myAnimatorHandler = GetComponent<EnemyAnimatorHandler>();
        myHealthHandler = GetComponent<EnemyHealthHandler>();
        myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        myMovement = GetComponent<EnemyMovementHandler>();
        myAnimator = GetComponentInChildren<Animator>();
        skillVFX = GetComponentInChildren<ParticleSystem>();
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
        myAnimatorHelper.OnAttackAnimationPerformed += () => { HealthMananger.Instance.TakeDamage(damageToStronghold);};
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
        skillVFX.Play();
        myHealthHandler.RestShieldPercentage(shieldPercentageRecover);
        StartCoroutine(SkillCooldownRoutine());
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
        elapsedTime = 0;
        OnUpdateSkillCooldownUI?.Invoke(elapsedTime, skillCooldownTime);
        CastSkill();
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
