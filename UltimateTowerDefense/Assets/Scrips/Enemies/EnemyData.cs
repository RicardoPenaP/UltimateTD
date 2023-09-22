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
    [SerializeField] private int baseDamageToStronghold = 1;

    
    public string EnemyName { get { return enemyName; } }
    public EnemyLevelingData LevelingData { get { return levelingData; } }
    public int BaseHealth { get { return baseHealth; } }
    public int BaseShield { get { return baseShield; } }   
    public float BaseMovementSpeed { get { return baseMovementSpeed; } }
    public float DistanceFromNextileOffset { get { return distanceFromNextTileOffset; } }
    public int BaseGoldReward { get { return baseGoldReward; } }
    public int DamageToStronghold { get { return baseDamageToStronghold; } }

}
