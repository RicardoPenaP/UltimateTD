using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusAnimatorHelper : MonoBehaviour
{
    private CactusIA myIA;

    private void Awake()
    {
        myIA = GetComponentInParent<CactusIA>();
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
