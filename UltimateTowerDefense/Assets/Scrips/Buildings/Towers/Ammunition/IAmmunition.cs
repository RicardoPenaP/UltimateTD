using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAmmunition 
{
    public event Action OnHit;
    public static readonly float ammoRange = 100;
    public void SetTarget(Transform target);
    public void SetTarget(EnemyDamageHandler target);
    public void SetDamage(int damage);
}
