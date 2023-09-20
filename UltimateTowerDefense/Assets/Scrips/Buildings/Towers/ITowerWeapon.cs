using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerWeapon
{
    public void Attack(GameObject ammunition, int attackDamage, Transform attackObjectivePos);

    public void AimAt(Vector3 aimPos);
}
