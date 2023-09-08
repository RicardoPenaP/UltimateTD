using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour,ITowerWeapon
{
    [Header("Catapult")]    
    [SerializeField] private Transform shootingPos;

    private Animator myAnimator;
    private readonly int ATTACK_ANIMATION_HASH = Animator.StringToHash("Attack");

    private IAmmunition myAmmunition;

    private Vector3 objectivePos;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack(GameObject ammunition, int attackDamage, Vector3 attackObjectivePos)
    {
        myAmmunition = Instantiate(ammunition, shootingPos.position, Quaternion.identity, shootingPos).GetComponent<IAmmunition>();
        myAmmunition.SetDamage(attackDamage);
        this.objectivePos = attackObjectivePos;
        myAnimator.SetTrigger(ATTACK_ANIMATION_HASH);
    }

    public void AimAt(Vector3 aimPos)
    {
        Vector3 newDirection = (aimPos - transform.position).normalized;
        Vector3 baseAimDirection = new Vector3(newDirection.x, 0, newDirection.z);        
        transform.forward = baseAimDirection;       
    }

    public void AnimatorAttackCompletedHelper()
    {
        (myAmmunition as MonoBehaviour).transform.SetParent(transform.parent);
        myAmmunition.SetMovementDirection(objectivePos);
    }
}
