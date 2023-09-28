using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSceneManangement;

public class PauseMenu : Singleton<PauseMenu>
{
    private static readonly float DEFAULT_TIME_SCALE_VALUE = 1f;
    private static readonly float PAUSE_TIME_SCALE_VALUE = 0f;

    [Header("Pause Menu")]
    [Header("Buttons Reference")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;

    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    protected override void Awake()
    {
        base.Awake();
        SubmitButtonsEvents();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void SubmitButtonsEvents()
    {
        resumeButton?.onClick.AddListener(ToggleMenu);
        mainMenuButton?.onClick.AddListener(GoToMainMenu);
    }

    public void ToggleMenu()
    {
        isPaused = !isPaused;
        gameObject.SetActive(!gameObject.activeInHierarchy);
        Time.timeScale = isPaused? PAUSE_TIME_SCALE_VALUE : DEFAULT_TIME_SCALE_VALUE;
    }

    public void GoToMainMenu()
    {
        ToggleMenu();
        GameScenesLoader.LoadGameScene(GameScenes.MainMenu);
    }

    
}
