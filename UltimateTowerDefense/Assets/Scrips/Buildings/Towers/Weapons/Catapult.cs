using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour,ITowerWeapon
{
    [Header("Catapult")]    
    [SerializeField] private Transform shootingPos;

    private Animator myAnimator;
    private readonly int ATTACK_ANIMATION_HASH = Animator.StringToHash("Attack");
   
    private Stone myAmmunition;

    private Transform objectivePos;
    private EnemyDamageHandler target;

    private bool canAttack = true;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack(GameObject ammunition, int attackDamage, Transform attackObjectivePos)
    {
        if (!canAttack)
        {
            return;
        }

        if (myAmmunition == null)
        {
            myAmmunition = Instantiate(ammunition, shootingPos.position, Quaternion.identity, shootingPos).GetComponent<Stone>();
        }
        canAttack = false;       
        myAmmunition.SetDamage(attackDamage);        
        this.objectivePos = attackObjectivePos;
        myAmmunition.SetTarget(objectivePos);
        myAnimator.SetTrigger(ATTACK_ANIMATION_HASH);
    }

    public void Attack(GameObject ammunition, int attackDamage, EnemyDamageHandler target)
    {
        if (!canAttack)
        {
            return;
        }

        if (myAmmunition == null)
        {
            myAmmunition = Instantiate(ammunition, shootingPos.position, Quaternion.identity, shootingPos).GetComponent<Stone>();
        }
        canAttack = false;
        this.target = target;
        this.objectivePos = target.GetEnemyAimPoint();
        myAmmunition.SetDamage(attackDamage);
        myAmmunition.SetTarget(objectivePos);        
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
        if (myAmmunition == null)
        {
            return;
        }
        canAttack = true;       
        myAmmunition.transform.SetParent(transform.parent);
        myAmmunition.StartMovement();
        myAmmunition = null;
        
    }
}
