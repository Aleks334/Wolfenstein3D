using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/AudioCue Event Channel")]
public class AudioCueEventChannelSO : ScriptableObject
{
    public event Action<AudioCueSO, AudioConfigurationSO, Vector3> OnAudioCueRequested;

    public void RaiseEvent(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 position)
    {
        if (OnAudioCueRequested != null)
        {
            OnAudioCueRequested.Invoke(audioCue, audioConfiguration, position);
        }
        else
            Debug.LogWarning("An AudioCue was requested, but nobody picked it up.");
    }
}