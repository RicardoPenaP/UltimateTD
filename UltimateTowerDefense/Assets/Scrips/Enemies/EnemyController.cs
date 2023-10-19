using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnemiesInterface;

public enum EnemyType { Slave, SkeletonWarrior, Crossbow, DeathKnigh, Lich, DeathRider, SiegeEngine, Gargolye }
[RequireComponent(typeof(EnemyMovementHandler),typeof(EnemyDamageHandler))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;

    private IEnemy myAI;
    private EnemyMovementHandler myMovement;
    private EnemyHealthHandler myHealthHandler;
    private EnemyAnimatorHelper myAnimatorHelper;    
    //For testing
    [SerializeField] private int level;

    [SerializeField] private int currentMaxHealth;
    [SerializeField] private int currentMaxShield;
    [SerializeField] private float currentMovementSpeed;
    [SerializeField] private int currentGoldReward;
    [SerializeField] private int currentDamageToStronghold;
    public int CurrentLevel { get { return level; } }
   
    private void OnDrawGizmos()
    {
        if (!myData)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myData.AttackRange);
    }

    private void Awake()
    {        
        myMovement = GetComponent<EnemyMovementHandler>();
        myHealthHandler = GetComponent<EnemyHealthHandler>();
        myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        myAI = GetComponent<IEnemy>();
        SubscribeToEvents();
    }

    private void Start()
    {
        if (EnemyOutsideScreenIndicator.Instance)
        {
            EnemyOutsideScreenIndicator.Instance.AddIndicator(this);
        }
    }

    private void OnEnable()
    {
        InitializeEnemy();
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;       
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
        if (EnemyOutsideScreenIndicator.Instance)
        {
            EnemyOutsideScreenIndicator.Instance.RemoveIndicator(this);
        }
    }

    private void SubscribeToEvents()
    {
        myHealthHandler.OnDie += () => { BankMananger.Instance.AddGold(currentGoldReward); };
        myAnimatorHelper.OnDieAnimationEnded += () => { this.gameObject.SetActive(false); };        
    }

    private void UnsubscribeToEvents()
    {
        myHealthHandler.OnDie -= () => { BankMananger.Instance.AddGold(currentGoldReward); };
        myAnimatorHelper.OnDieAnimationEnded -= () => { this.gameObject.SetActive(false); };
    }

    private void InitializeHealthHandler()
    {
        myHealthHandler.InitializeHandler(currentMaxHealth, currentMaxShield);
       
    }
    private void InitializeMovementHandler()
    {
        myMovement.InitializeMovementHandler(currentMovementSpeed);
    }
    private void InitializeMyAI()
    {
        myAI.InitializeEnemy(myData.AttackRange,currentDamageToStronghold);
    }
    
    public void SetPath(Path path)
    {
        myMovement.SetPath(path);
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > 100)
        {
            return; 
        }
        this.level = level;
        SetLevelStats();
        InitializeEnemy();
    }

    private void SetLevelStats()
    {
        currentMaxHealth = Mathf.RoundToInt( myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseHealth, level));
        currentMaxShield = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseShield, level));
        currentDamageToStronghold = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseDamageToStronghold, level));
        currentMovementSpeed = myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseMovementSpeed, level);
        currentGoldReward = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseGoldReward, level));
        InitializeEnemy();
    }
    
    private void InitializeEnemy()
    {
        InitializeMyAI();
        InitializeHealthHandler();
        InitializeMovementHandler();       
    }

    public void SubmitToOnDie(Action method)
    {
        myHealthHandler.OnDie += method;
    }

}
