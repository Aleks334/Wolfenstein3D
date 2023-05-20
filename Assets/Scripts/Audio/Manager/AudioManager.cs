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

        _pool.SetupPool();
    }

    private void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioSettings, Vector3 position, bool disableSoundOnSceneChange)
    {
        AudioClip[] clipsToPlay = audioCue.GetClips(audioCue.DefaultClipGroup);
        SoundEmitter available = _pool.Request();

        available.PlayAudioClip(clipsToPlay, audioSettings, audioCue.looping, position);

        if (!audioCue.looping)
            available.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
        
        if(disableSoundOnSceneChange)
            available.OnForceToDisableMusic += OnSoundEmitterForcedToDisableMusic;
    }
    
    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
    {
        soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
        soundEmitter.Stop();
        _pool.Return(soundEmitter);
    }

    private void OnSoundEmitterForcedToDisableMusic(SoundEmitter soundEmitter)
    {
        soundEmitter.OnForceToDisableMusic -= OnSoundEmitterForcedToDisableMusic;
        soundEmitter.Stop();
        _pool.Return(soundEmitter);
        Debug.LogWarning($"{soundEmitter.name} returned to pool");
    }

    private Transform SetPoolParent()
    {
        return transform.GetChild(0);
    }
}
