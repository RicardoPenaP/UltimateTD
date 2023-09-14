using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemyData",menuName ="EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Data")]

    [Header("Reference Settings")]
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private string enemyName = "";

    [Header("Health Settings")]
    [SerializeField, Min(1f)] private int maxHealth = 100;
    [SerializeField, Min(0f)] private int maxShield = 100;

    [Header("Level Settings")]
    [SerializeField, Min(1f)] private float healthLevelMultiplier = 1.5f;

    [Header("Movement Settings")]
    [SerializeField, Min(0f)] private float movementSpeed;   
    [Tooltip("The minimun near distance from the next tile to cosider that you are in there")]
    [SerializeField, Min(0f)] private float distanceFromNextTileOffset = 0.08f;

    [Header("Rewards Settings")]
    [SerializeField, Min(0f)] private int goldReward = 25;

    [Header("Damage Settings")]
    [SerializeField] private int damageToStronghold = 1;


    public EnemyController EnemyPrefab { get { return enemyPrefab; } }
    public string EnemyName { get { return enemyName; } }
    public int MaxHealth { get { return maxHealth; } }
    public int MaxShield { get { return maxShield; } }
    public float HealthLevelMultiplier { get { return healthLevelMultiplier; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float DistanceFromNextileOffset { get { return distanceFromNextTileOffset; } }
    public int GoldReward { get { return goldReward; } }
    public int DamageToStronghold { get { return damageToStronghold; } }


}
