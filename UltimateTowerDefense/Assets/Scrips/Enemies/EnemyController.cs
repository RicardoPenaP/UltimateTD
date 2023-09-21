using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;


    private int maxHealth;
    private int currentHealth;

    private int maxShield;
    private int currentShield;

    private int currentAttackDamage;

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

        //Update Enemy UI;
    }

    private void HealDamage(int healedAmount)
    {
        currentHealth += healedAmount;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        //UpdateUI
    }

}
