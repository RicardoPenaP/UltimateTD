using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSceneManangement;

public class PlayMenu : MonoBehaviour
{
    [Header("Play Menu")]
    [Header("Menus Reference")]
    [SerializeField] private MainMenu mainMenu;
    [Header("Button References")]
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private Button singleRoad;
    [SerializeField] private Button doubleRoad;
    [SerializeField] private Button tripleRoad;
    [SerializeField] private Button quadRoad;

    private void Awake()
    {
        SubmitToButtonsEvents();
    }

    private void SubmitToButtonsEvents()
    {
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        singleRoad.onClick.AddListener(PlaySingleRoad);
    }

    private void BackToMainMenu()
    {
        CloseMenu();
        mainMenu.OpenMenu();
    }

    private void PlaySingleRoad()
    {
        CloseMenu();
        GameScenesLoader.LoadGameScene(GameScenes.SingleRoadGame);//for testing only
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
