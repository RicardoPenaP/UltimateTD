using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemyData",menuName ="EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Data")]   
    [SerializeField] private string enemyName = "";

    [Header("Leveling Settings")]
    [SerializeField] private EnemyLevelingData levelingData;

    [Header("Health Settings")]
    [SerializeField, Min(1f)] private int baseHealth = 100;
    [SerializeField, Min(0f)] private int baseShield = 100;

    [Header("Movement Settings")]
    [SerializeField, Min(0f)] private float baseMovementSpeed;   
    [Tooltip("The minimun near distance from the next tile to cosider that you are in there")]
    [SerializeField, Min(0f)] private float distanceFromNextTileOffset = 0.08f;

    [Header("Rewards Settings")]
    [SerializeField, Min(0f)] private int baseGoldReward = 25;

    [Header("Damage Settings")]
    [SerializeField, Min(0)] private int baseDamageToStronghold = 1;
    [SerializeField, Min(0)] private float attackRange = 1f;
    [SerializeField] private bool isRanged = false;

    public string EnemyName { get { return enemyName; } }   
    public int BaseHealth { get { return baseHealth; } }
    public int BaseShield { get { return baseShield; } }   
    public float BaseMovementSpeed { get { return baseMovementSpeed; } }
    public float DistanceFromNextileOffset { get { return distanceFromNextTileOffset; } }
    public int BaseGoldReward { get { return baseGoldReward; } }
    public int BaseDamageToStronghold { get { return baseDamageToStronghold; } }
    public float AttackRange { get { return attackRange; } }
    public bool IsRanged { get { return isRanged; } }

    public float GetLevelRelatedStatValue(EnemyStatToAugment wantedStatValue, int level)
    {
        float statValue = 0f;
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

        return statValue * levelingData.GetAugmentCoeficient(wantedStatValue, level);
        
    }


}
