using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void UpdateEnemyUIDelegate(int healthCurrentValue, int healthMaxValue, int shieldCurrentValue, int shieldMaxValue);
   
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;

    public UpdateEnemyUIDelegate OnUIUpdate;

    private int maxHealth;
    private int currentHealth;

    private int maxShield;
    private int currentShield;

    private int currentAttackDamage;
    private float attackRange;

    private float normalMovementSpeed;
    private float movementSpeedMultiplier;
    private float currentMovementSpeed;
    

    private void Awake()
    {
        InitHandlers();

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

}
