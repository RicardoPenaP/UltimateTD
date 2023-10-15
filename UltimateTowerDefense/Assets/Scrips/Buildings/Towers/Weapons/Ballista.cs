using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour, ITowerWeapon
{
    [Header("Ballista")]
    [SerializeField] private Transform ballistaBody;
    [SerializeField] private Transform shootingPos;
    public void Attack(GameObject ammunition, int attackDamage, Transform attackObjectivePos)
    {        
        IAmmunition ammoshooted = Instantiate(ammunition, shootingPos.position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoshooted.SetDamage(attackDamage);
        ammoshooted.SetTarget(attackObjectivePos);

    }

    public void Attack(GameObject ammunition, int attackDamage, EnemyDamageHandler target)
    {
        IAmmunition ammoShooted = Instantiate(ammunition, shootingPos.position, shootingPos.rotation, transform.parent).GetComponent<IAmmunition>();
        ammoShooted.SetDamage(attackDamage);
        ammoShooted.SetTarget(target);

    }

    public void AimAt(Vector3 aimPos)
    {
        Vector3 newDirection = (aimPos - transform.position).normalized;
        Vector3 baseAimDirection = new Vector3(newDirection.x, 0, newDirection.z);
        Vector3 bodyAimDirection = newDirection;
        transform.forward = baseAimDirection;
        ballistaBody.forward = bodyAimDirection;
    }
}
