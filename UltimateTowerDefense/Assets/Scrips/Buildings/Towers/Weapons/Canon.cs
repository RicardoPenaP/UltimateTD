using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour,ITowerWeapon
{
    [Header("Canon")]
    [SerializeField] private Transform canonBody;
    [SerializeField] private Transform shootingPos;
    public void Attack(GameObject ammunition, int attackDamage, Vector3 attackObjectivePos)
    {
        Vector3 objectivedirection = (attackObjectivePos - shootingPos.position).normalized;
        IAmmunition ammoShooted = Instantiate(ammunition, shootingPos.position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoShooted.SetDamage(attackDamage);
        ammoShooted.SetMovementDirection(objectivedirection);

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
