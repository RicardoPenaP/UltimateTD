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
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button resumeButton;

    private static readonly int OPEN_MENU_HASH = Animator.StringToHash("OpenMenu");
    private static readonly int CLOSE_MENU_HASH = Animator.StringToHash("CloseMenu");

    private MenuAnimationHelper myAnimationHelper;
    private Animator myAnimator;

    private bool menuClosedButStillPaused = false;   
    private bool isPaused = false;
    public bool MenuClosedButStillPaused { get { return menuClosedButStillPaused; } }
    public bool IsPaused { get { return isPaused; } }


    protected override void Awake()
    {
        base.Awake();
        myAnimationHelper = GetComponent<MenuAnimationHelper>();
        myAnimator = GetComponent<Animator>();
        SubscribeToButtonsEvents();
        SubscribeToAnimationEvents();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnsubscribeFromButtonsEvents();
        UnsubscribeFromAnimationEvents();
    }

    private void SubscribeToButtonsEvents()
    {
        resumeButton?.onClick.AddListener(ToggleMenu);
        mainMenuButton?.onClick.AddListener(GoToMainMenu);
        settingsButton?.onClick.AddListener(GoToSettingsMenu);
    }

    private void UnsubscribeFromButtonsEvents()
    {
        resumeButton?.onClick.AddListener(ToggleMenu);
        mainMenuButton?.onClick.AddListener(GoToMainMenu);
        settingsButton?.onClick.RemoveListener(GoToSettingsMenu);
    }

    private void SubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished += () => gameObject.SetActive(false);
    }

    private void UnsubscribeFromAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished -= () => gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (SceneTranstitionFade.Instance.FadeInProgress)
        {
            return;
        }
        SetIsPaused(!isPaused);
        if (isPaused)
        {
            gameObject.SetActive(true);
            myAnimator.Play(OPEN_MENU_HASH);
        }
        else
        {
            myAnimator.Play(CLOSE_MENU_HASH);
        }  
    }

    public void ToggleMenuStillPaused()
    {
        if (SceneTranstitionFade.Instance.FadeInProgress)
        {
            return;
        }   
        
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            myAnimator.Play(OPEN_MENU_HASH);
            menuClosedButStillPaused = false;
        }
        else
        {
            menuClosedButStillPaused = true;
            myAnimator.Play(CLOSE_MENU_HASH);
        }
    }

    private void GoToMainMenu()
    {
        ToggleMenu();
        SceneTranstitionFade.Instance?.FadeIn(OnFadeEnds);       
    }

    private void GoToSettingsMenu()
    {
        ToggleMenuStillPaused();
        SettingsMenu.Instance?.OpenMenu();
    }

    private void OnFadeEnds()
    {        
        GameScenesLoader.LoadGameScene(GameScenes.MainMenu);        
    }

    public void SetIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
        Time.timeScale = isPaused ? PAUSE_TIME_SCALE_VALUE : DEFAULT_TIME_SCALE_VALUE;
    }
}
