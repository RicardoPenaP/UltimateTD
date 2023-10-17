using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour   
{
    [Header("Settings Menu")]
    [Header("Menus Reference")]
    [SerializeField] private MainMenu mainMenuReference;
    [Header("Sliders Reference")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [Header("Buttons Reference")]
    [SerializeField] private Button goBack;
    [SerializeField] private Button reset;


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
        SubscribeToSlidersEvents();        
    }

    private void Start()
    {
        InitVolumeValues();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnsubscribeFromButtonsEvents();
        UnsubscribeToAnimationEvents();
        UnsubscribeFromSlidersEvents();
    }

    private void InitVolumeValues()
    {
        float musicVolume = 0f;
        float sfxVolume= 0f;
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", StaticVolumeValues.DEFAULT_MUSIC_VOLUME);
            musicVolume = StaticVolumeValues.DEFAULT_MUSIC_VOLUME;
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("sfxVolume", StaticVolumeValues.DEFAULT_SFX_VOLUME);
            sfxVolume = StaticVolumeValues.DEFAULT_SFX_VOLUME;
        }

        musicVolumeSlider.value = musicVolume;       
        SFXVolumeSlider.value = sfxVolume;        
    }

    private void SubscribeToButtonsEvents()
    {       
        goBack?.onClick.AddListener(GoToMainMenu);
        reset?.onClick.AddListener(ResetVolumeValues);
    }

    private void UnsubscribeFromButtonsEvents()
    {        
        goBack?.onClick.RemoveListener(GoToMainMenu);
        reset?.onClick.RemoveListener(ResetVolumeValues);
    }

    private void SubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished += () => gameObject.SetActive(false);
    }

    private void UnsubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished -= () => gameObject.SetActive(false);
    }

    private void SubscribeToSlidersEvents()
    {
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void UnsubscribeFromSlidersEvents()
    {
        musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
        SFXVolumeSlider.onValueChanged.RemoveListener(SetSFXVolume);
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

    private void SetMusicVolume(float value)
    {
        StaticVolumeValues.SetMusicVolume(value);
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    private void SetSFXVolume(float value)
    {
        StaticVolumeValues.SetSFXVolume(value);
        PlayerPrefs.SetFloat("sfxVolume", value);
    }

    private void ResetVolumeValues()
    {
        PlayerPrefs.DeleteAll();
        InitVolumeValues();
    }

}
