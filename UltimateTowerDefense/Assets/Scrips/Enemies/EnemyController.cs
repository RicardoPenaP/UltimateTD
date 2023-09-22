using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void UpdateEnemyUIDelegate(int healthCurrentValue, int healthMaxValue, int shieldCurrentValue, int shieldMaxValue);    
   
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;
    [SerializeField] private bool canMove = true;

    public UpdateEnemyUIDelegate OnUIUpdate;

    private List<Tile> path;

    [SerializeField]private int level;

    private int maxHealth;
    [SerializeField] private int currentHealth;

    private int maxShield;
    [SerializeField] private int currentShield;

    [SerializeField] private int damageToStronghold;
    [SerializeField] private int goldReward;

    private float defaultMovementSpeed;
    private float movementSpeedMultiplier;
    [SerializeField] private float currentMovementSpeed;
    private float distanceFromNextTileOffset;

    private bool isAlive = true;
    

    public List<Tile> Path { get { return path; } }    
   
    public float CurrentMovementSpeed { get { return currentMovementSpeed * movementSpeedMultiplier; } }
    public float DistanceFromNextTileOffset { get { return distanceFromNextTileOffset; } }

    public bool IsAlive { get { return isAlive; } }
    public bool CanMove { get { return canMove; } }

    private void Awake()
    {
        InitHandlers();
    }

    private void Start()
    {             
        movementSpeedMultiplier = 1;
        distanceFromNextTileOffset = myData.DistanceFromNextileOffset;
        SetLevel(level);
    }

    private void InitHandlers()
    {
        EnemyDamageHandler myDamageHandler = GetComponent<EnemyDamageHandler>();
        if (myDamageHandler)
        {
            myDamageHandler.OnTakeDamage += TakeDamage;
            myDamageHandler.OnHealDamage += HealDamage;
        }
    }

    private void TakeDamage(int damageAmount)
    {
        int damageTaken = damageAmount;
        if (currentShield >= damageTaken)
        {
            currentShield -= damageTaken;
            damageTaken = 0;
        }
        else
        {
            damageTaken -= currentShield;
        }

        if (damageTaken > 0)
        {
            currentHealth -= damageTaken;
            damageTaken = 0;
        }
        
        if (currentHealth <= 0)
        {
            isAlive = false;
            //Die Behaviour
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
        this.path = path;
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > 100)
        {
            return; 
        }

        this.level = level;
        SetLevelStats();
        currentHealth = maxHealth;
        currentShield = maxShield;
        currentMovementSpeed = defaultMovementSpeed;

    }

    private void SetLevelStats()
    {
        maxHealth = Mathf.RoundToInt( myData.GetLevelRelatedStatValue(StatToAugment.BaseHealth, level));
        maxShield = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(StatToAugment.BaseShield, level));
        damageToStronghold = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(StatToAugment.BaseDamageToStronghold, level));
        defaultMovementSpeed = myData.GetLevelRelatedStatValue(StatToAugment.BaseMovementSpeed, level);
        goldReward = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(StatToAugment.BaseGoldReward, level));
    }
}
