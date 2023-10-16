using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : MonoBehaviour,ITowerWeapon
{
    [Header("Gun")]    
    [SerializeField] private Transform[] shootingPos;
    public event Action OnAttack;
    private int shootingPosIndex = 0;
    public void Attack(GameObject ammunition, int attackDamage, Transform attackObjectivePos)
    {       
        IAmmunition ammoshooted = Instantiate(ammunition, shootingPos[shootingPosIndex].position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoshooted.SetDamage(attackDamage);
        ammoshooted.SetTarget(attackObjectivePos);
        shootingPosIndex++;
        shootingPosIndex = shootingPosIndex >= shootingPos.Length ? 0 : shootingPosIndex;
        OnAttack?.Invoke();
    }

    public void Attack(GameObject ammunition, int attackDamage, EnemyDamageHandler target)
    {
        OnAttack?.Invoke();
        IAmmunition ammoshooted = Instantiate(ammunition, shootingPos[shootingPosIndex].position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoshooted.SetDamage(attackDamage);
        ammoshooted.SetTarget(target);
        shootingPosIndex++;
        shootingPosIndex = shootingPosIndex >= shootingPos.Length ? 0 : shootingPosIndex;
    }

    public void AimAt(Vector3 aimPos)
    {
        Vector3 newDirection = (aimPos - transform.position).normalized;
        Vector3 baseAimDirection = new Vector3(newDirection.x, 0, newDirection.z);        
        transform.forward = baseAimDirection;
        
    }
}
