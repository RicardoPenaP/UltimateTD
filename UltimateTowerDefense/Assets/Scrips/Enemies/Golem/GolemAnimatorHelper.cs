using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnimatorHelper : MonoBehaviour
{
    private GolemIA myIA;

    private void Awake()
    {
        myIA = GetComponentInParent<GolemIA>();
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
