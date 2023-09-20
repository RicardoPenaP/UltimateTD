using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAiming : MonoBehaviour
{
    [Header("Tower Aiming")]   

    private ITowerWeapon myWeapon;        
    private TowerController myController;
    private EnemyDamageHandler target;

    private bool canAttack = true;
    private float attackCooldownTime;

    private void Awake()
    {
        myWeapon = GetComponentInChildren<ITowerWeapon>();
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
        LookForTarget();

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
         
        myWeapon.Attack(myController.AmmunitionPrefab,myController.AttackDamage,target.GetEnemyAimPoint());
        SetAttackCooldownTime(myController.AttackRatio);
        StartCoroutine(AttackCooldownTime());
    }

    private void AimTowerCanon()
    {
        myWeapon.AimAt(target.transform.position);
    }

    private void LookForTarget()
    {
        if (!target)
        {
            SetTarget();
            return;
        }

        if (!target.IsAlive)
        {
            SetTarget();
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > myController.AttackRange)
        {
            SetTarget();
            return;
        }

        if (!target.gameObject.activeInHierarchy)
        {
            SetTarget();
            return;
        }
        
    }

    private void SetTarget()
    {
        //Takes all the near objects and search for a valid enemy in range

        Collider[] nearObjects = Physics.OverlapSphere(transform.position, myController.AttackRange);       

        foreach (Collider nearObject in nearObjects)
        {
            EnemyDamageHandler enemy = nearObject.GetComponent<EnemyDamageHandler>();
            if (enemy)
            {
                if (enemy.IsAlive)
                {
                    target = enemy;
                    return;
                }                
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
