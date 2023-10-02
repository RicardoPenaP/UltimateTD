using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour,IAmmunition
{
    [Header("Canon Ball")]
    [SerializeField,Min(0f)] private float movementSpeed = 30f;

    private int damage;   
    private Transform target;
    private EnemyDamageHandler targetDamageHandler;

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

        if (enemy == targetDamageHandler)
        {
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

        if (targetDamageHandler.IsAlive)
        {
            transform.forward = (target.position - transform.position).normalized;           
        }
        else
        {
            transform.forward = (transform.position-startingPos).normalized;
        }

        transform.forward = (target.position - transform.position).normalized;
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
        this.target = target;
        targetDamageHandler = target.GetComponent<EnemyDamageHandler>();
    }

    public void SetTarget(EnemyDamageHandler target)
    {

    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

   
}
