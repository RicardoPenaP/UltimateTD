using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IAmmunition
{
    [Header("Bullet")]
    [SerializeField, Min(0f)] private float movementSpeed = 30f;

    private int damage;
    private Transform target;

    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (target.gameObject.activeInHierarchy)
        {
            transform.forward = (target.position - transform.position).normalized;
        }
        else
        {
            transform.forward = (transform.position - startingPos).normalized;
        }

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
