using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemiesWaves
{    
    [System.Serializable]
    public struct EnemyToSpawn
    {
        [Header("Enemy To Spawn")]
        [Tooltip("Enemy")]
        [SerializeField] EnemyType enemy;
        [Tooltip("Prefab reference")]
        [SerializeField] private EnemyController enemyPrefabReference;
        [Tooltip("Amount of total units of this enemy to spawn along the wave")]
        [SerializeField, Min(0)] private int amountToSpawn;
        [Tooltip("Amount of time between each unit spawn along the wave")]
        [SerializeField, Min(0f)] private float timeBetweenSpawn;
        [Tooltip("The unit level")]
        [SerializeField, Range(1, 100)] private int enemyLevel;

        public EnemyType Enemy { get { return enemy; } }
        public EnemyController EnemyPrefabReference { get { return enemyPrefabReference; } }
        public int AmountToSpawn { get { return amountToSpawn; } }
        public float TimeBetweenSpawn { get { return timeBetweenSpawn; } }
        public int EnemyLevel { get { return enemyLevel; } }
       
    }

    [CreateAssetMenu(fileName = "NewWaveData", menuName = "WaveData")]
    public class WaveData : ScriptableObject
    {
        [Header("Wave Data")]
        [SerializeField] private EnemyToSpawn[] enemiesToSpawn;

        public EnemyToSpawn[] EnemiesToSpawn { get { return enemiesToSpawn; } }
    }
}

