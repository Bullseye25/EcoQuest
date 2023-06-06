using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    MUSIC,
    EFFECT
};

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicHolder;
    [SerializeField] private AudioClip gameMusic;

    [Space]
    [SerializeField] private List<AudioSource> sources = new List<AudioSource>();

    private void Start()
    {
        if (gameMusic != null)
            Play(gameMusic, (source) =>
            {
                musicHolder = source;
                musicHolder.volume = 0;
                musicHolder.Play();
                StartCoroutine(MusicVolumeOperator(source));

            }, AudioType.MUSIC);
    }

    IEnumerator MusicVolumeOperator(AudioSource source, bool startGame = true)
    {
        yield return new WaitForSeconds(Time.smoothDeltaTime);

        if (startGame)
        {
            source.volume += Time.deltaTime;
            if (source.volume < 0.5f)
                StartCoroutine(MusicVolumeOperator(source));
        }
        else
        {
            source.volume -= Time.deltaTime;
            if (source.volume > 0.01f)
                StartCoroutine(MusicVolumeOperator(source, false));
        }

    }

    public void StartLevel()
    {
        StartCoroutine(MusicVolumeOperator(musicHolder));
    }

    public void EndLevel()
    {
        StartCoroutine(MusicVolumeOperator(musicHolder, false));
    }

    public void Play(AudioClip clip, Action<AudioSource> action, AudioType audioType = AudioType.EFFECT)
    {
        var source = GetSource();
        source.clip = clip;

        if (audioType == AudioType.MUSIC)
        {
            source.loop = true;
            action(source);
        }
        else
            source.Play();
    }

    public void Play(AudioClip clip)
    {
        var source = GetSource();
        source.clip = clip;
        source.volume = 0.5f;
        source.Play();
    }

    private AudioSource GetSource()
    {
        foreach(AudioSource source in sources)
        {
            if (!source.isPlaying)
                return source;
        }

        var newSource = gameObject.AddComponent<AudioSource>();
        sources.Add(newSource);
        newSource.loop = false;
        newSource.playOnAwake = false;
        newSource.volume = 0.5f;
        return newSource;
    }
}