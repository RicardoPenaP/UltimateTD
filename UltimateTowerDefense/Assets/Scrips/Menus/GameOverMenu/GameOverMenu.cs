using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameSceneManangement;

public class GameOverMenu : Singleton<GameOverMenu>
{
    public enum GameOverMenuToOpen { GameOver,GameCompleted}
    [Header("Game Over Menu")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameOverStatsPanel gameOverStatsPanel;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button playAgainButton;

    private bool isGameOver;

    public bool IsGameOver { get { return isGameOver; } }
    protected override void Awake()
    {
        base.Awake();
        SubscribeToButtonsEvents();
    }

    private void Start()
    {
        isGameOver = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnsubscribeToButtonsEvents();
    }

    private void SubscribeToButtonsEvents()
    {
        mainMenuButton?.onClick.AddListener(GoToMainMenu);
        playAgainButton?.onClick.AddListener(PlayAgain);
    }

    private void UnsubscribeToButtonsEvents()
    {
        mainMenuButton?.onClick.RemoveListener(GoToMainMenu);
        playAgainButton?.onClick.RemoveListener(PlayAgain);
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

    public void OpenGameOverMenu(GameOverMenuToOpen menuToOpen)
    {
        isGameOver = true;
        switch (menuToOpen)
        {
            case GameOverMenuToOpen.GameOver:
                titleText.text = "Game Over";
                break;
            case GameOverMenuToOpen.GameCompleted:
                titleText.text = "Congratulations, you successfully defended your kingdom";
               
                break;
            default:
                break;
        }
       
        gameOverStatsPanel.SetTimePlayed(GameMananger.Instance.TimePlayed);
        gameOverStatsPanel.SetEnemiesKilled(GameMananger.Instance.EnemiesKilled);
        gameOverStatsPanel.SetWavesCleared(GameMananger.Instance.WavesCleared);
       
        gameObject.SetActive(true);
    }

}
