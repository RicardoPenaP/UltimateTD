using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemyData",menuName ="EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Data")]   
    
    [Header("Leveling Settings")]
    [SerializeField] private EnemyLevelingData levelingData;

    [Header("Health Settings")]
    [SerializeField, Min(1f)] private int baseHealth = 100;
    [SerializeField, Min(0f)] private int baseShield = 100;

    [Header("Movement Settings")]
    [SerializeField, Min(0f)] private float baseMovementSpeed; 

    [Header("Rewards Settings")]
    [SerializeField, Min(0f)] private int baseGoldReward = 25;

    [Header("Damage Settings")]
    [SerializeField, Min(0)] private int baseDamageToStronghold = 1;
    [SerializeField, Min(0)] private float attackRange = 1f;

    public float AttackRange { get { return attackRange; } }   

    public float GetLevelRelatedStatValue(EnemyStatToAugment wantedStatValue, int level)
    {
        float statValue = 0f;
        float statCoeficient = levelingData.GetAugmentCoeficient(wantedStatValue);
        switch (wantedStatValue)
        {
            case EnemyStatToAugment.BaseHealth:
                statValue = baseHealth;
                break;
            case EnemyStatToAugment.BaseShield:
                statValue = baseShield;
                break;
            case EnemyStatToAugment.BaseMovementSpeed:
                statValue = baseMovementSpeed;
                break;
            case EnemyStatToAugment.BaseGoldReward:
                statValue = baseGoldReward;
                break;
            case EnemyStatToAugment.BaseDamageToStronghold:
                statValue = baseDamageToStronghold;
                break;
            default:
                break;
        }

        for (int i = 0; i < level-1; i++)
        {
            statValue *= statCoeficient;
        }

        return statValue;
        
    }


}
