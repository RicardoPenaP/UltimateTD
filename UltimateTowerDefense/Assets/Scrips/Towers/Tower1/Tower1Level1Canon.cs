using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1Level1Canon : MonoBehaviour,ITowerCanon
{
    [Header("Tower 1 Level 1 Canon")]
    [SerializeField] private Transform shootingPos;
    public void Attack(GameObject ammunition,int attackDamage, Vector3 attackObjectivePos)
    {
        
    }

    public void AimAt(Vector3 aimPos)
    {
        Vector3 newDirection = (aimPos - transform.position).normalized;
        Vector3 aimDirection = new Vector3(newDirection.x,0,newDirection.z);
        transform.right = aimDirection;
    }
}
