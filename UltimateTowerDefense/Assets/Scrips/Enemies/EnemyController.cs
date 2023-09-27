using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AnimatorHandler;


[RequireComponent(typeof(EnemyMovementHandler),typeof(EnemyDamageHandler))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;
    [SerializeField] private bool canMove = true;

    private int level;
    private int currentHealth;
    private int currentShield;
    private int damageToStronghold;
    private int goldReward;
    private float currentMovementSpeed;

    
    public event Action OnDie;

    private List<Tile> path;
    private EnemyAnimatorHandler myAnimatorHandler;
    private EnemyMovementHandler myMovement;
    private EnemyHealthHandler myHealthHandler;

    private int maxHealth;  

    private int maxShield;
    

    private float defaultMovementSpeed;
    private float movementSpeedMultiplier;
    
    private float distanceFromNextTileOffset;

    private bool isAlive = true;
    
    public List<Tile> Path { get { return path; } }    
   
    public float AttackRange { get { return myData.AttackRange; } }
    public int DamageToStronghold { get { return damageToStronghold; } }
    public int CurrentLevel { get { return level; } }
    public float CurrentMovementSpeed { get { return currentMovementSpeed * movementSpeedMultiplier; } }
    public float MovementSpeedMultiplier { get { return movementSpeedMultiplier; } set { movementSpeedMultiplier = value; } }
    public float DistanceFromNextTileOffset { get { return distanceFromNextTileOffset; } }

    public bool IsAlive { get { return isAlive; } }
    public bool CanMove { get { return canMove; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myData.AttackRange);
    }

    private void Awake()
    {        
        myMovement = GetComponent<EnemyMovementHandler>();
        myHealthHandler = GetComponent<EnemyHealthHandler>();
        myAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
    }

    private void OnEnable()
    {        
        InitializeEnemy();
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }

    private void Start()
    {             
        movementSpeedMultiplier = 1;
        distanceFromNextTileOffset = myData.DistanceFromNextileOffset;
        SetLevel(level);
    }

    private void InitializeHealthHandler()
    {
        myHealthHandler.InitializeHandler(maxHealth, maxShield);
    }

    
    public void SetPath(List<Tile> path)
    {
        this.path = path;
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
        maxHealth = Mathf.RoundToInt( myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseHealth, level));
        maxShield = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseShield, level));
        damageToStronghold = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseDamageToStronghold, level));
        defaultMovementSpeed = myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseMovementSpeed, level);
        goldReward = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(EnemyStatToAugment.BaseGoldReward, level));
    }
    
    private void Desactivate()
    {       
        gameObject.SetActive(false);
    }

    private void InitializeEnemy()
    {
        InitializeHealthHandler();        
        currentMovementSpeed = defaultMovementSpeed;
        movementSpeedMultiplier = 1;        
        canMove = true;
        
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }
}
