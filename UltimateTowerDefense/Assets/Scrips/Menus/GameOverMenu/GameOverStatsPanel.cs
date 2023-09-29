using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverStatsPanel : MonoBehaviour
{
    
    [Header("Game Over Stats Panel")]
    [SerializeField] private TextMeshProUGUI timePlayedText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI wavesClearedText;


    public void SetTimePlayed(float timePlayed)
    {
        timePlayedText.text = $"Time played: {timePlayed.ToString("00:00:00")}";
    }

    public void SetEnemiesKilled(int enemiesKilled)
    {
        enemiesKilledText.text = $"Enemies killed: {enemiesKilled}";
    }

    public void SetWavesCleared(int wavesCleared)
    {
        wavesClearedText.text = $"Waves cleared: {wavesCleared}";
    }
}
