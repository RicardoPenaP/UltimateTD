using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class GameOverStatsPanel : MonoBehaviour
{
    
    [Header("Game Over Stats Panel")]
    [SerializeField] private TextMeshProUGUI timePlayedText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI wavesClearedText;


    public void SetTimePlayed(float timePlayed)
    {
        TimeSpan t = TimeSpan.FromSeconds(timePlayed);
        timePlayedText.text = $"Time played: {t.Hours.ToString("00")}:{t.Minutes.ToString("00")}:{t.Seconds.ToString("00")}";                             //+string.Format("{0:D2}:{1:D2}:{2:D3}",t.Hours,t.Minutes,t.Seconds);
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
