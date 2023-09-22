using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimatorHelper : MonoBehaviour
{
    public event Action OnAttackAnimationStarted;
    public event Action OnAttackAnimationPerformed;
    public event Action OnAttackAnimationEnded;

    public event Action OnDieAnimationStarted;
    public event Action OnDieAnimationPerformed;
    public event Action OnDieAnimationEnded;

    public void AttackAnimationStarted()
    {
        OnAttackAnimationStarted?.Invoke();
    }

    public void AttackAnimationPerformed()
    {
        OnAttackAnimationPerformed?.Invoke();
    }

    public void AttackAnimationEnded()
    {
        OnAttackAnimationEnded?.Invoke();
    }

    public void DieAnimationStarted()
    {
        OnDieAnimationStarted?.Invoke();
    }

    public void DieAnimationPerformed()
    {
        OnDieAnimationPerformed?.Invoke();
    }

    public void DieAnimationEnded()
    {
        OnDieAnimationEnded?.Invoke();
    }

}
