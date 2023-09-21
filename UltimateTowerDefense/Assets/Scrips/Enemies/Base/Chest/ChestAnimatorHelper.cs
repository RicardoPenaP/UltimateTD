using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimatorHelper : MonoBehaviour
{
    private ChestIA myIA;

    private void Awake()
    {
        myIA = GetComponentInParent<ChestIA>();
    }

    public void AttackAnimationCompleted()
    {
        myIA.AttackAnimationCompleted();
    }

    public void DieAnimationCompleted()
    {
        myIA.DieAnimationCompleted();
    }
}
