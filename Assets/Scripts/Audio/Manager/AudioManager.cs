using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel;
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

    [SerializeField] private SoundEmitterPoolSO _pool;

    [SerializeField] private List<SoundEmitter> _takenFromPool;

    [Header("Event Channels for canceling sounds")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;

    private void Awake()
    {
        _takenFromPool = new();

        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested += PlayAudioCue;

        _pool.InitPoolParent += SetPoolParent;
        _pool.SetupPool();

        _onPlayerDeath.OnEventRaised += ReturnAllToPool;
        _onGameOver.OnEventRaised += ReturnAllToPool;
    }

    private void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioSettings, Vector3 position, bool disableSoundOnSceneChange)
    {
        AudioClip[] clipsToPlay = audioCue.GetClips(audioCue.DefaultClipGroup);
        SoundEmitter available = _pool.Request();

        _takenFromPool.Add(available);

        available.PlayAudioClip(clipsToPlay, audioSettings, audioCue.looping, position);

        if (!audioCue.looping)
            available.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
        
        if(disableSoundOnSceneChange)
            available.OnForceToDisableMusic += OnSoundEmitterForcedToDisableMusic;
    }

    private void ReturnAllToPool()
    {
        foreach (var soundEmitter in _takenFromPool)
        {
            //skip sound emitter with background music
            if (soundEmitter.GetComponent<AudioSource>().loop)
                continue;

            soundEmitter.Stop();
            _pool.Return(soundEmitter);
        }

        _takenFromPool.Clear();
    }
    
    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
    {
        soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
        soundEmitter.Stop();
        _pool.Return(soundEmitter);

        _takenFromPool.Remove(soundEmitter);
    }

    private void OnSoundEmitterForcedToDisableMusic(SoundEmitter soundEmitter)
    {
        soundEmitter.OnForceToDisableMusic -= OnSoundEmitterForcedToDisableMusic;
        soundEmitter.Stop();
        _pool.Return(soundEmitter);

        _takenFromPool.Remove(soundEmitter);
        //Debug.LogWarning($"{soundEmitter.name} returned to pool");
    }

    private Transform SetPoolParent()
    {
        return transform.GetChild(0);
    }
}
