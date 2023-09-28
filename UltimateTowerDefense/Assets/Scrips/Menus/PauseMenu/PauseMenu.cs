using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static readonly float DEFAULT_TIME_SCALE_VALUE = 1f;
    private static readonly float PAUSE_TIME_SCALE_VALUE = 1f;

    [Header("Pause Menu")]
    [Header("Buttons Reference")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;

    private bool isPaused = false;

    private void Awake()
    {
        SubmitButtonsEvents();
    }

    private void SubmitButtonsEvents()
    {
        resumeButton?.onClick.AddListener(ToggleMenu);
    }

    public void ToggleMenu()
    {
        isPaused = !isPaused;
        gameObject.SetActive(!isPaused);
        Time.timeScale = isPaused? PAUSE_TIME_SCALE_VALUE : DEFAULT_TIME_SCALE_VALUE;
    }

    
}
