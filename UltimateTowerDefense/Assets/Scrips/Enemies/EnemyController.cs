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

    private int maxHealth;
    private int currentHealth;

    private int maxShield;
    private int currentShield;

    private int damageToStronghold;

    private float defaultMovementSpeed;
    private float movementSpeedMultiplier;
    private float currentMovementSpeed;
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
        //testing only
        currentMovementSpeed = myData.MovementSpeed;
        movementSpeedMultiplier = 1;
        distanceFromNextTileOffset = myData.DistanceFromNextileOffset;
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
}
