using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource soundSource;

    private Coroutine fadeOutCoroutine;

    public void playMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }
    
    public void ChangeMusicFadeOut(float fadeTime, AudioClip newMusic)
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }

        fadeOutCoroutine = StartCoroutine(FadeOutMusic(fadeTime, newMusic));
    }

    private IEnumerator FadeOutMusic(float fadeTime, AudioClip newMusic)
    {
        float startVolume = musicSource.volume;
        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeTime);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;

        if (newMusic != null)
        {
            playMusic(newMusic);
        }
    }

    public void playSoundFx(AudioClip soundFx)
    {
        soundSource.PlayOneShot(soundFx);
    }
}
