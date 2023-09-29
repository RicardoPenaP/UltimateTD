using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class EnemyUI : MonoBehaviour
{   
    private void Awake()
    {
        UIHealthAndShieldBar myUIBar = GetComponentInChildren<UIHealthAndShieldBar>();
        GetComponentInParent<EnemyHealthHandler>().OnUpdateUI += myUIBar.UpdateBar;
        UICooldownBar myCooldownBar = GetComponentInChildren<UICooldownBar>();
        GetComponentInParent<IEnemy>()?.SubscribeToUpdateSkillCooldownUI(myCooldownBar.UpdateFillImage);
    }
}
