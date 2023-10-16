using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITowerWeapon
{
    public event Action OnAttack;
    public void Attack(GameObject ammunition, int attackDamage, Transform attackObjectivePos);
    public void Attack(GameObject ammunition, int attackDamage, EnemyDamageHandler target);

    public void AimAt(Vector3 aimPos);

    
}
