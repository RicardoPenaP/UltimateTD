using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimatorHelper : MonoBehaviour
{
    private SlimeIA myIA;

    private void Awake()
    {
        myIA = GetComponentInParent<SlimeIA>();
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