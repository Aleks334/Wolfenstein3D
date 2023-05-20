using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel;
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

    [SerializeField] private SoundEmitterPoolSO _pool;

    [Header("Event Channels for canceling sounds")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;

    private void Awake()
    {

        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested += PlayAudioCue;

        _pool.InitPoolParent += SetPoolParent;
        _pool.SetupPool();

        _onPlayerDeath.OnEventRaised += _pool.ReturnAllToPool;
        _onGameOver.OnEventRaised += _pool.ReturnAllToPool;
    }

    private void OnDisable()
    {
        _SFXEventChannel.OnAudioCueRequested -= PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested -= PlayAudioCue;

        _pool.InitPoolParent -= SetPoolParent;

        _onPlayerDeath.OnEventRaised -= _pool.ReturnAllToPool;
        _onGameOver.OnEventRaised -= _pool.ReturnAllToPool;
    }

    private void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioSettings, Vector3 position)
    {
        List<AudioClip> clipsToPlay = audioCue.GetClips(audioCue.DefaultClipGroup);
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
