using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;
using AnimatorHandler;
using System;

public class GargolyeAI : MonoBehaviour,IEnemy
{
    [Header("Gargolye AI")]
    [SerializeField] private GargolyeColdFireBall coldFireBallPrefab;
    [SerializeField, Min(0)] private int damageToStrongholdPercentageAugment = 100;
    [SerializeField, Min(0)] private float skillCooldownTime = 3;
    [SerializeField] private Transform launchPos;

    private Action<float, float> OnUpdateSkillCooldownUI;

    private EnemyAnimatorHandler myAnimatorHandler;
    private EnemyAnimatorHelper myAnimatorHelper;
    private EnemyMovementHandler myMovement;
    private Animator myAnimator;

    private EnemyState myState;

    private bool canAttack = true;
    private bool canCastSkill = false;
    private int skillDamage;
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
        SetAnimationsEvents();
        myMovement.OnPathEnded += PathEnded;
    }

    private void SetAnimationsEvents()
    {
        myAnimatorHelper.OnAttackAnimationPerformed += () => { HealthMananger.Instance.TakeDamage(damageToStronghold); };
        myAnimatorHelper.OnAttackAnimationEnded += () => { canAttack = true; };
                
        myAnimatorHelper.OnSkilCastPerformed += () => { HealthMananger.Instance.TakeDamage(skillDamage); LaunchColdFireball(); };
        myAnimatorHelper.OnSkilCastEnded += () => { StartCoroutine(SkillCooldownRoutine()); };
        myAnimatorHelper.OnSkilCastEnded += () => { canAttack = true; };
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

    private void SkilCast()
    {
        canCastSkill = false;
        myAnimatorHandler.PlayATriggerAnimation(TrigerAnimationsToPlay.SkilCast);
    }

    private void LaunchColdFireball()
    {
        Instantiate(coldFireBallPrefab, launchPos.position, launchPos.rotation, transform).GetComponent<GargolyeColdFireBall>().SetRange(attackRange);
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
        canAttack = false;
        if (canCastSkill)
        {           
            SkilCast();
        }
        else
        {
            Attack();
        } 
        
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
        yield return new WaitForSeconds(skillCooldownTime);
        canCastSkill = true;       
    }

    public void InitializeEnemy(float attackRange, int damageToStronghold)
    {
        this.attackRange = attackRange;
        this.damageToStronghold = damageToStronghold;
        skillDamage = Mathf.RoundToInt((float)damageToStronghold * (1 + (float)damageToStrongholdPercentageAugment / 100));
    }

    public void SubscribeToUpdateSkillCooldownUI(Action<float, float> OnUpdateSkillCooldownUIAction)
    {

    }
}
