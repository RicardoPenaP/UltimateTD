using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmunition 
{
    public static readonly float ammoRange = 100;
    public void SetTarget(Transform target);
    public void SetTarget(EnemyDamageHandler target);
    public void SetDamage(int damage);
}
