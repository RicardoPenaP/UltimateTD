using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAiming : MonoBehaviour
{
    [Header("Tower Aiming")]
    [SerializeField] private LayerMask validLayerToAim;
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
        if (!HaveAValidTarged())
        {
            LookForTarget();
            return;
        }

        AimTowerCanon();
        Attack();
    }

    private bool HaveAValidTarged()
    {
        if (!target)
        {
            return false;
        }

        if (!target.IsAlive)
        {
            return false;
        }

        if (Vector3.Distance( target.transform.position,transform.position) > myController.AttackRange)
        {
            return false;
        }
        return true;
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
        SetTarget();
    }

    private void SetTarget()
    {
        //Takes all the near objects and search for a valid enemy in range

        Collider[] nearObjects = Physics.OverlapSphere(transform.position, myController.AttackRange, validLayerToAim);       

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
