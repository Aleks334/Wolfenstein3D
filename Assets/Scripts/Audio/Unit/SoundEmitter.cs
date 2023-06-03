using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SoundEmitter : MonoBehaviour
{
    private AudioSource _audioSource;
    public event Action<SoundEmitter> OnSoundFinishedPlaying;

    [SerializeField] private VoidEventChannelSO _voidLoadSceneChannel;
    [SerializeField] private SoundEmitterPoolSO _pool;

    private List<AudioClip> _clips;
    private int _counter = 0;
    private AudioConfigurationSO _localConfig;

    private void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        _clips = new();
    }

    public void SubscribeToVoidLoadScene()
    {
        _voidLoadSceneChannel.OnEventRaised += DisableMusic;
    }

    private void OnDisable()
    {
        _voidLoadSceneChannel.OnEventRaised -= DisableMusic;

        if (_localConfig != null)
            _localConfig.OnVolumeChanged.OnEventRaised -= UpdateVolume;
    }

    private void DisableMusic()
    {
        _counter = 0;

        Stop();
        _pool.Return(this);
    }

    public void PlayAudioClip(List<AudioClip> clips, AudioConfigurationSO settings, bool hasToLoop, Vector3 position)
    {
        _clips = clips;

        _audioSource.clip = clips[_counter];
        _audioSource.loop = hasToLoop;

        _localConfig = settings;
        settings.ApplyTo(_audioSource);

        settings.OnVolumeChanged.OnEventRaised += UpdateVolume;

        _audioSource.transform.position = position;
        _audioSource.Play();

        if(!hasToLoop)
            StartCoroutine(FinishedPlaying(clips[_counter].length));
    }
    
    IEnumerator FinishedPlaying(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        if (_counter < _clips.Count - 1)
        {
            _counter++;
            PlayAudioClip(_clips, _localConfig, false, transform.position);
        }
        else
        {
            _counter = 0;
            OnSoundFinishedPlaying.Invoke(this); // The AudioManager will pick this up
        }
            
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public void Resume()
    {
        _audioSource.Play();
    }

    private void UpdateVolume(float newVolume)
    {
        _audioSource.volume = newVolume;
    }
}