using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class GameManangerNextWavePanel : MonoBehaviour
{
    [Header("Game Mananger Next Wave Panel")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button startNowButton;

    public void TogglePanel()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void UpdateTimer(float time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        timerText.text = $"{t.Minutes.ToString("00")}:{t.Seconds.ToString("00")}:{t.Milliseconds.ToString("00")}";
    }

    public void SubmitToStarNowButtonOnClickEvent(UnityAction action)
    {
        startNowButton.onClick.AddListener(action);
    }

    public void UnSubmitToStarNowButtonOnClickEvent(UnityAction action)
    {
        startNowButton.onClick.RemoveListener(action);
    }
}
