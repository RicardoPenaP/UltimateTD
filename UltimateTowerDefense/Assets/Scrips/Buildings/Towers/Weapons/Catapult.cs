using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour,ITowerWeapon
{
    [Header("Catapult")]    
    [SerializeField] private Transform shootingPos;

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack(GameObject ammunition, int attackDamage, Vector3 attackObjectivePos)
    {
        Vector3 objectivedirection = (attackObjectivePos - shootingPos.position).normalized;
        IAmmunition ammoshooted = Instantiate(ammunition, shootingPos.position, Quaternion.identity, transform.parent).GetComponent<IAmmunition>();
        ammoshooted.SetDamage(attackDamage);
        ammoshooted.SetMovementDirection(objectivedirection);

    }

    public void AimAt(Vector3 aimPos)
    {
        Vector3 newDirection = (aimPos - transform.position).normalized;
        Vector3 baseAimDirection = new Vector3(newDirection.x, 0, newDirection.z);        
        transform.forward = baseAimDirection;       
    }
}
