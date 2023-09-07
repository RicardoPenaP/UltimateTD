using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IAmmunition
{
    [Header("Bullet")]
    [SerializeField, Min(0f)] private float movementSpeed = 30f;

    private int damage;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 newDirection)
    {
        transform.forward = newDirection;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy)
        {
            enemy.TakeDamage(damage);
        }

        //Destroy Behaviour
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
