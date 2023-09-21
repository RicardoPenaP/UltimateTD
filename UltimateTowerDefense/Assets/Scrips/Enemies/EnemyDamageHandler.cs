using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDamageHandler : MonoBehaviour
{
    public delegate void DamageHandler(int amount);
    

    public DamageHandler OnTakeDamage;
    public DamageHandler OnHealDamage;
   
    private Transform myAimPoint;

    public bool IsAlive { get { return false; } }
    private void Awake()
    {
        myAimPoint = GetComponentInChildren<EnemyAimPoint>().transform;
    }

    public void TakeDamage(int damageAmount)
    {
        OnTakeDamage?.Invoke(damageAmount);
    }

    public void HealDamage(int healAmount)
    {
        OnHealDamage?.Invoke(healAmount);
    }

    public Transform GetEnemyAimPoint()
    {        
        return myAimPoint;
    }
}
