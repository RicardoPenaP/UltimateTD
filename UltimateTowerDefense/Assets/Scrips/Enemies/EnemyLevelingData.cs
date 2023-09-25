using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStatToAugment { BaseHealth, BaseShield, BaseMovementSpeed, BaseGoldReward, BaseDamageToStronghold}
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

    public float GetAugmentCoeficient(EnemyStatToAugment wantedStatCoeficient, int level)
    {        
        float resultingPercentage = 0f;
        switch (wantedStatCoeficient)
        {
            case EnemyStatToAugment.BaseHealth:
                resultingPercentage = (float)baseHealthAugment / 100;
                break;
            case EnemyStatToAugment.BaseShield:
                resultingPercentage = (float)baseShieldAugment / 100;
                break;
            case EnemyStatToAugment.BaseMovementSpeed:
                resultingPercentage = (float)baseMovementSpeedAugment / 100;
                break;
            case EnemyStatToAugment.BaseGoldReward:
                resultingPercentage = (float)baseGoldRewardAugment / 100;
                break;
            case EnemyStatToAugment.BaseDamageToStronghold:
                resultingPercentage = (float)baseDamageToStrongholdAugment / 100;
                break;
            default:
                break;
        }
        resultingPercentage *= level-1;
        float augmentCoeficient = 1f+resultingPercentage;
        return augmentCoeficient;
    }
}
