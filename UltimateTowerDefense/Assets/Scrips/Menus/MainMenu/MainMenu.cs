using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{    
    [Header("Main Menu")]
    [Header("Menus Reference")]
    [SerializeField] private PlayMenu playMenu;   
    [Header("Buttons Reference")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
   
    private static readonly int OPEN_MENU_HASH = Animator.StringToHash("OpenMenu");
    private static readonly int CLOSE_MENU_HASH = Animator.StringToHash("CloseMenu");

    private MenuAnimationHelper myAnimationHelper;
    private Animator myAnimator;
    private void Awake()
    {
        myAnimationHelper = GetComponent<MenuAnimationHelper>();
        myAnimator = GetComponent<Animator>();
        SubscribeToButtonsEvents();
        SubscribeToAnimationEvents();
    }

    private void Start()
    {
        SceneTranstitionFade.Instance?.FadeOut();
        AmbientalMusicPlayer.Instance?.ChangeMusicWithTransition(AmbientalMusicToPlay.MainMenuMusic);
    }

    private void OnDestroy()
    {
        UnsubscribeToButtonsEvents();
        UnsubcribeToAnimationEvents();
    }

    private void SubscribeToButtonsEvents()
    {
        playButton?.onClick.AddListener(GoToPlayMenu);
        settingsButton?.onClick.AddListener(GoToSettinsMenu);
        exitButton?.onClick.AddListener(ExitApplication);

    }

    private void UnsubscribeToButtonsEvents()
    {
        playButton?.onClick.RemoveListener(GoToPlayMenu);
        settingsButton?.onClick.RemoveListener(GoToSettinsMenu);
        exitButton?.onClick.RemoveListener(ExitApplication);
    }

    private void SubscribeToAnimationEvents()
    {       
        myAnimationHelper.OnCloseAnimationFinished += CloseMenu;
    }

    private void UnsubcribeToAnimationEvents()
    {        
        myAnimationHelper.OnCloseAnimationFinished -= CloseMenu;
    }

    private void GoToPlayMenu()
    {
        myAnimator.SetTrigger(CLOSE_MENU_HASH);
        playMenu.OpenMenu(); 
    }

    private void GoToSettinsMenu()
    {
        myAnimator.SetTrigger(CLOSE_MENU_HASH);
        SettingsMenu.Instance?.OpenMenu();
    }
    

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void ExitApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        myAnimator.SetTrigger(OPEN_MENU_HASH);
    }
}
