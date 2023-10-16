using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Canon : MonoBehaviour,ITowerWeapon
{
    [Header("Canon")]
    [SerializeField] private Transform canonBody;
    [SerializeField] private Transform shootingPos;
    public event Action OnAttack;
    public void Attack(GameObject ammunition, int attackDamage, Transform attackObjectivePos)
    {
        OnAttack?.Invoke();
        IAmmunition ammoShooted = Instantiate(ammunition, shootingPos.position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoShooted.SetDamage(attackDamage);
        ammoShooted.SetTarget(attackObjectivePos);

    }

    public void Attack(GameObject ammunition, int attackDamage, EnemyDamageHandler target)
    {
        OnAttack?.Invoke();
        IAmmunition ammoShooted = Instantiate(ammunition, shootingPos.position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoShooted.SetDamage(attackDamage);
        ammoShooted.SetTarget(target);

    }

    public void AimAt(Vector3 aimPos)
    {
        Vector3 newDirection = (aimPos - transform.position).normalized;
        Vector3 baseAimDirection = new Vector3(newDirection.x, 0, newDirection.z);
        Vector3 bodyAimDirection = newDirection;
        transform.forward = baseAimDirection;
        canonBody.forward = bodyAimDirection;
    }
}
