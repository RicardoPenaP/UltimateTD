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

    public float GetAugmentCoeficient(EnemyStatToAugment wantedStatCoeficient)
    {        
        float augmentCoeficient = 0f;
        switch (wantedStatCoeficient)
        {
            case EnemyStatToAugment.BaseHealth:
                augmentCoeficient = (float)baseHealthAugment / 100;
                break;
            case EnemyStatToAugment.BaseShield:
                augmentCoeficient = (float)baseShieldAugment / 100;
                break;
            case EnemyStatToAugment.BaseMovementSpeed:
                augmentCoeficient = (float)baseMovementSpeedAugment / 100;
                break;
            case EnemyStatToAugment.BaseGoldReward:
                augmentCoeficient = (float)baseGoldRewardAugment / 100;
                break;
            case EnemyStatToAugment.BaseDamageToStronghold:
                augmentCoeficient = (float)baseDamageToStrongholdAugment / 100;
                break;
            default:
                break;
        }

        augmentCoeficient = 1f+augmentCoeficient;
        return augmentCoeficient;
    }
}
