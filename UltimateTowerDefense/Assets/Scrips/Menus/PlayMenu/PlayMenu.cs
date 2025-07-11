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

    private static readonly int OPEN_MENU_HASH = Animator.StringToHash("OpenMenu");
    private static readonly int CLOSE_MENU_HASH = Animator.StringToHash("CloseMenu");

    private MenuAnimationHelper myAnimationHelper;
    private Animator myAnimator;



    private void Awake()
    {        
        myAnimationHelper = GetComponent<MenuAnimationHelper>();
        myAnimator = GetComponent<Animator>();
        SubcribeToButtonsEvents();
        SubscribeToAnimationEvents();

    }

    private void OnDestroy()
    {
        UnsubcribeToButtonsEvents();
        UnsubscribeToAnimationEvents();
    }

    private void SubcribeToButtonsEvents()
    {
        backToMainMenuButton?.onClick.AddListener(BackToMainMenu);
        singleRoad?.onClick.AddListener(PlaySingleRoad);
        doubleRoad?.onClick.AddListener(PlayDoubleRoad);
        tripleRoad?.onClick.AddListener(PlayTripleRoad);
        quadRoad?.onClick.AddListener(PlayQuadRoad);
    }

    private void SubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished += () => gameObject.SetActive(false);
    }

    private void UnsubcribeToButtonsEvents()
    {
        backToMainMenuButton?.onClick.RemoveListener(BackToMainMenu);
        singleRoad?.onClick.RemoveListener(PlaySingleRoad);
        doubleRoad?.onClick.RemoveListener(PlayDoubleRoad);
        tripleRoad?.onClick.RemoveListener(PlayTripleRoad);
        quadRoad?.onClick.RemoveListener(PlayQuadRoad);
    }

    private void UnsubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished -= () => gameObject.SetActive(false); 
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
        myAnimator.SetTrigger(CLOSE_MENU_HASH);
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
        myAnimator.SetTrigger(OPEN_MENU_HASH);
    }
}
