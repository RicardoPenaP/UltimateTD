using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour,IAmmunition
{
    [Header("Bullet")]
    [SerializeField, Min(0f)] private float movementSpeed = 30f;

    public event Action OnHit;

    private MeshRenderer myMeshRenderer;
    private Collider myCollider;
    private TrailRenderer myTrailRenderer;

    private int damage;
    private Transform targetPosition;
    private EnemyDamageHandler target;


    private Vector3 startingPos;

    private void Awake()
    {
        myMeshRenderer = GetComponentInChildren<MeshRenderer>();
        myTrailRenderer = GetComponentInChildren<TrailRenderer>();
        myCollider = GetComponent<Collider>();
    }

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
            enemy.TakeDamage(damage);
            Destroy();
        }


    }

    private void Move()
    {
        if (!target)
        {
            Destroy();
            return;
        }
        if (Vector3.Distance(transform.position, startingPos) >= IAmmunition.AMMO_RANGE)
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
        OnHit?.Invoke();
        myMeshRenderer.enabled = false;
        myCollider.enabled = false;
        myTrailRenderer.enabled = false;
        StartCoroutine(DestroyRoutine());
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

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(IAmmunition.DESTROY_AWAIT_TIME);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
