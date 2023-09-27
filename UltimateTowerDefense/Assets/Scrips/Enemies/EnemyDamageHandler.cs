using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyController),typeof(BoxCollider), typeof(Rigidbody))]
public class EnemyDamageHandler : MonoBehaviour
{
    public event Action<int> OnTakeDamage;
    public event Action<int> OnHealDamage;

    private Transform myAimPoint;
    private EnemyHealthHandler myHealthHandler;

    public bool IsAlive { get { return myHealthHandler.IsAlive; } }
    private void Awake()
    {
        myAimPoint = GetComponentInChildren<EnemyAimPoint>().transform;
        myHealthHandler = GetComponent<EnemyHealthHandler>();
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
