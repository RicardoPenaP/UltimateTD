using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SceneTranstitionFade : Singleton<SceneTranstitionFade>
{
    [Header("Scene Transition Fade")]
    [SerializeField] private float fadeTime = 1f;

    private Image fadeImage;


    protected override void Awake()
    {
        base.Awake();
        fadeImage = GetComponentInChildren<Image>();
        gameObject.SetActive(false);
    }

    public void FadeIn(Action OnFadeCompletedActions)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeRoutine(true, OnFadeCompletedActions));
    }

    public void FadeOut(Action OnFadeCompletedActions)
    {
        OnFadeCompletedActions += () => gameObject.SetActive(false);
        StartCoroutine(FadeRoutine(true, OnFadeCompletedActions));
    }

    private IEnumerator FadeRoutine(bool fadeIn,Action OnFadeCompletedActions)
    {
        float initialValue = fadeIn ? 1 : 0;
        float targetValue = fadeIn ? 0 : 1;
        float deltaT = 0;
        Color modifiedColor = fadeImage.color;       

        while (deltaT < fadeTime)
        {
            deltaT = Time.deltaTime;
            float progress = deltaT / fadeTime;
            modifiedColor.a = Mathf.Lerp(initialValue, targetValue, progress);
            fadeImage.color = modifiedColor;

            yield return null;
        }

        OnFadeCompletedActions?.Invoke();
    }
}
