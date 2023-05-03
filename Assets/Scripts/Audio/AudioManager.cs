using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel;
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

    private void Awake()
    {
        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested += PlayAudioCue;
    }

    private void PlayAudioCue(AudioCueSO audioData, AudioConfigurationSO audioSettings, Vector3 position)
    {
        
    }
}
