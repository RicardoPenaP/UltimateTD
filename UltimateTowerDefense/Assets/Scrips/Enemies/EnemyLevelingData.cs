using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatToAugment { BaseHealth, BaseShield, BaseMovementSpeed, BaseGoldReward, BaseDamageToStronghold}
[CreateAssetMenu(fileName = "NewEnemyLevelingData", menuName = "EnemyLevelingData")]
public class EnemyLevelingData : ScriptableObject
{
    [Header("Enemy Leveling Data")]
    [Header("Percentage Augment Per Level")]
    [Header("Health")]
    [SerializeField, Range(0, 100)] private int baseHealthAugment = 0;
    [SerializeField, Range(0, 100)] private int baseShieldAugment = 0;

    [Header("Movement")]
    [SerializeField, Range(0, 100)] private int baseMovementSpeedAugment = 0;

    [Header("Rewards")]
    [SerializeField, Range(0, 100)] private int baseGoldRewardAugment = 0;

    [Header("Damage")]
    [SerializeField, Range(0, 100)] private int baseDamageToStrongholdAugment = 0;

    public float GetAugmentCoeficient(StatToAugment wantedStatCoeficient, int currentLevel)
    {        
        float resultingPercentage = 0f;
        switch (wantedStatCoeficient)
        {
            case StatToAugment.BaseHealth:
                resultingPercentage = (float)baseHealthAugment / 100;
                break;
            case StatToAugment.BaseShield:
                resultingPercentage = (float)baseShieldAugment / 100;
                break;
            case StatToAugment.BaseMovementSpeed:
                resultingPercentage = (float)baseMovementSpeedAugment / 100;
                break;
            case StatToAugment.BaseGoldReward:
                resultingPercentage = (float)baseGoldRewardAugment / 100;
                break;
            case StatToAugment.BaseDamageToStronghold:
                resultingPercentage = (float)baseDamageToStrongholdAugment / 100;
                break;
            default:
                break;
        }
        resultingPercentage *= currentLevel-1;
        float augmentCoeficient = 1f+resultingPercentage;
        return augmentCoeficient;
    }
}
