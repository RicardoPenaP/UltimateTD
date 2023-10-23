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
    private AmbientalMusicToPlay currentMusicPlaying;
    

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
        StaticVolumeValues.OnSetMusicVolume += SetAudioSourceVolume;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayAnAmbientalMusic(currentMusicPlaying);
        } 
    }

    private void OnDestroy()
    {
        StaticVolumeValues.OnSetMusicVolume -= SetAudioSourceVolume;
    }

    private void SetAudioSourceVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PlayAnAmbientalMusic(AmbientalMusicToPlay musicToPlay)
    {
        currentMusicPlaying = musicToPlay;
        //audioSource.PlayOneShot(ambientalSongs[(int)musicToPlay]);
        switch (musicToPlay)
        {
            case AmbientalMusicToPlay.MainMenuMusic:
                audioSource.PlayOneShot(ambientalSongs[0]);
                break;
            case AmbientalMusicToPlay.StanByGameSceneMusic:
                audioSource.PlayOneShot(ambientalSongs[1]);
                break;
            case AmbientalMusicToPlay.WaveGameSceneMusic:
                audioSource.PlayOneShot(ambientalSongs[2]);
                break;
            default:
                break;
        }
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
