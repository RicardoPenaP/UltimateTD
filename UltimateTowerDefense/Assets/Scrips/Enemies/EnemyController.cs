using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMovement),typeof(EnemyDamageHandler))]
public class EnemyController : MonoBehaviour
{
    public delegate void UpdateEnemyUIDelegate(int healthCurrentValue, int healthMaxValue, int shieldCurrentValue, int shieldMaxValue);    
   
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;
    [SerializeField] private bool canMove = true;

    [Header("Testing Values")]
    [SerializeField] private int level;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentShield;
    [SerializeField] private int damageToStronghold;
    [SerializeField] private int goldReward;
    [SerializeField] private float currentMovementSpeed;

    public UpdateEnemyUIDelegate OnUIUpdate;
    public event Action OnDie;

    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private readonly int DIE_HASH = Animator.StringToHash("Die");

    private List<Tile> path;
    private Animator myAnimator;
    EnemyMovement myMovement; 

    private int maxHealth;  

    private int maxShield;
    

    private float defaultMovementSpeed;
    private float movementSpeedMultiplier;
    
    private float distanceFromNextTileOffset;

    private bool isAlive = true;
    

    public List<Tile> Path { get { return path; } }    
   
    public float CurrentMovementSpeed { get { return currentMovementSpeed * movementSpeedMultiplier; } }
    public float DistanceFromNextTileOffset { get { return distanceFromNextTileOffset; } }

    public bool IsAlive { get { return isAlive; } }
    public bool CanMove { get { return canMove; } }

    private void Awake()
    {        
        myMovement = GetComponent<EnemyMovement>();
        InitHandlers();
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        ResetStats();
    }


    private void Start()
    {             
        movementSpeedMultiplier = 1;
        distanceFromNextTileOffset = myData.DistanceFromNextileOffset;
        SetLevel(level);
    }

    private void OnDestroy()
    {
        DeInitHandlers();
    }

    private void InitHandlers()
    {
        EnemyDamageHandler myDamageHandler = GetComponent<EnemyDamageHandler>();
        if (myDamageHandler)
        {
            myDamageHandler.OnTakeDamage += TakeDamage;
            myDamageHandler.OnHealDamage += HealDamage;
        }
                
        if (myMovement)
        {
            myMovement.OnPathEnded += PathEnded;
        }

        EnemyAnimatorHelper myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        if (myAnimatorHelper)
        {
            myAnimatorHelper.OnAttackAnimationPerformed += DealDamageToStronghold;
            myAnimatorHelper.OnAttackAnimationEnded += Desactivate;

            myAnimatorHelper.OnDieAnimationEnded += Desactivate;
        }
    }

    private void DeInitHandlers()
    {
        EnemyDamageHandler myDamageHandler = GetComponent<EnemyDamageHandler>();
        if (myDamageHandler)
        {
            myDamageHandler.OnTakeDamage -= TakeDamage;
            myDamageHandler.OnHealDamage -= HealDamage;
        }
        
        if (myMovement)
        {
            myMovement.OnPathEnded -= PathEnded;
        }

        EnemyAnimatorHelper myAnimatorHelper = GetComponentInChildren<EnemyAnimatorHelper>();
        if (myAnimatorHelper)
        {
            myAnimatorHelper.OnAttackAnimationPerformed -= DealDamageToStronghold;
            myAnimatorHelper.OnAttackAnimationEnded -= Desactivate;

            myAnimatorHelper.OnDieAnimationEnded -= Desactivate;
        }
    }

    private void TakeDamage(int damageAmount)
    {
        if (!isAlive)
        {
            return;
        }
        int damageTaken = damageAmount;
        if (currentShield >= damageTaken)
        {
            currentShield -= damageTaken;
            damageTaken = 0;
        }
        else
        {
            damageTaken -= currentShield;
            currentShield = 0;
        }

        if (damageTaken > 0)
        {
            currentHealth -= damageTaken;
            damageTaken = 0;
        }
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            Die();
        }

        UpdateUI();
    }

    private void HealDamage(int healedAmount)
    {
        currentHealth += healedAmount;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        UpdateUI();
    }

    private void UpdateUI()
    {
        OnUIUpdate?.Invoke(currentHealth, maxHealth, currentShield, maxShield);
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
        ResetStats();
        UpdateUI();
    }

    private void SetLevelStats()
    {
        maxHealth = Mathf.RoundToInt( myData.GetLevelRelatedStatValue(StatToAugment.BaseHealth, level));
        maxShield = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(StatToAugment.BaseShield, level));
        damageToStronghold = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(StatToAugment.BaseDamageToStronghold, level));
        defaultMovementSpeed = myData.GetLevelRelatedStatValue(StatToAugment.BaseMovementSpeed, level);
        goldReward = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(StatToAugment.BaseGoldReward, level));
    }

    private void PathEnded()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    private void Die()
    {
        myAnimator.SetTrigger(DIE_HASH);
        OnDie?.Invoke();
        isAlive = false;
        BankMananger.Instance.AddGold(goldReward);
    }

    private void DealDamageToStronghold()
    {
        HealthMananger.Instance.TakeDamage(damageToStronghold);
    }

    private void Desactivate()
    {
        gameObject.SetActive(false);
    }

    private void ResetStats()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        currentMovementSpeed = defaultMovementSpeed;
        isAlive = true;
        UpdateUI();
    }
}
