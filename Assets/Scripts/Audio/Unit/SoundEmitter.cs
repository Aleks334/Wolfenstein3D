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
    }

    private void DisableMusic()
    {
        //Debug.LogWarning("DisableMusic" + _audioSource.clip);
        _counter = 0;
       // _clips.Clear();

        Stop();
        _pool.Return(this);
    }

    /// <summary>
    /// Plays all audio clips from list
    /// </summary>
    public void PlayAudioClip(List<AudioClip> clips, AudioConfigurationSO settings, bool hasToLoop, Vector3 position)
    {
        _clips = clips;

        _audioSource.clip = clips[_counter];
        _audioSource.loop = hasToLoop;

        _localConfig = settings;
        settings.ApplyTo(_audioSource);

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
           // Debug.LogWarning("FinishedPlaying next clip" + _audioSource.clip);
            _counter++;
            PlayAudioClip(_clips, _localConfig, false, transform.position);
        }
        else
        {
           // Debug.LogWarning("FinishedPlaying end clips" + _audioSource.clip);
            _counter = 0;
           // _clips.Clear();
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
}