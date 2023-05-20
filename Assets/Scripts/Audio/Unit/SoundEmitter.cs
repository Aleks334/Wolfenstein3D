using System;
using System.Collections;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    private AudioSource _audioSource;
    public event Action<SoundEmitter> OnSoundFinishedPlaying;

    public event Action<SoundEmitter> OnForceToDisableMusic;
    [SerializeField] private VoidEventChannelSO _voidLoadSceneChannel;

    private void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    private void OnEnable()
    {
        _voidLoadSceneChannel.OnEventRaised += DisableMusic;
    }

    private void OnDisable()
    {
        _voidLoadSceneChannel.OnEventRaised -= DisableMusic;
    }

    private void DisableMusic()
    {
        //Debug.LogWarning($"Disable music on {this.name}");
        OnForceToDisableMusic?.Invoke(this);
    }

    /// <summary>
    /// Plays all audio clips from array (currently only first clip!)
    /// </summary>
    public void PlayAudioClip(AudioClip[] clips, AudioConfigurationSO settings, bool hasToLoop, Vector3 position)
    {
        _audioSource.clip = clips[0];
        _audioSource.loop = hasToLoop;
        settings.ApplyTo(_audioSource);
        _audioSource.transform.position = position;
        _audioSource.Play();

        if(!hasToLoop)
            StartCoroutine(FinishedPlaying(clips[0].length));
    }
    
    IEnumerator FinishedPlaying(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        OnSoundFinishedPlaying.Invoke(this); // The AudioManager will pick this up
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public void Resume()
    {
        _audioSource.Play();
    }
}