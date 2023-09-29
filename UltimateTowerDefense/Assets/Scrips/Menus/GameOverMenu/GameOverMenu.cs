using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameSceneManangement;

public class GameOverMenu : MonoBehaviour
{
    [Header("Game Over Menu")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameOverStatsPanel gameOverStatsPanel;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button playAgainButton;

    private void Awake()
    {
        SubmitToButtonsEvents();
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

    public void OpenGameOverMenu()
    {
        titleText.text = "Game Over";
        gameObject.SetActive(true);
    }

    public void OpenGameCompleteMenu()
    {
        titleText.text = "Congratulations, you successfully defended your kingdom";
        gameObject.SetActive(true);
    }
}
