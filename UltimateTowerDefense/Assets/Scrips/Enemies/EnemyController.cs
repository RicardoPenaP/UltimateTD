using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnemiesInterface;

[RequireComponent(typeof(EnemyMovementHandler),typeof(EnemyDamageHandler))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;

    public event Action OnDie;

    private IEnemy myAI;
    private EnemyMovementHandler myMovement;
    private EnemyHealthHandler myHealthHandler;

    private int level;

    private int currentMaxHealth;
    private int currentMaxShield;
    private float currentMovementSpeed;
    private int currentGoldReward;
    private int currentDamageToStronghold;
    public int CurrentLevel { get { return level; } }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myData.AttackRange);
    }

    private void Awake()
    {        
        myMovement = GetComponent<EnemyMovementHandler>();
        myHealthHandler = GetComponent<EnemyHealthHandler>();
        myAI = GetComponent<IEnemy>();
    }

    private void OnEnable()
    {
        InitializeEnemy();
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        OnDie?.Invoke();
        BankMananger.Instance.AddGold(currentGoldReward);
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
    
    public void SetPath(List<Tile> path)
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

}
