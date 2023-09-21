using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimatorHelper : MonoBehaviour
{
    private MushroomIA myIA;

    private void Awake()
    {
        myIA = GetComponentInParent<MushroomIA>();
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
