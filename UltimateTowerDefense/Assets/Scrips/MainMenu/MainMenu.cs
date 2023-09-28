using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [Header("Buttons Reference")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        SubmitButtonsEvents();
    }

    private void SubmitButtonsEvents()
    {
        playButton?.onClick.AddListener(CloseMenu);
        settingsButton?.onClick.AddListener(CloseMenu);
        exitButton?.onClick.AddListener(ExitApplication);

    }

    private void CloseMenu()
    {
        gameObject.SetActive(true);
    }

    private void ExitApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
