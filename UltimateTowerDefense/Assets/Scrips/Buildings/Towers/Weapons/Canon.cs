using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour,ITowerWeapon
{
    [Header("Canon")]
    [SerializeField] private Transform canonBody;
    [SerializeField] private Transform shootingPos;
    public void Attack(GameObject ammunition, int attackDamage, Transform attackObjectivePos)
    {       
        IAmmunition ammoShooted = Instantiate(ammunition, shootingPos.position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoShooted.SetDamage(attackDamage);
        ammoShooted.SetTarget(attackObjectivePos);

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
