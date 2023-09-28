using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimatorHandler;
using System;

public class EnemyHealthHandler : MonoBehaviour
{
    [Header("Enemy Health Handler")]
    [SerializeField] private bool inmortal=false;
    public event Action<int, int, int, int> OnUpdateUI;
    public event Action OnDie;

    private EnemyAnimatorHandler myAnimatorHandler;
    private EnemyDamageHandler myDamageHandler;

    private bool isAlive = true;
    private int maxHealth;
    private int currentHealth;
    private int maxShield;
    private int currentShield;    

    public bool IsAlive { get { return isAlive; } }

    private void Awake()
    {
        myAnimatorHandler = GetComponent<EnemyAnimatorHandler>();
        myDamageHandler = GetComponent<EnemyDamageHandler>();
        SetEventSubcription();
    }

    private void SetEventSubcription()
    {
        myDamageHandler.OnTakeDamage += TakeDamage;
        myDamageHandler.OnHealDamage += HealDamage;
    }

    private void TakeDamage(int damageAmount)
    {
        if (inmortal)
        {
            return;
        }
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
        if (!isAlive)
        {
            return;
        }
        currentHealth += healedAmount;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        UpdateUI();
    }

    private void UpdateUI()
    {
        OnUpdateUI?.Invoke(currentHealth, maxHealth, currentShield, maxShield);
    }

    private void Die()
    {
        myAnimatorHandler.PlayATriggerAnimation(TrigerAnimationsToPlay.Die);
        OnDie?.Invoke();
        
        isAlive = false;       
    }

    public void InitializeHandler(int maxHealth,int maxShield)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        this.maxShield = maxShield;
        currentShield = maxShield;
        isAlive = true;
        UpdateUI();
    }

    public void RestShieldPercentage(int percentageRested)
    {
        if (!isAlive)
        {
            return;
        }
        int amountRested = Mathf.RoundToInt((float)(maxShield * percentageRested) / 100);
        currentShield += amountRested;
        currentShield = currentShield > maxShield ? maxShield : currentShield;

    }

}
