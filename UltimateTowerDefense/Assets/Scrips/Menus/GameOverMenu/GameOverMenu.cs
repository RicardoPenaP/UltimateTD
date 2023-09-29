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

    }

    public void OpenGameOverMenu()
    {
        titleText.text = "Game Over";
    }

    public void OpenGameCompleteMenu()
    {
        titleText.text = "Congratulations, you successfully defended your kingdom";
    }
}
