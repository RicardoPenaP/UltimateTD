using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmunition 
{
    public void SetTarget(Transform target);
    public void SetDamage(int damage);
}
