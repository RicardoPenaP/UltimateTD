using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;
using AnimatorHandler;
using System;

public class LichAI : MonoBehaviour,IEnemy
{
    [Header("Lich AI")]
    [SerializeField] private EnemyController minionPrefab;
    [SerializeField] private Transform summonPos;
    [SerializeField, Min(0)] private int amountOfMinionsPerCast;
    [SerializeField, Min(0f)] private float skillCooldownTime = 3;
    [SerializeField, Min(0f)] private float betweenSummonTime = 0.5f;

    private Action<float, float> OnUpdateSkillCooldownUI;

    private EnemyAnimatorHandler myAnimatorHandler;
    private EnemyController myController;
    private EnemyAnimatorHelper myAnimatorHelper;
    private EnemyMovementHandler myMovement;

    private List<EnemyController> minions = new List<EnemyController>();

    private EnemyState myState;

    private bool canAttack = true;

    private float attackRange;
    private int damageToStronghold;

    private void Awake()
    {
        myAnimatorHandler = GetComponent<EnemyAnimatorHandler>();
        myController = GetComponent<EnemyController>();
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
        MinionsDestruction();
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
        myAnimatorHelper.OnAttackAnimationStarted += () => { canAttack = false; };
        myAnimatorHelper.OnAttackAnimationPerformed += () => { HealthMananger.Instance.TakeDamage(damageToStronghold); };
        myAnimatorHelper.OnAttackAnimationEnded += () => { canAttack = true; };

        myAnimatorHelper.OnSkilCastPerformed += SummonMinions;
        myAnimatorHelper.OnSkilCastEnded += SkilCasted; 
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

    private void SkilCast()
    {
        canAttack = false;
        myMovement.SetCanMove(false);
        myAnimatorHandler.PlayATriggerAnimation(TrigerAnimationsToPlay.SkilCast);
    }

    private void SummonMinions()
    {
        StartCoroutine(SummonNewMinionsRoutine());
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

    private void MinionsDestruction()
    {
        if (minions.Count<1)
        {
            return;
        }
        foreach (EnemyController minion in minions)
        {
            if (!minion.gameObject.activeInHierarchy)
            {
                minions.Remove(minion);
                Destroy(minion.gameObject);
                return;
            }
        }
    }

    private void SkilCasted()
    {
        canAttack = true;
        myMovement.SetCanMove(true);
        StartCoroutine(SkillCooldownRoutine());
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
        SkilCast();
    }

    private IEnumerator SummonNewMinionsRoutine()
    {        
        for (int i = 0; i < amountOfMinionsPerCast; i++)
        {
            EnemyController newMinion = Instantiate(minionPrefab, summonPos.position, Quaternion.identity, transform.parent);
            
            newMinion.SetPath(myMovement.GetRemainingPath());
            newMinion.SetLevel(myController.CurrentLevel);
            minions.Add(newMinion);
            yield return new WaitForSeconds(betweenSummonTime);
        }
       
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
