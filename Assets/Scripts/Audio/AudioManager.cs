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

    private void PlayAudioCue(AudioCueSO audioData, AudioConfigurationSO audioSettings, Vector3 position)
    {
        int numOfClips = audioData.audioClips.Length;
        bool hasToLoop = audioData.looping;

        AudioClip[] clipsToPlay = new AudioClip[numOfClips];

        //Passing clips from audioCueSO to array based on enum PlaybackMode (in GetNextClip method)
        for (int i = 0; i < numOfClips; i++)
        {
            clipsToPlay[i] = audioData.GetNextClip();
        }

        SoundEmitter availableSoundEmitter = GetAvailableSoundEmitter();
      /*  if (!hasToLoop)
        {
            availableSoundEmitter.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
        }*/

        availableSoundEmitter.PlayAudioClip(clipsToPlay[0], audioSettings, hasToLoop, position);
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
    /*
    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
    {
        soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
        
    }*/
}
