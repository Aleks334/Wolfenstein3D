using System;
using System.Collections;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    private AudioSource _audioSource;
    public event Action<SoundEmitter> OnSoundFinishedPlaying;

    private void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
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

    /// <summary>
    /// Used when the SFX finished playing audio clip. (for not looped audioCues)
    /// </summary>
    public void Stop()
    {
        _audioSource.Stop();
    }
}