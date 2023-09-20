using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IAmmunition
{
    [Header("Bullet")]
    [SerializeField, Min(0f)] private float movementSpeed = 30f;

    private int damage;
    private Transform target;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.forward = (target.position - transform.position).normalized;
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyDamageHandler enemy = other.GetComponent<EnemyDamageHandler>();

        if (enemy)
        {
            enemy.TakeDamage(damage);
        }

        //Destroy Behaviour
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
