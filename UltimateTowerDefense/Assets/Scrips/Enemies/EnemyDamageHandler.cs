using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    private EnemyController myController;

    public bool IsAlive { get { return myController.IsAlive; } }
    private void Awake()
    {
        myController = GetComponentInParent<EnemyController>();
    }

    public void TakeDamage(int damage)
    {
        myController.TakeDamage(damage);
    }

    public void HealDamage(int healAmount)
    {
        myController.HealDamage(healAmount);
    }

    public void HealMaxHealthPercentage(int percentage)
    {
        int healthHealed = Mathf.RoundToInt((float)myController.GetCurrentMaxHealth() * (1f + ((float)percentage / 100)));
        myController.HealDamage(healthHealed);
    }

    public Transform GetEnemyAimPoint()
    {
        return myController.AimPoint;
    }
}
