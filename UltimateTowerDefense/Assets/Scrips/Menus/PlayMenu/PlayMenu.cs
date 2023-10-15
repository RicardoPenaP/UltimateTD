using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSceneManangement;
using System;

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
        doubleRoad.onClick.AddListener(PlayDoubleRoad);
        tripleRoad.onClick.AddListener(PlayTripleRoad);
        quadRoad.onClick.AddListener(PlayQuadRoad);
    }

    private void BackToMainMenu()
    {
        CloseMenu();
        mainMenu.OpenMenu();
    }

    private void PlaySingleRoad()
    {
        GameMode.GameModeOption = GameModeOptions.SingleRoad;
        CloseMenu();

        LoadScene();
    }
    private void PlayDoubleRoad()
    {
        GameMode.GameModeOption = GameModeOptions.DoubleRoad;
        CloseMenu();
        LoadScene();
    }
    private void PlayTripleRoad()
    {
        GameMode.GameModeOption = GameModeOptions.TripleRoad;
        CloseMenu();
        LoadScene();
    }
    private void PlayQuadRoad()
    {
        GameMode.GameModeOption = GameModeOptions.QuadRoad;
        CloseMenu();
        LoadScene();
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void LoadScene()
    {        
        SceneTranstitionFade.Instance.FadeIn(OnFadeEnds);        
    }

    private void OnFadeEnds()
    {       
        GameScenesLoader.LoadGameScene(GameScenes.GameScene);        
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
