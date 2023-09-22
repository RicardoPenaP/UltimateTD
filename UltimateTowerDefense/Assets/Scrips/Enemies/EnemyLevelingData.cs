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

    //public int BaseHealthAugment { get { return baseHealthAugment; } }
    //public int BaseShieldAugment { get { return baseShieldAugment; } }
    //public int BaseMovementSpeedAugment { get { return baseMovementSpeedAugment; } }
    //public int BaseGoldRewardAugment { get { return baseGoldRewardAugment; } }
    //public int BaseDamageToStrongholdAugment { get { return baseDamageToStrongholdAugment; } }

    public float GetAugmentCoeficient(StatToAugment wantedStatCoeficient, int currentLevel)
    {        
        float resultingPercentage = 0f;
        switch (wantedStatCoeficient)
        {
            case StatToAugment.BaseHealth:
                resultingPercentage = baseHealthAugment / 100;
                break;
            case StatToAugment.BaseShield:
                resultingPercentage = baseShieldAugment / 100;
                break;
            case StatToAugment.BaseMovementSpeed:
                resultingPercentage = baseMovementSpeedAugment / 100;
                break;
            case StatToAugment.BaseGoldReward:
                resultingPercentage = baseGoldRewardAugment / 100;
                break;
            case StatToAugment.BaseDamageToStronghold:
                resultingPercentage = baseDamageToStrongholdAugment / 100;
                break;
            default:
                break;
        }
        resultingPercentage *= currentLevel;
        float augmentCoeficient = 1f+resultingPercentage;
        return augmentCoeficient;
    }
}
