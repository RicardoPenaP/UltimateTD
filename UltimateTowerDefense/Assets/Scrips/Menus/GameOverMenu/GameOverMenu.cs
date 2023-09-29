using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameSceneManangement;

public class GameOverMenu : Singleton<GameOverMenu>
{
    [Header("Game Over Menu")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameOverStatsPanel gameOverStatsPanel;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button playAgainButton;

    protected override void Awake()
    {
        base.Awake();
        SubmitToButtonsEvents();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void SubmitToButtonsEvents()
    {
        mainMenuButton?.onClick.AddListener(GoToMainMenu);
        playAgainButton?.onClick.AddListener(PlayAgain);
    }

    private void GoToMainMenu()
    {
        gameObject.SetActive(false);
        GameScenesLoader.LoadGameScene(GameScenes.MainMenu);
    }

    private void PlayAgain()
    {
        gameObject.SetActive(false);
        GameScenesLoader.ReloadCurrentScene();
    }

    public void OpenGameOverMenu(float timePlayed,int enemiesKilled,int wavesCleared)
    {
        titleText.text = "Game Over";
        gameOverStatsPanel.SetTimePlayed(timePlayed);
        gameOverStatsPanel.SetEnemiesKilled(enemiesKilled);
        gameOverStatsPanel.SetWavesCleared(wavesCleared);
        gameObject.SetActive(true);
    }

    public void OpenGameCompleteMenu(float timePlayed, int enemiesKilled, int wavesCleared)
    {
        titleText.text = "Congratulations, you successfully defended your kingdom";
        gameOverStatsPanel.SetTimePlayed(timePlayed);
        gameOverStatsPanel.SetEnemiesKilled(enemiesKilled);
        gameOverStatsPanel.SetWavesCleared(wavesCleared);
        gameObject.SetActive(true);
    }
}
