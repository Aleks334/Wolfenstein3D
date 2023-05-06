using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel;
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

    [SerializeField] private int _initalPoolSize = 1;
    private Transform _soundEmitterPool;

    [SerializeField] private SoundEmitter _soundEmitterPrefab;

    [SerializeField] private List<SoundEmitter> _soundEmittersOnScene;

    private void Awake()
    {
        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested += PlayAudioCue;

        GameObject soundEmitterPool = new GameObject("SoundEmitterPool");
        soundEmitterPool.transform.parent = this.transform;
        _soundEmitterPool = soundEmitterPool.transform;

        for (int i = 0; i < _initalPoolSize; i++)
        {
            CreateNewSoundEmitter();
        }
    }

    private SoundEmitter CreateNewSoundEmitter()
    {
        SoundEmitter SoundEmitter = GameObject.Instantiate(_soundEmitterPrefab, _soundEmitterPool);
        _soundEmittersOnScene.Add(SoundEmitter);

        return SoundEmitter;
    }

    private void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioSettings, Vector3 position)
    {
        AudioClip clipToPlay = audioCue.GetNextClip();
            //_pool.Request();
            SoundEmitter availableSoundEmitter = GetAvailableSoundEmitter();

            availableSoundEmitter.PlayAudioClip(clipToPlay, audioSettings, audioCue.looping, position);
            if (!audioCue.looping)
                availableSoundEmitter.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
          
    }

    private SoundEmitter GetAvailableSoundEmitter()
    {
        SoundEmitter availableSoundEmitter = null;

        foreach (var soundEmitter in _soundEmittersOnScene)
        {
            if (soundEmitter._status == SoundEmitter.SoundEmitterStatus.Free)
            {
                availableSoundEmitter = soundEmitter;
                soundEmitter._status = SoundEmitter.SoundEmitterStatus.Used;
                break;
            }
        }

        if(availableSoundEmitter == null)
        {
            availableSoundEmitter = CreateNewSoundEmitter();
        }

        return availableSoundEmitter;
    }
    
    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
    {
        soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
        soundEmitter.Stop();
        //_pool.Return(soundEmitter);
    }
}
