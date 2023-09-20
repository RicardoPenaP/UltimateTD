using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour,IAmmunition
{
    [Header("Canon Ball")]
    [SerializeField,Min(0f)] private float movementSpeed = 30f;

    private int damage;
    private Transform target;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.right = (target.position - transform.position).normalized;
        transform.position += transform.right * movementSpeed * Time.deltaTime;
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
