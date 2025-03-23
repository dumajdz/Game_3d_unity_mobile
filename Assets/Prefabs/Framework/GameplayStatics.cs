using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameplayStatics : MonoBehaviour
{
    class AudioSrcContext : MonoBehaviour
    {

    }
    static private ObjectPool<AudioSource> AudioPool = new(CreateAudioSrc,null ,null , DestoryAudioSrc, false, 5, 10);

    public static void GameStarted()
    {
        AudioPool.Clear();
    }
    private static void DestoryAudioSrc(AudioSource audioSrc)
    {
        GameObject.Destroy(audioSrc.gameObject);
    }

    private static AudioSource CreateAudioSrc()
    {
        GameObject audioSrcGameObject = new GameObject("AudioSource",typeof(AudioSource),typeof(AudioSrcContext));
        AudioSource audioSrc = audioSrcGameObject.GetComponent<AudioSource>();

        audioSrc.volume = 1.0f;
        audioSrc.spatialBlend = 1.0f;
        audioSrc.rolloffMode = AudioRolloffMode.Linear;

        return audioSrc;
    }

    public static void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }
    public static void PlayAudioAtLoc(AudioClip audioToPlay, Vector3 Playloc, float volume)
    {
        AudioSource newSrc = AudioPool.Get();
        newSrc.volume = volume;
        newSrc.gameObject.transform.position = Playloc;
        newSrc.PlayOneShot(audioToPlay);
        newSrc.GetComponent<AudioSrcContext>().StartCoroutine(ReleaseAudioSrc(newSrc, audioToPlay.length));

    }

    private static IEnumerator ReleaseAudioSrc(AudioSource newSrc, float lenght)
    {
        yield return new WaitForSeconds(lenght);
    }

    internal static void PlayAudioAtPlayer(AudioClip abilityAudio, float volume)
    {
        PlayAudioAtLoc(abilityAudio, Camera.main.transform.position, volume);
    }
}
