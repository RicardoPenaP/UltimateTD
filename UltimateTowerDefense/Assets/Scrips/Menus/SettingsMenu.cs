using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Singleton<SettingsMenu>
{
    [Header("Settings Menu")]
    [Header("Menus Reference")]
    [SerializeField] private MainMenu mainMenuReference;
    [Header("Sliders Reference")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [Header("Buttons Reference")]
    [SerializeField] private Button goBack;
    

    private static readonly int OPEN_MENU_HASH = Animator.StringToHash("OpenMenu");
    private static readonly int CLOSE_MENU_HASH = Animator.StringToHash("CloseMenu");

    private MenuAnimationHelper myAnimationHelper;
    private Animator myAnimator;


    private bool isPaused = false;
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
        UnsubscribeToAnimationEvents();
    }

    private void SubscribeToButtonsEvents()
    {       
        goBack?.onClick.AddListener(GoToMainMenu);
    }

    private void UnsubscribeFromButtonsEvents()
    {        
        goBack?.onClick.AddListener(GoToMainMenu);
    }

    private void SubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished += () => gameObject.SetActive(false);
    }

    private void UnsubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished -= () => gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        myAnimator.Play(OPEN_MENU_HASH);       
    }

    private void GoToMainMenu()
    {
        myAnimator.Play(CLOSE_MENU_HASH);
        mainMenuReference.OpenMenu(); 
    }

}