using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    private EnemyController myController;

    public bool IsAlive { get { return myController.IsAlive; } }
    private void Awake()
    {
        myController = GetComponentInParent<EnemyController>();
    }

    public void TakeDamage(int damage)
    {
        myController.TakeDamage(damage);
    }
}
