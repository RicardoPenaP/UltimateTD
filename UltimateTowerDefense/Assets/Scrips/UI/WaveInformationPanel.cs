using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveInformationPanel : Singleton<WaveInformationPanel>
{
    private TextMeshProUGUI currentWaveText;
    private TextMeshProUGUI enemiesLeftText;

    protected override void Awake()
    {
        base.Awake();
        currentWaveText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        enemiesLeftText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void UpdateWaveInformationPanel(int currenWave, int enemiesLeft)
    {
        currentWaveText.text = $"Current Wave: {currenWave}";
        enemiesLeftText.text = $"Enemies Left: {enemiesLeft}";
    }
}
