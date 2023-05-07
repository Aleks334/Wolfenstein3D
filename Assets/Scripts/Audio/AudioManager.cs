using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel;
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

    [SerializeField] private SoundEmitterPoolSO _pool;

    private void Awake()
    {
        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested += PlayAudioCue;

        _pool.InitPoolParent += SetPoolParent;
    }

    private void Start()
    {
        _pool.SetupPool();
    }

    private void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioSettings, Vector3 position)
    {
        AudioClip[] clipsToPlay = audioCue.GetClips();
        SoundEmitter available = _pool.Request();

        available.PlayAudioClip(clipsToPlay, audioSettings, audioCue.looping, position);

        if (!audioCue.looping)
            available.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
    }
    
    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
    {
        soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
        soundEmitter.Stop();
        _pool.Return(soundEmitter);
    }

    private Transform SetPoolParent()
    {
        return transform.GetChild(0);
    }
}
