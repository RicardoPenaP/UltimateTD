using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyToSpawn
{
    [Header("Enemy To Spawn")]
    [Tooltip("Prefab reference")]
    [SerializeField] private EnemyData enemyDataReference;
    [Tooltip("Amount of total units of this enemy to spawn along the wave")]
    [SerializeField, Min(0)] private int amountToSpawn;
    [Tooltip("Amount of time between each unit spawn along the wave")]
    [SerializeField, Min(0f)] private float timeBetweenSpawn;
    [Tooltip("The unit level")]
    [SerializeField, Range(1,100)] private int enemyLevel;
    [Tooltip("If this unit is a Boss or not")]
    [SerializeField] private bool isABoss;

    public EnemyData EnemyDataReference { get { return enemyDataReference; } }
    public int AmountToSpawn { get { return amountToSpawn; } }
    public float TimeBetweenSpawn { get { return timeBetweenSpawn; } }
    public int EnemyLevel { get { return enemyLevel; } }
    public bool IsABoss { get { return isABoss; } }
}

[CreateAssetMenu(fileName = "NewWaveData",menuName ="WaveData")]
public class WaveData : ScriptableObject
{
    [Header("Wave Data")]
    [SerializeField] private EnemyToSpawn[] enemiesToSpawn;

    public EnemyToSpawn[] EnemiesToSpawn { get { return enemiesToSpawn; } }
}
