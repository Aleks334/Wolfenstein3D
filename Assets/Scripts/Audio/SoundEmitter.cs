using System;
using System.Collections;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    private AudioSource _audioSource;
    public event Action<SoundEmitter> OnSoundFinishedPlaying;

    public SoundEmitterStatus _status;

    private void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        _status = SoundEmitterStatus.Free;
        gameObject.SetActive(false);
    }

    public void PlayAudioClip(AudioClip clip, AudioConfigurationSO settings, bool hasToLoop, Vector3 position)
    {
        gameObject.SetActive(true);

        _audioSource.clip = clip;
        _audioSource.loop = hasToLoop;
        settings.ApplyTo(_audioSource);

        _audioSource.transform.position = position;
        _audioSource.Play();

       // if(!hasToLoop)
       //     StartCoroutine(FinishedPlaying(clip.length));
    }
    /*
    IEnumerator FinishedPlaying(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        _status = SoundEmitterStatus.Free;
        gameObject.SetActive(false);
        OnSoundFinishedPlaying.Invoke(this); // The AudioManager will pick this up
    }*/

    public enum SoundEmitterStatus
    {
        Free,
        Used
    }
}