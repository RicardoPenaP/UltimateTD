using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController),typeof(BoxCollider), typeof(Rigidbody))]
public class EnemyDamageHandler : MonoBehaviour
{
    public delegate void DamageHandler(int amount);    

    public DamageHandler OnTakeDamage;
    public DamageHandler OnHealDamage;

    private EnemyController myController;
    private Transform myAimPoint;

    public bool IsAlive { get { return myController.IsAlive; } }
    private void Awake()
    {
        myController = GetComponent<EnemyController>();
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
