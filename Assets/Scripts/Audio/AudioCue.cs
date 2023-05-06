using UnityEngine;

public class AudioCue : MonoBehaviour
{
    [Header("Sound definition")]
    [SerializeField] private AudioCueSO _audioData;
    [SerializeField] private bool _playOnStart = false;

    [Header("Sound Configuration")]
    [SerializeField] private AudioConfigurationSO _audioSettings;
    [SerializeField] private AudioCueEventChannelSO _audioCueEventChannel;

    private void Start()
    {
        if (_playOnStart)
            PlayAudioCue();
    }

    public void PlayAudioCue()
    {
        _audioCueEventChannel.RaiseEvent(_audioData, _audioSettings, transform.position);
    }
}
