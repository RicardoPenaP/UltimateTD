using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyToSpawn
{
    [SerializeField] private EnemyData enemyPrefab;
    [SerializeField, Min(0)] private int amountToSpawn;
    [SerializeField, Min(0f)] private float timeBetweenSpawn;
    [SerializeField, Range(1,100)] private int enemyLevel;

    public EnemyData EnemyPrefab { get { return enemyPrefab; } }
    public int AmountToSpawn { get { return amountToSpawn; } }
    public float TimeBetweenSpawn { get { return timeBetweenSpawn; } }
    public int EnemyLevel { get { return enemyLevel; } }
}

[CreateAssetMenu(fileName = "NewWaveData",menuName ="WaveData")]
public class WaveData : ScriptableObject
{
    [Header("Wave Data")]
    [SerializeField] private EnemyToSpawn[] enemiesToSpawn;

    public EnemyToSpawn[] EnemiesToSpawn { get { return enemiesToSpawn; } }
}
