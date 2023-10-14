using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesWaves;

[System.Serializable]
public struct LevelWaves
{
    [SerializeField] private LevelWaveData[] levelWaveData;
    public LevelWaveData[] LevelWaveData { get { return levelWaveData; } }
}

[CreateAssetMenu(fileName = "NewLevelWaveData", menuName = "LevelWaveData")]
public class LevelWaveData : ScriptableObject
{
    [SerializeField] private WaveData[] waveData;
    public WaveData[] WaveData { get { return waveData; } }
}
