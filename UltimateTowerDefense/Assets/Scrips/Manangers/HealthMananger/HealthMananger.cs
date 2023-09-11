using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMananger : Singleton<HealthMananger>
{
    [Header("Health Mananger")]
    [SerializeField,Min(0)] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private UIBar healthBar;

    protected override void Awake()
    {
        base.Awake();
        healthBar = GetComponentInChildren<UIBar>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //GameOver behaviour
        }

        healthBar.UpdateBar(currentHealth, maxHealth);
    }
}
