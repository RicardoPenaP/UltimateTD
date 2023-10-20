using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class GameManangerNextWavePanel : MonoBehaviour
{
    [Header("Game Mananger Next Wave Panel")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button startNowButton;

    private static readonly int OPEN_MENU_HASH = Animator.StringToHash("OpenMenu");
    private static readonly int CLOSE_MENU_HASH = Animator.StringToHash("CloseMenu");

    private MenuAnimationHelper myAnimationHelper;
    private Animator myAnimator;

    private bool panelState = false;

    private void Awake()
    {
        myAnimationHelper = GetComponent<MenuAnimationHelper>();
        myAnimator = GetComponent<Animator>();       
        SubscribeToAnimationEvents();

    }

    private void OnDestroy()
    {
        UnsubscribeToAnimationEvents();
    }

    private void SubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished += () => gameObject.SetActive(false);
    }

    private void UnsubscribeToAnimationEvents()
    {
        myAnimationHelper.OnCloseAnimationFinished -= () => gameObject.SetActive(false);
    }

    public void TogglePanel()
    {
        panelState = !panelState;

        if (panelState)
        {
            gameObject.SetActive(true);
            myAnimator.Play(OPEN_MENU_HASH);
        }
        else
        {
            UpdateTimer(0f);
            myAnimator.Play(CLOSE_MENU_HASH);
        }
       
    }

    public void UpdateTimer(float time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        timerText.text = $"{t.Minutes.ToString("00")}:{t.Seconds.ToString("00")}:{t.Milliseconds.ToString("00")}";
    }

    public void SubscribeToStarNowButtonOnClickEvent(UnityAction action)
    {
        startNowButton.onClick.AddListener(action);
    }

    public void UnsubscribeToStarNowButtonOnClickEvent(UnityAction action)
    {
        startNowButton.onClick.RemoveListener(action);
    }
}
