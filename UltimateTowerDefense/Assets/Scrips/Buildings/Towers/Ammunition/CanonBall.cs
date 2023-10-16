using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanonBall : MonoBehaviour,IAmmunition
{
    [Header("Canon Ball")]
    [SerializeField,Min(0f)] private float movementSpeed = 30f;

    public event Action OnHit;

    private int damage;   
    private Transform targetPosition;
    private EnemyDamageHandler target;

    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyDamageHandler enemy = other.GetComponent<EnemyDamageHandler>();

        if (enemy == target)
        {
            OnHit?.Invoke();
            enemy.TakeDamage(damage);
            Destroy();
        }

        
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position,startingPos) >= IAmmunition.ammoRange)
        {
            Destroy();
            return;
        }

        if (target?.IsAlive == true)
        {
            transform.forward = (targetPosition.position - transform.position).normalized;           
        }
        else
        {
            Destroy();
        }

        transform.forward = (targetPosition.position - transform.position).normalized;
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    private void Destroy()
    {
        //Destroy Behaviour
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.targetPosition = target;       
    }

    public void SetTarget(EnemyDamageHandler target)
    {
        this.target = target;
        targetPosition = target.GetEnemyAimPoint();
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

   
}
