using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesWaves;

[System.Serializable]
public struct EnemiesReference
{
    private EnemyType enemyType;
    private EnemyController enemyPrefab;

    public EnemyType EnemyType { get { return enemyType; } }
    public EnemyController EnemyPrefab { get { return enemyPrefab; } }
}
[CreateAssetMenu(fileName = "NewGameData", menuName = "GameData")]
public class GameData : ScriptableObject
{
    [Header("Game Data")]
    [Header("Enemies Reference")]
    [SerializeField] private EnemiesReference[] undeadEnemies;

    [Header("Wave Reference")]
    [SerializeField] private WaveData[] singleRoadWaves;
    [SerializeField] private WaveData[] doubleRoadWaves;
    [SerializeField] private WaveData[] tripleRoadWaves;
    [SerializeField] private WaveData[] quadRoadWaves;

    public WaveData[] SingleRoadWaves { get { return singleRoadWaves; } }
    public WaveData[] DoubleRoadWaves { get { return doubleRoadWaves; } }
    public WaveData[] TripleRoadWaves { get { return tripleRoadWaves; } }
    public WaveData[] QuadRoadWaves { get { return quadRoadWaves; } }

}
