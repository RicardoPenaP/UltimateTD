using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,ITowerWeapon
{
    [Header("Gun")]    
    [SerializeField] private Transform[] shootingPos;

    private int shootingPosIndex = 0;
    public void Attack(GameObject ammunition, int attackDamage, Vector3 attackObjectivePos)
    {
        Vector3 objectivedirection = (attackObjectivePos - shootingPos[shootingPosIndex].position).normalized;
        IAmmunition ammoshooted = Instantiate(ammunition, shootingPos[shootingPosIndex].position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoshooted.SetDamage(attackDamage);
        ammoshooted.SetMovementDirection(objectivedirection);

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
