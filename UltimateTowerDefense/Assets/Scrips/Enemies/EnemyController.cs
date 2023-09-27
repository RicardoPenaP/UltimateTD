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
    [SerializeField] private bool canMove = true;

    public event Action OnDie;

    private int level;
    private int damageToStronghold;
    private int goldReward;

    private EnemyMovementHandler myMovement;
    private EnemyHealthHandler myHealthHandler;
    private IEnemy myAI;

    public float AttackRange { get { return myData.AttackRange; } }
    public int DamageToStronghold { get { return damageToStronghold; } }
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
    }

    private void OnEnable()
    {        
        InitializeEnemy();
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        OnDie?.Invoke();
    }

    private void Start()
    {    
        SetLevel(level);
    }

    private void InitializeHealthHandler()
    {
        myHealthHandler.InitializeHandler(myData.BaseHealth, myData.BaseShield);
    }
    private void InitializeMovementHandler()
    {
        myMovement.InitializeMovementHandler(myData.BaseMovementSpeed);
    }
    private void InitializeMyAI()
    {
        myAI.InitializeEnemy(myData.AttackRange,myData.BaseDamageToStronghold);
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
        //maxHealth = Mathf.RoundToInt( myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseHealth, level));
        //maxShield = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseShield, level));
        //damageToStronghold = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseDamageToStronghold, level));
        //defaultMovementSpeed = myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseMovementSpeed, level);
        //goldReward = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseGoldReward, level));
    }
    
    private void InitializeEnemy()
    {
        InitializeMyAI();
        InitializeHealthHandler();
        InitializeMovementHandler();
        
    }

}
