using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class LichAI : MonoBehaviour
{
    [Header("Lich AI")]
    [SerializeField] private EnemyController minionPrefab;
    [SerializeField] private Transform summonPos;
    [SerializeField, Min(0)] private int amountOfMinionsPerCast;
    [SerializeField, Min(0f)] private float skillCooldownTime = 3;
    [SerializeField, Min(0f)] private float betweenSummonTime = 0.5f;
    

    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private readonly int SKILL_HASH = Animator.StringToHash("Skil");

    private EnemyController myController;
    private EnemyAnimatorHelper myAnimatorHelper;
    private EnemyMovement myMovement;
    private Animator myAnimator;

    private List<EnemyController> minions = new List<EnemyController>();

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
        myAnimatorHelper.OnAttackAnimationPerformed += () => { HealthMananger.Instance.TakeDamage(myController.DamageToStronghold); };
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
            default:
                break;
        }
        
    }

    private void CastSkill()
    {
        canAttack = false;
        myController.SetCanMove(false);
        myAnimator.SetTrigger(SKILL_HASH);        
    }

    private void SummonMinions()
    {
        StartCoroutine(SummonNewMinionsRoutine());
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
        myController.SetCanMove(false);
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
        myController.SetCanMove(true);
        StartCoroutine(SkillCooldownRoutine());
    }

    private IEnumerator SkillCooldownRoutine()
    {
        yield return new WaitForSeconds(skillCooldownTime);
        CastSkill();
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

    
}