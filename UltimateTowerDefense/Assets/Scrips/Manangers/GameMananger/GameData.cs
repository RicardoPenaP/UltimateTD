using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesWaves;

[System.Serializable]
public struct EnemiesReference
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyController enemyPrefab;

    public EnemyType EnemyType { get { return enemyType; } }
    public EnemyController EnemyPrefab { get { return enemyPrefab; } }
}

[System.Serializable]
public struct LevelWaves 
{
    [SerializeField] private WaveData[] waveData;   
    public WaveData[] WaveData { get { return waveData; } }
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

    [SerializeField] private LevelWaves[] singleRoadLevelWaves;
    [SerializeField] private LevelWaves[] doubleRoadLevelWaves;
    [SerializeField] private LevelWaves[] tripleRoadLevelWaves;
    [SerializeField] private LevelWaves[] quadRoadLevelWaves;


    public static EnemiesReference[] UndeadEnemies { get { return null;} }
    public WaveData[] SingleRoadWaves { get { return singleRoadWaves; } }
    public WaveData[] DoubleRoadWaves { get { return doubleRoadWaves; } }
    public WaveData[] TripleRoadWaves { get { return tripleRoadWaves; } }
    public WaveData[] QuadRoadWaves { get { return quadRoadWaves; } }

    //public LevelWaves[] SingleRoadLevelWaves { get { return singleRoadLevelWaves; } }
    //public LevelWaves[] DoubleRoadLevelWaves { get { return doubleRoadLevelWaves; } }
    //public LevelWaves[] TripleRoadLevelWaves { get { return tripleRoadLevelWaves; } }
    //public LevelWaves[] QuadRoadLevelWaves { get { return quadRoadLevelWaves; } }

    public EnemyController GetEnemyPrefab(EnemyType enemyType)
    {
        EnemyController enemyPrefab = null;
        foreach (EnemiesReference enemy in undeadEnemies)
        {
            if (enemyType == enemy.EnemyType)
            {
                enemyPrefab = enemy.EnemyPrefab;
            }
        }
        return enemyPrefab;
    }

    public WaveData[] GetRandomWaveData(GameModeOptions gameMode)
    {
        int auxIndex = 0;
        switch (gameMode)
        {
            case GameModeOptions.SingleRoad:
                auxIndex = Random.Range(0, singleRoadLevelWaves.Length);
                return singleRoadLevelWaves[auxIndex].WaveData;
                
            case GameModeOptions.DoubleRoad:
                auxIndex = Random.Range(0, doubleRoadLevelWaves.Length);
                return doubleRoadLevelWaves[auxIndex].WaveData;
                
            case GameModeOptions.TripleRoad:
                auxIndex = Random.Range(0, tripleRoadLevelWaves.Length);
                return tripleRoadLevelWaves[auxIndex].WaveData;
             
            case GameModeOptions.QuadRoad:
                auxIndex = Random.Range(0, quadRoadLevelWaves.Length);
                return quadRoadLevelWaves[auxIndex].WaveData;
        }
        return null; 
    }
}
