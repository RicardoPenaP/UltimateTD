using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAiming : MonoBehaviour
{
    [Header("Tower Aiming")]   

    private ITowerCanon myCanon;
    private TowerController myController;
    private EnemyController target;

    private bool canAttack = true;
    private float attackCooldownTime;

    private void Awake()
    {
        myCanon = GetComponentInChildren<ITowerCanon>();
        myController = GetComponent<TowerController>();
    }

    private void Start()
    {
        SetAttackCooldownTime(myController.AttackRatio);
    }

    private void Update()
    {
        UpdateTowerCanon();
    }

    private void SetAttackCooldownTime(float attackRatio)
    {
        attackCooldownTime = 1 / attackRatio;
    }

    private void UpdateTowerCanon()
    {
        SetTarget();

        if (!target)
        {
            return;
        }

        AimTowerCanon();
        Attack();
    }

    private void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        canAttack = false;
         
        myCanon.Attack(myController.AmmunitionPrefab,myController.AttackDamage,target.transform.position);
        StartCoroutine(AttackCooldownTime());
    }

    private void AimTowerCanon()
    {
        myCanon.AimAt(target.transform.position);
    }

    private void SetTarget()
    {
        //Takes all the near objects and search for a valid enemy in range
        Collider[] nearObjects = Physics.OverlapSphere(transform.position, myController.AttackRange);       

        foreach (Collider nearObject in nearObjects)
        {
            EnemyController enemy = nearObject.GetComponent<EnemyController>();
            if (enemy)
            {
                target = enemy;
                return;
            }
        }

        //If doesn't find a valid target set the target null
        target = null;
    }

    private IEnumerator AttackCooldownTime()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }
}
