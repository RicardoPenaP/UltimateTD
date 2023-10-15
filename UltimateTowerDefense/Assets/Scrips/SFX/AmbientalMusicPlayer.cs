using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmbientalMusicToPlay { MainMenuMusic, StanByGameSceneMusic, WaveGameSceneMusic}
public class AmbientalMusicPlayer : Singleton<AmbientalMusicPlayer>
{
    [Header("Ambiental Music Player")]
    [SerializeField] private AudioClip[] ambientalSongs;
    [SerializeField,Min(0f)] private float transitionTime = 1f;

    private AudioSource audioSource;


    protected override void Awake()
    {
        base.Awake();
        if (AmbientalMusicPlayer.Instance == this)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAnAmbientalMusic(AmbientalMusicToPlay musicToPlay)
    {
        audioSource.PlayOneShot(ambientalSongs[(int)musicToPlay]);
    }

    public void ChangeMusicWithTransition(AmbientalMusicToPlay musicToPlay)
    {
        StartCoroutine(TransitionRoutine(musicToPlay));
    }

    private IEnumerator TransitionRoutine(AmbientalMusicToPlay musicToPlay)
    {
        float deltaTime = 0;
        float targetTime = transitionTime / 2;
        float initialValue = audioSource.volume;
        float targetValue = 0;

        while (deltaTime < targetTime )
        {
            deltaTime += Time.unscaledDeltaTime;
            float progress = deltaTime / targetTime;
            audioSource.volume = Mathf.Lerp(initialValue, targetValue, progress);
            yield return null;
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(ambientalSongs[(int)musicToPlay]);

        deltaTime = 0;
        targetTime = transitionTime / 2;
        targetValue = initialValue;
        initialValue = audioSource.volume;

        while (deltaTime < targetTime)
        {
            deltaTime += Time.unscaledDeltaTime;
            float progress = deltaTime / targetTime;
            audioSource.volume = Mathf.Lerp(initialValue, targetValue, progress);
            yield return null;
        }
       
    }


}
