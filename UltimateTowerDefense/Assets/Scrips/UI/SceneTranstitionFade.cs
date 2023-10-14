using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SceneTranstitionFade : Singleton<SceneTranstitionFade>
{
    [Header("Scene Transition Fade")]
    [SerializeField,Min(0)] private float fadeTime = 1f;

    private Image fadeImage;

    protected override void Awake()
    {
        base.Awake();
        fadeImage = GetComponentInChildren<Image>();        
        
    }


    public void FadeIn(Action OnFadeCompletedActions = null)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeRoutine(true, OnFadeCompletedActions));
    }

    public void FadeOut(Action OnFadeCompletedActions = null)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        OnFadeCompletedActions += () => gameObject.SetActive(false);
        StartCoroutine(FadeRoutine(false, OnFadeCompletedActions));
    }

    private IEnumerator FadeRoutine(bool fadeIn,Action OnFadeCompletedActions)
    {
        float initialValue = fadeIn ? 0 : 1;
        float targetValue = fadeIn ? 1 : 0;
        float deltaT = 0;
        Color modifiedColor = fadeImage.color;       

        while (deltaT < fadeTime)
        {
            deltaT += Time.deltaTime;
            float progress = deltaT / fadeTime;
            modifiedColor.a = Mathf.Lerp(initialValue, targetValue, progress);
            fadeImage.color = modifiedColor;

            yield return null;
        }

        OnFadeCompletedActions?.Invoke();
    }
}
