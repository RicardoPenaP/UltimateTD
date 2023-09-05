using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmunition 
{
    public void SetMovementDirection(Vector3 newDirection);
    public void SetDamage(int damage);
}
